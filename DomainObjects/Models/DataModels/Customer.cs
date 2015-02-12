using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.DataAccessInterface;
using DomainObjects.Models.Base;

namespace DomainObjects.Models.DataModels
{
        [StoredProcedureList("_List_CustomerAndAddress"), StoredProcedureSave("_Save_Customer"), InitialCatalog(DataBaseCatalog.DataBaseA)]
    public class Customer
    {
            public int CustomerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            [StringLength(4, ErrorMessage = "State value cannot exceed 4 characters. ")]
            public string State { get; set; }
            [StringLength(4, ErrorMessage = "Zip value cannot exceed 20 characters. ")]
            public string Zip { get; set; }
    }

    public class CustomerFilter
    {
        public int? CustomerId { get; set; }
    }
}
