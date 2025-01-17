﻿using AIStudio.Wpf.Home.ViewModels;
using System.Windows;
using System.Windows.Controls;
using AIStudio.Wpf.Controls;

namespace AIStudio.Wpf.Home.Views
{
    /// <summary>
    /// NoticeIconView.xaml 的交互逻辑
    /// </summary>
    public partial class NoticeIconView : UserControl
    {
        public NoticeIconView()
        {
            InitializeComponent();
        }

        private NoticeIconViewModel NoticeIconViewModel { get { return (DataContext as NoticeIconViewModel); } }

    }
}
