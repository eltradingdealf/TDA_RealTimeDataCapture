using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using RealTimeDataCapture2.dao;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace RealTimeDataCapture2.dao {

    /// <summary>
    /// Data Access Object for MariaDB Server.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Sept 2020
    /// </date>
    class DataAccessMariadbDAO_x_markets: IMarketsDAO {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Queue<Market> selectMarketList() {

                Queue<Market> result = new Queue<Market>();
                MySqlDataReader reader = null;

                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {                
                    try {                     
                        conn.Open();

                        //PREPARE SQL
                        MySqlCommand cmd = new MySqlCommand(Constants.QUERY_SELECT_X_MARKETS_ACTIVE, conn);
                        log.Info("Conn opened");

                        //EXEC QUERY
                        cmd.Prepare();
                        reader = cmd.ExecuteReader();

                        while (reader.Read()) { 
                            Market s = new Market();

                            s.symbol = reader.GetString(0);
                            s.active = reader.GetInt16(1);
                            s.market = reader.GetString(2);                            
                            s.short_text = reader.GetString(3);

                            result.Enqueue(s);
                        }                              
                    }
                    catch (Exception ex) { 
                        Console.WriteLine("BAD EXECUTION QUERYING MARKETS MASTER TABLE. " + ex.Message);
                        throw ex;
                    }
                    finally {
                        try { 
                            if (null != reader) { 
                                reader.Close();
                            }
                        }catch { }
                    }
                }//using conn

                return result;

        }//fin selectMarketList
    }
}
