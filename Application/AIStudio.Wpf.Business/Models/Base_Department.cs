﻿using System;
using System.Collections.Generic;



namespace AIStudio.Wpf.EFCore.Models
{
    public partial class Base_Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string ParentIds { get; set; }
        public string ParentNames { get; set; }
        public int Level { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string ModifyId { get; set; }
        public string ModifyName { get; set; }
        public string TenantId { get; set; }
    }
}