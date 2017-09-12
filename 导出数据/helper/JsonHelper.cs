using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;

namespace 导出数据.helper
{
    /// <summary>
    /// Json辅助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 实体类转化为Json字符串
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="Entity">实体类</param>
        /// <returns>Json字符串</returns>
        public static string EntityToJsonString<T>(T Entity)
        {
            if (Entity == null)
            {
                return null;
            }
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(Entity.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                dcjs.WriteObject(ms, Entity);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        /// <summary>
        /// 对象转化为Json字符串
        /// </summary>
        /// <param name="ObjectEntity">对象</param>
        /// <returns>Json字符串</returns>
        public static T ObjectCopy<T>(T ObjectEntity)
        {
            return JsonHelper.JsonStringToEntity<T>(JsonHelper.EntityToJsonString(ObjectEntity));
        }

        /// <summary>
        /// Json字符串转化为实体类
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>实体类</returns>
        public static T JsonStringToEntity<T>(this string JsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonString));
            T Entity = (T)ser.ReadObject(ms);
            ms.Close();
            return Entity;
        }

        /// <summary>
        /// 对象转化为Json字符串
        /// </summary>
        /// <param name="ObjectEntity">对象</param>
        /// <returns>Json字符串</returns>
        public static string ObjectToJsonString(object ObjectEntity)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(ObjectEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.ObjectToJsonString(): " + ex.Message);
            }
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

        /// <summary>
        /// Json字符串转化为对象
        /// </summary>
        /// <param name="_Type">对象类型</typeparam>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>对象类</returns>
        public static object JsonStringToObject(Type _Type, string JsonString)
        {
            object objectReturn = null;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                if (!string.IsNullOrEmpty(JsonString))
                {
                    objectReturn = jss.Deserialize(JsonString, _Type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("JsonHelper.JsonStringToObject(): " + ex.Message);
            }
            return objectReturn;
        }

        /// <summary>
        /// Json字符串转化为对象
        /// </summary>
        /// <param name="_Type">对象类型</typeparam>
        /// <param name="objectValue">Json字符串</param>
        /// <returns>对象类</returns>
        public static object JsonStringToObject(Type _Type, object objectValue)
        {
            object objectReturn = null;
            if (objectValue != null)
            {
                if (_Type.FullName == typeof(String).FullName
                || _Type.FullName == typeof(Int32).FullName
                || _Type.FullName == typeof(Int64).FullName
                || _Type.FullName == typeof(Boolean).FullName
                || _Type.FullName == typeof(Int16).FullName
                || _Type.FullName == typeof(DateTime).FullName)
                {
                    objectReturn = Convert.ChangeType(objectValue, _Type);
                }
                else if (_Type.FullName == typeof(Boolean?).FullName)
                {
                    objectReturn = objectValue;
                }
                else
                {
                    Type objectType = objectValue.GetType();
                    if (_Type.IsGenericType)
                    {
                        if (objectType.FullName == typeof(System.Collections.ArrayList).FullName)
                        {
                            List<object> objectList = JsonHelper.JsonStringToObject<List<object>>(JsonHelper.ObjectToJsonString(objectValue));
                            if (objectList.Count > 0)
                            {
                                Type GenericArgumentType = _Type.GetGenericArguments()[0];
                                object objectTypeList = Activator.CreateInstance(_Type);
                                for (var i = 0; i < objectList.Count; i++)
                                {
                                    _Type.GetMethod("Add").Invoke(objectTypeList, new object[] { JsonHelper.JsonStringToObject(GenericArgumentType, JsonHelper.ObjectToJsonString(objectList[i])) });
                                }
                                objectReturn = objectTypeList;
                            }
                        }
                        else
                        {
                            if (_Type == typeof(DateTime) || _Type == typeof(DateTime?))
                            {
                                objectReturn = Convert.ToDateTime(objectValue);
                            }
                        }
                    }
                    else
                        if (_Type.IsArray)
                        {
                            if (objectType.FullName == typeof(System.Collections.ArrayList).FullName)
                            {
                                List<object> objectList = JsonHelper.JsonStringToObject<List<object>>(JsonHelper.ObjectToJsonString(objectValue));
                                if (objectList.Count > 0)
                                {
                                    Type GenericArgumentType = _Type.GetGenericArguments()[0];
                                    Array objectTypeList = Array.CreateInstance(GenericArgumentType, objectList.Count);
                                    for (var i = 0; i < objectList.Count; i++)
                                    {
                                        objectTypeList.SetValue(JsonHelper.JsonStringToObject(GenericArgumentType, JsonHelper.ObjectToJsonString(objectList[i])), i);
                                    }
                                    objectReturn = objectTypeList;
                                }
                            }
                        }
                        else
                        {
                            objectReturn = JsonHelper.JsonStringToObject(_Type, JsonHelper.ObjectToJsonString(objectValue));
                        }
                }
            }
            return objectReturn;
        }

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable JsonStringToDataTable(this string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion

        /// <summary>
        /// DataTable转化为字典集合
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>字典集合</returns>
        public static List<Dictionary<string, object>> DataTableToDictionaryList(this DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// DataSet转化为字典集合字典
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns>字典集合字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDictionary(this DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();
            foreach (DataTable dt in ds.Tables)
                result.Add(dt.TableName, DataTableToDictionaryList(dt));
            return result;
        }

        /// <summary>
        /// DataTable转化为Json字符串
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>Json字符串</returns>
        public static string DataTableToJsonString(this DataTable dt)
        {
            return JsonHelper.ObjectToJsonString(dt.DataTableToDictionaryList());
        }

        /// <summary>
        /// Json字符串转化为字典集合字典
        /// </summary>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>字典集合字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> JsonStringToDictionaryList(this string JsonString)
        {
            return JsonHelper.JsonStringToObject<Dictionary<string, List<Dictionary<string, object>>>>(JsonString);
        }

        /// <summary>
        /// Json字符串转化为字典集合
        /// </summary>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>字典集合</returns>
        public static Dictionary<string, object> JsonStringToDictionary(this string JsonString)
        {
            return JsonHelper.JsonStringToObject<Dictionary<string, object>>(JsonString);
        }

        /// <summary>
        /// Json字符串转化为字典集合
        /// </summary>
        /// <param name="JsonString">Json字符串</param>
        /// <returns>字典集合</returns>
        public static List<Dictionary<string, object>> JsonStringToListDictionary(this string JsonString)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(JsonString))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(JsonString);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        list.Add(dictionary);
                    }
                }
            }
            return list;
        }

        public static String JoinString<T>(this IEnumerable<T> enumerable, string separater)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                str.Append(separater);
                str.Append(enumerator.Current.ToString());
            }
            if (str.Length > 0)
            {
                return str.ToString().Remove(0, separater.Length);
            }
            return string.Empty;
        }

        public static String JoinString<T>(this IEnumerable<T> enumerable, string separater, Func<T, String> func)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                str.Append(separater);
                str.Append(func(enumerator.Current));
            }
            if (str.Length > 0)
            {
                return str.ToString().Remove(0, separater.Length);
            }
            return string.Empty;
        }
    }
}
