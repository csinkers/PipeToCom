using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace NamedPipeSerialProxy.UI
{
    /// <summary>
    /// Description of RichTextBoxAppender.
    /// </summary>
    public class RichTextBoxAppender : AppenderSkeleton
    {
        delegate void UpdateControlDelegate(log4net.Core.LoggingEvent loggingEvent);
        public RichTextBox RichTextBox { set; get; }

        void UpdateControl(log4net.Core.LoggingEvent loggingEvent)
        {
            // I looked at the TortoiseCVS code to figure out how
            // to use the RTB as a colored logger.  It noted a performance
            // problem when the buffer got long, so it cleared it every 100K.
            if (RichTextBox.TextLength > 100000)
            {
                RichTextBox.Clear();
                RichTextBox.SelectionColor = Color.Gray;
                RichTextBox.AppendText("(earlier messages cleared because of log length)\n\n");
            }

            switch (loggingEvent.Level.ToString())
            {
                case "INFO":
                    RichTextBox.SelectionColor = Color.Black;
                    break;
                case "WARN":
                    RichTextBox.SelectionColor = Color.Blue;
                    break;
                case "ERROR":
                    RichTextBox.SelectionColor = Color.Red;
                    break;
                case "FATAL":
                    RichTextBox.SelectionColor = Color.DarkOrange;
                    break;
                case "DEBUG":
                    RichTextBox.SelectionColor = Color.DarkGreen;
                    break;
                default:
                    RichTextBox.SelectionColor = Color.Black;
                    break;
            }

            var sb = new StringBuilder();
            using var sw = new StringWriter(sb);
            Layout.Format(sw, loggingEvent);
            sw.Flush();
            RichTextBox.AppendText(sb.ToString());
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            if (RichTextBox == null) 
                return;

            if (RichTextBox.InvokeRequired) // make thread safe
                RichTextBox.Invoke(new UpdateControlDelegate(UpdateControl), loggingEvent);
            else
                UpdateControl(loggingEvent);
        }

        public static void SetRichTextBox(RichTextBox rtb)
        {
            rtb.ReadOnly = true;
            rtb.HideSelection = false; // allows rtb to always append at the end
            rtb.Clear();

            foreach (RichTextBoxAppender appender in GetAppenders().OfType<RichTextBoxAppender>())
                appender.RichTextBox = rtb;
        }

        static IEnumerable<IAppender> GetAppenders()
        {
            var appenders = new ArrayList();
            appenders.AddRange(((Hierarchy)LogManager.GetRepository()).Root.Appenders);

            foreach (var log in LogManager.GetCurrentLoggers())
                appenders.AddRange(((Logger)log.Logger).Appenders);

            return (IAppender[])appenders.ToArray(typeof(IAppender));
        }
    }
}
