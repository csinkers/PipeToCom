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

        static string GetLogString(byte[] buffer, int length) =>
            Encoding.UTF8.GetString(buffer, 0, length)
            .Replace("\r", "\\r")
            .Replace("\n", "\\n");

        public Connection(Settings settings) => CurrentSettings = settings ?? throw new ArgumentNullException(nameof(settings));

        public void Start()
        {
            if (IsStarted)
                return;

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

            _stopEvent.Set(); // Signal the port forwarder thread to stop
            _portForwarder.Join(); // Wait for port forwarder thread to stop
            IsStarted = false;
        }

        void PortForwarder()
        {
            var serialBuffer = new byte[_serialPort.ReadBufferSize];
            var pipeBuffer = new byte[_serialPort.ReadBufferSize];

            _serialPort.Open();
            _namedPipe.Connect();
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
                                Logger.Debug("Read (NP): " + GetLogString(pipeBuffer, actualLength));

                                _serialPort.BaseStream.BeginWrite(
                                    pipeBuffer,
                                    0,
                                    actualLength,
                                    serialPortAsyncResult =>
                                    {
                                        _serialPort.BaseStream.EndWrite(serialPortAsyncResult);
                                        Logger.Debug("Wrote (CP): " + GetLogString(pipeBuffer, actualLength));
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
                                Logger.Debug("Read (CP): " + GetLogString(serialBuffer, actualLength));

                                _namedPipe.BeginWrite(
                                    serialBuffer,
                                    0,
                                    actualLength,
                                    namedPipeAsyncResult =>
                                    {
                                        _namedPipe.EndWrite(namedPipeAsyncResult);
                                        Logger.Debug("Wrote (NP): " + GetLogString(serialBuffer, actualLength));
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