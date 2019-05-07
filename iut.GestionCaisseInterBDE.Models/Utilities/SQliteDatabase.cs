using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace iut.GestionCaisseInterBDE.Models.Utilities
{
    public class SQLiteDatabase : IDatabase
    {

        private string connString { get;  set; }

        /// <summary>
        /// Creates an instance with the given connection string
        /// </summary>
        /// <param name="connString">The connection string</param>
        public SQLiteDatabase(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Executes the gives SQL query
        /// </summary>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="Parameters">Query parameters and their values</param>
        /// <returns>Number of rows affected</returns>
        public int ExecuteCommand(string query, Dictionary<string, object> Parameters = null)
        {
            using (var conn = new SQLiteConnection(connString))
            {
                conn.ConnectionString = connString;
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;

                    //Add Parameters
                    if (Parameters != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in Parameters)
                        {
                            DbParameter parameter = cmd.CreateParameter();
                            parameter.ParameterName = kvp.Key;
                            parameter.Value = kvp.Value;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    //Execute Query
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Query an SQL database
        /// </summary>
        /// <param name="query">Select query that returns a data table</param>
        /// <param name="Parameters">Query parameters with their values</param>
        /// <returns>Query results as a DataTable</returns>
        public DataTable Select(string query, Dictionary<string, object> Parameters = null)
        {
            DataTable dt = new DataTable();

            //Create Query
            using (var conn = new SQLiteConnection(connString))
            {
                conn.ConnectionString = connString;
                using (DbCommand cmd = conn.CreateCommand())
                using (DbDataAdapter da = new SQLiteDataAdapter())
                {
                    cmd.CommandText = query;
                    da.SelectCommand = cmd;

                    //Add Parameters
                    if (Parameters != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in Parameters)
                        {
                            DbParameter parameter = cmd.CreateParameter();
                            parameter.ParameterName = kvp.Key;
                            parameter.Value = kvp.Value;
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    //Execute Query
                    conn.Open();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

    }
}
