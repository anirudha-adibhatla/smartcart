﻿using Makeathon.Common.ViewModels;
using Makeathon.Messaging.Models;
using Makeathon.Messaging.Toast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using static Makeathon.Messaging.Toast.Toast;

namespace Makeathon
{
    public delegate void StateChangedHandler(MainUserControlViewModel.State _currentState);
    public delegate void AddLogEntryHandler(string _message, LogMessageType _logMessageType, string _feature);
    public delegate void ShowToastHandler(string _message, ToastMessageType _toastMessageType);
    public delegate void AddSmartCartItemHandler(SmartCartItem item);

    public class MainUserControlViewModel : BaseViewModel
    {
        SynchronizationContext uiContext;

        private Toast statusMessage;

        #region add cart item
        private static event AddSmartCartItemHandler AddSmartCartItemEvent;

        public static void AddSmartCartItem(SmartCartItem item)
        {
            AddSmartCartItemEvent?.Invoke(item);
        }

        void AddSmartCartItemEventHandler(SmartCartItem item)
        {            
            uiContext.Send(x => SmartCartItems.Add(item), null);
        }
        #endregion

        #region State
        //Refers to current state of the application
        public enum State
        {
            Default = 0,
            Busy,
            Ready
        }

        private State currentState;

        public State CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                RaisePropertyChanged("CurrentState");
            }
        }

        public static event StateChangedHandler StateChangedEvent;

        public static void SetState(State _state)
        {
            StateChangedEvent?.Invoke(_state);
        }

        void StateChangedHandler(State _state)
        {
            CurrentState = _state;
        }

        #endregion

        #region messaging

        #region toast
        public static event ShowToastHandler ShowToastEvent;

        public static void Toast(string _message, ToastMessageType _toastMessageType)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(()=> {
                ShowToastEvent?.Invoke(_message, _toastMessageType);
            }));            
        }

        private void ShowToastEventHandler(string _message, ToastMessageType _toastMessageType)
        {
            Toast toast = new Messaging.Toast.Toast();
            toast.Send(_message, _toastMessageType);
        }
        #endregion

        #region liveLogs
        private ObservableCollection<LogMessage> _logs;

        public ObservableCollection<LogMessage> Logs
        {
            get { return _logs; }
            set { _logs = value; RaisePropertyChanged("Logs"); }
        }

        public static event AddLogEntryHandler AddLogEvent;

        public static void AddLog(string _message, LogMessageType _logMessageType, string _feature = "")
        {
            AddLogEvent?.Invoke(_message, _logMessageType, _feature);
        }

        private void AddLogHandler(string _message, LogMessageType _logMessageType, string _feature)
        {
            LogMessage msg = new LogMessage();
            msg.Feature = _feature + " | ";
            msg.Message = _message;
            msg.LogMessageType = _logMessageType;
            msg.LogTime = DateTime.Now.ToString() + " | ";
            Logs.Add(msg);
        }

        #endregion

        public static void ToastAndLog(string _logMessage, LogMessageType _logMessageType, string _toastMessage, ToastMessageType _toastMessageType, string _feature = "")
        {
            Toast(_toastMessage, _toastMessageType);
            AddLog(_logMessage, _logMessageType, _feature);
        }
        #endregion

        private ICommand btnCmd;

        public ICommand BtnCmd
        {
            get { return btnCmd; }
            set
            {
                btnCmd = value;
                RaisePropertyChanged("BtnCmd");
            }
        }

        private ObservableCollection<SmartCartItem> smartCartItems;

        public ObservableCollection<SmartCartItem> SmartCartItems
        {
            get { return smartCartItems; }
            set { smartCartItems = value; RaisePropertyChanged("SmartCartItems"); }
        }


        public MainUserControlViewModel()
        {
            Logs = new ObservableCollection<LogMessage>();
            //set event handlers
            StateChangedEvent += StateChangedHandler;
            AddLogEvent += AddLogHandler;
            ShowToastEvent += ShowToastEventHandler;
            AddSmartCartItemEvent += AddSmartCartItemEventHandler;

            //set current state to ready
            SetState(State.Ready);

            SmartCartItems = new ObservableCollection<SmartCartItem>();

            uiContext = SynchronizationContext.Current;
        }

        public async Task StartGetItemsLoop()
        {
            await Task.Run(()=> {
                System.Timers.Timer myTimer = new System.Timers.Timer();
                // Tell the timer what to do when it elapses
                myTimer.Elapsed += new ElapsedEventHandler(GetItemsPollEvent);
                // Set it to go off every five seconds
                myTimer.Interval = 500;
                // And start it        
                myTimer.Enabled = true;
            });
        }

        private void GetItemsPollEvent(object src, ElapsedEventArgs e)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000");
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "application/json";
            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                List<SmartCartItem> items = JsonConvert.DeserializeObject<List<SmartCartItem>>(sr.ReadToEnd());

                items = items.Except(SmartCartItems.ToList()).ToList();
                foreach (var item in items)
                {
                    MainUserControlViewModel.AddSmartCartItem(item);
                }
            }
        }

        ~MainUserControlViewModel()
        {
            //unsubscribe StateChanged event from StateChangedHandler
            StateChangedEvent -= StateChangedHandler;
        }
    }
}