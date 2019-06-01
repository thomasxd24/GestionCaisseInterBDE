using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace iut.GestionCaisseInterBDE.Persistance
{
    public interface ISQLDatabase
    {
        /// <summary>
        /// Executes the gives SQL query
        /// </summary>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="Parameters">Query parameters and their values</param>
        /// <returns>Number of rows affected</returns>
        int ExecuteCommand(string query, Dictionary<string, object> Parameters = null);


        /// <summary>
        /// Query an SQL database
        /// </summary>
        /// <param name="query">Select query that returns a data table</param>
        /// <param name="Parameters">Query parameters with their values</param>
        /// <returns>Query results as a DataTable</returns>
        DataTable Select(string query, Dictionary<string, object> Parameters = null);

    }
}
