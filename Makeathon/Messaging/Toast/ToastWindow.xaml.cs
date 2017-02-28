using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Makeathon.Messaging.Toast
{
    /// <summary>
    /// Interaction logic for ToastWindow.xaml
    /// </summary>
    public partial class ToastWindow : MetroWindow
    {
        public ToastWindow()
        {
            double topBuffer = 35;
            double leftBuffer = 5;

            InitializeComponent();

            Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.Topmost = true;
                this.Owner = System.Windows.Application.Current.MainWindow;
                RECT mainWindowRect = GetWindowRectangle(Application.Current.MainWindow);

                this.Left = (mainWindowRect.Left + Application.Current.MainWindow.ActualWidth) - (this.Width + leftBuffer);
                this.Top = mainWindowRect.Top + topBuffer;
            }));
        }

        private void DoubleAnimationUsingKeyFrames_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // Make sure RECT is actually OUR defined struct, not the windows rect.
        public static RECT GetWindowRectangle(Window window)
        {
            RECT rect;
            GetWindowRect((new WindowInteropHelper(window)).Handle, out rect);

            return rect;
        }
    }
}
