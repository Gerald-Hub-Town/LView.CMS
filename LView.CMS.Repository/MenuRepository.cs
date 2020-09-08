using System;
using System.Collections.Generic;
using System.Text;
using LView.CMS.Core.Repository;
using MySqlX.XDevAPI.Common;
using LView.CMS.Models;
using LView.CMS.IRepositoryxxx;
using Microsoft.Extensions.Options;
using LView.CMS.Core.Options;
using LView.CMS.Core;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace LView.CMS.Repository
{
    public class MenuRepository : BaseRepository<Menu, Int32>, IMenuRepository
    {
        public MenuRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption = options.Get("LViewCMS");
            if (_dbOption == null)
                throw new ArgumentNullException(nameof(DbOption));
            _dbConnection = DbFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public async Task<Int32> ChangeDisplayStatusByIdAsync(int id, bool status)
        {
            string sql = "update Menu set IsDisplay=@IsDisplay where  Id=@Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                IsDisplay = status ? 1 : 0,
                Id = id
            });
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update Menu set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update Menu set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<Boolean> GetDisplayStatusByIdAsync(int id)
        {
            string sql = "select IsDisplay from Menu where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<bool>(sql, new
            {
                Id = id
            });
        }

        public async Task<Boolean> IsExistsNameAsync(string name)
        {
            string sql = "select Id from Menu where Name=@Name and IsDelete=0";
            var result = await _dbConnection.QueryAsync<int>(sql, new
            {
                Name = name
            });
            if (result != null && result.Count() > 0)
                return true;
            else
                return false;
        }

        public async Task<Boolean> IsExistsNameAsync(string name, Int32 id)
        {
            string sql = "select Id from Menu where Name=@Name and Id <> @Id and IsDelete=0";
            var result = await _dbConnection.QueryAsync<int>(sql, new
            {
                Name = name,
                Id = id
            });
            if (result != null && result.Count() > 0)
                return true;
            else
                return false;
        }
    }
}
