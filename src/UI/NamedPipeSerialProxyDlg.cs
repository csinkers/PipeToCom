using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NamedPipeSerialProxy.Core;
using NamedPipeSerialProxy.Service;

namespace NamedPipeSerialProxy.UI
{
    public partial class NamedPipeSerialProxyDlg : Form
    {
        RichTextBoxAppender _rtbAppender;

        public NamedPipeSerialProxyDlg()
        {
            InitializeComponent();
        }

        void NamedPipeSerialProxyDlg_Load(object sender, EventArgs e)
        {
            var settings = Settings.Load(Settings.Filename);

            namedPipeComboBox.Items.AddRange(Directory.GetFiles(@"\\.\pipe\").Cast<object>().ToArray());
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames().OrderBy(x => x).Cast<object>().ToArray());
            parityComboBox.Items.AddRange(Enum.GetNames(typeof(Parity)).Cast<object>().ToArray());
            stopBitsComboBox.Items.AddRange(Enum.GetNames(typeof(StopBits)).Cast<object>().ToArray());

            namedPipeComboBox.Text = settings.PipePath;
            SetSerialPort(settings);
            SetBaudRate(settings);
            SetParity(settings);
            SetDataBits(settings);
            SetStopBits(settings);

            richTextBox1.ReadOnly = true;
            richTextBox1.HideSelection = false; // allows text box to always append at the end
            richTextBox1.Clear();
            _rtbAppender = new RichTextBoxAppender(richTextBox1, Log.Instance);
        }

        void btnTest_Click(object sender, EventArgs e)
        {
            if (Connection != null && Connection.IsStarted)
            {
                btnTest.Text = "Test";
                Connection.Stop();
            }
            else
            {
                try
                {
                    var settings = BuildSettings();
                    Connection = new Connection(settings);
                    Connection.Start();
                    btnTest.Text = "Testing...";
                }
                catch (FormatException exception) { MessageBox.Show(exception.Message); }
            }
        }

        protected Connection Connection { get; set; }
        void btnWriteConfig_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "n2c",
                Filter = "NP2COM (*.n2c)|*.n2c",
                InitialDirectory = Settings.EntryPointDirectory
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try { BuildSettings().Save(sfd.FileName); }
            catch (FormatException exception) { MessageBox.Show(exception.Message); }
        }

        void btnWriteDefaultConfig_Click(object sender, EventArgs e)
        {
            try { BuildSettings().Save(Settings.Filename); }
            catch (FormatException exception) { MessageBox.Show(exception.Message); }
        }

        void btnTestService_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"ConfigurationFiles (.n2c) files have to be in the current directory: ({Path.GetFullPath(".")})");
            var comService = new NamedPipeSerialProxyService();
            comService.Start();
        }

        static readonly Regex PipeNameRegex = new Regex(@"\\\\(?<machine>[^\\]+)\\pipe\\(?<pipe>\w+)");

        Settings BuildSettings()
        {
            var namedPipe = PipeNameRegex.Match(namedPipeComboBox.Text ?? "");
            if (!namedPipe.Success)
                throw new FormatException($"Pipe name \"{namedPipeComboBox.Text}\" is invalid");

            if (!Enum.TryParse(parityComboBox.Text, out Parity parity))
                throw new FormatException($"Parity \"{parityComboBox.Text}\" is not a valid value");

            if (!Enum.TryParse(stopBitsComboBox.Text, out StopBits stopBits))
                throw new FormatException($"Stop bits \"{stopBitsComboBox.Text}\" is not a valid value");

            return new Settings
            {
                BaudRate = int.Parse(baudRateComboBox.Text),
                ComPort = serialPortComboBox.Text,
                Parity = parity,
                StopBits = stopBits,
                DataBits = int.Parse(dataBitsComboBox.Text),
                MachineName = namedPipe.Groups["machine"].Value,
                NamedPipe = namedPipe.Groups["pipe"].Value,
            };
        }

        void SetSerialPort(Settings settings)
        {
            if (serialPortComboBox.Items.Count <= 0) return;

            int? index = serialPortComboBox.Items
                .OfType<string>()
                .Select((x, i) => x == settings.ComPort ? (int?) i : null)
                .FirstOrDefault(x => x.HasValue);

            serialPortComboBox.SelectedIndex = index ?? 0;
        }

        void SetBaudRate(Settings settings)
        {
            var index = baudRateComboBox.Items
                .OfType<string>()
                .Select((x, i) => int.Parse(x) == settings.BaudRate ? (int?) i : null)
                .FirstOrDefault(x => x.HasValue);

            baudRateComboBox.SelectedIndex = index ?? 0;
        }

        void SetDataBits(Settings settings)
        {
            var index = dataBitsComboBox.Items
                .OfType<string>()
                .Select((x, i) => int.Parse(x) == settings.DataBits ? (int?) i : null)
                .FirstOrDefault(x => x.HasValue);

            dataBitsComboBox.SelectedIndex = index ?? 0;
        }

        void SetParity(Settings settings)
        {
            var index = parityComboBox.Items
                .OfType<string>()
                .Select((x, i) => x == settings.Parity.ToString() ? (int?) i : null)
                .FirstOrDefault(x => x.HasValue);

            parityComboBox.SelectedIndex = index ?? 0;
        }

        void SetStopBits(Settings settings)
        {
            var index = stopBitsComboBox.Items
                .OfType<string>()
                .Select((x, i) => x == settings.StopBits.ToString() ? (int?) i : null)
                .FirstOrDefault(x => x.HasValue);

            stopBitsComboBox.SelectedIndex = index ?? 0;
        }
    }
}
