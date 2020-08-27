using LView.CMS.IServices;
using LView.CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LView.CMS.Core.Helper;
using System.Threading.Tasks;
using LView.CMS.Site.Validation;
using FluentValidation.Results;

namespace LView.CMS.Site.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string LoadData([FromQuery] MenuRequestModel model)
        {
            return JsonHelper.ObjectToJson(_service.LoadData(model));
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            return View(_service.GetChildListByParentId(0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> AddOrModify([FromForm] MenuAddOrModifyModel model)
        {
            var result = new BaseResult();
            MenuValidation validationRules = new MenuValidation();
            ValidationResult results = validationRules.Validate(model);
            if (results.IsValid)
                result = await _service.AddOrModifyAsync(model);
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }

            return JsonHelper.ObjectToJson(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Delete(int[] menuIds)
        {
            return JsonHelper.ObjectToJson(await _service.DeleteIdsAsync(menuIds));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> ChangeDisplayStatus([FromForm] ChangeStatusModel model)
        {
            var result = new BaseResult();
            ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            ValidationResult results = validationRules.Validate(model);
            if (results.IsValid)
                result = await _service.ChangeDisplayStatusAsync(model);
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }

            return JsonHelper.ObjectToJson(result);
        }

        [HttpGet]
        public async Task<string> IsExistsName([FromForm] MenuAddOrModifyModel model)
        {
            var result = await _service.IsExistsNameAsync(model);
            return JsonHelper.ObjectToJson(result);
        }

        public string LoadDataWithParentId([FromQuery] int ParentId = -1)
        {
            return JsonHelper.ObjectToJson(_service.GetChildListByParentId(ParentId));
        }
    }
}
