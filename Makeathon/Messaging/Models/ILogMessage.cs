using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makeathon.Messaging.Models
{
    public enum LogMessageType
    {
        INFO,
        ERROR,
        WARNING,
        SUCCESS,
        ATTENTION,
        CRITICAL_INFO
    }
    interface ILogMessage
    {
        string LogTime { get; set; }
        string Message { get; set; }
        string Feature { get; set; }
        LogMessageType LogMessageType { get; set; }
    }
}
