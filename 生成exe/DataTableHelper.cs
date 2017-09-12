using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;

namespace 生成exe
{
    public class DataTableHelper
    {
        public static string DataTableToEntityStringCs(string SqlConnectionString, string tableName, string webUrl)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format(@"
DECLARE @tbValue VARCHAR(200)
SET @tbValue = (
	SELECT cast (ds.value AS VARCHAR(200)) 描述   
	FROM sys.extended_properties ds  
		LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
	WHERE  ds.minor_id=0 and  
	tbs.name='{0}'--表名
) 
SELECT
    a.name 字段名,
    g.[value] AS 字段说明,
    a.isnullable ,
    b.name ,
    case when a. length=-1 then 8000 else a. length end length,
    a.colorder 字段序号,
    @tbValue 表描述
FROM
       syscolumns a
       left join systypes b on a. xtype=b .xusertype
       inner join sysobjects d on a. id=d .id and d .xtype= 'U' and d.name <>'dtproperties'
       left join sys. extended_properties g on a.id =g. major_id AND a. colid = g .minor_id
WHERE d .[name] ='{0}' --表名字
order by a. id,a .colorder", tableName);
            StringBuilder builder = null;
            DataTable table = ExecuteDataTable(SqlConnectionString, cmd);
            if (table != null && table.Rows.Count > 0)
            {
                builder = new StringBuilder();
                builder.AppendFormat("using System; \r\nusing System.Collections.Generic; \r\nusing System.Linq; \r\nusing System.Text; \r\nusing System.Runtime.Serialization;\r\n", new object[0]);
                builder.AppendFormat("\r\nnamespace {0} \r\n{{ \r\n    //{2} \r\n    [DataContract] \r\n    public class {1}\r\n    {{   \r\n        private HashSet<String> EntityToSqlParameters = null;\r\n", webUrl, tableName, table.Rows[0][6] != DBNull.Value ? table.Rows[0][6].ToString() : tableName);
                foreach (DataRow dtRow in table.Rows)
                {
                    string codeName = dtRow[0].ToString();
                    string description = dtRow[1] == DBNull.Value ? "" : dtRow[1].ToString();
                    string isnullable = (int)dtRow[2] == 1 ? "可为空" : "不可为空";
                    string typeName = SqlTypeToCSharpType(dtRow[3].ToString());
                    switch (typeName.ToLower())
                    {
                        case "int":
                        case "bool":
                        case "boolean":
                        case "long":
                        case "float":
                        case "double":
                        case "datetime":
                        case "int16":
                        case "int32":
                        case "int64":
                        case "decimal":
                            if ((int)dtRow[2] == 1)
                            {
                                typeName = typeName + "?";
                            }
                            break;
                    }
                    string length = "长度为" + (int)dtRow[4];
                    builder.AppendFormat("\r\n        private {0} _{1};", typeName, codeName);
                    builder.AppendFormat("\r\n        /// <summary>\r\n        /// {2}\r\n        /// {3}\r\n        /// {4}\r\n        /// </summary>\r\n        [DataMember] \r\n        public {0} {1} \r\n        {{\r\n            set {{ _{1} = value; if (EntityToSqlParameters == null) {{ EntityToSqlParameters = new HashSet<String>(); }};EntityToSqlParameters.Add(\"{1}\"); }} \r\n            get {{ return _{1}; }} \r\n        }}\r\n",
                        typeName, codeName, description, length, isnullable);
                }
                builder.Append("\r\n    }\r\n}");
            }
            else
            {
                builder = new StringBuilder("可能存在查询语句错误，返回值null");
            }
            return builder.ToString();
        }

