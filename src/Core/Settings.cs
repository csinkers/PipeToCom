using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Xml.Serialization;
using static System.FormattableString;

namespace NamedPipeSerialProxy.Core
{
    public class Settings
    {
        public string MachineName { get; set; }
        public string NamedPipe { get; set; }
        public string ComPort { get; set; }
        public int BaudRate { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        [XmlIgnore] public string PipePath => Invariant($@"\\{MachineName}\pipe\{NamedPipe}");

        public static string EntryPointDirectory
        {
            get
            {
                var entryFile = Assembly.GetEntryAssembly()?.Location;
                return string.IsNullOrEmpty(entryFile) ? null : Path.GetDirectoryName(entryFile);
            }
        }

        public static string Filename
        {
            get
            {
                var dir = EntryPointDirectory;
                return string.IsNullOrEmpty(dir) ? null : Path.Combine(dir, "Settings.n2c");
            }
        }

        public static Settings Defaults => new Settings
        {
            MachineName = ".",
            NamedPipe = "pos_com1",
            ComPort = "COM5",
            BaudRate = 9600,
            StopBits = StopBits.One,
            Parity = Parity.None,
            DataBits = 8,
        };

        public static Settings Load(string filename)
        {
            if (!File.Exists(filename))
                return Defaults;

            using var fs = File.OpenRead(filename);
            return (Settings)new XmlSerializer(typeof(Settings)).Deserialize(fs);
        }

        public void Save(string filename)
        {
            using var fs = File.Open(filename, FileMode.Create);
            new XmlSerializer(typeof(Settings)).Serialize(fs, this);
        }
    }
}