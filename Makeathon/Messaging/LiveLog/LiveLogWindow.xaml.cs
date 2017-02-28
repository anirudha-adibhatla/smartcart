using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace Makeathon.Messaging.LiveLog
{
    /// <summary>
    /// Interaction logic for LiveLogWindow.xaml
    /// </summary>
    public partial class LiveLogWindow : MetroWindow
    {
        private LiveLogViewModel liveLogViewModel;

        public LiveLogWindow()
        {
            InitializeComponent();
        }

        public void SetDataContext(LiveLogViewModel _liveLogViewModel)
        {
            Dispatcher.Invoke(() =>
            {
                liveLogViewModel = _liveLogViewModel;
                this.DataContext = liveLogViewModel;
                liveLogUserControl.DataContext = liveLogViewModel;
                liveLogViewModel.ShowYesNoMessageBoxRequested += new LiveLogViewModel.ShowYesNoMessageBoxEventHandler(ShowYesNOMessageBoxEventHandler);
            });
        }

        public void SafeShowDialog()
        {
            Dispatcher.Invoke(() =>
            {
                this.Show();
            });
        }

        public void SafeClose()
        {
            Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                e.Cancel = true;
                this.Close();
            });
        }

        private async Task<bool> ShowYesNoMessageBox(string _title, string _message)
        {
            MetroDialogSettings mds = new MetroDialogSettings();
            mds.AffirmativeButtonText = "Yes";
            mds.NegativeButtonText = "No";
            MessageDialogResult result = await this.ShowMessageAsync(_title, _message, MessageDialogStyle.AffirmativeAndNegative, mds);

            if (result == MessageDialogResult.Affirmative)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> ShowYesNOMessageBoxEventHandler(object sender, YesNoMessageBoxEventArgs args)
        {
            args.answer = await ShowYesNoMessageBox(args.Title, args.Message);
            return true;
        }
    }
}
