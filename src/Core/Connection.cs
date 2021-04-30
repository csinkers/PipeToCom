using System;
using System.IO;
using System.IO.Pipes;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace NamedPipeSerialProxy.Core
{
    public class Connection
    {
        static readonly ILog Logger = Log.Instance;

        readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        NamedPipeClientStream _namedPipe;
        SerialPort _serialPort;
        Thread _portForwarder;

        public bool IsStarted { get; private set; }
        protected Settings CurrentSettings { get; }

        static string GetLogString(byte[] buffer, int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                var b = buffer[i];
                if (b < 0x20)
                {
                    switch (b)
                    {
                        case 0x00: sb.Append("(NUL)"); break;
                        case 0x01: sb.Append("(SOH)"); break;
                        case 0x02: sb.Append("(STX)"); break;
                        case 0x03: sb.Append("(ETX)"); break;
                        case 0x04: sb.Append("(EOT)"); break;
                        case 0x05: sb.Append("(ENQ)"); break;
                        case 0x06: sb.Append("(ACK)"); break;
                        case 0x07: sb.Append("(BEL)"); break;
                        case 0x08: sb.Append("(BS)"); break;
                        case 0x09: sb.Append("\\t"); break;
                        case 0x0A: sb.Append("\\r"); break;
                        case 0x0B: sb.Append("(VT)"); break;
                        case 0x0C: sb.Append("(FF)"); break;
                        case 0x0D: sb.Append("\\n"); break;
                        case 0x0E: sb.Append("(SO)"); break;
                        case 0x0F: sb.Append("(SI)"); break;
                        case 0x10: sb.Append("(DLE)"); break;
                        case 0x11: sb.Append("(DC1)"); break;
                        case 0x12: sb.Append("(DC2)"); break;
                        case 0x13: sb.Append("(DC3)"); break;
                        case 0x14: sb.Append("(DC4)"); break;
                        case 0x15: sb.Append("(NAK)"); break;
                        case 0x16: sb.Append("(SYN)"); break;
                        case 0x17: sb.Append("(ETB)"); break;
                        case 0x18: sb.Append("(CAN)"); break;
                        case 0x19: sb.Append("(EM)"); break;
                        case 0x1A: sb.Append("(SUB)"); break;
                        case 0x1B: sb.Append("(ESC)"); break;
                        case 0x1C: sb.Append("(FS)"); break;
                        case 0x1D: sb.Append("(GS)"); break;
                        case 0x1E: sb.Append("(RS)"); break;
                        case 0x1F: sb.Append("(US)"); break;
                    }
                }
                else sb.Append(Convert.ToChar(b));
            }
            return sb.ToString();
        }

        public Connection(Settings settings) => CurrentSettings = settings ?? throw new ArgumentNullException(nameof(settings));

        public void Start()
        {
            if (IsStarted)
                return;

            Log.Instance.Debug($"Starting proxy from {CurrentSettings.ComPort} to {CurrentSettings.NamedPipe}");
            _serialPort = new SerialPort(
                CurrentSettings.ComPort,
                CurrentSettings.BaudRate,
                CurrentSettings.Parity,
                CurrentSettings.DataBits,
                CurrentSettings.StopBits)
            {
                RtsEnable = true,
                DtrEnable = true,
                Encoding = Encoding.UTF8
            };

            _namedPipe = new NamedPipeClientStream(
                CurrentSettings.MachineName,
                CurrentSettings.NamedPipe,
                PipeDirection.InOut,
                PipeOptions.Asynchronous);

            _portForwarder = new Thread(PortForwarder);
            _portForwarder.Start();

            IsStarted = true;
        }

        public void Stop()
        {
            if (!IsStarted)
                return;

            Log.Instance.Debug($"Stopping proxy from {CurrentSettings.ComPort} to {CurrentSettings.NamedPipe}");
            _stopEvent.Set(); // Signal the port forwarder thread to stop
            _portForwarder.Join(); // Wait for port forwarder thread to stop
            IsStarted = false;
        }

        void PortForwarder()
        {
            try { ForwarderLoop(); }
            catch (Exception e) { Log.Instance.Error(e.ToString()); }
        }

        void ForwarderLoop()
        {
            var serialBuffer = new byte[_serialPort.ReadBufferSize];
            var pipeBuffer = new byte[_serialPort.ReadBufferSize];

            _serialPort.Open();
            _namedPipe.Connect(5000);
            _namedPipe.ReadMode = PipeTransmissionMode.Byte;

            var pipeEvent = new ManualResetEvent(true);
            var serialEvent = new ManualResetEvent(true);
            var waitHandles = new WaitHandle[] { serialEvent, pipeEvent, _stopEvent };

            int waitResult;
            do
            {
                if (pipeEvent.WaitOne(0))
                {
                    pipeEvent.Reset();

                    _namedPipe.BeginRead(
                        pipeBuffer,
                        0,
                        pipeBuffer.Length,
                        namedPipeAsyncResult =>
                        {
                            try
                            {
                                int actualLength = _namedPipe.EndRead(namedPipeAsyncResult);
                                Logger.Debug("NP: " + GetLogString(pipeBuffer, actualLength));

                                _serialPort.BaseStream.BeginWrite(
                                    pipeBuffer,
                                    0,
                                    actualLength,
                                    serialPortAsyncResult =>
                                    {
                                        _serialPort.BaseStream.EndWrite(serialPortAsyncResult);
                                        // Logger.Debug("Wrote (CP): " + GetLogString(pipeBuffer, actualLength));
                                    }, null);
                            }
                            catch (IOException) { }
                            catch (ObjectDisposedException) { /* Aborted due to close */ }
                            catch (InvalidOperationException) { /* Aborted due to close */ }

                            pipeEvent.Set();
                        }, null);
                }

                if (serialEvent.WaitOne(0))
                {
                    serialEvent.Reset();

                    _serialPort.BaseStream.BeginRead(
                        serialBuffer,
                        0,
                        serialBuffer.Length,
                        serialPortAsyncResult =>
                        {
                            try
                            {
                                int actualLength = _serialPort.BaseStream.EndRead(serialPortAsyncResult);
                                Logger.Debug("CP: " + GetLogString(serialBuffer, actualLength));

                                _namedPipe.BeginWrite(
                                    serialBuffer,
                                    0,
                                    actualLength,
                                    namedPipeAsyncResult =>
                                    {
                                        _namedPipe.EndWrite(namedPipeAsyncResult);
                                        // Logger.Debug("Wrote (NP): " + GetLogString(serialBuffer, actualLength));
                                    }, null);
                            }
                            catch (IOException) { }
                            catch (ObjectDisposedException) { /* Aborted due to close */ }
                            catch (InvalidOperationException) { /* Aborted due to close */ }
                            serialEvent.Set();
                        }, null);
                }

                waitResult = WaitHandle.WaitAny(waitHandles);
            }
            while (waitResult != 2);

            _serialPort.Close();
            _namedPipe.Close();
        }
    }
}
