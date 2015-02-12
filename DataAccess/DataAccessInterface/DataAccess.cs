using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using DomainObjects.Models.Base;

namespace DataAccess.DataAccessInterface
{
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// The _database
        /// </summary>
        private readonly SqlDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DataAccess(string connectionString)
        {
            _database = new SqlDatabase(connectionString);
        }

        /// <summary>
        /// Executes the query stored proc.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        /// <returns></returns>
        public DataAccessModels.ReturnModel ExecuteQueryStoredProc<T>(DataAccessModels.InputModel<T> inputModel) where T : new()
        {
            var rm = new DataAccessModels.ReturnModel
            {
                DataSet = new DataSet(),
                Message = "Failed to execute query",
                Success = false,
                Output = inputModel.Output
            };
            var comm = CreateStoredProcedureCommand(inputModel.StoredProcedure);
            for (var i = 0; i < inputModel.QueryParameters.Count; i++)
                _database.AddInParameter(comm, inputModel.QueryParameters[i].Name, inputModel.QueryParameters[i].DbType,
                    inputModel.QueryParameters[i].Value);

            for (var i = 0; i < inputModel.Output.Count; i++)
                _database.AddOutParameter(comm, inputModel.Output[i].Name, inputModel.Output[i].DbType,
                    inputModel.Output[i].Size);
            try
            {
                rm.DataSet = _database.ExecuteDataSet(comm);
                for (var i = 0; i < rm.Output.Count; i++)
                {
                    rm.Output[i].Value = _database.GetParameterValue(comm, rm.Output[i].Name);
                }
                rm.Success = true;
                rm.Message = string.Empty;
            }
            catch (Exception exc)
            {
                rm.Exception = exc;
                rm.Message = "Unable to execute query stored procedure: " + inputModel.StoredProcedure + ", " +
                             exc.InnerException;
                rm.Exception = exc;
                rm.Success = false;
            }
            return rm;
        }

        /// <summary>
        /// Validates the session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        public bool ValidateSessionId(string sessionId)
        {
            bool result;

            //var comm = CreateStoredProcedureCommand("_SALI_ValidateWebServiceSession");
            //var connectionBuilder = new SqlConnectionStringBuilder(comm.Connection.ConnectionString);
            //connectionBuilder["Initial Catalog"] = DataBaseCatalog.CustomerMgmt.ToString();
            //comm.Connection.ConnectionString = connectionBuilder.ConnectionString;
            //_database.AddInParameter(comm, "@SessionId", DbType.String, sessionId);
            //_database.AddOutParameter(comm, "@IsValid", DbType.Boolean, int.MaxValue);
            //_database.AddOutParameter(comm, "@UserName", DbType.String, int.MaxValue);
            //_database.AddOutParameter(comm, "@UserType", DbType.String, int.MaxValue);
            //_database.AddOutParameter(comm, "@ApplicationType", DbType.Int16, int.MaxValue);
            //_database.AddOutParameter(comm, "@IsDemoSession", DbType.Boolean, int.MaxValue);
            //try
            //{
            //    _database.ExecuteNonQuery(comm);
            //    result = (bool)_database.GetParameterValue(comm, "IsValid");

            //}
            //catch (Exception)
            //{
            //    result = false;
            //}
            return true;
        }

        /// <summary>
        /// Creates the stored procedure command.
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <returns></returns>
        protected DbCommand CreateStoredProcedureCommand(string commandString)
        {
            var result = _database.CreateConnection().CreateCommand();
            result.CommandType = CommandType.StoredProcedure;
            result.CommandTimeout = int.MaxValue;
            result.CommandText = commandString;
            return result;
        }
    }
}
