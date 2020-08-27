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
    public class ManagerLogRepository : BaseRepository<ManagerLog, Int32>, IManagerLogRepository
    {
        public ManagerLogRepository(IOptionsSnapshot<DbOption> options)
        {
            _dbOption = options.Get("LViewCMS");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = DbFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update ManagerLog set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ManagerLog set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}
