using Makeathon.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Makeathon.Messaging.Toast.Toast;

namespace Makeathon.Messaging.Models
{
    public class LogMessage : BaseViewModel, ILogMessage
    {
        #region props
        private string _feature;

        public string Feature
        {
            get { return _feature; }
            set { _feature = value; RaisePropertyChanged("Feature"); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged("Message"); }
        }

        private string _logTime;

        public string LogTime
        {
            get { return _logTime; }
            set { _logTime = value; RaisePropertyChanged("LogTime"); }
        }

        private LogMessageType _logMessageType;

        public LogMessageType LogMessageType
        {
            get { return _logMessageType; }
            set { _logMessageType = value; RaisePropertyChanged("LogMessageType"); }
        }
        #endregion
    }
}
