﻿using System;
using System.Collections.Generic;

namespace AIStudio.Wpf.Entity.Models
{
    public partial class Base_User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public int Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string DepartmentId { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string ModifyId { get; set; }
        public string ModifyName { get; set; }
        public string TenantId { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
    }
}
