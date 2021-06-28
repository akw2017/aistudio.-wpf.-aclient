﻿using NetSparkleUpdater.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetSparkleUpdater.UI.WPF
{
    /// <summary>
    /// Interaction logic for CheckingForUpdatesWindow.xaml.
    /// 
    /// Window that shows while NetSparkle is checking for updates.
    /// </summary>
    public partial class CheckingForUpdatesWindow : Window, ICheckingForUpdates
    {
        /// <inheritdoc/>
        public event EventHandler UpdatesUIClosing;

        /// <summary>
        /// Create the window that tells the user that SparkleUpdater is checking
        /// for updates
        /// </summary>
        public CheckingForUpdatesWindow()
        {
            InitializeComponent();
            Closing += CheckingForUpdatesWindow_Closing;
        }

        private void CheckingForUpdatesWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= CheckingForUpdatesWindow_Closing;
            UpdatesUIClosing?.Invoke(sender, new EventArgs());
        }

        void ICheckingForUpdates.Close()
        {
            Close();
        }

        void ICheckingForUpdates.Show()
        {
            Show();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
