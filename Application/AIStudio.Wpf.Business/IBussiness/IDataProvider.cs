﻿using AIStudio.AOP;
using AIStudio.Core;
using AIStudio.Core.Models;
using AIStudio.Wpf.Service.AppClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIStudio.Wpf.Business
{

    public interface IDataProvider
    {
        Task<AjaxResult> GetToken(string url, string userName, string password, int headMode, TimeSpan timeout);

        //[LogHandler]
        Task<AjaxResult<T>> GetData<T>(string url, Dictionary<string, string> data);

        //[LogHandler]
        Task<AjaxResult<T>> GetData<T>(string url, string json = "{}");
        Task<UploadResult> UploadFileByForm(string path);
    }
}
