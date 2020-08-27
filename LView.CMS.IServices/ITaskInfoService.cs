using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LView.CMS.ViewModels;

namespace LView.CMS.IServices
{
    public interface ITaskInfoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TableDataModel> LoadDataAsync(TaskInfoRequestModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> SystemStoppedAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> ResumeSystemStoppedAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<BooleanResult> UpdateStatusByIdsAsync(int[] ids, int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<TaskInfoDto>> GetListByJobStatusAsync(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BooleanResult> IsExistsNameAsync(TaskInfoAddOrModifyModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BaseResult> AddOrModifyAsync(TaskInfoAddOrModifyModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BooleanResult> DeleteAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TaskInfoDto>> GetListByIdsAsync(int[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TaskInfoDto> GetByIdAsync(int id);
    }
}
