﻿using AIStudio.Core;
using AIStudio.Wpf.Base_Manage.Views;
using AIStudio.Wpf.BasePage.DTOModels;
using AIStudio.Wpf.BasePage.ViewModels;
using AIStudio.Wpf.Entity.DTOModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using AIStudio.Wpf.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AIStudio.Wpf.Base_Manage.ViewModels
{
    public class Base_RoleViewModel : BaseWindowViewModel<Base_RoleDTO>
    {
        public Base_RoleViewModel():base("Base_Manage", typeof(Base_RoleEditViewModel),typeof(Base_RoleEdit),"RoleName")
        {
           
        }

        protected override async Task GetData(bool iswaiting = false)
        {
            await base.GetData(iswaiting);
        }

        protected override async void Edit(Base_RoleDTO para = null)
        {
            var viewmodel = new Base_RoleEditViewModel(para, Area, Identifier);

            var dialog = new Base_RoleEdit(viewmodel);
            dialog.ValidationAction = (() =>
            {
                if (!string.IsNullOrEmpty(viewmodel.Data.Error))
                    return false;
                else
                    return true;
            });
            var res = (DialogResult)await WindowBase.ShowChildWindowAsync(dialog, "编辑表单", Identifier);
            if (res == DialogResult.OK)
            {
                try
                {
                    ShowWait();
                    viewmodel.Data.Actions = new ObservableCollection<string>(BaseTreeItemViewModelHelper.GetChecked(viewmodel.ActionsTreeData).OfType<Base_ActionTree>().Select(p => p.Id));
                    var result = await _dataProvider.GetData<AjaxResult>($"/Base_Manage/Base_Role/SaveData", JsonConvert.SerializeObject(viewmodel.Data));
                    if (!result.Success)
                    {
                        throw new Exception(result.Msg);
                    }
                    GetData(true);
                }
                catch (Exception ex)
                {
                    Controls.MessageBox.Error(ex.Message);
                }
                finally
                {
                    HideWait();
                }
            }
        }

        protected override async Task Delete(string id = null)
        {
            await base.Delete(id);
        }

        protected override void Print()
        {
            base.Print(Data);
        }

        protected override void Search(object para=null)
        {
            base.Search(para);
        }
    }
}
