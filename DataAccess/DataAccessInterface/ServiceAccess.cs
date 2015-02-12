using System.Collections.Generic;
using System.Data.SqlClient;
using DomainObjects.DataAccessInterface;
using DomainObjects.Models.Base;
using DomainObjects.Models.Request;

namespace DataAccess.DataAccessInterface
{
    public class ServiceAccess : IServiceAccess
    {
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public RequestList<T> Select<T>(DataAccessModels.InputModel<T> input) where T : new()
        {
            System.Reflection.MemberInfo info1 = typeof(T);
            var connectionBuilder = new SqlConnectionStringBuilder(ConnectionFetcher.Get());
            var model = new RequestList<T> { Success = false, Message = "Invalid Session ID" };
            foreach (var customAttributeData in info1.GetCustomAttributes(true))
            {
                if (customAttributeData.GetType() == typeof(StoredProcedureList))
                {
                    input.StoredProcedure = ((StoredProcedureList)customAttributeData).Proc;
                }
                if (customAttributeData.GetType() != typeof(InitialCatalog)) continue;
                var catalog = ((InitialCatalog)customAttributeData).Catalog;
                connectionBuilder["Initial Catalog"] = catalog.ToString();
            }
            if (string.IsNullOrEmpty(input.StoredProcedure))
            {
                model.Message = "Missing Attribute [StoredProcedureList(*Your Proc Here*)]";
                model.Success = false;
                return model;
            }
            var da = new DataAccess(connectionBuilder.ConnectionString);
            var validationDa = new DataAccess(ConnectionFetcher.Get());
            if (!validationDa.ValidateSessionId(input.SessionId))
            {
                model.Message = "Invalid Session ID";
                model.Success = false;
                return model;
            }
            var dbReuslt = da.ExecuteQueryStoredProc(input);
            if (dbReuslt.Success)
            {
                var data = dbReuslt.DataSet;
                model.Obj = ObjectConverter.ToList(data.Tables[0], new List<T>());
                model.Success = true;
                model.Message = "";
                model.TotalCount = model.Obj.Count;
            }
            else
            {
                model.Obj = null;
                model.Success = false;
                model.Message = dbReuslt.Message;
                model.Exception = dbReuslt.Exception;
                model.TotalCount = 0;
            }
            return model;
        }
        /// <summary>
        /// Saves the specified input.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public RequestList<T> Save<T>(DataAccessModels.InputModel<T> input) where T : new()
        {
            System.Reflection.MemberInfo info1 = typeof(T);
            var connectionBuilder = new SqlConnectionStringBuilder(ConnectionFetcher.Get());
            var model = new RequestList<T> { Success = false, Message = "Invalid Session ID" };
            foreach (var customAttributeData in info1.GetCustomAttributes(true))
            {
                var a = customAttributeData.GetType();
                if (customAttributeData.GetType() == typeof(StoredProcedureSave))
                {

                    input.StoredProcedure = ((StoredProcedureSave)(customAttributeData)).Proc;
                }
                if (customAttributeData.GetType() != typeof(InitialCatalog)) continue;
                var catalog = ((InitialCatalog)customAttributeData).Catalog;
                connectionBuilder["Initial Catalog"] = catalog.ToString();
            }
            if (string.IsNullOrEmpty(input.StoredProcedure))
            {
                model.Message = "Missing Attribute [StoredProcedureSave(*Your Proc Here*)]";
                model.Success = false;
                return model;
            }
            var da = new DataAccess(connectionBuilder.ConnectionString);
            var validationDa = new DataAccess(ConnectionFetcher.Get());
            if (!validationDa.ValidateSessionId(input.SessionId))
            {
                model.Message = "Invalid Session ID";
                model.Success = false;
                return model;
            }
            var dbReuslt = da.ExecuteQueryStoredProc(input);
            if (dbReuslt.Success)
            {
                var data = dbReuslt.DataSet;
                model.Obj = ObjectConverter.ToList(data.Tables[0], new List<T>());
                model.Success = true;
                model.Message = "";
                model.TotalCount = model.Obj.Count;
            }
            else
            {
                model.Obj = null;
                model.Success = false;
                model.Message = dbReuslt.Message;
                model.Exception = dbReuslt.Exception;
                model.TotalCount = 0;
            }
            return model;
        }


    }
}
