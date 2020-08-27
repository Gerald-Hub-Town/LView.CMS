using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using LView.CMS.ViewModels.Manager;

namespace LView.CMS.IServices
{
    public interface IManagerService
    {
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        Task<TableDataModel> LoadDataAsync(ManagerRequestModel model);

        /// <summary>
        /// 新增或修改服务
        /// </summary>
        /// <param name="model">新增或修改视图实体</param>
        /// <returns></returns>
        Task<BaseResult> AddOrModifyAsync(ManagerAddOrModifyModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键Id数组</param>
        /// <returns></returns>
        Task<BaseResult> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 修改锁定状态
        /// </summary>
        /// <param name="model">修改锁定状态实体</param>
        /// <returns></returns>
        Task<BaseResult> ChangeLockStatusAsync(ChangeStatusModel model);

        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns></returns>
        Task<Manager> SignInAsync(LoginModel model);

        Task<BaseResult> ChangePasswordAsync(ChangePasswordModel model);

        Task<Manager> GetManagerByIdAsync(int id);

        Task<Manager> GetManagerContainRoleNameByIdAsync(int id);

        Task<BaseResult> UpdateManagerInfoAsync(ChangeInfoModel model);
    }
}
