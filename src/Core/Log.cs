using System;
using System.Diagnostics.Tracing;

namespace NamedPipeSerialProxy.Core
{
    public class Log : ILog
    {
        public static Log Instance = new Log();
        public event EventHandler<(EventLevel, string)> Received;

        public void Debug(string message) => OnReceived((EventLevel.Verbose, message));
        public void Info(string message) => OnReceived((EventLevel.Informational, message));
        public void Warn(string message) => OnReceived((EventLevel.Warning, message));
        public void Error(string message) => OnReceived((EventLevel.Error, message));
        public void Critical(string message) => OnReceived((EventLevel.Critical, message));
        protected virtual void OnReceived((EventLevel, string) e) => Received?.Invoke(this, e);
    }
}