        public static string DataTableToEntityStringCsNew(string SqlConnectionString, string tableName, string webUrl, string MappingString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format(@"
DECLARE @tbValue VARCHAR(200)
SET @tbValue = (
	SELECT cast (ds.value AS VARCHAR(200)) 描述   
	FROM sys.extended_properties ds  
		LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
	WHERE  ds.minor_id=0 and  
	tbs.name='{0}'--表名
) 
SELECT
    a.name 字段名,
    g.[value] AS 字段说明,
    a.isnullable ,
    b.name ,
    case when a. length=-1 then 8000 else a. length end length,
    a.colorder 字段序号,
    @tbValue 表描述
FROM
       syscolumns a
       left join systypes b on a. xtype=b .xusertype
       inner join sysobjects d on a. id=d .id and d .xtype= 'U' and d.name <>'dtproperties'
       left join sys. extended_properties g on a.id =g. major_id AND a. colid = g .minor_id
WHERE d .[name] ='{0}' --表名字
order by a. id,a .colorder", tableName);
            StringBuilder builder = null;
            DataTable table = ExecuteDataTable(SqlConnectionString, cmd);
            Dictionary<string, object> dic = null;
            if (!string.IsNullOrEmpty(MappingString))
            {
                MappingString = MappingString.Replace(" ", "").Replace("\",", "\":").Replace("{", "").Replace("}", "");
                MappingString = "{" + MappingString + "}";
                dic = JsonStringToDictionary(MappingString);
            }
            if (table != null && table.Rows.Count > 0)
            {
                builder = new StringBuilder();
                builder.AppendFormat("using System; \r\nusing System.Collections.Generic; \r\nusing System.Linq; \r\nusing System.Text; \r\nusing System.Runtime.Serialization;\r\nusing fw.fwDal; \r\n", new object[0]);
                builder.AppendFormat("\r\nnamespace {0} \r\n{{ \r\n    //{2}\r\n    [DataContract] \r\n    public class {1} : FWEntityObject\r\n    {{   \r\n", webUrl, tableName, table.Rows[0][6] != DBNull.Value ? table.Rows[0][6].ToString() : tableName);
                foreach (DataRow dtRow in table.Rows)
                {
                    string codeName = dtRow[0].ToString();
                    string description = dtRow[1] == DBNull.Value ? "" : dtRow[1].ToString();
                    string isnullable = (int)dtRow[2] == 1 ? "可为空" : "不可为空";
                    string typeName = SqlTypeToCSharpType(dtRow[3].ToString());
                    switch (typeName.ToLower())
                    {
                        case "int":
                        case "bool":
                        case "boolean":
                        case "long":
                        case "float":
                        case "double":
                        case "datetime":
                        case "int16":
                        case "int32":
                        case "int64":
                        case "decimal":
                            if ((int)dtRow[2] == 1)
                            {
                                typeName = typeName + "?";
                            }
                            break;
                    }
                    string length = "长度为" + (int)dtRow[4];
                    if (dic != null)
                    {
                        foreach (string key in dic.Keys)
                        {
                            if (dic[key].Equals(codeName))
                            {
                                codeName = key;
                            }
                        }
                    }
                    string newCodeName =  codeName.Substring(0, 1).ToUpper() + codeName.Substring(1);
                    builder.AppendFormat("\r\n        private {0} m{1};", typeName, newCodeName);
                    builder.AppendFormat("\r\n        /// <summary>\r\n        /// {2}\r\n        /// {3}\r\n        /// {4}\r\n        /// </summary>\r\n        [DataMember] \r\n        public {0} {5} \r\n        {{\r\n            get {{ return m{1}; }} \r\n            set {{ m{1} = changeValue(\"{5}\", m{1}, value); }} \r\n        }}\r\n",
                        typeName, newCodeName, description, length, isnullable, codeName);
                }
                builder.Append("\r\n    }\r\n}");
            }
            else
            {
                builder = new StringBuilder("可能存在查询语句错误，返回值null");
            }
            return builder.ToString();
        }

        public static string DataTableToMappingEntityString(string SqlConnectionString, string tableName, bool isOld)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format(@"
DECLARE @tbValue VARCHAR(200)
SET @tbValue = (
	SELECT cast (ds.value AS VARCHAR(200)) 描述   
	FROM sys.extended_properties ds  
		LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
	WHERE  ds.minor_id=0 and  
	tbs.name='{0}'--表名
) 
SELECT
    a.name 字段名,
    g.[value] AS 字段说明,
    a.isnullable ,
    b.name ,
    case when a. length=-1 then 8000 else a. length end length,
    a.colorder 字段序号,
    @tbValue 表描述
FROM
       syscolumns a
       left join systypes b on a. xtype=b .xusertype
       inner join sysobjects d on a. id=d .id and d .xtype= 'U' and d.name <>'dtproperties'
       left join sys. extended_properties g on a.id =g. major_id AND a. colid = g .minor_id
WHERE d .[name] ='{0}' --表名字
order by a. id,a .colorder", tableName);
            StringBuilder builder = null;
            DataTable table = ExecuteDataTable(SqlConnectionString, cmd);
            if (table != null && table.Rows.Count > 0)
            {
                builder = new StringBuilder();
                builder.AppendFormat("                    case \"{0}\":\r\n                        propertyNameMapping = new FWDictionary<String, String>() {{", (isOld ? "MappingEntity__" : "M") + tableName);
                foreach (DataRow dtRow in table.Rows)
                {
                    string codeName = dtRow[0].ToString();
                    builder.AppendFormat("\r\n                            {{\"{0}\",\"{1}\"}}{2}", codeName.Replace("PK_", "").Replace("FK_", ""), codeName, table.Rows[table.Rows.Count - 1] == dtRow ? "" : ",");
                }
                builder.Append("\r\n                        };\r\n                        break;");
            }
            else
            {
                builder = new StringBuilder("可能存在查询语句错误，返回值null");
            }
            return builder.ToString();
        }

        public static string SqlTypeToCSharpType(string SqlType)
        {
            string CSharpType = "int";
            switch (SqlType.ToLower())
            {
                case "bigint":
                    CSharpType = "Int64";
                    break;
                case "binary":
                case "sql_variant":
                    CSharpType = "Object";
                    break;
                case "bit":
                    CSharpType = "Boolean";
                    break;
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varchar":
                case "xml":
                case "text":
                    CSharpType = "String";
                    break;
                case "datetime":
                    CSharpType = "DateTime";
                    break;
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    CSharpType = "Decimal";
                    break;
                case "float":
                    CSharpType = "Double";
                    break;
                case "image":
                case "timestamp":
                case "varbinary":
                    CSharpType = "Byte[]";
                    break;
                case "int":
                    CSharpType = "Int32";
                    break;
                case "real":
                    CSharpType = "Single";
                    break;
                case "smalldatetime":
                    CSharpType = "DateTime";
                    break;
                case "smallint":
                    CSharpType = "Int16";
                    break;
                case "tinyint":
                    CSharpType = "Byte";
                    break;
                case "uniqueidentifier":
                    CSharpType = "Guid";
                    break;
                default:
                    CSharpType = "String";
                    break;
            }
            return CSharpType;
        }

        public static DataTable ExecuteDataTable(string SqlConnectionString, SqlCommand cmd)
        {
            DataTable table2;
            try
            {
                if (string.IsNullOrEmpty(SqlConnectionString))
                {
                }
                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    if (connection == null)
                    {
                        throw new Exception("数据库连接SqlConnection对象不存在");
                    }
                    cmd.Connection = connection;
                    DataSet dataSet = new DataSet();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataSet);
                        cmd.Parameters.Clear();
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                    table2 = dataSet.Tables[0];
                }
            }
            catch (Exception)
            {
                table2 = null;
            }
            return table2;
        }

