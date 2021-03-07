﻿using AIStudio.Wpf.BasePage.ViewModels;
using AIStudio.Wpf.Business.DTOModels;

namespace AIStudio.Wpf.OA_Manage.ViewModels
{
    public class OA_DefTypeEditViewModel : BaseEditViewModel<OA_DefTypeDTO>
    {
        public OA_DefTypeEditViewModel(OA_DefTypeDTO data, string area, string identifier, string title = "编辑表单") : base(data, area, identifier, title)
        {
            if (Data == null)
            {
                InitData();
            }
            else
            {
                GetData(Data);
            }
        }

		protected override void InitData()
		{
			Data = new OA_DefTypeDTO();
		}

        protected override void GetData(OA_DefTypeDTO para)
        {
            base.GetData(para);
        }       
    }
}
