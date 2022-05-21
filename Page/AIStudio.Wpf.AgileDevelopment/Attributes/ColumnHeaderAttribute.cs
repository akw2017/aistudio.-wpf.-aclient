﻿using AIStudio.Core;
using AIStudio.Wpf.AgileDevelopment.Commons;
using AIStudio.Wpf.AgileDevelopment.Converter;
using AIStudio.Wpf.AgileDevelopment.ItemSources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Linq;

namespace AIStudio.Wpf.AgileDevelopment.Attributes
{
    public class ColumnHeaderAttribute : DisplayNameAttribute
    {
        public ColumnHeaderAttribute()
        {

        }

        public ColumnHeaderAttribute(string displayName) : base(displayName) { }


        public Visibility Visibility
        {
            get; set;
        } = Visibility.Visible;

        public int DisplayIndex
        {
            get; set;
        } = int.MaxValue;

        public string StringFormat
        {
            get; set;
        }

        public bool Ignore
        {
            get; set;
        }

        public Type Converter
        {
            get; set;
        }

        public string ItemSource
        {
            get; set;
        }

        public object ConverterParameter
        {
            get; set;
        }

        public string ForegroundExpression
        {
            get; set;
        }

        public string BackgroundExpression
        {
            get; set;
        }

        public bool IsPin
        {
            get; set;
        }

        public bool IsRequired
        {
            get; set;
        }

        public bool IsReadOnly
        {
            get; set;
        }

        public ControlType ControlType
        {
            get; set;
        } = ControlType.None;

        public HorizontalAlignment HorizontalAlignment
        {
            get; set;
        }

        public string SortMemberPath
        {
            get; set;
        }

        public bool CanUserSort
        {
            get; set;
        } = true;

        public static DataGridColumnCustom GetDataGridColumnCustom(PropertyInfo property)
        {
            DataGridColumnCustom dataGridColumnCustom = new DataGridColumnCustom();
            dataGridColumnCustom.Binding = property.Name;

            var attribute = ColumnHeaderAttribute.GetPropertyAttribute(property);
            if (attribute != null)
            {
                if (attribute.Ignore)
                {
                    return null;
                }

                dataGridColumnCustom.Header = attribute.DisplayName ?? property.Name;
                dataGridColumnCustom.StringFormat = attribute.StringFormat;
                dataGridColumnCustom.Visibility = attribute.Visibility;
                if (attribute.Converter != null)
                {
                    dataGridColumnCustom.Converter = attribute.Converter.FullName;
                    dataGridColumnCustom.ConverterParameter = attribute.ConverterParameter ?? attribute.ItemSource ?? property.Name;
                }
                dataGridColumnCustom.ForegroundExpression = attribute.ForegroundExpression;
                dataGridColumnCustom.BackgroundExpression = attribute.BackgroundExpression;
                dataGridColumnCustom.HorizontalAlignment = attribute.HorizontalAlignment;
                dataGridColumnCustom.DisplayIndex = attribute.DisplayIndex;
                dataGridColumnCustom.SortMemberPath = attribute.SortMemberPath ?? property.Name;
                dataGridColumnCustom.CanUserSort = attribute.CanUserSort;
            }
            else if (ItemSourceDictionary.Dictionarys.ContainsKey(property.Name))
            {
                var dic = ItemSourceDictionary.Dictionarys[property.Name];
                dataGridColumnCustom.Header = dic.Text;
                dataGridColumnCustom.DisplayIndex = int.MaxValue;
                dataGridColumnCustom.SortMemberPath = property.Name;
                dataGridColumnCustom.CanUserSort = true;
                dataGridColumnCustom.Converter = typeof(ObjectToStringConverter).FullName;
                if (!string.IsNullOrEmpty(dic.Code))
                {
                    dataGridColumnCustom.ConverterParameter = dic.Code;
                }
                else
                {
                    dataGridColumnCustom.ConverterParameter = dic.Value;
                }
            }
            else
            {
                dataGridColumnCustom.Header = property.Name;
                dataGridColumnCustom.DisplayIndex = int.MaxValue;
                dataGridColumnCustom.SortMemberPath = property.Name;
                dataGridColumnCustom.CanUserSort = true;
            }

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(long) || property.PropertyType == typeof(int?) || property.PropertyType == typeof(long?))
            {
                if (string.IsNullOrEmpty(dataGridColumnCustom.StringFormat))
                {
                    dataGridColumnCustom.StringFormat = "n0";
                }
            }
            else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(double?) || property.PropertyType == typeof(decimal?))
            {
                if (string.IsNullOrEmpty(dataGridColumnCustom.StringFormat))
                {
                    dataGridColumnCustom.StringFormat = "f3";
                }
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                if (string.IsNullOrEmpty(dataGridColumnCustom.StringFormat))
                {
                    dataGridColumnCustom.StringFormat = "yyyy-MM-dd HH:mm:ss";
                }
            }

            return dataGridColumnCustom;
        }

        public static ColumnHeaderAttribute GetPropertyAttribute(PropertyInfo pi)
        {
            if (ItemSourceDictionary.IgnoreSource.Contains(pi.Name))
            {
                return new ColumnHeaderAttribute() { Ignore = true };
            }

            if (pi != null)
            {
                Object[] attrs = pi.GetCustomAttributes(typeof(ColumnHeaderAttribute), true);
                foreach (var att in attrs)
                {
                    ColumnHeaderAttribute attribute = att as ColumnHeaderAttribute;
                    if ((attribute != null) && (attribute != ColumnHeaderAttribute.Default))
                    {
                        return attribute;
                    }
                }
            }

            return null;
        }

        public static ValidationAttribute GetValidationAttribute(PropertyInfo pi)
        {
            if (pi != null)
            {
                Object[] attrs = pi.GetCustomAttributes(typeof(ValidationAttribute), true);
                foreach (var att in attrs)
                {
                    ValidationAttribute attribute = att as ValidationAttribute;
                    if (attribute != null)
                    {
                        return attribute;
                    }
                }
            }

            return null;
        }
    }
}
