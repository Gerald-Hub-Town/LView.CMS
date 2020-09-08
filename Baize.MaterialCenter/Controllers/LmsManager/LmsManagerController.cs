using Baize.Core.Models;
using Baize.Entities.MaterialCenter.Entity;
using Baize.Entities.MaterialCenter.Input;
using Baize.Services.MaterialCenter.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Baize.Core.Helpers;
namespace Baize.MaterialCenter.Controllers.Process
{
    /// <summary>
    /// 工 具：白泽CodeBuilder Version:1.1.7.9
    /// 创 建：Gerald
    /// 日 期：2020/9/5 11:49:55
    /// 描 述：后台管理员表单
    /// </summary>
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "materialcenter")]
    [Route("api/materialcenter/v{version:apiVersion}/[controller]/[action]")]
    public class LmsManagerController : BaseController
    {
        /// <summary>
        /// 字段
        /// </summary>
        private readonly ILmsManagerService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public LmsManagerController(ILmsManagerService service)
        {
            _service = service;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("upload")]
        public async Task<ResponseResult> UploadAsync([Required]IFormFile file)
        {
            await _service.UploadAsync(file, this.CurrentUser);
            return Success();
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("download")]
        public async Task<IActionResult> DownloadAsync()
        {
            var fileName = $"替换成你的模板.xlsx";
            var bytes = await _service.DownloadAsync();
            if (bytes != null)
                return File(bytes, fileName.GetContentType(), fileName);

            return NotFound();
        }

        /// <summary>
        /// 新增后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        public async Task<ResponseResult> AddAsync(LmsManagerAddInput input)
        {
            input.CurrentUser = this.CurrentUser;
            await _service.AddAsync(input);
            return Success();
        }

        /// <summary>
        /// 修改后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("modify")]
        public async Task<ResponseResult> ModifyAsync(LmsManagerModifyInput input)
        {
            input.CurrentUser = this.CurrentUser;
            await _service.ModifyAsync(input);
            return Success();
        }

        /// <summary>
        /// 获取后台管理员表单 - 单个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("getentity")]
        public async Task<ResponseResult<LmsManagerEntity>> GetEntityAsync(LmsManagerQueryInput input)
        {
            var res = await _service.GetEntityAsync(input);
            return Success(res);
        }

        /// <summary>
        /// 获取后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("getlist")]
        public async Task<ResponseResult<IEnumerable<LmsManagerEntity>>> GetListAsync(LmsManagerQueryInput input)
        {
            var res = await _service.GetEntityListAsync(input);
            return Success(res);
        }

        /// <summary>
        /// 分页获取后台管理员表单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("getpagelist")]
        public async Task<ResponseResult<PageEntity<IEnumerable<LmsManagerEntity>>>> GetPageListAsync(PageEntity<LmsManagerQueryInput> input)
        {
            var res = await _service.GetPageListAsync(input);
            return Success(res);
        }

        /// <summary>
        /// 导出后台管理员表单
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("export")]
        public async Task<IActionResult> ExportAsync(LmsManagerQueryInput input)
        {
            var fileName = $"_{System.Guid.NewGuid()}.xlsx";
            var list = await _service.GetEntityListAsync(input);
            if (list?.Count() > 0)
            {
                var bytes = ExcelHelper.EPPlusExportExcelToBytes(list);
                return File(bytes, fileName.GetContentType(), fileName);
            }
            return BadRequest();
        }
    }
}