﻿using AIStudio.Wpf.Base_Manage.ViewModels;
using AIStudio.Wpf.Controls;

namespace AIStudio.Wpf.Base_Manage.Views
{
    /// <summary>
    /// Base_DictionaryEdit.xaml 的交互逻辑
    /// </summary>
    public partial class Base_DictionaryEdit : BaseDialog
    {
        public Base_DictionaryEdit(object viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }
    }
}
