﻿using AIStudio.Core;
using AIStudio.Wpf.Base_Manage.Views;
using AIStudio.Wpf.BasePage.ViewModels;
using AIStudio.Wpf.Business.DTOModels;
using AIStudio.Wpf.Business.DTOModels.DTOModels;
using AIStudio.Wpf.Service.AppClient;
using Newtonsoft.Json;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Util.Controls;

namespace AIStudio.Wpf.Base_Manage.ViewModels
{
    public class BuildCodeViewModel : BaseWindowViewModel<BuildCode>
    {
        private ObservableCollection<Base_DbLinkDTO> _base_DbLinkDTO;
        public ObservableCollection<Base_DbLinkDTO> Base_DbLinkDTO
        {
            get { return _base_DbLinkDTO; }
            set
            {
                if (_base_DbLinkDTO != value)
                {
                    _base_DbLinkDTO = value;
                    RaisePropertyChanged("Base_DbLinkDTO");
                }
            }
        }

        private string _linkId;
        public string LinkId
        {
            get { return _linkId; }
            set
            {
                if (_linkId != value)
                {
                    _linkId = value;
                    RaisePropertyChanged("LinkId");
                }
            }
        }

        private ICommand _addCommand;
        public new ICommand AddCommand
        {
            get
            {
                return this._addCommand ?? (this._addCommand = new CanExecuteDelegateCommand(() => this.Edit(), () => Data != null && Data.Count(p => p.IsChecked) > 0));
            }
        }      

        private string directory;
        private string tmpFileText;
        private string savePath;

        public BuildCodeViewModel() : base("Base_Manage", typeof(BuildCodeEditViewModel), typeof(BuildCodeEdit))
        {
            directory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString()).ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();
            GetData();
        }

        private async Task GetDbTableInfo()
        {
            var result = await _dataProvider.GetData<List<Base_DbLinkDTO>>($"/{Area}/BuildCode/GetAllDbLink");
            if (!result.IsOK)
            {
                throw new Exception(result.ErrorMessage);
            }
            else
            {
                Base_DbLinkDTO = new ObservableCollection<Base_DbLinkDTO>(result.ResponseItem);
            }
        }

