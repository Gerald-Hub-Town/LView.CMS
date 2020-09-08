using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LView.CMS.IServices;
using LView.CMS.ViewModels;
using LView.CMS.Core.Helper;
using LView.CMS.Core.Extensions;
using LView.CMS.Site.Validation;
using FluentValidation.Results;

namespace LView.CMS.Site.Controllers
{
    public class ManagerRoleController : BaseController
    {
        private readonly IManagerRoleService _roleservice;
        public ManagerRoleController(IManagerRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> LoadData([FromQuery] ManagerRoleRequestModel model)
        {
            var result = await _roleservice.LoadDataAsync(model);
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpGet]
        public IActionResult AddOrModify([FromForm] ManagerRoleAddOrModifyModel model)
        {
            if (!model.Id.IsNullOrWhiteSpace())
            {
                ViewData["menuids"] = _roleservice.GetMenusByRoleId(model.Id);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/ManagerRole/AddOrModify")]
        public async Task<string> AddOrModifyAsync([FromForm] ManagerRoleAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerRoleValidation validationRules = new ManagerRoleValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = await _roleservice.AddOrModify(item);
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
        public async Task<string> Delete(string[] roleId)
        {
            var result = await _roleservice.DeleteIdsAsync(roleId);
            return JsonHelper.ObjectToJSON(result);
        }
    }
}
