﻿using AIStudio.Core;
using AIStudio.Wpf.Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIStudio.Wpf.DataBusiness.Base_Manage
{
    public interface IBase_ActionBusiness: IBaseBusiness<Base_Action>
    {
        Task<List<Base_Action>> GetDataListAsync(Base_ActionsInputDTO input);
        Task<List<Base_ActionDTO>> GetTreeDataListAsync(Base_ActionsInputDTO input);
        Task<Base_Action> GetTheDataAsync(IdInputDTO input);
        Task AddDataAsync(ActionEditInputDTO input);
        Task UpdateDataAsync(ActionEditInputDTO input);
        Task DeleteDataAsync(List<string> ids);
        Task<List<Base_Action>> GetPermissionListAsync(Base_ActionsInputDTO input);
        Task<List<Base_Action>> GetAllActionListAsync();
        Task<List<Base_ActionDTO>> GetMenuTreeListAsync(Base_ActionsInputDTO input);
        Task<List<Base_ActionDTO>> GetActionTreeListAsync(Base_ActionsInputDTO input);
        Task SaveDataAsyncAsync(ActionEditInputDTO input);
    }

    [Map(typeof(Base_Action))]
    public class ActionEditInputDTO : Base_Action
    {
        public List<Base_Action> permissionList { get; set; } = new List<Base_Action>();
    }

    public class Base_ActionsInputDTO
    {
        public string[] ActionIds { get; set; }
        public string parentId { get; set; }
        public ActionType[] types { get; set; }
        public bool selectable { get; set; }
        public bool checkEmptyChildren { get; set; }
    }

    public class Base_ActionDTO : TreeModel
    {
        public ActionType Type { get; set; }
        public String Url { get; set; }
        public string path { get => Url; }
        public bool NeedAction { get; set; }
        public string TypeText { get => ((ActionType)Type).ToString(); }
        public string NeedActionText { get => NeedAction ? "是" : "否"; }
        public new object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
        public bool selectable { get; set; }
        [JsonIgnore]
        public string Icon { get; set; }
        public string icon { get => Icon; }
        public int Sort { get; set; }
        public List<string> PermissionValues { get; set; }
    }
}