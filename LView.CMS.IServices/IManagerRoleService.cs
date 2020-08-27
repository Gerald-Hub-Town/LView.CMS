using System;
using System.Collections.Generic;
using System.Text;
using LView.CMS.Models;
using LView.CMS.ViewModels;

namespace LView.CMS.IServices
{
    public interface IManagerRoleService
    {
        TableDataModel LoadData(ManagerRoleRequestModel model);

        BaseResult AddOrModify(ManagerRoleAddOrModifyModel model);

        BaseResult DeleteIds(int[] roleId);

        List<ManagerRole> GetListByCondition(ManagerRoleRequestModel model);

        List<MenuNavView> GetMenusByRoleId(int roleId);
    }
}
