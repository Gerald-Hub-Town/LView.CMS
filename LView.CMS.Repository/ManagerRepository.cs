using Dapper;
using LView.CMS.Core;
using LView.CMS.Core.Options;
using LView.CMS.Core.Repository;
using LView.CMS.IRepository;
using LView.CMS.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LView.CMS.Repository
{
    public class ManagerRepository : BaseRepository<LMSManager, Int32>, IManagerRepository
    {
        public ManagerRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption = options.Get("LViewCMS");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = DbFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int ChangeLockStatusById(int id, bool status)
        {
            string sql = "update [Manager] set IsLock=@IsLock where  Id=@Id";
            return _dbConnection.Execute(sql, new
            {
                IsLock = status ? 1 : 0,
                Id = id,
            });
        }

        public async Task<int> ChangeLockStatusByIdAsync(int id, bool status)
        {
            string sql = "update [Manager] set IsLock=@IsLock where  Id=@Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                IsLock = status ? 1 : 0,
                Id = id,
            });
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update [Manager] set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update [Manager] set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public bool GetLockStatusById(int id)
        {
            string sql = "select IsLock from [dbo].[Manager] where Id=@Id and IsDelete=0";
            return _dbConnection.QueryFirstOrDefault<bool>(sql, new
            {
                Id = id,
            });
        }

        public async Task<bool> GetLockStatusByIdAsync(int id)
        {
            string sql = "select IsLock from [dbo].[Manager] where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<bool>(sql, new
            {
                Id = id,
            });
        }

        public async Task<string> GetPasswordByIdAsync(Int32 Id)
        {
            string sql = "select Password from Manager where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<string>(sql, new
            {
                Id = Id,
            });
        }

        public async Task<int> ChangePasswordByIdAsync(Int32 Id, string Password)
        {
            string sql = "update Manager set Password=@Password where Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Password = Password,
                Id = Id
            });
        }

        public async Task<LMSManager> GetManagerContainRoleNameByIdAsync(int id)
        {
            string sql = @"SELECT   mr.RoleName, m.Id, m.RoleId, m.UserName, m.Password, m.Avatar, m.NickName, m.Mobile, m.Email, m.LoginCount, 
                m.LoginLastIp, m.LoginLastTime, m.AddManagerId, m.AddTime, m.ModifyManagerId, m.ModifyTime, m.IsLock, 
                m.IsDelete, m.Remark
FROM      Manager AS m INNER JOIN
                ManagerRole AS mr ON m.RoleId = mr.Id where m.Id=@Id and m.IsDelete=0 ";
            return await _dbConnection.QueryFirstOrDefaultAsync<LMSManager>(sql, new
            {
                Id = id
            });
        }
    }
}
