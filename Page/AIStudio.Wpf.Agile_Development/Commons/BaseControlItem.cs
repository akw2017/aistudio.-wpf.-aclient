﻿using AIStudio.Core;
using AIStudio.Wpf.Agile_Development.Attributes;
using AIStudio.Wpf.Business;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;


namespace AIStudio.Wpf.Agile_Development.Commons
{
    public abstract class BaseControlItem : BindableBase
    {
        protected static IUserData _userData { get; }

        static BaseControlItem()
        {
            _userData = ContainerLocator.Current.Resolve<IUserData>();
        }

        public int DisplayIndex
        {
            get; set;
        }

        public object Header
        {
            get; set;
        }

        public string PropertyName
        {
            get; set;
        }

        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                SetProperty(ref _visibility, value);
            }
        }

        public ControlType ControlType
        {
            get; set;
        }

        public object ItemSource
        {
            get; set;
        }

        public bool IsRequired
        {
            get; set;
        }

        public string StringFormat
        {
            get; set;
        }

        public bool IsReadOnly
        {
            get; set;
        }

        /// <summary>
        /// 正则校验表达式
        /// </summary>
        public string Regex
        {
            get; set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get; set;
        }

        public static bool GetControlItem(PropertyInfo property, BaseControlItem baseControlItem)
        {
            string itemSource = property.Name;
            var attribute = ColumnHeaderAttribute.GetPropertyAttribute(property);
            if (attribute != null)
            {
                if (attribute.Ignore)
                {
                    return false;
                }

                baseControlItem.Header = attribute.DisplayName ?? property.Name;
                baseControlItem.ControlType = attribute.ControlType;
                baseControlItem.IsRequired = attribute.IsRequired;
                baseControlItem.StringFormat = attribute.StringFormat;
                baseControlItem.DisplayIndex = attribute.DisplayIndex;

                if (!string.IsNullOrEmpty(attribute.ItemSource))
                {
                    itemSource = attribute.ItemSource;
                }
            }
            else if (_userData.Base_Dictionary.ContainsKey(property.Name))
            {
                var dic = _userData.Base_Dictionary[property.Name];
                baseControlItem.Header = dic.Text;
                baseControlItem.ControlType = dic.ControlType;
                baseControlItem.DisplayIndex = int.MaxValue;

                if (!string.IsNullOrEmpty(dic.Code))
                {
                    itemSource = dic.Code;
                }
            }
            else
            {
                baseControlItem.Header = property.Name;
                baseControlItem.DisplayIndex = int.MaxValue;
            }

            if (_userData.ItemSource.ContainsKey(itemSource))
            {
                //树形控件使用树形数据集
                if (baseControlItem.ControlType == ControlType.TreeSelect || baseControlItem.ControlType == ControlType.MultiTreeSelect)
                {
                    baseControlItem.ItemSource = _userData.ItemSource[$"{itemSource}Tree"];
                }
                else
                {
                    baseControlItem.ItemSource = _userData.ItemSource[itemSource];
                }
            }

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                if (string.IsNullOrEmpty(baseControlItem.StringFormat))
                {
                    baseControlItem.StringFormat = "n0";
                }
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.IntegerUpDown;
                }
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                if (string.IsNullOrEmpty(baseControlItem.StringFormat))
                {
                    baseControlItem.StringFormat = "n0";
                }
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.LongUpDown;
                }
            }
            else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
            {
                if (string.IsNullOrEmpty(baseControlItem.StringFormat))
                {
                    baseControlItem.StringFormat = "f3";
                }
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.DoubleUpDown;
                }
            }
            else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
            {
                if (string.IsNullOrEmpty(baseControlItem.StringFormat))
                {
                    baseControlItem.StringFormat = "f3";
                }
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.DecimalUpDown;
                }
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                if (string.IsNullOrEmpty(baseControlItem.StringFormat))
                {
                    baseControlItem.StringFormat = "yyyy-MM-dd HH:mm:ss";
                }
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.DateTimeUpDown;
                }
            }
            else
            {
                if (baseControlItem.ControlType == ControlType.None)
                {
                    baseControlItem.ControlType = ControlType.TextBox;
                }
            }

            baseControlItem.PropertyName = property.Name;

            return true;
        }

        public static void ObjectToList<T>(object value, IEnumerable<T> items) where T : BaseControlItem
        {
            IDictionary<string, object> dictionary = null;
            if (value is ExpandoObject keyValuePairs)
            {
                dictionary = (IDictionary<string, object>)keyValuePairs;
            }

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.PropertyName))
                    continue;

                if (value == null)
                {
                    item.Value = null;
                    continue;
                }

                if (dictionary != null)
                {
                    if (dictionary.ContainsKey(item.PropertyName))
                    {
                        item.Value = dictionary[item.PropertyName];
                    }
                    else
                    {
                        item.Value = null;
                    }
                }
                else
                {
                    var property = value.GetType().GetProperty(item.PropertyName);
                    if (property.CanRead)
                    {
                        item.Value = property.GetValue(value);
                    }
                }
            }
        }

        public static void ListToObject<T>(object value, IEnumerable<T> items) where T : BaseControlItem
        {
            string error = string.Empty;
            IDictionary<string, object> dictionary = null;
            if (value is ExpandoObject keyValuePairs)
            {
                dictionary = (IDictionary<string, object>)keyValuePairs;
            }

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.PropertyName))
                    continue;

                if (dictionary != null)
                {
                    if (dictionary.ContainsKey(item.PropertyName) || item.Value != null)
                    {
                        dictionary[item.PropertyName] = item.Value;
                    }

                    if (string.IsNullOrEmpty(error))
                    {
                        if (item.IsRequired)
                        {
                            if (item.Value == null || item.Value?.ToString() == string.Empty)
                            {
                                error = item.ErrorMessage ?? "必输项";
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Regex))
                        {
                            if (item.Value != null && item.Value?.ToString() != string.Empty)
                            {
                                Regex re = new Regex(item.Regex);
                                if (!re.IsMatch(item.Value?.ToString()))
                                {
                                    error = item.ErrorMessage ?? "正则校验失败";
                                    continue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    var propertyInfo = value.GetType().GetProperty(item.PropertyName);
                    if (propertyInfo.CanWrite)
                    {
                        object data;
                        if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            if (String.IsNullOrEmpty(item.Value?.ToString()))
                                data = null;
                            else
                                data = Convert.ChangeType(item.Value, propertyInfo.PropertyType.GetGenericArguments()[0]);
                        }
                        else
                        {
                            data = Convert.ChangeType(item.Value, propertyInfo.PropertyType);
                        }

                        propertyInfo.SetValue(value, data);
                    }
                }
            }

            if (dictionary != null)
            {
                dictionary["Error"] = error;
            }
        }

        public static Dictionary<string, object> ListToDictionary<T>(IEnumerable<T> items) where T : BaseControlItem
        {
            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.PropertyName))
                    continue;
                if (item.Value == null || item.Value.ToString() == string.Empty)
                    continue;

                keyValue.Add(item.PropertyName, item.Value);
            }

            return keyValue;
        }

        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
