using AutoMapper;
using LView.CMS.Core.Helper;
using LView.CMS.Core.Options;
using LView.CMS.IServices;
using LView.CMS.Models;
using LView.CMS.ViewModels;
using LView.CMS.ViewModels.Manager;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLBuilder.Core.Repositories;
using SQLBuilder.Core;
using System.Text;
using LView.CMS.Core.Extensions;

namespace LView.CMS.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<DbOption> _option;
        private readonly IRepository _repository;
        //private readonly Func<string, IRepository> _handler;

        public ManagerService(IMapper mapper, IOptionsSnapshot<DbOption> options, Func<string, IRepository> handler)
        {
            _mapper = mapper;
            _option = options;
            _repository = handler(null);
        }

        public async Task<LMSManager> SignInAsync(LoginModel model)
        {
            model.Password = AESEncryptHelper.Encode(model.Password.Trim(), LViewCMSKeys.AesEncryptKeys);
            model.UserName = model.UserName.Trim();
            var manager = await _repository.FindEntityAsync<LMSManager>(x => x.UserName == model.UserName);
            if (manager != null)
            {
                manager.LoginLastTime = DateTime.Now;
                manager.LoginCount += 1;
                manager.LoginLastIp = model.Ip;
                _repository.Update<LMSManager>(manager);

                var managerLog = new ManagerLog
                {
                    Id = Guid.NewGuid().ToString(),
                    ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
                    AddManagerId = manager.Id,
                    AddManagerNickName = manager.NickName,
                    AddTime = DateTime.Now,
                    AddIP = model.Ip,
                    Remark = "用户登录"
                };
                await _repository.InsertAsync<ManagerLog>(managerLog);
            }
            return manager;
        }

        public void TestInsert(LoginModel model)
        {
            var managerLog = new ManagerLog
            {
                Id = Guid.NewGuid().ToString(),
                ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
                AddManagerId = "6345939F-AD8B-46EF-B221-4BB1D83519F3",
                AddManagerNickName = "霓虹下的野鬼",
                AddTime = DateTime.Now,
                AddIP = model.Ip,
                Remark = "用户登录"
            };
            var count = _repository.Insert(managerLog);
            if (count > 0) { } else { }
        }

        public async Task<LMSManager> GetManagerByCondition(ManagerAddOrModifyModel model)
        {
            var sql = new StringBuilder("SELECT * FROM LMSMANAGER WHERE 1 = 1");
            if (!string.IsNullOrEmpty(model.UserName))
            {
                sql.Append($"AND USERNAME LIKE '%:USERNAME%'");
            }
            var manager = await _repository.FindEntityAsync<LMSManager>(sql.ToString(), new { USERNAME = model.UserName });
            if (manager != null)
                return manager;
            else
                return new LMSManager();
        }

        public async Task<BaseResult> AddOrModifyAsync(ManagerAddOrModifyModel model)
        {
            var result = new BaseResult();
            LMSManager manager;
            if (string.IsNullOrEmpty(model.Id))
            {
                //manager = _mapper.Map<LMSManager>(model);
                manager = model.MapTo<LMSManager>();
                manager.Password = AESEncryptHelper.Encode(LViewCMSKeys.DefaultPassword, LViewCMSKeys.AesEncryptKeys);
                manager.LoginCount = 0;
                manager.Id = Guid.NewGuid().ToString();
                manager.AddManagerId = Guid.NewGuid().ToString();
                manager.AddTime = DateTime.Now;
                manager.IsLock = 0;
                manager.IsDelete = 0;
                if (await _repository.InsertAsync<LMSManager>(manager) > 0)
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
                manager = await _repository.FindEntityAsync<LMSManager>(x => x.Id == model.Id);
                if (manager != null)
                {
                    manager = model.MapTo<LMSManager>();
                    manager.ModifyManagerId = Guid.NewGuid().ToString();
                    manager.ModifyTime = DateTime.Now;
                    if (await _repository.UpdateAsync<LMSManager>(manager) > 0)
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
                            await _repository.DeleteAsync<LMSManager>(p => p.Id == x);
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

        public async Task<TableDataModel> LoadDataAsync(ManagerRequestModel model)
        {
            var sql = new StringBuilder($@"
                WITH T AS(
                SELECT AA.*, BB.ROLENAME AS ROLENAME
                    FROM LMSMANAGER AA
                INNER JOIN MANAGERROLE BB
                    ON AA.ROLEID = BB.ID
                WHERE AA.ISDELETE = :ISDELETE");
            if (!string.IsNullOrEmpty(model.Key))
                sql.Append($" AND USERNAME LIKE '%{model.Key}%'");
            sql.Append(")");
            var (recordList, recordCount) = await _repository.FindListByWithAsync<LMSManager>(sql.ToString(), new { ISDELETE = 0 }, "AA.Id", true, model.Limit, model.Page);
            var viewList = new List<ManagerListModel>();
            recordList?.ToList().ForEach(x =>
            {
                //var item = _mapper.Map<ManagerListModel>(x);
                var item = x.MapTo<ManagerListModel>();
                viewList.Add(item);
            });
            return new TableDataModel
            {
                count = (int)recordCount,
                data = viewList,
            };
        }

        public async Task<BaseResult> ChangeLockStatusAsync(ChangeStatusModel model)
        {
            var result = new BaseResult();
            //var oridata = await _repository.FindEntityAsync<LMSManager>(model.Id);
            var oridata = await _repository.FindEntityAsync<LMSManager>(x => x.Id == model.Id);
            if (oridata.IsLock != model.Status)
            {
                oridata.IsLock = model.Status;
                var count = await _repository.UpdateAsync(oridata);
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
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonDataStatusChangeCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonDataStatusChangeMsg;
            }
            return result;
        }

        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>状态</returns>
        //public async Task<LMSManager> SignInAsync(LoginModel model)
        //{
        //    model.Password = AESEncryptHelper.Encode(model.Password.Trim(), LViewCMSKeys.AesEncryptKeys);
        //    model.UserName = model.UserName.Trim();
        //    string conditions = $"select * from {nameof(LMSManager)} where IsDelete=0 ";//未删除的
        //    conditions += $"and (UserName = :UserName or Mobile = :UserName or Email = :UserName) and Password = :Password";
        //    //var manager1 = await _repositorya.GetAsync($"select * from {nameof(LMSManager)} where IsDelete=0");
        //    var manager = await _repositorya.GetAsync(conditions, new { UserName = model.UserName, Password = model.Password });

        //    OracleRepository db = new OracleRepository(_option.Get("LViewCMS").ConnectionString)
        //    {
        //        IsEnableFormat = false,
        //        SqlIntercept = (x, y) =>
        //        {
        //            return null;
        //        }
        //    };

        //    var sql = @"SELECT * FROM LMSMANAGER WHERE ISDELETE = 0 AND (USERNAME = :USERNAME OR MOBILE = :USERNAME OR EMAIL = :USERNAME) AND PASSWORD = :PASSWORD";
        //    //var manager2 = db.FindList<LMSManager>(sql, new
        //    //{
        //    //    USERNAME = model.UserName,
        //    //    PASSWORD = model.Password
        //    //}).ToList().FirstOrDefault();
        //    //var manager1 = db.FindEntity<LMSManager>(x => x.UserName == model.UserName || x.Email == model.UserName || x.Mobile == model.UserName);
        //    var manager11 = _repositoryaa.FindEntityAsync<LMSManager>(x => x.UserName == model.UserName);

        //    if (manager != null)
        //    {
        //        manager.LoginLastIp = model.Ip;
        //        //manager.LoginCount += 1;
        //        manager.LoginLastTime = DateTime.Now;
        //        //_repositorya.Update(manager);
        //        db.Update<LMSManager>(manager);
        //        //await _managerLogRepository.InsertAsync(new ManagerLog()
        //        //{
        //        //    ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
        //        //    AddManageId = manager.Id,
        //        //    AddManagerNickName = manager.NickName,
        //        //    AddTime = DateTime.Now,
        //        //    AddIp = model.Ip,
        //        //    Remark = "用户登录"
        //        //});
        //    }
        //    return manager;
        //}

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">修改密码实体</param>
        /// <returns>结果</returns>
        public async Task<BaseResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            BaseResult result = new BaseResult();
            var oriData = await _repository.FindEntityAsync<LMSManager>(model.Id);
            if (oriData.Password == AESEncryptHelper.Encode(model.OldPassword, LViewCMSKeys.AesEncryptKeys))
            {
                var newPassword = AESEncryptHelper.Encode(model.NewPassword, LViewCMSKeys.AesEncryptKeys);
                oriData.Password = newPassword;
                var count = await _repository.UpdateAsync(oriData);
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
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.OldPasswordErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.OldPasswordErrorMsg;
            }
            return result;
        }

        /// <summary>
        /// 获取个人资料By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LMSManager> GetManagerByIdAsync(string id)
        {
            return await _repository.FindEntityAsync<LMSManager>(id);
        }

        public async Task<LMSManager> GetManagerContainRoleNameByIdAsync(int id)
        {
            var sql = @"SELECT   mr.RoleName, m.Id, m.RoleId, m.UserName, m.Password, m.Avatar, m.NickName, m.Mobile, m.Email, m.LoginCount, 
                m.LoginLastIp, m.LoginLastTime, m.AddManagerId, m.AddTime, m.ModifyManagerId, m.ModifyTime, m.IsLock, 
                m.IsDelete, m.Remark
FROM      Manager AS m INNER JOIN
                ManagerRole AS mr ON m.RoleId = mr.Id where m.Id=@Id and m.IsDelete=0 ";
            //return await _repositorya.GetManagerContainRoleNameByIdAsync(id);
            return await _repository.FindEntityAsync<LMSManager>(sql, new
            {
                Id = id
            });
        }

        /// <summary>
        /// 个人资料修改
        /// </summary>
        /// <param name="model">个人资料修改实体</param>
        /// <returns>结果</returns>
        public async Task<BaseResult> UpdateManagerInfoAsync(ChangeInfoModel model)
        {
            BaseResult result = new BaseResult();
            //TODO Modify
            var manager = await _repository.FindEntityAsync<LMSManager>(model.Id);
            if (manager != null)
            {
                _mapper.Map(model, manager);
                if (await _repository.UpdateAsync(manager) > 0)
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
            return result;
        }
    }
}
