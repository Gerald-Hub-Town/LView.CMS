using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LView.CMS.Core.Repository;
using LView.CMS.Models;

namespace LView.CMS.IRepositoryxxx
{
    public interface IMenuRepository : IBaseRepository<Menu, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns></returns>
        Int32 DeleteLogical(Int32[] ids);

        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns></returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// <summary>
        /// 根据主键获取显示状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<Boolean> GetDisplayStatusByIdAsync(Int32 id);

        /// <summary>
        /// 修改显示状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="status">更改后的状态</param>
        /// <returns></returns>
        Task<Int32> ChangeDisplayStatusByIdAsync(Int32 id, bool status);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name">别名</param>
        /// <returns></returns>
        Task<Boolean> IsExistsNameAsync(string name);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name">别名</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<Boolean> IsExistsNameAsync(string name, Int32 id);
    }
}
