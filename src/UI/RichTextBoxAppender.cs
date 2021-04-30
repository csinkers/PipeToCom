using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Windows.Forms;
using NamedPipeSerialProxy.Core;

namespace NamedPipeSerialProxy.UI
{
    /// <summary>
    /// Description of RichTextBoxAppender.
    /// </summary>
    public class RichTextBoxAppender
    {
        readonly RichTextBox _richTextBox;
        delegate void UpdateControlDelegate(EventLevel severity, string message);

        public RichTextBoxAppender(RichTextBox richTextBox, ILog log)
        {
            if (log == null) throw new ArgumentNullException(nameof(log));
            _richTextBox = richTextBox ?? throw new ArgumentNullException(nameof(richTextBox));
            log.Received += (sender, e) => Append(e.Item1, e.Item2);
        }

        void UpdateControl(EventLevel severity, string message)
        {
            // I looked at the TortoiseCVS code to figure out how
            // to use the RTB as a colored logger.  It noted a performance
            // problem when the buffer got long, so it cleared it every 100K.
            if (_richTextBox.TextLength > 100000)
            {
                _richTextBox.Clear();
                _richTextBox.SelectionColor = Color.Gray;
                _richTextBox.AppendText("(earlier messages cleared because of log length)\n\n");
            }

            switch (severity)
            {
                case EventLevel.Informational: _richTextBox.SelectionColor = Color.Black; break;
                case EventLevel.Warning: _richTextBox.SelectionColor = Color.DarkOrange; break;
                case EventLevel.Error: _richTextBox.SelectionColor = Color.Red; break;
                case EventLevel.Critical: _richTextBox.SelectionColor = Color.MediumPurple; break;
                case EventLevel.Verbose: _richTextBox.SelectionColor = Color.Green; break;
                default: _richTextBox.SelectionColor = Color.Black; break;
            }

            var formatted = $"{DateTime.Now:O} [{severity}] {message}";
            _richTextBox.AppendText(formatted);
        }

        public void Append(EventLevel severity, string message)
        {
            if (_richTextBox == null)
                return;

            if (_richTextBox.InvokeRequired) // make thread safe
                _richTextBox.Invoke((UpdateControlDelegate)UpdateControl, severity, message);
            else
                UpdateControl(severity, message);
        }
    }
}
