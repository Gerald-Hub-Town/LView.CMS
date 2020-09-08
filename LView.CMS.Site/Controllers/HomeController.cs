using LView.CMS.Core.Extensions;
using LView.CMS.Core.Helper;
using LView.CMS.Core.Models;
using LView.CMS.IServices;
using LView.CMS.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace LView.CMS.Site.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IManagerRoleService _managerRoleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IManagerRoleService managerRoleService, IHttpContextAccessor httpContextAccessor)
        {
            _managerRoleService = managerRoleService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewData["NickName"] = User.Claims.FirstOrDefault(x => x.Type == "NickName")?.Value;
            ViewData["Avatar"] = User.Claims.FirstOrDefault(x => x.Type == "Avatar")?.Value;
            return View();
        }

        /// <summary>
        /// 控制中心
        /// </summary>
        /// <returns></returns>
        public IActionResult Main()
        {
            ViewData["LoginCount"] = User.Claims.FirstOrDefault(x => x.Type == "LoginCount")?.Value;
            ViewData["LoginLastIp"] = User.Claims.FirstOrDefault(x => x.Type == "LoginLastIp")?.Value;
            ViewData["LoginLastTime"] = User.Claims.FirstOrDefault(x => x.Type == "LoginLastTime")?.Value;
            return View();
        }

        public string getmenu()
        {
            var roleid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var list = _managerRoleService.GetMenusByRoleId(roleid);
            var navviewtree = list.GenerateTree(x => x.Id, x => x.ParentId);

            return JsonHelper.ObjectToJson(navviewtree);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
