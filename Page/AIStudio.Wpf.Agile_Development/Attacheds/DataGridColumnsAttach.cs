﻿using AIStudio.Wpf.Agile_Development.Commons;
using AIStudio.Wpf.Agile_Development.Extensions;
using AIStudio.Wpf.Controls.Behaviors;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AIStudio.Wpf.Agile_Development
{
    public class DataGridColumnsAttach
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached(
                "BindableColumns",
                typeof(ObservableCollection<DataGridColumnCustom>),
                typeof(DataGridColumnsAttach),
                new UIPropertyMetadata(null, OnBindableColumnsChanged));

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumnCustom> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }

        public static ObservableCollection<DataGridColumnCustom> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumnCustom>)element.GetValue(BindableColumnsProperty);
        }

        private static void OnBindableColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                ObservableCollection<DataGridColumnCustom> columns = e.NewValue as ObservableCollection<DataGridColumnCustom>;
                BehaviorCollection Behaviors = Interaction.GetBehaviors(dataGrid);
                Behaviors.Clear();
                dataGrid.Columns.Clear();
                Behaviors.Add(new SelectAllBahavior());

                if (columns == null)
                {
                    return;
                }

                var colums1 = columns.Where(p => p.DisplayIndex == int.MaxValue).ToList();
                var colums2 = columns.Except(colums1).OrderBy(p => p.DisplayIndex).ToList();
                foreach (DataGridColumnCustom column in colums1)
                {
                    dataGrid.Columns.Add(GetDataColumn(column));
                }
                foreach (DataGridColumnCustom column in colums2)
                {
                    dataGrid.Columns.Add(GetDataColumn(column));
                }            

                columns.CollectionChanged += (sender, e2) => {
                    NotifyCollectionChangedEventArgs ne = e2;
                    if (ne.Action == NotifyCollectionChangedAction.Reset)
                    {
                        dataGrid.Columns.Clear();
                        foreach (DataGridColumnCustom column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(GetDataColumn(column));
                        }
                    }
                    else if (ne.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (DataGridColumnCustom column in ne.NewItems)
                        {
                            dataGrid.Columns.Add(GetDataColumn(column));
                        }
                    }
                    else if (ne.Action == NotifyCollectionChangedAction.Move)
                    {
                        dataGrid.Columns.Move(ne.OldStartingIndex, ne.NewStartingIndex);
                    }
                    else if (ne.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (DataGridColumnCustom column in ne.OldItems)
                        {
                            dataGrid.Columns.Remove(GetDataColumn(column));
                        }
                    }
                    else if (ne.Action == NotifyCollectionChangedAction.Replace)
                    {
                        dataGrid.Columns[ne.NewStartingIndex] = GetDataColumn(ne.NewItems[0] as DataGridColumnCustom);
                    }
                };
            }
        }

        private static DataGridColumn GetDataColumn(DataGridColumnCustom columnCustom)
        {
            //var column = new DataGridTextColumn();
            //column.IsReadOnly = true;
            //column.Header = columnCustom.Header;
            //column.Binding = new Binding(columnCustom.Binding);

            var column = new DataGridTemplateColumn();
            column.IsReadOnly = true;
            column.Header = columnCustom.Header;
            if (columnCustom.DisplayIndex != int.MaxValue)
            {
                column.DisplayIndex = columnCustom.DisplayIndex;
            }
            column.Visibility = columnCustom.Visibility;
            column.CanUserSort = columnCustom.CanUserSort;
            column.CanUserReorder = columnCustom.CanUserReorder;
            column.CanUserResize = columnCustom.CanUserResize;
            column.SortMemberPath = columnCustom.SortMemberPath;
            if (columnCustom.MaxWidth > 0)
            {
                column.MaxWidth = columnCustom.MaxWidth;
            }
            if (columnCustom.MinWidth > 0)
            {
                column.MinWidth = columnCustom.MinWidth;
            }

            DataTemplate dt = new DataTemplate();

            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory textblock_fef = new FrameworkElementFactory(typeof(TextBlock));
            fef.AppendChild(textblock_fef);

            Binding bind = new Binding(columnCustom.PropertyName);
            textblock_fef.SetBinding(TextBlock.TextProperty, bind);
            textblock_fef.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textblock_fef.SetValue(TextBlock.HorizontalAlignmentProperty, columnCustom.HorizontalAlignment);

            if (!string.IsNullOrEmpty(columnCustom.StringFormat))
            {
                bind.StringFormat = columnCustom.StringFormat;
            }
            if (!string.IsNullOrEmpty(columnCustom.Converter))
            {
                bind.Converter = Activator.CreateInstance(columnCustom.Converter.GetAssemblyType()) as IValueConverter;
                bind.ConverterParameter = columnCustom.ConverterParameter;
            }

            if (!string.IsNullOrEmpty(columnCustom.ForegroundExpression))
            {
                Binding bindColor = new Binding(columnCustom.ForegroundExpression.StartsWith(";") ? columnCustom.PropertyName : ".");
                bindColor.Converter = new Converter.ExpressionConverter();
                bindColor.ConverterParameter = columnCustom.ForegroundExpression;
                textblock_fef.SetBinding(TextBlock.ForegroundProperty, bindColor);
            }

            if (!string.IsNullOrEmpty(columnCustom.BackgroundExpression))
            {
                Binding bindColor = new Binding(columnCustom.BackgroundExpression.StartsWith(";") ? columnCustom.PropertyName : ".");
                bindColor.Converter = new Converter.ExpressionConverter();
                bindColor.ConverterParameter = columnCustom.BackgroundExpression;
                fef.SetBinding(Grid.BackgroundProperty, bindColor);
            }
            dt.VisualTree = fef;
            column.CellTemplate = dt;
            return column;
        }
    }
}
