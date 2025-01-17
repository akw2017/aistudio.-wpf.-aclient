﻿using AIStudio.Core;
using AIStudio.Wpf.Base_Manage.Views;
using AIStudio.Wpf.BasePage.ViewModels;
using AIStudio.Wpf.Entity.DTOModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using AIStudio.Wpf.Controls;

namespace AIStudio.Wpf.Base_Manage.ViewModels
{
    public class Base_UserViewModel : BaseWindowViewModel<Base_UserDTO>
    {
        public Base_UserViewModel():base("Base_Manage", typeof(Base_UserEditViewModel), typeof(Base_UserEdit))
        {

        }

        protected override void GetData(bool iswaiting = false)
        {
            base.GetData(iswaiting);
        }

        protected override async void Edit(Base_UserDTO para = null)
        {
            var viewmodel = new Base_UserEditViewModel(para, Area, Identifier);
            var dialog = new Base_UserEdit(viewmodel);
            dialog.ValidationAction = (() =>
            {
                if (!string.IsNullOrEmpty(viewmodel.Data.Error))
                    return false;
                else
                    return true;
            });
            var res = (BaseDialogResult)await WindowBase.ShowDialogAsync2(dialog, Identifier);
            if (res == BaseDialogResult.OK)
            {
                try
                {
                    ShowWait();
                    viewmodel.Data.RoleIdList = viewmodel.SelectedRoles.Select(p => p.Value).ToList();
                    viewmodel.Data.DepartmentId = viewmodel.SelectedDepartment?.Id;
                    var result = await _dataProvider.GetData<AjaxResult>("/Base_Manage/Base_User/SaveData", JsonConvert.SerializeObject(viewmodel.Data));
                    if (!result.Success)
                    {
                        throw new Exception(result.Msg);
                    }
                    GetData(true);
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

        protected override void Delete(string id = null)
        {
            base.Delete(id);
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
