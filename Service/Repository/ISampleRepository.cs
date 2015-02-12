using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.DataModels;
using DomainObjects.Models.Request;

namespace Service.Repository
{
    public interface ISampleRepository
    {
        RequestList<Customer> GetCustomersList(CustomerFilter filter);
        RequestList<Customer> SaveCustoemr(Customer item);
        RequestList<DBBColor> GetColorList(DBBColorFilter filter);
    }
}
