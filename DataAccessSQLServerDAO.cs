using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using MongoDB.Bson;
namespace RealTimeDataCapture2.dao {

    /// <summary>
    /// Data Access Obejct for SQL Server.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class DataAccessSQLServerDAO {

        /// <summary>
        /// Select the list of Symbols avalaibles for work with.
        /// </summary>
        /// <returns></returns>
        //public Queue<Symbol> selectSymbolList()  {

        //    Queue<Symbol> result = new Queue<Symbol>();
            
        //    SqlDataReader reader = null;


        //    using (SqlConnection conn = new SqlConnection(""))// Constants.CONN_SQLSERVER_STR)) {      
        //        try {                     
        //            conn.Open();

        //            //PREPARE SQL
        //            SqlCommand cmd = new SqlCommand(Constants.QUERY_SELECT_SYMBOLS, conn);
               
        //            //EXEC QUERY
        //            cmd.Prepare();
        //            reader = cmd.ExecuteReader();

        //            while (reader.Read()) { 
        //                Symbol s = new Symbol();

        //                s.code = reader.GetString(0);
        //                s.operate = reader.GetString(1);
        //                s.active = reader.GetString(2);
        //                s.description = reader.GetString(3);
                    
        //                result.Enqueue(s);
        //            }                              
        //        }
        //        catch (Exception ex) { 
        //            Console.WriteLine("BAD EXECUTION QUERYING SYMBOLS MASTER TABLE. " + ex.Message);
        //            throw ex;
        //        }
        //        finally {
        //            try { 
        //                if (null != reader) { 
        //                    reader.Close();
        //                }
        //            }catch { }
        //        }
        //    }//using conn

        //    return result;

        //}//fin selectSymbolList
        

    }//fin clase
}//fin
