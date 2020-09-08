using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LView.CMS.Models;
using LView.CMS.ViewModels;

namespace LView.CMS.IServices
{
    public interface IManagerRoleService
    {
        Task<TableDataModel> LoadDataAsync(ManagerRoleRequestModel model);

        Task<BaseResult> AddOrModify(ManagerRoleAddOrModifyModel model);

        Task<BaseResult> DeleteIdsAsync(string[] Ids);

        List<ManagerRole> GetListByCondition(ManagerRoleRequestModel model);

        List<MenuNavView> GetMenusByRoleId(string roleId);
    }
}
