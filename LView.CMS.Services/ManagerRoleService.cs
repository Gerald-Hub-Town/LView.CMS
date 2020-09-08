using System;
using System.Collections.Generic;
using System.Text;
using LView.CMS.IServices;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using LView.CMS.IRepositoryxxx;
using AutoMapper;
using System.Linq;
using LView.CMS.Core.Extensions;
using SQLBuilder.Core.Repositories;

namespace LView.CMS.Services
{
    public class ManagerRoleService : IManagerRoleService
    {
        private readonly IManagerRoleRepository _respository;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public ManagerRoleService(IMapper mapper, Func<string, IRepository> handler)
        {
            _mapper = mapper;
            _repository = handler(null);
        }

        public BaseResult AddOrModify(ManagerRoleAddOrModifyModel model)
        {
            var result = new BaseResult();
            ManagerRole managerRole;
            if (model.Id == 0)
            {
                managerRole = _mapper.Map<ManagerRole>(model);
                managerRole.AddManagerId = "";
                managerRole.IsDelete = false;
                managerRole.AddTime = DateTime.Now;
                if (_respository.InsertByTrans(managerRole) > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            else
            {
                managerRole = _respository.Get(model.Id);
                if (managerRole != null)
                {
                    _mapper.Map(model, managerRole);
                    managerRole.ModifyManagerId = "";
                    managerRole.ModifyTime = DateTime.Now;
                    if (_respository.UpdateByTrans(managerRole) > 0)
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                    }
                    else
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                    }
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonFailNoDataCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonFailNoDataMsg;
                }
            }
            return result;
        }

        public BaseResult DeleteIds(int[] roleId)
        {
            var result = new BaseResult();
            if (roleId.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;
            }
            else
            {
                var count = _respository.DeleteLogical(roleId);
                if (count > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            return result;
        }

        public List<ManagerRole> GetListByCondition(ManagerRoleRequestModel model)
        {
            //string condition = "WHERE ISDELETE = 0";
            //if (!model.Key.IsNullOrWhiteSpace())
            //{
            //    condition += $"AND ROLENAME LIKE '%@KEY%'";
            //}
            //return _respository.GetList(condition, new
            //{
            //    KEY = model.Key
            //}).ToList();
            var sql = new StringBuilder("SELECT * FROM MANAGERROLE WHERE ISDELETE = 1");
            if (!model.Key.IsNullOrWhiteSpace())
            {
                sql.Append($"AND ROLENAME LIKE '%:ROLENAME%'");
            }
            var roleList = _repository.FindList<ManagerRole>(sql.ToString(), new { ROLENAME = model.Key });
            return roleList.ToList();
        }

        public TableDataModel LoadData(ManagerRoleRequestModel model)
        {
            string condition = "WHERE ISDELETE = 0";
            if (!model.Key.IsNullOrWhiteSpace())
            {
                condition += $"AND ROLENAME LIKE '%@KEY%'";
            }
            return new TableDataModel
            {
                count = _respository.RecordCount(condition),
                data = _respository.GetListPaged(model.Page, model.Limit, condition, "ID DESC", new
                {
                    KEY = model.Key
                }),
            };
        }

        public List<MenuNavView> GetMenusByRoleId(int roleId)
        {
            var sql = @"
SELECT M.ID,
       M.PARENTID,
       M.NAME,
       M.DISPLAYNAME,
       M.ICONURL,
       M.LINKURL,
       M.SORT,
       RP.PERMISSION,
       M.ISDISPLAY,
       M.ISSYSTEM,
       M.ADDMANAGERID,
       M.ADDTIME,
       M.MODIFYMANAGERID,
       M.MODIFYTIME,
       M.ISDELETE
  FROM ROLEPERMISSION RP
 INNER JOIN MENU M
    ON RP.MENUID = M.ID
 WHERE RP.ROLEID = :ROLEID
   AND M.ISDELETE = 1
";
            var menuList = _repository.FindList<Menu>(sql, new { ROLEID = roleId });
            if (menuList?.Count() == 0)
                return null;
            var menuNavViewList = new List<MenuNavView>();
            menuList.ToList().ForEach(x =>
            {
                //var navView = _mapper.Map<MenuNavView>(x);
                var navView = x.MapTo<MenuNavView>();
                menuNavViewList.Add(navView);
            });
            return menuNavViewList;
        }
    }
}
