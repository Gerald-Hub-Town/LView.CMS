using Baize.Core.Models;
using Baize.Entities.MaterialCenter.Entity;
using Baize.Entities.MaterialCenter.Input;
using Baize.Services.MaterialCenter.IService;
using SQLBuilder.Core.Repositories;
using System;
using MagicOnion;
using MagicOnion.Server;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Baize.Entities.Const;
using System.Linq.Expressions;

namespace Baize.Services.MaterialCenter.Service
{
    using Baize.Core.Extensions;
    using Baize.Entities.MaterialCenter.Response;
    using System.Text;
    using System.IO;
    using Baize.Core.Exceptions;
    using Baize.Core.Const;
    using Baize.Core.Helpers;
    using Microsoft.Extensions.Options;
    using System.Linq;

    /// <summary>
    /// 工 具：白泽CodeBuilder Version:1.1.7.9
    /// 创 建：Gerald
    /// 日 期：2020/9/5 11:49:55
    /// 描 述：后台管理员表单
    /// </summary>
    public class LmsManagerService :ServiceBase<ILmsManagerService>, ILmsManagerService
    {
        /// <summary>
        /// 仓储字段
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// 静态配置
        /// </summary>
        private readonly List<AssetsOptions> _assetsOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LmsManagerService(Func<string, IRepository> handler,
            IOptions<List<AssetsOptions>> options)
        {
            _repository = handler(null);
            _assetsOptions = options.Value;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async UnaryResult<double> UploadAsync(IFormFile file, CurrentUser currentUser)
        {
            await Task.CompletedTask;
            return ErrorCode.Successful;
            /*
            var result = false;
            var options = _assetsOptions.First(x => x.AssetsType == "excel");
            //检查文件大小
            if (file != null &&
                file.Length > 0 &&
                file.Length <= options.MaxSize)
            {
                //提取上传的文件文件后缀
                var suffix = Path.GetExtension(file.FileName)?.ToLower();
                //允许的文件格式
                var fileTypes = options.FileTypes.Split(',').Select(x => x.ToLower());
                //检查文件格式
                if (!suffix.IsNullOrEmpty() && fileTypes.Contains(suffix))
                {
                    //读取Excel文件流
                    var list = ExcelHelper.EPPlusReadExcel<LmsManagerExcelInput>(file.OpenReadStream()).FirstOrDefault();
                    //判断是否有内容
                    if (list?.Count > 0)
                    {
                        var inserts = new List<LmsManagerEntity>();
                        var updates = new List<LmsManagerEntity>();
                        list.MapTo<LmsManagerEntity>().ForEach(_ =>
                        {
                            var has = _repository.FindEntity<LmsManagerEntity>(x => x.Account == _.Account);
                            if (has == null)
                            {
                                _.Create(currentUser);
                                inserts.Add(_);
                            }
                            else
                            {
                                _.Modify(has.Id, currentUser);
                                updates.Add(_);
                            }
                        });
                        var dbTran = _repository.BeginTrans();
                        try
                        {
                            inserts.ForEach(async x => { await _repository.InsertAsync(x); });
                            updates.ForEach(async x => { await _repository.UpdateAsync(x); });
                            dbTran.Commit();
                            await Task.CompletedTask;
                        }
                        catch (Exception)
                        {
                            dbTran.Rollback();
                            throw;
                        }
                        result = true;
                    }
                }
            }

            if (!result)
                throw new TipsException(ErrorCode.Invalid_Input.ToString());
            */
        }

        public async UnaryResult<byte[]> DownloadAsync()
        {
            var options = _assetsOptions.First(x => x.AssetsType == "excel");
            var path = Path.Combine(options.FilePath, "Template", "替换成你的文件名.xlsx");
            if (File.Exists(path))
                return await File.ReadAllBytesAsync(path);

            return null;
        }

        /// <summary>
        /// 新增后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async UnaryResult<double> AddAsync(LmsManagerAddInput input)
        {
            var entity = input.MapTo<LmsManagerEntity>();
            entity.Create(input.CurrentUser);
            await _repository.InsertAsync(entity);
            return ErrorCode.Successful;
        }

        /// <summary>
        /// 修改后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async UnaryResult<double> ModifyAsync(LmsManagerModifyInput input)
        {
            var entity = input.MapTo<LmsManagerEntity>();
            entity.Modify(input.Id, input.CurrentUser);
            await _repository.UpdateAsync(entity);
            return ErrorCode.Successful;
        }

        /// <summary>
        /// 获取单个:后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async UnaryResult<LmsManagerEntity> GetEntityAsync(LmsManagerQueryInput input)
        {
            var condition = this.GetEntityExpression(input);

            return await _repository.FindEntityAsync(condition);
        }
        /// <summary>
        /// 获取所有后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async UnaryResult<IEnumerable<LmsManagerEntity>> GetEntityListAsync(LmsManagerQueryInput input)
        {
            var condition = this.GetEntityExpression(input);

            return await _repository.FindListAsync(condition);
        }
        /// <summary>
        /// 分页获取后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async UnaryResult<PageEntity<IEnumerable<LmsManagerEntity>>> GetPageListAsync(PageEntity<LmsManagerQueryInput> input)
        {
            var condition = this.GetEntityExpression(input.Data);

            //分页查询
            var (data, total) = await _repository.FindListAsync(condition, input.OrderField, input.Ascending, input.PageSize, input.PageIndex);
            return new PageEntity<IEnumerable<LmsManagerEntity>>
            {
                PageIndex = input.PageIndex,
                Ascending = input.Ascending,
                PageSize = input.PageSize,
                OrderField = input.OrderField,
                Total = total,
                Data = data
            };
        }

        /// <summary>
        /// 获取单个表达式目录树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Expression<Func<LmsManagerEntity,bool>> GetEntityExpression(LmsManagerQueryInput input)
        {
            var condition = LinqExtensions.True<LmsManagerEntity>();

            //主键Id
            if (!input.Id.IsNullOrEmpty())
                condition = condition.And(x => x.Id == input.Id);

            //用户名
            if (!input.UserName.IsNullOrEmpty())
                condition = condition.And(x => x.UserName.Contains(input.UserName));
            
            //昵称
            if (!input.NickName.IsNullOrEmpty())
                condition = condition.And(x => x.NickName.Contains(input.NickName));
            
            //手机号码
            if (!input.Mobile.IsNullOrEmpty())
                condition = condition.And(x => x.Mobile.Contains(input.Mobile));
            
            //邮箱
            if (!input.Email.IsNullOrEmpty())
                condition = condition.And(x => x.Email.Contains(input.Email));
            

            //描述
            if (!input.Remark.IsNullOrEmpty())
                condition = condition.And(x => x.Remark.Contains(input.Remark));

            //是否有效
            if ((input.Enabled ?? -1) != -1)
                condition = condition.And(x => x.Enabled == input.Enabled);

            return condition;
        }
    }
}