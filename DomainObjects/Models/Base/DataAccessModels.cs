using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Models.Base
{
    public class DataAccessModels
    {
        public class ReturnModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ReturnModel"/> class.
            /// </summary>
            public ReturnModel()
            {
                Output = new List<QueryParameter>();
            }
            public DataSet DataSet { get; set; }
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<QueryParameter> Output { get; set; }
            public Exception Exception { get; set; }

        }
        public class InputModel<T> where T : new()
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InputModel"/> class.
            /// </summary>
            /// <param name="T"></param>
            /// <param name="sessionId"></param>
            public InputModel(string sessionId)
            {
                QueryParameters = new List<QueryParameter>();
                Output = new List<QueryParameter>();
                Type = new T();
                SessionId = sessionId;
            }
            public string StoredProcedure { get; set; }
            public string SessionId { get; set; }
            public List<QueryParameter> QueryParameters { get; set; }
            public List<QueryParameter> Output { get; set; }
            public T Type { get; set; }
        }
    }

    public class QueryParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameter"/> class.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="size">The size.</param>
        public QueryParameter(DbType dbType, string name, object value, int? size)
        {
            Size = 0;
            DbType = dbType;
            Value = value;
            Name = name;
            if (size.HasValue)
            {
                Size = size.Value;
            }
            else
            {
                Size = -1;
            }

        }

        public DbType DbType { get; private set; }
        public string Name { get; private set; }
        public object Value { get; set; }
        public int Size { get; private set; }
    }
}
