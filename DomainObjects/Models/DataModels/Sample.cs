using DomainObjects.DataAccessInterface;
using DomainObjects.Models.Base;

namespace DomainObjects.Models.DataModels
{
    [StoredProcedureList("_List_SomeData"), StoredProcedureSave("_Save_SomeData"), InitialCatalog(DataBaseCatalog.DataBaseA)]
    class SampleObject
    {
        public int? SomeInt { get; set; }
        public string SomeText
        {
            get
            {
                return SomeInt < 10 ? "You don't have enough things" : "You have enough things";
            }
        }
    }
}
