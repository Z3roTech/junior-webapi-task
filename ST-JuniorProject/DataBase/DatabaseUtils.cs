using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

#if NETCOREAPP2_0_OR_GREATER

using Microsoft.Data.SqlClient;

#else
using System.Data.SqlClient;
#endif

using System.Linq;

namespace StranzitOnline.Common.Tools
{
    /// <summary>
    /// Утилиты для работы с курсором, который возвращает SQLCommand.ExecuteReader
    /// </summary>
    public static class DatabaseUtils
    {
        /// <summary>
        /// Проверка, существует ли колонка в ответе процедуры
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получение настроек по умолчанию для модели представления
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PermissionId"></param>
        /// <param name="TargetViewModel"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetViewModel(int CustomerId, int PermissionId, string TargetViewModel, string ConnectionString)
        {
            try
            {
                var result = new Dictionary<string, string>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(@"GetViewModel", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cl_id", CustomerId);
                        command.Parameters.AddWithValue("@per_id", PermissionId);
                        command.Parameters.AddWithValue("@model_name", TargetViewModel);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // получаем список информации о доступных отчетах
                                result.Add(reader["cname"].ToString(), reader["cvalue"].ToString());
                            }
                        }
                    }
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Выполнить хранимую процедуру с параметрами
        /// </summary>
        /// <param name="spName">имя ХП</param>
        /// <param name="connectionString">строка подключения</param>
        /// <param name="spParams">параметры запроса</param>
        /// <returns>все результирующие курсоры</returns>
        public static IEnumerable<IEnumerable<Dictionary<string, object>>> ExecuteSP(string spName, Dictionary<string, object> spParams = null, string connectionString = null, SqlConnection connection = null)
        {
            var wasConnected = connection != null;
            var resultList = new List<List<Dictionary<string, object>>>();
            if (connection == null) connection = new SqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed) connection.Open();
            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection can not be opened");
            using (var cmd = new SqlCommand(spName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (spParams != null)
                    cmd.Parameters.AddRange(spParams.Select(x => new SqlParameter(x.Key, x.Value)).ToArray());
                var adapter = new SqlDataAdapter(cmd);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                foreach (DataTable dt in dataset.Tables)
                {
                    var headers = new List<string>();
                    var currentList = new List<Dictionary<string, object>>();
                    foreach (DataColumn col in dt.Columns)
                        headers.Add(col.ColumnName);
                    foreach (DataRow row in dt.Rows)
                    {
                        currentList.Add(headers.ToDictionary(x => x, x => row[x]));
                    }
                    resultList.Add(currentList);
                }
            }
            if (!wasConnected)
            {
                connection.Close();
                connection.Dispose();
            }
            return resultList;
        }

        /// <summary>
        /// Выполнить запрос с параметрами
        /// </summary>
        /// <param name="spName">имя ХП</param>
        /// <param name="connectionString">строка подключения</param>
        /// <param name="spParams">параметры запроса</param>
        /// <returns>все результирующие курсоры</returns>
        public static IEnumerable<IEnumerable<Dictionary<string, object>>> ExecuteRequest(string spName, Dictionary<string, object> spParams = null, string connectionString = null, SqlConnection connection = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var wasConnected = connection != null;
            var resultList = new List<List<Dictionary<string, object>>>();
            if (connection == null) connection = new SqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed) connection.Open();
            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection can not be opened");
            using (var cmd = new SqlCommand(spName, connection))
            {
                cmd.CommandType = commandType;
                if (spParams != null)
                    cmd.Parameters.AddRange(spParams.Select(x => new SqlParameter(x.Key, x.Value)).ToArray());
                var adapter = new SqlDataAdapter(cmd);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                foreach (DataTable dt in dataset.Tables)
                {
                    var headers = new List<string>();
                    var currentList = new List<Dictionary<string, object>>();
                    foreach (DataColumn col in dt.Columns)
                        headers.Add(col.ColumnName);
                    foreach (DataRow row in dt.Rows)
                    {
                        currentList.Add(headers.ToDictionary(x => x, x => row[x]));
                    }
                    resultList.Add(currentList);
                }
            }
            if (!wasConnected)
            {
                connection.Close();
                connection.Dispose();
            }
            return resultList;
        }

        /// <summary>
        /// Значение из базы в целое
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(object value, int defaultValue = 0)
        {
            if (value == DBNull.Value) return defaultValue;
            return int.TryParse(value.ToString(), out int result) ? result : defaultValue;
        }

        /// <summary>
        /// Значение из базы в логическое
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(object value, bool defaultValue = false)
        {
            if (value == DBNull.Value) return defaultValue;
            return Convert.ToBoolean(value);
        }

        internal static DateTime ToDateTime(object value)
        {
            if (value == DBNull.Value) return default;
            return DateTime.TryParse(value.ToString(), out DateTime dateTime) ? dateTime : default;
        }

        internal static float ToFloat(object value)
        {
            if (value == DBNull.Value) return default;
            return float.TryParse(value.ToString(), out float floatValue) ? floatValue : default;
        }

        /// <summary>
        /// Преобразование объекта - JSON строки в словарь
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(object value)
        {
            if (value == DBNull.Value) return default;
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(value.ToString());
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }
    }
}