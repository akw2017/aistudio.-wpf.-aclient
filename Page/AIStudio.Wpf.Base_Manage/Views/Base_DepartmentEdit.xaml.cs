﻿using AIStudio.Wpf.Base_Manage.ViewModels;
using AIStudio.Wpf.Controls;

namespace AIStudio.Wpf.Base_Manage.Views
{
    /// <summary>
    /// Base_DepartmentEdit.xaml 的交互逻辑
    /// </summary>
    public partial class Base_DepartmentEdit : BaseDialog
    {
        public Base_DepartmentEdit(object viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }
    }
}
