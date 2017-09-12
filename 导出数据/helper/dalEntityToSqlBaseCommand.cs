using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace 导出数据.helper
{
    /// <summary>
    /// 基于实体的数据库操作
    /// </summary>
    public class dalEntityToSqlBaseCommand
    {

        /// <summary>
        /// 查看满足条件的单条记录
        /// </summary>
        /// <typeparam name="T">表对应的对象</typeparam>
        /// <param name="AfterWhereSqlString">条件</param>
        /// <param name="SqlParameterList">条件参数</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static T View<T>(string connectionString, String AfterWhereSqlString, List<SqlParameter> SqlParameterList)
        {
            SqlCommand Command = new SqlCommand();
            StringBuilder sbSql = new StringBuilder();
            T Entity = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);
            sbSql.AppendFormat("select top 1 * from {0} where {1}", t.Name, AfterWhereSqlString);
            Command.CommandText = sbSql.ToString();
            if (SqlParameterList != null)
            {
                foreach (SqlParameter Parameter in SqlParameterList)
                {
                    Command.Parameters.Add(Parameter);
                }
            }
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(connectionString, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                PropertyInfo[] PropertyInfoS = t.GetProperties();
                foreach (PropertyInfo pi in PropertyInfoS)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        Object ObjectValue = dt.Rows[0][pi.Name];
                        if (ObjectValue != DBNull.Value)
                        {
                            ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                            pi.SetValue(Entity, ObjectValue, null);
                        }
                    }
                }
                return Entity;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 查看满足条件的单条记录
        /// </summary>
        /// <typeparam name="T">表对应的对象</typeparam>
        /// <param name="AfterWhereSqlString">条件</param>
        /// <param name="SqlParameterList">条件参数</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static T View<T>(SqlTransaction Transaction, String AfterWhereSqlString, List<SqlParameter> SqlParameterList)
        {
            SqlCommand Command = new SqlCommand();
            StringBuilder sbSql = new StringBuilder();
            T Entity = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);
            sbSql.AppendFormat("select top 1 * from {0} where {1}", t.Name, AfterWhereSqlString);
            Command.CommandText = sbSql.ToString();
            if (SqlParameterList != null)
            {
                foreach (SqlParameter Parameter in SqlParameterList)
                {
                    Command.Parameters.Add(Parameter);
                }
            }
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(Transaction, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                PropertyInfo[] PropertyInfoS = t.GetProperties();
                foreach (PropertyInfo pi in PropertyInfoS)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        Object ObjectValue = dt.Rows[0][pi.Name];
                        if (ObjectValue != DBNull.Value)
                        {
                            ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                            pi.SetValue(Entity, ObjectValue, null);
                        }
                    }
                }
                return Entity;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// DataTable转换成EntityList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> Dt2EntityList<T>(DataTable dt)
        {
            List<T> EntityList = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                T Entity = default(T);
                PropertyInfo[] PropertyInfoS = null;
                Object ObjectValue = null;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfoS = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoS)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            ObjectValue = dt.Rows[i][pi.Name];
                            if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(Entity, ObjectValue, null);
                            }
                        }
                    }
                    EntityList.Add(Entity);
                }
            }
            return EntityList;
        }
        /// <summary>
        /// 查看满足条件的单条记录
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="Command">SqlCommand对象</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static T SelectEntity<T>(string connectionString, SqlCommand Command)
        {
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(connectionString, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                T Entity = (T)Activator.CreateInstance(typeof(T));
                Type t = typeof(T);
                PropertyInfo[] PropertyInfoS = t.GetProperties();
                foreach (PropertyInfo pi in PropertyInfoS)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        Object ObjectValue = dt.Rows[0][pi.Name];
                        if (ObjectValue != DBNull.Value)
                        {
                            ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                            pi.SetValue(Entity, ObjectValue, null);
                        }
                    }
                }
                return Entity;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 查看满足条件的记录列表
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="Command">SqlCommand对象</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static List<T> SelectZtreeList<T>(string connectionString, SqlCommand Command)
        {
            List<T> EntityList = new List<T>();
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(connectionString, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                //bool isTree = false;
                T Entity = default(T);
                //if (typeof(T).FullName.Equals("Project.Module.Common.Data.DataZtree"))
                //{
                //    isTree = true;
                //}
                PropertyInfo[] PropertyInfoS = null;
                Object ObjectValue = null;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfoS = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoS)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            ObjectValue = dt.Rows[i][pi.Name];
                            if (pi.Name.Equals("id") || pi.Name.Equals("pId"))
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(Entity, "\"" + ObjectValue + "\"", null);
                            }
                            else if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(Entity, ObjectValue, null);
                            }
                        }
                    }
                    EntityList.Add(Entity);
                }
            }
            return EntityList;
        }
        /// <summary>
        /// 查看满足条件的记录列表
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="Command">SqlCommand对象</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static List<T> SelectEntityList<T>(string connectionString, SqlCommand Command)
        {
            List<T> EntityList = new List<T>();
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(connectionString, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                T Entity = default(T);
                PropertyInfo[] PropertyInfoS = null;
                Object ObjectValue = null;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfoS = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoS)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            ObjectValue = dt.Rows[i][pi.Name];
                            if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(Entity, ObjectValue, null);
                            }
                        }
                    }
                    EntityList.Add(Entity);
                }
            }
            return EntityList;
        }

        /// <summary>
        /// 查看满足条件的记录列表
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="Command">SqlCommand对象</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static List<T> SelectEntityList<T>(SqlTransaction Transaction, SqlCommand Command)
        {
            List<T> EntityList = new List<T>();
            DataTable dt = dalSqlBaseCommandHelper.ExecuteDataTable(Transaction, Command);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    T Entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfo[] PropertyInfoS = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoS)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            Object ObjectValue = dt.Rows[i][pi.Name];
                            if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(Entity, ObjectValue, null);
                            }
                        }
                    }
                    EntityList.Add(Entity);
                }
            }
            return EntityList;
        }

        public static Object TypeConvert(Object ObjectValue, Type _Type)
        {
            if (ObjectValue != null)
            {
                Type ObjectValueType = ObjectValue.GetType();
                if (ObjectValueType != _Type)
                {
                    if (ObjectValueType == typeof(Boolean) && (_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    {
                        if (Convert.ToBoolean(ObjectValue))
                        {
                            ObjectValue = 1;
                        }
                        else
                        {
                            ObjectValue = 0;
                        }
                    }
                    //if (ObjectValueType == typeof(Int16) && (_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    //{
                    //    ObjectValue = Convert.ToInt32(ObjectValue);
                    //}
                    if ((_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    {
                        ObjectValue = Convert.ToInt32(ObjectValue);
                    }
                    if (ObjectValueType == typeof(Decimal) && (_Type == typeof(Double) || _Type == typeof(Double?)))
                    {
                        ObjectValue = Convert.ToDouble(ObjectValue);
                    }
                }
            }
            return ObjectValue;
        }

        public static T ConvertTo<T>(Object _Object, Dictionary<String, String> PropertyNameMapping)
        {
            Type tEntity = typeof(T);
            T Entity = (T)Activator.CreateInstance(typeof(T));

            Type tObject = _Object.GetType();
            HashSet<String> EntityToSqlParameters = (HashSet<String>)tObject.GetField("EntityToSqlParameters", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_Object);
            if (EntityToSqlParameters != null && EntityToSqlParameters.Count > 0)
            {
                foreach (String Key in EntityToSqlParameters)
                {
                    PropertyInfo pi_Object = tObject.GetProperty(Key);
                    if (pi_Object != null && PropertyNameMapping.ContainsKey(Key))
                    {
                        PropertyInfo pi_Entity = tEntity.GetProperty(PropertyNameMapping[Key]);
                        if (pi_Entity != null)
                        {
                            Object ObjectValue = pi_Object.GetValue(_Object, null);
                            if (ObjectValue != null)
                            {
                                Type ObjectValueType = ObjectValue.GetType();
                                Type _Type = pi_Entity.PropertyType;
                                if (ObjectValueType != _Type)
                                {
                                    if ((_Type == typeof(Decimal) || _Type == typeof(Decimal?)))
                                    {
                                        ObjectValue = Convert.ToDecimal(ObjectValue);
                                    }
                                    if ((_Type == typeof(Int32) || _Type == typeof(Int32?)))
                                    {
                                        ObjectValue = Convert.ToInt32(ObjectValue);
                                    }
                                }
                                //if (pi_Object.PropertyType.FullName == "System.Int32" && pi_Entity.PropertyType.FullName == "System.Boolean")
                                //{
                                //    pi_Entity.SetValue(Entity, pi_Object.GetValue(_Object, null).ToString() == "1" ? true : false, null);
                                //}
                            }
                            pi_Entity.SetValue(Entity, ObjectValue, null);
                        }
                    }
                }
            }
            return Entity;
        }
    }
}
