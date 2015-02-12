using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace DataAccess.DataAccessInterface
{
    public static class ObjectConverter
    {
        /// <summary>
        /// Converts to single object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <param name="index">The index.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public static T ToSingle<T>(DataTable dt, int index, T myObject)
        {
            var properties = typeof(T).GetProperties();
            var objT = Activator.CreateInstance<T>();
            if (dt.Rows.Count < 1) return objT;
            var row = dt.Rows[index];
            foreach (var propertyInfo in properties)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    if (dataColumn.ColumnName == propertyInfo.Name)
                    {
                        try
                        {
                            var property = propertyInfo.PropertyType;
                            var checkNullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                            if (checkNullable != null)
                            {
                                property = checkNullable;
                            }
                            propertyInfo.SetValue(objT,
                                propertyInfo.PropertyType.Name == "Byte[]"
                                    ? Convert.ChangeType(row[dataColumn], property)
                                    : Convert.ChangeType(row[dataColumn].ToString(), property), null);
                        }
                        catch (Exception)
                        {
                            //property set as null - This is the expected result
                        }
                    }
                }
            }

            return objT;
        }
        /// <summary>
        /// Converts to object list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <param name="myObjectList">My object list.</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(DataTable dt, IList<T> myObjectList)
        {
            var properties = typeof(T).GetProperties();
            foreach (DataRow row in dt.Rows)
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var propertyInfo in properties)
                {
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        if (dataColumn.ColumnName == propertyInfo.Name)
                        {
                            try
                            {
                                var property = propertyInfo.PropertyType;
                                var checkNullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                                if (checkNullable != null)
                                {
                                    property = checkNullable;
                                }
                                propertyInfo.SetValue(objT,
                                    propertyInfo.PropertyType.Name == "Byte[]"
                                        ? Convert.ChangeType(row[dataColumn], property)
                                        : Convert.ChangeType(row[dataColumn].ToString(), property), null);
                            }
                            catch (Exception)
                            {
                                //property set as null - This is the expected result
                            }

                        }
                    }
                }

                myObjectList.Add(objT);
            }
            return myObjectList;
        }
    }
    public static class EnumerableExtender
    {
        /// <summary>
        /// To the data set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(this IList<T> data) where T : class
        {
            var retOut = new DataSet();
            var table = new DataTable();
            var properties = typeof(T).GetProperties();
            foreach (var column in properties.Select(prop => new DataColumn(prop.Name)))
            {
                table.Columns.Add(column);
            }
            foreach (var t in data)
            {
                var row = table.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(t, null);
                }
                table.Rows.Add(row);
            }
            retOut.Tables.Add(table);
            return retOut;
        }
    }
}
