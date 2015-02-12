using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.Base;

namespace DataAccess.DataAccessInterface
{
    public interface IDataAccess
    {
        DataAccessModels.ReturnModel ExecuteQueryStoredProc<T>(DataAccessModels.InputModel<T> inputModel) where T : new();
        bool ValidateSessionId(string sessionId);
    }
}
