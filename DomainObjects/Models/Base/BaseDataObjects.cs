using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Models
{
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

    public class BaseDataObject
    {
        /// <summary>
        /// Gets or sets the property bag.
        /// </summary>
        /// <value>
        /// The property bag.
        /// </value>
        public Dictionary<string, string> PropertyBag { get; set; }
    }
}
