using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// <para>
/// This class acts as a mediator for toast messages
/// We declare the ToastMessageReceived event here
/// </para>
/// </summary>
namespace Makeathon.Messaging.Toast
{
    public delegate void ToastMessageRecievedHandler(string _message, Toast.ToastMessageType _type);

    class ToastMessageMediator
    {
        public event ToastMessageRecievedHandler ToastMessageRecieved;

        public void Send(string _message, Toast.ToastMessageType _type)
        {
            if (ToastMessageRecieved != null)
            {
                ToastMessageRecieved(_message, _type);
            }
        }

        public ToastMessageMediator()
        {
            ToastMessageRecieved += new ToastMessageRecievedHandler(Receive);
        }

        public void Receive(string _message, Toast.ToastMessageType _type)
        {
            ToastWindow toastWindow = new ToastWindow();
            ToastViewModel toastViewModel = new ToastViewModel();
            toastViewModel.Message = _message;
            toastViewModel.Type = _type;

            toastWindow.DataContext = toastViewModel;
            toastWindow.Show();
        }
    }
}
