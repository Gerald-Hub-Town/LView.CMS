using System;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LView.CMS.IServices
{
    public interface IMenuService
    {
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        TableDataModel LoadData(MenuRequestModel model);

        /// <summary>
        /// 新增或修改服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BaseResult> AddOrModifyAsync(MenuAddOrModifyModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        Task<BaseResult> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 更改显示的状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BaseResult> ChangeDisplayStatusAsync(ChangeStatusModel model);

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BooleanResult> IsExistsNameAsync(MenuAddOrModifyModel model);

        /// <summary>
        /// 根据父菜单ID返回子菜单列表
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        List<Menu> GetChildListByParentId(int ParentId);
    }
}
