using System;
using System.Collections.Generic;
using System.Text;
using LView.CMS.IServices;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using AutoMapper;
using System.Linq;
using SQLBuilder.Core.Repositories;
using System.Threading.Tasks;
using LView.CMS.Core.Extensions;

namespace LView.CMS.Services
{
    public class ManagerRoleService : IManagerRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public ManagerRoleService(IMapper mapper, Func<string, IRepository> handler)
        {
            _mapper = mapper;
            _repository = handler(null);
        }

        public async Task<BaseResult> AddOrModify(ManagerRoleAddOrModifyModel model)
        {
            var result = new BaseResult();
            ManagerRole managerRole;
            if (model.Id.IsNullOrWhiteSpace())
            {
                managerRole = model.MapTo<ManagerRole>();
                managerRole.Id = Guid.NewGuid().ToString();
                managerRole.AddManagerId = "6345939F-AD8B-46EF-B221-4BB1D83519F3";
                managerRole.IsDelete = 0;
                managerRole.AddTime = DateTime.Now;
                if (await _repository.InsertAsync<ManagerRole>(managerRole) > 0)
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
                managerRole = _repository.FindEntity<ManagerRole>(x => x.Id == model.Id);
                if (managerRole != null)
                {
                    managerRole = model.MapTo<ManagerRole>();
                    managerRole.ModifyManagerId = "6345939F-AD8B-46EF-B221-4BB1D83519F3";
                    managerRole.ModifyTime = DateTime.Now;
                    if (_repository.Update<ManagerRole>(managerRole) > 0)
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

        public async Task<BaseResult> DeleteIdsAsync(string[] Ids)
        {
            var result = new BaseResult();
            var count = 0;
            if (Ids.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;
            }
            else
            {
                try
                {
                    await Task.Run(() =>
                    {
                        Ids.ToList().ForEach(async x =>
                        {
                            await _repository.DeleteAsync<ManagerRole>(p => p.Id == x);
                            count++;
                        });
                    });
                }
                catch (Exception ex)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }

                if (count > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
            }
            return result;
        }

        public List<ManagerRole> GetListByCondition(ManagerRoleRequestModel model)
        {
            var sql = new StringBuilder("SELECT * FROM MANAGERROLE WHERE ISDELETE = 0");
            if (!model.Key.IsNullOrWhiteSpace())
            {
                sql.Append($"AND ROLENAME LIKE '%:ROLENAME%'");
            }
            var roleList = _repository.FindList<ManagerRole>(sql.ToString(), new { ROLENAME = model.Key });
            return roleList.ToList();
        }

        public async Task<TableDataModel> LoadDataAsync(ManagerRoleRequestModel model)
        {
            var sql = new StringBuilder($@"
                WITH T AS(
                SELECT AA.* 
                    FROM MANAGERROLE AA
                WHERE AA.ISDELETE = :ISDELETE");
            if (!string.IsNullOrEmpty(model.Key))
                sql.Append($" AND ROLENAME LIKE '%{model.Key}%'");
            sql.Append(")");
            var (recordList, recordCount) = await _repository.FindListByWithAsync<ManagerRole>(sql.ToString(), new { ISDELETE = 0 }, "AA.Id", true, model.Limit, model.Page);
            return new TableDataModel
            {
                count = (int)recordCount,
                data = recordList
            };
        }

        public List<MenuNavView> GetMenusByRoleId(string roleId)
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
