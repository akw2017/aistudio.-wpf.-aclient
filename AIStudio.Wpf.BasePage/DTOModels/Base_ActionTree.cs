﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Util.Controls;

namespace AIStudio.Wpf.BasePage.DTOModels
{
    public class Base_ActionTree : BaseTreeItemViewModel, INotifyPropertyChanged
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public string Value { get; set; }

        public bool NeedAction { get; set; }

        public string Icon { get; set; }

        public int Sort { get; set; }

        public string TypeText { get; set; }

        public string NeedActionText { get; set; }
        public List<string> PermissionValues { get; set; }
        public string Text { get; set; }
        public object children { get; set; }

        private ObservableCollection<Base_ActionTree> _children;
        public new ObservableCollection<Base_ActionTree> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
                ClearChild();
                AddChildRange(_children ?? new ObservableCollection<Base_ActionTree>());
            }
        }
    }
}
