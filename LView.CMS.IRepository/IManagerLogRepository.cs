using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LView.CMS.Models;
using LView.CMS.Core.Repository;

namespace LView.CMS.IRepository
{
    public interface IManagerLogRepository : IBaseRepository<ManagerLog, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);
    }
}
