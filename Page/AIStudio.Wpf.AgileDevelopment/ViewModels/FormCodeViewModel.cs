﻿using AIStudio.Core;
using AIStudio.Wpf.AgileDevelopment.ItemSources;
using AIStudio.Wpf.AgileDevelopment.Models;
using AIStudio.Wpf.AgileDevelopment.Views;
using AIStudio.Wpf.BasePage.ViewModels;
using AIStudio.Wpf.Controls;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AIStudio.Wpf.AgileDevelopment.ViewModels
{
    public class FormCodeViewModel : BasePageViewModel
    { 

        private Base_UserDTO_Test _base_User = new Base_UserDTO_Test();
        public Base_UserDTO_Test Base_User
        {
            get
            {
                return _base_User;
            }
            set
            {
                SetProperty(ref _base_User, value);
            }
        }

        public Dictionary<string, ObservableCollection<ISelectOption>> Items { get; set; } = ItemSourceDictionary.Items;

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return this._submitCommand ?? (this._submitCommand = new DelegateCommand<object>(para => this.Submit(para)));
            }
        }

        private ICommand _bulidCommand;
        public ICommand BulidCommand
        {
            get
            {
                return this._bulidCommand ?? (this._bulidCommand = new DelegateCommand<object>(para => this.Bulid(para)));
            }
        }

        public void Submit(object para)
        {
            string message = string.Empty;
            Type type = para.GetType();
            if (type.GetProperty("Error") != null)
            {
                message = type.GetProperty("Error").GetValue(para)?.ToString();
            }

            if (string.IsNullOrEmpty(message))
            {
                foreach (var prop in type.GetProperties())
                {
                    if (prop.CanRead && prop.CanWrite)
                    {
                        var ignoreAttr = prop.GetCustomAttributes(typeof(System.Xml.Serialization.XmlIgnoreAttribute), true);
                        if (ignoreAttr.Length > 0)
                        {
                            continue;
                        }

                        var displayAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                        string name = string.Empty;
                        if (displayAttr.Length > 0)
                        {
                            name = (displayAttr[0] as DisplayNameAttribute).DisplayName;
                        }
                        else
                        {
                            name = prop.Name;
                        }

                        var value = prop.GetValue(para);
                        if (value is IEnumerable<object> list)
                        {
                            value = string.Join(",", list.Select(p => p?.ToString()));
                        }

                        if (value == null || value.ToString() == "")
                        {
                            continue;
                        }
                        message += $"{name}:{value?.ToString()},";
                    }
                }
            }

            MessageBox.Show(System.Windows.Application.Current.MainWindow, message);
        }

        public FormCodeViewModel()
        {
           

        }       

        protected async void Bulid(object para)
        {
            if (para is Form form)
            {
                var items = form.Items.OfType<FormCodeItem>();

                var viewmodel = new FormCodeEditViewModel("123");
                var dialog = new FormCodeEdit(viewmodel);

                var res = (BaseDialogResult)await WindowBase.ShowDialogAsync(dialog, Identifier);
            }
        }
    }
}