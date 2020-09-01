
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace RealTimeDataCapture2.dao {

    /// <summary>
    /// Data Access Object for MariaDB Server.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Julio 2017
    /// </date>
    /// <update>
    /// Sept 2020
    /// </update>
    class DataAccessMariadbDAO_ibex : IDataAccessDAO {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void deleteTicks() {
            log.Debug("Deleting ticks IBEX in MariaDB Init");

            try {
                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_DELETE_TICKS_ALL_IBEX, conn);
                    log.Info("Conn opened");

                    cmd.Prepare();
                    log.Info("cmd prepared");

                    cmd.ExecuteNonQuery();
                    log.Info("query executed");
                }
            }
            catch (Exception ex) {
                log.Debug("ERROR DELETING TICKS DATA IN MARIADB-IBEX. " + ex.Message);
                log.Error("ERROR DELETING TICKS DATA IN MARIADB-IBEX. " + ex.Message);
            }

            log.Debug("Deleting tick IBEX in MariaDB Ends");
        }//fin deleteTicks



        /// <summary>
        /// Inserta Un Tick en la Base de datos MariaDB para mercado Ibex-35
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> insertTick(Tick _tick) {
                        
            log.Debug("Inserting tick in MariaDB");

            try {
                Tick_ibex tick = (Tick_ibex)_tick;

                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {
                    conn.Open();
                    log.Info("Params 11");
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_INSERT_TICK, conn);
                    log.Info("Params 12");
                    //cmd.Connection = conn;
                    log.Info("Params 13");

                        
                        

                    cmd.Parameters.AddWithValue("@tickdate", tick.date);
                    cmd.Parameters.AddWithValue("@ticktime", tick.time);
                    cmd.Parameters.AddWithValue("@tickmili", tick.milisecond);
                    cmd.Parameters.AddWithValue("@ope", tick.operation);
                    cmd.Parameters.AddWithValue("@tprice", tick.price);
                    cmd.Parameters.AddWithValue("@tvol", tick.volume);
                    cmd.Parameters.AddWithValue("@bprice", tick.buy);
                    cmd.Parameters.AddWithValue("@sprice", tick.sell);

                    log.Info("Params 14");
                    cmd.Prepare();
                    log.Info("Params 15");

                    cmd.ExecuteNonQuery();
                    log.Info("Params 16");
                }
            }
            catch (Exception ex) {
                log.Debug("ERROR INSERTING TICKS DATA IN MARIADB-IBEX35. " + ex.Message);
                log.Error("ERROR INSERTING TICKS DATA IN MARIADB-IBEX35. " + ex.Message);
                return false;
            }

            return true;
        }//fin insertTick



        /// <summary>
        /// Inserta o actualiza el registro de precios del ibex.
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> updatePrices(Dictionary<String, Int32> datetimemili, Prices _prices) {

            log.Debug("Updating Current Ibex prices Init");

            try {
                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_INSERT_UPDATE_PRICES_IBEX, conn);

                    Prices_int prices = (Prices_int)_prices;

                    cmd.Parameters.AddWithValue("@tickdate", datetimemili["thedate"]);
                    cmd.Parameters.AddWithValue("@ticktime", datetimemili["thetime"]);
                    cmd.Parameters.AddWithValue("@tickmili", datetimemili["milis"]);
                    cmd.Parameters.AddWithValue("@bprice", prices.buyprice);
                    cmd.Parameters.AddWithValue("@sprice", prices.sellprice);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                log.Debug("ERROR UPDATING FIBEX PRICES IN MARIADB-IBEX35. " + ex.Message);
                log.Error("ERROR UPDATING FIBEX PRICES IN MARIADB-IBEX35. " + ex.Message);
                return false;
            }

            log.Debug("Updating Current Ibex prices Ends");
            return true;
        }//fin updatePrices
    }//fin clase
}//fin
