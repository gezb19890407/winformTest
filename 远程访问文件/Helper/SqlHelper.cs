using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace 远程访问文件.Helper
{
    public class SqlHelper
    {
        public static DataTable ExecuteDataTable(string connectionString, SqlCommand Command)
        {
            return ExecuteDataset(connectionString, Command).Tables[0];
        }

        public static DataSet ExecuteDataset(string connectionString, SqlCommand Command)
        {
            try
            {
                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("数据库连接字符串不存在");
                }
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    if (Connection.State != ConnectionState.Open)
                    {
                        Connection.Open();
                    }
                    return ExecuteDataset(Connection, Command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSet ExecuteDataset(SqlConnection Connection, SqlCommand Command)
        {
            try
            {
                if (Connection == null)
                {
                    throw new Exception("数据库连接SqlConnection对象不存在");
                }
                Command.CommandTimeout = 600;
                PrepareCommand(Command, Connection, (SqlTransaction)null);
                //dalSqlHelper.ConsoleSql(Command);
                using (SqlDataAdapter da = new SqlDataAdapter(Command))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Command.Parameters.Clear();
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }
        }

        public static Int32 ExecuteNonQuery(string connectionString, SqlCommand Command)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                return ExecuteNonQuery(Connection, Command);
            }
        }

        private static Int32 ExecuteNonQuery(SqlConnection Connection, SqlCommand Command)
        {
            if (Connection == null)
            {
                throw new Exception("数据库连接SqlConnection对象不存在");
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Connection, (SqlTransaction)null);
            int retval = -1;
            try
            {
                retval = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Command.Parameters.Clear();
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
            return retval;
        }

        private static void PrepareCommand(SqlCommand Command, SqlConnection Connection, SqlTransaction Transaction)
        {
            if (Command == null)
            {
                throw new Exception("数据库命令command对象不存在");
            }
            Command.Connection = Connection;
            //dalSqlHelper.ConsoleSql(Command);
            if (Transaction != null)
            {
                if (Transaction.Connection == null)
                {
                    throw new Exception("数据库连接字符串不存在");
                }
                Command.Transaction = Transaction;
            }
        }
    }
}
