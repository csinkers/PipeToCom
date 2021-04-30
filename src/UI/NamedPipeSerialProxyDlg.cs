using System;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net.Layout;
using NamedPipeSerialProxy.Core;
using NamedPipeSerialProxy.Service;

namespace NamedPipeSerialProxy.UI
{
    public partial class NamedPipeSerialProxyDlg : Form
    {
        public NamedPipeSerialProxyDlg ()
        {
            InitializeComponent ();
        }

        void NamedPipeSerialProxyDlg_Load (object sender, EventArgs e)
        {
            namedPipeComboBox.Items.AddRange(Directory.GetFiles(@"\\.\pipe\"));
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            parityComboBox.Items.AddRange(Enum.GetNames(typeof(Parity)));
            stopBitsComboBox.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            if (namedPipeComboBox.Items.Count > 0) namedPipeComboBox.SelectedIndex = 0;
            if (serialPortComboBox.Items.Count > 0) serialPortComboBox.SelectedIndex = 0;
            baudRateComboBox.SelectedIndex = 10;
            parityComboBox.SelectedIndex = 0;
            dataBitsComboBox.SelectedIndex = 3;
            stopBitsComboBox.SelectedIndex = 1;

            var rtbAppender = new RichTextBoxAppender { RichTextBox = richTextBox1 };
            log4net.Config.BasicConfigurator.Configure(rtbAppender);
            rtbAppender.Layout = new PatternLayout("%-5p %d{HH:mm:ss,fff} %-22.22c{1} %-18.18M - %m%n");
        }

        void btnTest_Click (object sender, EventArgs e)
        {
            if (Connection != null && Connection.IsStarted)
            {
                btnTest.Text = "Test";
                Connection.Stop();
            }
            else
            {
                var namedPipe = Regex.Match((string) namedPipeComboBox.SelectedItem, @"\\\\(?<machine>[^\\]+)\\pipe\\(?<pipe>\w+)");
                Enum.TryParse((string) parityComboBox.SelectedItem, out Parity parity);
                Enum.TryParse((string) stopBitsComboBox.SelectedItem, out StopBits stopBits);
                Connection = new Connection(new Settings
                                                {
                                                    BaudRate = int.Parse((string) baudRateComboBox.SelectedItem),
                                                    ComPort = (string) serialPortComboBox.SelectedItem,
                                                    Parity = parity,
                                                    StopBits = stopBits,
                                                    DataBits = int.Parse((string) dataBitsComboBox.SelectedItem),
                                                    MachineName = namedPipe.Groups["machine"].Value,
                                                    NamedPipe = namedPipe.Groups["pipe"].Value,
                                                });
                Connection.Start();
                btnTest.Text = "Testing...";
            }
        }

        protected Connection Connection { get; set; }

        void btnWriteConfig_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
                          {
                              Filter = "NP2COM (*.n2c)|*.n2c",
                              AddExtension = true,
                              DefaultExt = "n2c",
                          };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var namedPipe = Regex.Match((string) namedPipeComboBox.SelectedItem,
                                            @"\\\\(?<machine>[^\\]+)\\pipe\\(?<pipe>\w+)");
                Enum.TryParse((string) parityComboBox.SelectedItem, out Parity parity);
                Enum.TryParse((string) stopBitsComboBox.SelectedItem, out StopBits stopBits);
                new Settings()
                    {
                        BaudRate = int.Parse((string) baudRateComboBox.SelectedItem),
                        ComPort = (string) serialPortComboBox.SelectedItem,
                        Parity = parity,
                        StopBits = stopBits,
                        DataBits = int.Parse((string) dataBitsComboBox.SelectedItem),
                        MachineName = namedPipe.Groups["machine"].Value,
                        NamedPipe = namedPipe.Groups["pipe"].Value,
                    }.Save(sfd.FileName);
            }
        }

        void btnTestService_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ConfigurationFiles (.n2c) files have to be in the current directory: (" + Path.GetFullPath(".") + ")");
            var comService = new NamedPipeSerialProxyService();
            comService.Start();
        }
    }
}
