using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessInterface;
using DomainObjects.Models.Base;
using DomainObjects.Models.DataModels;
using DomainObjects.Models.Request;

namespace Service.Repository
{
    public class SampleRepository : ISampleRepository
    {
        private readonly IServiceAccess _serviceAccess;
        private readonly string _currentSessionId;

        public SampleRepository(IServiceAccess serviceAccess, string sessionId)
        {
            _serviceAccess = serviceAccess;
            _currentSessionId = sessionId;
        }
        public RequestList<Customer> GetCustomersList(CustomerFilter filter)
        {
            var inputModel = new DataAccessModels.InputModel<Customer>(_currentSessionId)
            {
                Output = new List<QueryParameter>(),
                QueryParameters = new List<QueryParameter>
                {
                    new QueryParameter(DbType.String, "@CustomerId ", filter.CustomerId, -1)
                }
            };
            var result = _serviceAccess.Select(inputModel);
            return result;
        }

        public RequestList<Customer> SaveCustoemr(Customer item)
        {
            var inputModel = new DataAccessModels.InputModel<Customer>(_currentSessionId)
            {
                Output = new List<QueryParameter>
                {
                    new QueryParameter(DbType.Int32, "@Id ", -1, -1)
                },
                QueryParameters = new List<QueryParameter>
                {
                    new QueryParameter(DbType.Int32, "@CustomerId ", item.CustomerId, -1),
                    new QueryParameter(DbType.Int32, "@FirstName ", item.FirstName, -1),
                    new QueryParameter(DbType.Int16, "@LastName ", item.LastName, -1),
                    new QueryParameter(DbType.Int32, "@Line1 ", item.Line1, -1),
                    new QueryParameter(DbType.Int32, "@Line2 ", item.Line2, -1),
                    new QueryParameter(DbType.Int32, "@City ", item.City, -1),
                    new QueryParameter(DbType.Int32, "@State ", item.State, -1),
                    new QueryParameter(DbType.Int32, "@Zip ", item.Zip, -1),
                }
            };
            var result = _serviceAccess.Save(inputModel);
            return result;
        }

        public RequestList<DBBColor> GetColorList(DBBColorFilter filter)
        {
            var inputModel = new DataAccessModels.InputModel<DBBColor>(_currentSessionId)
            {
                Output = new List<QueryParameter>(),
                QueryParameters = new List<QueryParameter>
                {
                    new QueryParameter(DbType.String, "@Id ", filter.Id, -1)
                }
            };
            var result = _serviceAccess.Select(inputModel);
            return result;
        }
    }
}