        public static string GetFileAbsolutePath(string FileRelativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileRelativePath);
        }

        public static void WriteToFile(string FileRelativePath, string WriteString)
        {
            string fileAbsolutePath = GetFileAbsolutePath(FileRelativePath);
            if (!File.Exists(fileAbsolutePath))
            {
                File.Create(fileAbsolutePath).Close();
            }
            FileStream stream = new FileStream(fileAbsolutePath, FileMode.Open, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("gb2312"));
            stream.SetLength(0L);
            writer.Write(WriteString);
            writer.Close();
        }

        #region 停用
        public static string DataTableToEntityStringCs1(string SqlConnectionString, SqlCommand cmd, string tableName, string webUrl)
        {
            StringBuilder builder = null;
            DataTable table = ExecuteDataTable(SqlConnectionString, cmd);
            if (table != null)
            {
                DataColumnCollection columns = table.Columns;
                if (columns.Count > 0)
                {
                    builder = new StringBuilder();
                    builder.AppendFormat("using System; \r\nusing System.Collections.Generic; \r\nusing System.Linq; \r\nusing System.Text; \r\nusing System.Runtime.Serialization;\r\n", new object[0]);
                    builder.AppendFormat("\r\nnamespace {0} \r\n{{ \r\n    [DataContract] \r\n    public class {1}\r\n    {{   \r\n        private HashSet<String> EntityToSqlParameters = null;\r\n", webUrl, tableName);
                    foreach (DataColumn column in columns)
                    {
                        string name = column.DataType.Name;
                        if (column.AllowDBNull)
                        {
                            switch (column.DataType.Name.ToLower())
                            {
                                case "int":
                                case "bool":
                                case "boolean":
                                case "long":
                                case "float":
                                case "double":
                                case "datetime":
                                case "int16":
                                case "int32":
                                case "int64":
                                case "decimal":
                                    name = name + "?";
                                    break;
                            }
                        }
                        builder.AppendFormat("\r\n        private {0} _{1};", name, column.ColumnName);
                        builder.AppendFormat("\r\n        [DataMember] \r\n        public {0} {1} \r\n        {{\r\n            set {{ _{1} = value; if (EntityToSqlParameters == null) {{ EntityToSqlParameters = new HashSet<String>(); }};EntityToSqlParameters.Add(\"{1}\"); }} \r\n            get {{ return _{1}; }} \r\n        }}\r\n", name, column.ColumnName);
                    }
                }
                builder.Append("\r\n    }\r\n}");
            }
            else
            {
                builder = new StringBuilder("可能存在查询语句错误，返回值null");
            }
            return builder.ToString();
        }
        #endregion

        /// <summary>
        /// Json字符串转化为字典集合
        /// </summary>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>字典集合</returns>
        public static Dictionary<string, object> JsonStringToDictionary(string JsonString)
        {
            return JsonStringToObject<Dictionary<string, object>>(JsonString);
        }

        /// <summary>
        /// Json字符串转化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>对象类</returns>
        public static T JsonStringToObject<T>(string JsonString)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                if (!string.IsNullOrEmpty(JsonString))
                {
                    return jss.Deserialize<T>(JsonString);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JsonStringToObject(): " + ex.Message);
            }
        }
    }
}
