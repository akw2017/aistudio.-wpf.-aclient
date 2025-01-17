﻿using AIStudio.Core;
using AIStudio.Wpf.DataBusiness.AOP;
using AIStudio.Wpf.Entity.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIStudio.Wpf.DataBusiness.Base_Manage
{
    public class Base_AppSecretBusiness : BaseBusiness<Base_AppSecret>, IBase_AppSecretBusiness, ITransientDependency
    {
        public Base_AppSecretBusiness()
        {
        }

        #region 外部接口

        public async Task<PageResult<Base_AppSecret>> GetDataListAsync(PageInput<AppSecretsInputDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_AppSecret>();
            var search = input.Search;
            if (!search.keyword.IsNullOrEmpty())
            {
                where = where.And(x =>
                    x.AppId.Contains(search.keyword)
                    || x.AppSecret.Contains(search.keyword)
                    || x.AppName.Contains(search.keyword));
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

    
        public async Task<Base_AppSecret> GetTheDataAsync(IdInputDTO input)
        {
            return await GetEntityAsync(input.id);
        }

        public async Task<string> GetAppSecretAsync(string appId)
        {
            var theData = await GetIQueryable().Where(x => x.AppId == appId).FirstOrDefaultAsync();

            return theData?.AppSecret;
        }

  
        public async Task AddDataAsync(Base_AppSecret newData)
        {
            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(Base_AppSecret theData)
        {
            await UpdateAsync(theData);
        }

        [DataDeleteLog(UserLogType.接口密钥管理, "AppId", "应用Id")]
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion

        [DataRepeatValidate(new string[] { "AppId" }, new string[] { "应用Id" },Order =1)]
        [DataSaveLog(UserLogType.接口密钥管理, "AppId", "应用Id", Order =2)]
        public async Task SaveDataAsync(Base_AppSecret theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                InitEntity(theData);

                await AddDataAsync(theData);
            }
            else
            {
                UpdateEntity(theData);
                await UpdateDataAsync(theData);
            }
        }
    }
}