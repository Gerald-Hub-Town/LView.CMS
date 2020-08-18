using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LView.CMS.Core.Extensions;
using LView.CMS.Core.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace LView.CMS.Core
{
    /// <summary>
    /// Copy From yilezhu
    /// 20200818
    /// 数据库连接工厂类
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="strconn">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(string dbtype, string strconn)
        {
            if (dbtype.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库类型为空");
            if (strconn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库连接串为空");
            var dbType = GetDataBaseType(dbtype);
            return CreateConnection(dbType, strconn);
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        private static DataBaseType GetDataBaseType(string dbtype)
        {
            if (dbtype.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库类型为空");
            DataBaseType returnValue = DataBaseType.Oracle;
            foreach (DataBaseType dbType in Enum.GetValues(typeof(DataBaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="strConn">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(DataBaseType dbType, string strConn)
        {
            IDbConnection connection = null;
            if (strConn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库连接串为空");

            switch (dbType)
            {
                case DataBaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DataBaseType.MySQL:
                    connection = new MySqlConnection(strConn);
                    break;
                case DataBaseType.PostgreSQL:
                    connection = new NpgsqlConnection(strConn);
                    break;
                case DataBaseType.Oracle:
                    connection = new OracleConnection(strConn);
                    break;
                default:
                    throw new ArgumentNullException($"目前还不支持{dbType.ToString()}数据库类型");
            }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection;
        }
    }
}
