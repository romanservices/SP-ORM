using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.DataModels;
using DomainObjects.Models.Request;
using Service.Repository;

namespace Service.ServiceInterface
{
    public class SampleService : ISampleService
    {
        private ISampleRepository _repository;

        public SampleService(ISampleRepository repository)
        {
            _repository = repository;
        }
        public RequestList<Customer> ListCustomers(CustomerFilter filter)
        {
            return _repository.GetCustomersList(filter);
        }

        public RequestList<Customer> SaveCustomer(Customer item)
        {
            return _repository.SaveCustoemr(item);
        }

        public RequestList<DBBColor> ListColors(DBBColorFilter filter)
        {
            return _repository.GetColorList(filter);
        }
    }
}
