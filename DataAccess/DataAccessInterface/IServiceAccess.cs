using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects.Models.Base;
using DomainObjects.Models.Request;

namespace DataAccess.DataAccessInterface
{
    public interface IServiceAccess
    {
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        RequestList<T> Select<T>(DataAccessModels.InputModel<T> input) where T : new();
        /// <summary>
        /// Saves the specified input.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        RequestList<T> Save<T>(DataAccessModels.InputModel<T> input) where T : new();

    }
}
