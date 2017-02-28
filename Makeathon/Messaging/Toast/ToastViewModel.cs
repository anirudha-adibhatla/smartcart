using Makeathon.Common.ViewModels;

namespace Makeathon.Messaging.Toast
{
    class ToastViewModel : BaseViewModel
    {


        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        private Toast.ToastMessageType type;

        public Toast.ToastMessageType Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

    }
}
