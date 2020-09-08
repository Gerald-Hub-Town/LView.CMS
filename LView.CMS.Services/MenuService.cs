using LView.CMS.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using LView.CMS.IRepositoryxxx;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using LView.CMS.Core.Extensions;
using AutoMapper;
using Microsoft.Extensions.Options;
using SQLBuilder.Core.Repositories;
using LView.CMS.Core.Options;

namespace LView.CMS.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<DbOption> _option;
        private readonly IRepository _repository;

        public MenuService(
            IMapper mapper,
            IOptionsSnapshot<DbOption> options,
            Func<string, IRepository> handler)
        {
            _repository = handler(null);
            _mapper = mapper;
            _option = options;
        }

        public async Task<BaseResult> AddOrModifyAsync(MenuAddOrModifyModel model)
        {
            var result = new BaseResult();
            Menu menuModel;
            if (model.Id == 0)
            {
                menuModel = _mapper.Map<Menu>(model);
                menuModel.AddManagerId = "";
                menuModel.IsDelete = false;
                menuModel.AddTime = DateTime.Now;
                if (await _repository.InsertAsync(menuModel) > 0)
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
                //TODO Modify
                menuModel = await _repository.FindEntityAsync<Menu>(model.Id);
                if (model != null)
                {
                    _mapper.Map(model, menuModel);
                    menuModel.ModifyManagerId = "";
                    menuModel.ModifyTime = DateTime.Now;
                    if (await _repository.UpdateAsync(menuModel) > 0)
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

        public async Task<BaseResult> DeleteIdsAsync(int[] Ids)
        {
            var result = new BaseResult();
            if (Ids.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;

            }
            else
            {
                var count = await _repository.DeleteAsync<Menu>(Ids);
                if (count > 0)
                {
                    //成功
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    //失败
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }


            }
            return result;
        }

        public TableDataModel LoadData(MenuRequestModel model)
        {
            var sql = "SELECT * FROM MENU WHERE ISDELETE = 0";
            var (recordList, recordCount) = _repository.FindListByWith<Menu>(sql, new { }, "ID", true, model.Limit, model.Page);
            return new TableDataModel
            {
                count = (int)recordCount,
                data = recordList.ToList()
            };
        }

        public async void ChangeDisplayStatusAsync(ChangeStatusModel model)
        {
            //var result = new BaseResult();
            ////判断状态是否发生变化，没有则修改，有则返回状态已变化无法更改状态的提示
            //var isLock = await _repository.GetDisplayStatusByIdAsync(model.Id);
            //if (isLock == !model.Status)
            //{
            //    var count = await _repository.ChangeDisplayStatusByIdAsync(model.Id, model.Status);
            //    if (count > 0)
            //    {
            //        result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
            //        result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
            //    }
            //    else
            //    {
            //        result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
            //        result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
            //    }
            //}
            //else
            //{
            //    result.ResultCode = ResultCodeAddMsgKeys.CommonDataStatusChangeCode;
            //    result.ResultMsg = ResultCodeAddMsgKeys.CommonDataStatusChangeMsg;
            //}
            //return result;
        }

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async void IsExistsNameAsync(MenuAddOrModifyModel item)
        {
            //bool data = false;
            //if (item.Id > 0)
            //{
            //    data = await _repository.IsExistsNameAsync(item.Name, item.Id);
            //}
            //else
            //{
            //    data = await _repository.IsExistsNameAsync(item.Name);

            //}
            //var result = new BooleanResult
            //{
            //    Data = data,
            //};
            //return result;
        }

        public void GetChildListByParentId(int ParentId)
        {
            //string conditions = "where IsDelete=0 ";//未删除的
            //if (ParentId >= 0)
            //{
            //    conditions += " and ParentId =@ParentId";
            //}
            //return _repository.GetList(conditions, new
            //{
            //    ParentId = ParentId
            //}).ToList();

        }

        Task<BaseResult> IMenuService.ChangeDisplayStatusAsync(ChangeStatusModel model)
        {
            throw new NotImplementedException();
        }

        Task<BooleanResult> IMenuService.IsExistsNameAsync(MenuAddOrModifyModel model)
        {
            throw new NotImplementedException();
        }

        List<Menu> IMenuService.GetChildListByParentId(int ParentId)
        {
            throw new NotImplementedException();
        }
    }
}
