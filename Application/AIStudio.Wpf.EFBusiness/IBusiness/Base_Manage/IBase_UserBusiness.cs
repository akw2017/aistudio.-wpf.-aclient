﻿using AIStudio.Core;
using AIStudio.Wpf.EFCore.DTOModels;
using AIStudio.Wpf.EFCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIStudio.Wpf.EFBusiness.Base_Manage
{
    public interface IBase_UserBusiness : IBaseBusiness<Base_User>
    {
        Task<PageResult<Base_UserDTO>> GetDataListAsync(PageInput<Base_UsersInputDTO> input);
        Task<object> GetDataListAsyncByDepartment(string departmentid);
        Task<Base_UserDTO> GetTheDataAsync(string id);
        Task AddDataAsync(UserEditInputDTO input);
        Task UpdateDataAsync(UserEditInputDTO input);
        Task DeleteDataAsync(List<string> ids);
        Task SetUserRoleAsync(string userId, List<string> roleIds);

        Task<string> GetAvatar(string userId);
    }

    [Map(typeof(Base_User))]
    public class UserEditInputDTO : Base_User
    {
        public string newPwd { get; set; }
        public List<string> RoleIdList { get; set; }
    }

    public class Base_UsersInputDTO
    {
        public bool all { get; set; }
        public string userId { get; set; }
        public string keyword { get; set; }
    }
}