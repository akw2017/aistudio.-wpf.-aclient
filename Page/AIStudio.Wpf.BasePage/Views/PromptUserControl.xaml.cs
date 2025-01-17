﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIStudio.Wpf.BasePage.Views
{
    /// <summary>
    /// PromptUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PromptUserControl : UserControl
    {
        public PromptUserControl(string txt)
        {
            InitializeComponent();
            text.Text = txt;
        }
    }
}
