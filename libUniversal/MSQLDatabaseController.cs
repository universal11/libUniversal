using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace libUniversal
{
    public class MSQLDatabaseController
    {
        public SqlConnection connection = null;
        string connectionString = null;


        public void init(string connectionName)
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public bool isConnected()
        {
            return ((this.connection.State == ConnectionState.Open) ? true : false);
        }


        public static MSQLDatabaseController create(string connectionName)
        {
            MSQLDatabaseController databaseController = new MSQLDatabaseController();
            databaseController.init(connectionName);
            databaseController.connect();
            return databaseController;
        }

        public static MSQLDatabaseController createWithConnect(string connectionName)
        {
            MSQLDatabaseController databaseController = new MSQLDatabaseController();
            databaseController.init(connectionName);
            databaseController.connect();
            return databaseController;
        }

        public void connect()
        {

            this.connection = new SqlConnection(this.connectionString);
            try
            {
                this.connection.Open();
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.ToString());
            }

        }

        public SqlDataReader query(SqlCommand command)
        {
            SqlDataReader result = null;
            try
            {
                result = command.ExecuteReader();

            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.ToString());
            }
            return result;
        }

        public int update(SqlCommand command)
        {
            int result = 0;
            try
            {
                result = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.ToString());
            }
            return result;
        }

        ///<summary>
        /// Performs insert and returns number of rows.
        ///</summary>
        public int insert(SqlCommand command)
        {
            int result = 0;
            try
            {
                result = (Int32)command.ExecuteScalar();
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.ToString());
            }
            return result;
        }


        public static DateTime? parseDateTime(SqlDataReader reader, string fieldName)
        {
            if (DBNull.Value.Equals(reader[fieldName]))
            {
                return (DateTime?)null;
            }
            return (DateTime?)reader[fieldName];
        }

        public static int parseInteger(SqlDataReader reader, string fieldName)
        {
            if (DBNull.Value.Equals(reader[fieldName]))
            {
                return 0;
            }
            return Convert.ToInt32(reader[fieldName]);
        }

        public static bool parseBoolean(SqlDataReader reader, string fieldName)
        {
            if (DBNull.Value.Equals(reader[fieldName]))
            {
                return false;
            }
            return Convert.ToBoolean(reader[fieldName]);
        }

        public static string parseString(SqlDataReader reader, string fieldName)
        {
            if (DBNull.Value.Equals(reader[fieldName]))
            {
                return "";
            }
            return Convert.ToString(reader[fieldName]);
        }

        public static object parseParameter(object parameter)
        {
            return ((parameter == null) ? DBNull.Value : parameter);
        }

        public void close()
        {
            try
            {
                this.connection.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }



    }
}
