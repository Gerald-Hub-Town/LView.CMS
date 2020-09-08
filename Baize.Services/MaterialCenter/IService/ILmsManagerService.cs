using Baize.Core.Interfaces;
using Baize.Core.Models;
using Baize.Entities.MaterialCenter.Response;
using Baize.Entities.MaterialCenter.Input;
using Baize.Entities.MaterialCenter.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MagicOnion;

namespace Baize.Services.MaterialCenter.IService
{
    /// <summary>
    /// 工 具：白泽CodeBuilder Version:1.1.7.9
    /// 创 建：Gerald
    /// 日 期：2020/9/5 11:49:55
    /// 描 述：后台管理员表单
    /// </summary>
    public interface ILmsManagerService :IService<ILmsManagerService>, IDependency
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        UnaryResult<double> UploadAsync(IFormFile file, CurrentUser currentUser);

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <returns></returns>
        UnaryResult<byte[]> DownloadAsync();

        /// <summary>
        /// 新增后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UnaryResult<double> AddAsync(LmsManagerAddInput input);

        /// <summary>
        /// 修改后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UnaryResult<double> ModifyAsync(LmsManagerModifyInput input);

        /// <summary>
        /// 获取单个:后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UnaryResult<LmsManagerEntity> GetEntityAsync(LmsManagerQueryInput input);

        /// <summary>
        /// 获取后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UnaryResult<IEnumerable<LmsManagerEntity>> GetEntityListAsync(LmsManagerQueryInput input);
    
        /// <summary>
        /// 分页获取后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UnaryResult<PageEntity<IEnumerable<LmsManagerEntity>>> GetPageListAsync(PageEntity<LmsManagerQueryInput> input);
    }
}