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
      [StoredProcedureList("_List_Colors"), InitialCatalog(DataBaseCatalog.DataBaseB)]
    public class DBBColor
    {
          public int Id { get; set; }
          [StringLength(4, ErrorMessage = "Color value cannot exceed 100 characters. ")]
          public string Color { get; set; }
    }

    public class DBBColorFilter
    {
        public int? Id { get; set; }
    }
}
