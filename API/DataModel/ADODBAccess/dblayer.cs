using DataModel.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace DataModel.DBLayer
{
    public class DbLayer : IDisposable
    {
        public SqlConnection sqlConnection;

        public DbLayer()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CMS_Context"].ConnectionString);
        }

        public DbLayer(string connectionStringName)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
        }

        public void Dispose()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        ~DbLayer()
        {
            Dispose();
        }

        public DataSet fillDataSet(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection == null)
                sqlCommand.Connection = this.sqlConnection;
            int retryCount = 3;
            int retrySleepTimeInSeconds = 3;
            DataSet dataSet = null;
            while (retryCount >= 1)
            {
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);                   
                    return dataSet;

                }
                catch (Exception ex)
                {
                    retryCount--;
                    Thread.Sleep(retrySleepTimeInSeconds * 1000);
                    dataSet = null;
                    if (retryCount <= 0)
                        throw ex;
                }
            }
            return dataSet;
        }
        public int ExecuteNonQuery(SqlCommand sqlCommand)
        {
            if (sqlCommand.Connection == null)
                sqlCommand.Connection = this.sqlConnection;
            int retryCount = 3;
            int retrySleepTimeInSeconds = 3;
            int sqlRetval = Int32.MaxValue;
            while (retryCount >= 1)
            {
                try
                {
                    sqlConnection.Open();
                    sqlRetval = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    sqlRetval = Int32.MaxValue;
                    retryCount--;
                    Thread.Sleep(retrySleepTimeInSeconds * 1000);
                    if (retryCount <= 0)
                        throw ex;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
                if (sqlRetval != Int32.MaxValue)
                    return sqlRetval;
            }
            return sqlRetval;
        }
        public int ExecuteNonQuery(SqlCommand sqlCommand, bool getReturnValue)
        {
            SqlParameter retParam = new SqlParameter();
            if (sqlCommand.Connection == null)
                sqlCommand.Connection = this.sqlConnection;

            if (getReturnValue)
            {
                retParam.Direction = ParameterDirection.ReturnValue;
                sqlCommand.Parameters.Add(retParam);
            }

            int retryCount = 3;
            int retrySleepTimeInSeconds = 3;
            int sqlRetVal = Int32.MaxValue;

            while (retryCount >= 1)
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlRetVal = Convert.ToInt32(retParam.Value);
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    sqlRetVal = Int32.MaxValue;
                    retryCount--;
                    Thread.Sleep(retrySleepTimeInSeconds * 1000);
                    if (retryCount <= 0)
                        throw ex;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
                if (sqlRetVal != Int32.MaxValue)
                    return sqlRetVal;

            }
            return sqlRetVal;
        }

        public List<T> GetEntityList<T>(SqlCommand sqlCommand) where T : new()
        {
            return GetEntityList<T>(sqlCommand, 0);
        }

        public List<T> GetEntityList<T>(SqlCommand sqlCommand, int tableIndexInDataSet) where T : new()
        {
            try
            {
                return Utility.ConvertDataTableToEntityList<T>(fillDataSet(sqlCommand).Tables[tableIndexInDataSet]);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
