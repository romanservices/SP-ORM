using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.Base;

namespace DomainObjects.DataAccessInterface
{
    [AttributeUsage(AttributeTargets.All)]
    public class InitialCatalog : System.Attribute
    {
        public readonly DataBaseCatalog Catalog;
        public string Topic { get; set; }
        public InitialCatalog(DataBaseCatalog catelog)
        {
            Catalog = catelog;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class DataBaseEnvironment : System.Attribute
    {
        public readonly DataEnvironment Environments;
        public string Topic { get; set; }
        public DataBaseEnvironment(DataEnvironment environments)
        {
            Environments = environments;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class StoredProcedureList : System.Attribute
    {
        public readonly string Proc;
        public string Topic { get; set; }
        public StoredProcedureList(string proc)
        {
            Proc = proc;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class StoredProcedureSave : System.Attribute
    {
        public readonly string Proc;
        public string Topic { get; set; }
        public StoredProcedureSave(string proc)
        {
            Proc = proc;
        }
    }
}
