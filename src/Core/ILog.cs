using System;
using System.Diagnostics.Tracing;

namespace NamedPipeSerialProxy.Core
{
    public interface ILog
    {
        event EventHandler<(EventLevel, string)> Received;

        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Critical(string message);
    }
}