        protected override async void GetData(bool iswaiting = false)
        {
            try
            {
                if (iswaiting == false)
                {
                    ShowWait();
                }

                await GetDbTableInfo();
                LinkId = Base_DbLinkDTO.FirstOrDefault()?.Id;

                var result = await _dataProvider.GetData<List<BuildCode>>($"/{Area}/{typeof(BuildCode).Name.Replace("DTO", "")}/GetDbTableList?linkId={LinkId}");
                if (!result.IsOK)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else
                {
                    Pagination.Total = result.Total;
                    Data = new ObservableCollection<BuildCode>(result.ResponseItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (iswaiting == false)
                {
                    HideWait();
                }
            }          
        }

        protected override async void Edit(BuildCode para = null)
        {
            var viewmodel = new BuildCodeEditViewModel(para, Identifier);
            var dialog = new BuildCodeEdit(viewmodel);
            dialog.ValidationAction = (() =>
            {
                if (viewmodel.BuildType.Any(p => p == true) == false)
                    return false;

                if (string.IsNullOrEmpty(viewmodel.AreaName))
                    return false;

                return true;
            });
            var res = (BaseDialogResult)await WindowBase.ShowDialogAsync(dialog, Identifier);
            if (res == BaseDialogResult.OK)
            {
                try
                {
                    ShowWait();

                    List<string> ids = Data.Where(p => p.IsChecked).Select(p => p.TableName).ToList();
                    List<int> types = viewmodel.BuildType.Take(4).Select((p, index) => new { value = p == true ? 1 : 0, index = index }).Where(p => p.value == 1).Select(p => p.index).ToList();

                    if (types.Count > 0)
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("linkId", LinkId);
                        data.Add("areaName", viewmodel.AreaName);
                        data.Add("tablesJson", JsonConvert.SerializeObject(ids));
                        data.Add("buildTypesJson", JsonConvert.SerializeObject(types));

                        var result = await _dataProvider.GetData<AjaxResult>("/Base_Manage/BuildCode/Build", data);
                        if (!result.IsOK)
                        {
                            throw new Exception(result.ErrorMessage);
                        }
                    }

                    if (viewmodel.BuildType[4] == true)
                    {
                        Generate(viewmodel.AreaName, ids);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    HideWait();
                }
            }
        }

        private static readonly List<string> ignoreProperties =
            new List<string> { "Id", "CreateTime", "CreatorId", "CreatorName", "Deleted","ModifyTime", "ModifyId", "ModifyName", "TenantId" };

        private void Generate(string areaName,List<string> entityNames)
        {
            foreach (var entityName in entityNames)
            {
                #region Model
                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "DTO.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               "AIStudio.Wpf.Business.DTOModels",
                               "DTOModels",
                               $"{areaName}",
                               $"{entityName}DTO.cs");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);
                #endregion

                #region ViewModel
                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "Listviewmodel.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "ViewModels",
                               $"{entityName}ViewModel.cs");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);

                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "EditFormviewmodel.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "ViewModels",
                               $"{entityName}EditViewModel.cs");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);
                #endregion

                #region View
                string fullClassName = "connectionString." + entityName;

                //根据类名称创建类实例
                var type = System.Reflection.Assembly.Load("AIStudio.Wpf.Business.DTOModels").GetType(fullClassName);
                List<string> selectOptionsList = new List<string>();
                List<string> listColumnsList = new List<string>();
                List<string> formColumnsList = new List<string>();

                foreach (System.Reflection.PropertyInfo info in type.GetProperties().Where(p => !ignoreProperties.Contains(p.Name)))
                {
                    selectOptionsList.Add(
$"				<ComboBoxItem Tag=\"{info.Name}\">{info.Name}</ComboBoxItem>");

                    listColumnsList.Add(
$"				<DataGridTextColumn Header=\"{info.Name}\"  Binding=\"{{Binding {info.Name}}}\" IsReadOnly=\"True\"/>");

                    formColumnsList.Add(
"                        <HeaderedContentControl Style=\"{StaticResource LeftFormItemStyle}\">" + "\r\n" +
"                            <HeaderedContentControl.Header>" + "\r\n" +
"                                <TextBlock VerticalAlignment=\"Center\">" + "\r\n" +
"                                    <Run Foreground=\"Red\">*</Run>" + "\r\n" +
"                                    <Run>" + info.Name + "</Run>" + "\r\n" +
"                                </TextBlock>" + "\r\n" +
"                            </HeaderedContentControl.Header>" + "\r\n" +
"                            <TextBox Text=\"{Binding " + info.Name + ",Mode=TwoWay,UpdateSourceTrigger=PropertyChanged, ValidatesOnExceptions=True, ValidatesOnDataErrors=True, NotifyOnValidationError=True}\" Style=\"{StaticResource ToolTipErrorTextBox}\" IsReadOnly=\"{ Binding Disabled}\"></TextBox>" + "\r\n" +
"                        </HeaderedContentControl>");


                }

                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "List.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                tmpFileText = tmpFileText.Replace("%selectOptions%", string.Join("\r\n", selectOptionsList));
                tmpFileText = tmpFileText.Replace("%listColumns%", string.Join("\r\n", listColumnsList));

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "Views",
                               $"{entityName}View.xaml");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);

                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "EditForm.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                tmpFileText = tmpFileText.Replace("%formColumns%", string.Join("\r\n", formColumnsList));

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "Views",
                               $"{entityName}Edit.xaml");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);
                #endregion

                #region View.cs
                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "Listcs.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "Views",
                               $"{entityName}View.xaml.cs");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);

                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "EditFormcs.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "Views",
                               $"{entityName}Edit.xaml.cs");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);
                #endregion

                #region Resource
                tmpFileText = File.ReadAllText(Path.Combine(directory, "AIStudio.Wpf.Home", "BuildCodeTemplate", "Listresource.txt"));

                tmpFileText = tmpFileText.Replace("%areaName%", areaName).Replace("%entityName%", entityName);

                savePath = Path.Combine(
                               directory,
                               $"AIStudio.Wpf.{areaName}",
                               "Resources",
                               "Dock",
                               $"{entityName}ViewResource.xaml");

                FileHelper.WriteTxt(tmpFileText, savePath, Encoding.UTF8, FileMode.Create);
                #endregion
            }
        }



    }
}
