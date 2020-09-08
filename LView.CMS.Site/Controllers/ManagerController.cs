using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LView.CMS.Services;
using LView.CMS.IServices;
using LView.CMS.Core.Helper;
using LView.CMS.ViewModels.Manager;
using LView.CMS.ViewModels;
using LView.CMS.Site.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace LView.CMS.Site.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly IManagerService _service;
        private readonly IManagerRoleService _roleservice;
        public ManagerController(IManagerService service, IManagerRoleService roleservice)
        {
            _service = service;
            _roleservice = roleservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> LoadData([FromQuery] ManagerRequestModel model)
        {
            return JsonHelper.ObjectToJSON(await _service.LoadDataAsync(model));
        }

        [HttpGet]
        public IActionResult AddOrModify([FromForm] ManagerAddOrModifyModel model)
        {
            var roleList = _roleservice.GetListByCondition(new ManagerRoleRequestModel
            {
                Key = null
            });
            return View(roleList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Manager/AddOrModify")]
        public async Task<string> AddOrModifyAsync([FromForm] ManagerAddOrModifyModel model)
        {
            var result = new BaseResult();
            ManagerValidation validationRules = new ManagerValidation();
            ValidationResult results = validationRules.Validate(model);
            if (results.IsValid)
                result = await _service.AddOrModifyAsync(model);
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }

            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Manager/ChangeLockStatus")]
        public async Task<string> ChangeLockStatusAsync([FromForm] ChangeStatusModel item)
        {
            var result = new BaseResult();
            ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = await _service.ChangeLockStatusAsync(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Delete(string[] managerId)
        {
            var result = await _service.DeleteIdsAsync(managerId);
            return JsonHelper.ObjectToJSON(result);
        }
    }
}
