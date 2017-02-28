using System;

namespace Makeathon.Messaging.LiveLog
{
    public class YesNoMessageBoxEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool answer { get; set; }
    }
}
