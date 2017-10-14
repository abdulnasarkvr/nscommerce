using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace NscService
{
    public class SqlHelper
    {
        private string connectionString;
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        private int commandTimeout = 30;

        public SqlHelper()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);
                sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = commandTimeout;
                sqlCommand.Connection = sqlConnection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing data class." + Environment.NewLine + ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                //Clean Up Connection Object
                if (sqlConnection != null)
                {
                    if (sqlConnection.State != ConnectionState.Closed)
                    {
                        sqlConnection.Close();
                    }
                    sqlConnection.Dispose();
                }

                //Clean Up Command Object
                if (sqlCommand != null)
                {
                    sqlCommand.Dispose();
                }

            }

            catch (Exception ex)
            {
                throw new Exception("Error disposing data class." + Environment.NewLine + ex.Message);
            }

        }

        public void CloseConnection()
        {
            if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
        }

        public void GetExecuteScalarByCommand(string Command)
        {
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandTimeout = commandTimeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public string GetScalerOutputValue(string ParamName)
        {
            return sqlCommand.Parameters[ParamName].SqlValue.ToString();
        }

        public void GetExecuteNonQueryByCommand(string Command)
        {
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandTimeout = commandTimeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteNonQuery();

                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public DataSet GetDatasetByCommand(string Command)
        {
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandTimeout = commandTimeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Utility.SaveException(ex);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDatasetByCommand(string Command, CommandType commandType)
        {
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandTimeout = commandTimeout;
                sqlCommand.CommandType = commandType;

                sqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetDatasetBySQL(string Command)
        {
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandTimeout = commandTimeout;

                sqlConnection.Open();

                SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public SqlDataReader GetReaderBySQL(string strSQL)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand myCommand = new SqlCommand(strSQL, sqlConnection);
                return myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public SqlDataReader GetReaderByCmd(string Command)
        {
            SqlDataReader objSqlDataReader = null;
            try
            {
                sqlCommand.CommandText = Command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = commandTimeout;

                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;

                objSqlDataReader = sqlCommand.ExecuteReader();
                return objSqlDataReader;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }

        }

        public void AddParameterToSQLCommand(string ParameterName, SqlDbType ParameterType)
        {
            try
            {
                sqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType));
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddParameterToSQLCommand(string ParameterName, SqlDbType ParameterType, int ParameterSize)
        {
            try
            {
                sqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType, ParameterSize));
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddSetParameterToSQLCommand(string ParameterName, SqlDbType ParameterType, object Value)
        {
            try
            {
                sqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType));
                sqlCommand.Parameters[ParameterName].Value = Value;
                sqlCommand.Parameters[ParameterName].Direction = ParameterDirection.Input;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddOutputParameterToSQLCommand(string ParameterName, SqlDbType ParameterType)
        {
            try
            {
                sqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType));
                sqlCommand.Parameters[ParameterName].Direction = ParameterDirection.Output;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetSQLCommandParameterValue(string ParameterName, object Value)
        {
            try
            {
                sqlCommand.Parameters[ParameterName].Value = Value;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIndentityValue(int index)
        {
            return Convert.ToInt32(sqlCommand.Parameters[index].SqlValue.ToString());
        }

        public SqlDataAdapter GetDataAdapterForTable(string query)
        {
            SqlDataAdapter adpt = new SqlDataAdapter();
            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandTimeout = commandTimeout;

                sqlConnection.Open();

                adpt = new SqlDataAdapter(sqlCommand);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return adpt;
        }
    }
}