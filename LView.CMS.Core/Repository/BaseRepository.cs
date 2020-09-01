using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using LView.CMS.Core.Options;

namespace LView.CMS.Core.Repository
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        protected DbOption _dbOption;
        protected IDbConnection _dbConnection;

        #region 同步
        public T Get(TKey id) => _dbConnection.Get<T>(id);
        public T Get(string conditions, object parameters = null) => _dbConnection.QueryFirstOrDefault<T>(conditions, parameters);
        public IEnumerable<T> GetList() => _dbConnection.GetList<T>();
        public IEnumerable<T> GetList(object whereConditions) => _dbConnection.GetList<T>(whereConditions);
        public IEnumerable<T> GetList(string conditions, object parameters = null) => _dbConnection.GetList<T>(conditions, parameters);
        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {
            return _dbConnection.GetListPaged<T>(pageNumber, rowsPerPage, orderby, conditions, parameters);
        }
        public int? Insert(T entity) => _dbConnection.Insert(entity);
        public int Update(T entity) => _dbConnection.Update(entity);
        public int Delete(TKey id) => _dbConnection.Delete<T>(id);
        public int Delete(T entity) => _dbConnection.Delete(entity);
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(whereConditions, transaction, commandTimeout);
        }
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(conditions, parameters, transaction, commandTimeout);
        }
        public int RecordCount(string conditions = "", object parameters = null)
        {
            return _dbConnection.RecordCount<T>(conditions, parameters);
        }
        #endregion

        #region 异步
        public Task<T> GetAsync(TKey id) => _dbConnection.GetAsync<T>(id);
        public Task<T> GetAsync(string conditions, object parameters = null) => _dbConnection.QueryFirstOrDefaultAsync<T>(conditions, parameters);

        public Task<IEnumerable<T>> GetListAsync() => _dbConnection.GetListAsync<T>();
        public Task<IEnumerable<T>> GetListAsync(object whereConditions) => _dbConnection.GetListAsync<T>(whereConditions);
        public Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null) => _dbConnection.GetListAsync<T>(conditions, parameters);
        public Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null) => _dbConnection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        public Task<int?> InsertAsync(T entity) => _dbConnection.InsertAsync(entity);
        public Task<int> UpdateAsync(T entity) => _dbConnection.UpdateAsync<T>(entity);
        public Task<int> DeleteAsync(TKey id) => _dbConnection.DeleteAsync<T>(id);
        public Task<int> DeleteAsync(T entity) => _dbConnection.DeleteAsync<T>(entity);
        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null) => _dbConnection.DeleteListAsync<T>(whereConditions, transaction, commandTimeout);
        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null) => _dbConnection.DeleteListAsync<T>(conditions, parameters, transaction, commandTimeout);
        public Task<int> RecordCountAsync(string conditions = "", object parameters = null) => _dbConnection.RecordCountAsync<T>(conditions, parameters);
        #endregion

        #region IDispose Support
        private bool disposedValue = false;//检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //释放托管状态（托管对象）
                    _dbConnection?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
