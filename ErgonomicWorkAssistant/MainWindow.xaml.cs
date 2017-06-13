using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;


namespace ErgonomicWorkAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon notifyIcon = null;
        System.Windows.Forms.ContextMenu cms = null;
        System.Windows.Forms.MenuItem menuItem = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            #region Setup Tray Logic

            notifyIcon = new NotifyIcon();

            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            cms = new System.Windows.Forms.ContextMenu();

            menuItem = new System.Windows.Forms.MenuItem();
            menuItem.Index = 0;
            menuItem.Text = "Exit";
            menuItem.Click += MenuItem_Click;
            cms.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { menuItem });

            notifyIcon.ContextMenu = cms;
            notifyIcon.Icon = new Icon(@"..\..\Icons\Robot.ico");

            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;
            Closing += MainWindow_Closing;
            Closed += MainWindow_Closed;

            #endregion

            base.OnInitialized(e);
        }

        #region Events

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                if (notifyIcon != null)
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                    notifyIcon = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
            }
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Topmost = false;
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon.Visible = true;
                this.ShowInTaskbar = true;
                this.Topmost = true;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            notifyIcon.Visible = true;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            NotifyIcon_Click(sender, e);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (!(((MouseEventArgs)e).Button == MouseButtons.Right))
            {
                ShowNavigationWindow();
            }
        }

        #endregion

        #region Methods
        private void ShowNavigationWindow()
        {
            this.Show();
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
        #endregion
    }
}
