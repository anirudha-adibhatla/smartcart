using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// <para>
/// This class can instantiated by other classes to send toast messages 
/// This class handles the ToastMessageReceived event by creating and showing a toast window
/// </para>
/// </summary>
namespace Makeathon.Messaging.Toast
{
    public class Toast
    {
        public enum ToastMessageType
        {
            Default = 0,
            Info,
            Success,
            Error,
            Warning
        }

        private ToastMessageMediator toastMessageMediator;

        public Toast()
        {
            toastMessageMediator = new ToastMessageMediator();
        }

        public void Send(string _message, ToastMessageType _type = ToastMessageType.Info)
        {
            toastMessageMediator.Send(_message, _type);
        }
    }
}
