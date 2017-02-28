using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Makeathon.Common.Commands;
using Makeathon.Common.ViewModels;

namespace Makeathon.Messaging.LiveLog
{
    public class LiveLogViewModel : BaseViewModel
    {
        public enum MessageType
        {
            INFO,
            ERROR,
            WARNING,
            SUCCESS,
            ATTENTION,
            CONTINUE_BTN
        };

        public enum StatusType
        {
            READY,
            BUSY,
            ERROR,
            WAIT_FOR_CONTINUE_BTN
        };

        public enum Mode
        {
            Default = 0,
            NO_SCROLL_TO_BOTTOM_MODE,
            NORMAL
        }

        public delegate Task ShowYesNoMessageBoxEventHandler(object sender, YesNoMessageBoxEventArgs args);
        public event ShowYesNoMessageBoxEventHandler ShowYesNoMessageBoxRequested;



        private TaskCompletionSource<object> continueClicked;

        private bool shouldScrollToBottom;

        public bool ShouldScrollToBottom
        {
            get { return shouldScrollToBottom; }
            set
            {
                if (value != shouldScrollToBottom)
                {
                    shouldScrollToBottom = value;
                    RaisePropertyChanged("ShouldScrollToBottom");
                }
            }
        }


        private StatusType status;

        public StatusType Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        private Mode currentMode;

        public Mode CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }


        private ObservableCollection<LiveLogItem> liveLogs;

        public ObservableCollection<LiveLogItem> LiveLogs
        {
            get
            {
                return liveLogs;
            }
            set
            {
                if (value != liveLogs)
                {
                    liveLogs = value;
                    RaisePropertyChanged("LiveLogs");
                }
            }
        }

        private ICommand continueButtonCommand;

        public ICommand ContinueButtonCommand
        {
            get { return continueButtonCommand; }
            set { continueButtonCommand = value; }
        }


        public LiveLogViewModel()
        {
            CurrentMode = Mode.NORMAL;
            LiveLogs = new ObservableCollection<LiveLogItem>();
            continueButtonCommand = new ButtonCommand(new Action(() => { WaitForContinueToBeClicked(); }), new Func<bool>(() => { return true; }));
        }

        public void AddMessage(string _message, MessageType _type = MessageType.INFO, string _helpImage = null,
            bool _logThis = false,
            Exception exception = null,
            [CallerFilePathAttribute] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0,
            bool _setContinuButtonToVisible = false,
            bool _showTimeStamp = true)
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                LiveLogItem liveLogItem = new LiveLogItem();
                if (_showTimeStamp && _type != MessageType.ATTENTION)
                    liveLogItem.TimeStamp = DateTime.Now.ToString();
                liveLogItem.Message = _message;
                //if (_type == MessageType.ATTENTION)
                //    liveLogItem.Message = _message;
                //else
                //    liveLogItem.Message = DateTime.Now.ToString() + " | " + _message;
                liveLogItem.MessageType = _type;

                if (_helpImage != null)
                {
                    liveLogItem.HelpImage = _helpImage;
                    liveLogItem.Message = liveLogItem.Message + "\n";
                }

                if (_type == MessageType.ERROR)
                    Status = StatusType.ERROR;

                if (_setContinuButtonToVisible)
                    liveLogItem.ContinueBtnVisibility = Visibility.Visible;
                else
                    liveLogItem.ContinueBtnVisibility = Visibility.Hidden;

                if (CurrentMode != Mode.NO_SCROLL_TO_BOTTOM_MODE)
                    ShouldScrollToBottom = true;
                else if (CurrentMode == Mode.NO_SCROLL_TO_BOTTOM_MODE)
                    ShouldScrollToBottom = false;

                LiveLogs.Add(liveLogItem);
            }));
        }

        public void RaiseError(string _message, Exception exception = null, string _helpImage = null,
            bool _logThis = false,
            [CallerFilePathAttribute] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            AddMessage(_message, MessageType.ERROR, _helpImage, _logThis, exception, callerPath, callerMember, callerLine);
        }

        public void SetBusyStatus()
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Status = StatusType.BUSY;
            }));
        }

        public void SetReadyStatus()
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                if (Status != StatusType.ERROR)
                    Status = StatusType.READY;
            }));
        }

        public async Task AddContinueButtonAndWait(Exception exception = null, string _helpImage = null,
            bool _logThis = false,
            [CallerFilePathAttribute] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            StatusType prevStatus = Status;

            Status = StatusType.WAIT_FOR_CONTINUE_BTN;
            AddMessage("", MessageType.ATTENTION, _helpImage, _logThis, exception, callerPath, callerMember, callerLine, true);
            continueClicked = new TaskCompletionSource<object>();
            await continueClicked.Task;

            Status = prevStatus;
        }

        private void WaitForContinueToBeClicked()
        {
            if (continueClicked != null)
                continueClicked.TrySetResult(null);
        }

        public async Task<bool> ShowYesNoMessageBox(string _title, string _message)
        {
            YesNoMessageBoxEventArgs yesNoMessageBoxEventArgs = new YesNoMessageBoxEventArgs();
            yesNoMessageBoxEventArgs.Message = _message;
            yesNoMessageBoxEventArgs.Title = _title;

            if (ShowYesNoMessageBoxRequested != null)
                await ShowYesNoMessageBoxRequested(this, yesNoMessageBoxEventArgs);

            return yesNoMessageBoxEventArgs.answer;
        }
    }

    public class LiveLogItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string _propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(_propName));
        }

        private string message;

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (value != message)
                {
                    message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private LiveLogViewModel.MessageType messageType;

        public LiveLogViewModel.MessageType MessageType
        {
            get
            {
                return messageType;
            }
            set
            {
                if (value != messageType)
                {
                    messageType = value;
                    RaisePropertyChanged("MessageType");
                }
            }
        }

        private string helpImage;

        public string HelpImage
        {
            get
            {
                return helpImage;
            }
            set
            {
                if (value != helpImage)
                {
                    helpImage = value;
                    RaisePropertyChanged("HelpImage");
                }
            }
        }

        private Visibility continueBtnVisibility;

        public Visibility ContinueBtnVisibility
        {
            get { return continueBtnVisibility; }
            set
            {
                continueBtnVisibility = value;
                RaisePropertyChanged("ContinueBtnVisibility");
            }
        }

        private string timeStamp;

        public string TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
    }
}
