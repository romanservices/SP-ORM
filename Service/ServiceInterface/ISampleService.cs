using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.DataModels;
using DomainObjects.Models.Request;

namespace Service.ServiceInterface
{
    public interface ISampleService
    {
        RequestList<Customer> ListCustomers(CustomerFilter filter);
        RequestList<Customer> SaveCustomer(Customer item);
        RequestList<DBBColor> ListColors(DBBColorFilter filter);
    }
}
