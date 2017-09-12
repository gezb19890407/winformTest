using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace 导出数据.helper
{
    public class dalSqlBaseCommandHelper
    {
        ///// <summary>
        ///// 系统
        ///// </summary>
        //public static string SqlConnectionString
        //{
        //    get
        //    {
        //        if (System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"] == null)
        //        {
        //            throw new Exception("config 文件中ConnectionStrings节点不存在名字为SqlConnectionString的连接");
        //        }
        //        return System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString().Trim();
        //    }
        //}

        ///// <summary>
        ///// 获得config文件中ConnectionStrings节点下指定Name的数据库连接字符串
        ///// </summary>
        //public static string GetSqlConnectionString(String Name)
        //{
        //    if (System.Configuration.ConfigurationManager.ConnectionStrings[Name] == null)
        //    {
        //        throw new Exception("config 文件中ConnectionStrings节点不存在名为" + Name + "的连接");
        //    }
        //    return System.Configuration.ConfigurationManager.ConnectionStrings[Name].ToString().Trim();
        //}


        public static bool IsCanConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        public static string ExecuteFirstValue(string connectionString, SqlCommand command)
        {
            string value = null;
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            DataTable dt = ExecuteDataTable(connectionString, command);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                value = dt.Rows[0][0].ToString();
            }
            return value;
        }

        public static Int32 ExecuteScalar(String connectionString, SqlCommand Command)
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
                return ExecuteScalar(Connection, Command);
            }
        }

        private static Int32 ExecuteScalar(SqlConnection Connection, SqlCommand Command)
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
                retval = Convert.ToInt32(Command.ExecuteScalar());
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

        public static Int32 ExecuteScalar(SqlTransaction Transaction, SqlCommand Command)
        {
            if (Transaction == null)
            {
                throw new Exception("事务Transaction对象不存在");
            }
            if (Transaction != null && Transaction.Connection == null)
            {
                throw new Exception("数据库连接字符串不存在");
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Transaction.Connection, Transaction);
            int retval = -1;
            try
            {
                retval = Convert.ToInt32(Command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
            Command.Parameters.Clear();
            return retval;
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

        public static Int32 ExecuteNonQuery(SqlTransaction Transaction, SqlCommand Command)
        {
            if (Transaction == null)
            {
                throw new Exception("事务Transaction对象不存在");
            }
            if (Transaction != null && Transaction.Connection == null)
            {
                throw new Exception("数据库连接字符串不存在");
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Transaction.Connection, Transaction);
            int retval = -1;
            try
            {
                retval = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
            Command.Parameters.Clear();
            return retval;
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

        public static DataSet ExecuteDataset(SqlTransaction Transaction, SqlCommand Command)
        {
            if (Transaction == null)
            {
                throw new Exception("事务Transaction对象不存在");
            }
            if (Transaction != null && Transaction.Connection == null)
            {
                throw new Exception("数据库连接字符串不存在");
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Transaction.Connection, Transaction);
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter(Command))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Command.Parameters.Clear();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
        }

        public static DataTable ExecuteDataTable(string connectionString, SqlCommand Command)
        {
            return ExecuteDataset(connectionString, Command).Tables[0];
        }

        public static DataTable ExecuteDataTable(SqlTransaction Transaction, SqlCommand Command)
        {
            return ExecuteDataset(Transaction, Command).Tables[0];
        }

        public static DataTable ExecuteDataTable(SqlConnection Connection, SqlCommand Command)
        {
            return ExecuteDataset(Connection, Command).Tables[0];
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
