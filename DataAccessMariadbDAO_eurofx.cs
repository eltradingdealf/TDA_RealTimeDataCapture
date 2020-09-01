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
    /// Marzo 2019
    /// </date>
    /// <update>
    /// Sept 2020
    /// </update>
    class DataAccessMariadbDAO_eurofx : IDataAccessDAO {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void deleteTicks() {
            log.Debug("Deleting ticks EUROFX in MariaDB Init");

            try {
                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_DELETE_TICKS_ALL_EURO_FX, conn);
                    log.Info("Conn opened");

                    cmd.Prepare();
                    log.Info("cmd prepared");

                    cmd.ExecuteNonQuery();
                    log.Info("query executed");
                }
            }
            catch (Exception ex) {
                log.Debug("ERROR DELETING TICKS DATA IN MARIADB-EUROFX. " + ex.Message);
                log.Error("ERROR DELETING TICKS DATA IN MARIADB-EUROFX. " + ex.Message);
            }

            log.Debug("Deleting tick EUROFX in MariaDB Ends");
        }//fin deleteTicks



        /// <summary>
        /// Inserta Un Tick en la Base de datos MariaDB para mercado EURO-FX
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> insertTick(Tick _tick) {

            log.Debug("Inserting tick EUROFX in MariaDB Init");

            try {
                Tick_dec tick = (Tick_dec)_tick;

                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_INSERT_TICK_EURO_FX, conn);
                    log.Info("Conn opened");

                    cmd.Parameters.AddWithValue("@tickdate", tick.date);
                    cmd.Parameters.AddWithValue("@ticktime", tick.time);
                    cmd.Parameters.AddWithValue("@tickmili", tick.milisecond);
                    cmd.Parameters.AddWithValue("@ope", tick.operation);
                    cmd.Parameters.AddWithValue("@tprice", tick.price);
                    cmd.Parameters.AddWithValue("@tvol", tick.volume);
                    cmd.Parameters.AddWithValue("@bprice", tick.buy);
                    cmd.Parameters.AddWithValue("@sprice", tick.sell);
                    log.Info("Params ready");

                    cmd.Prepare();
                    log.Info("cmd prepared");

                    cmd.ExecuteNonQuery();
                    log.Info("query executed");

                }
            }
            catch (Exception ex) {
                log.Debug("ERROR INSERTING TICKS DATA IN MARIADB-EUROFX. " + ex.Message);
                log.Error("ERROR INSERTING TICKS DATA IN MARIADB-EUROFX. " + ex.Message);
                return false;
            }

            log.Debug("Inserting tick EUROFX in MariaDB Ends");
            return true;
        }//fin insertTick



        /// <summary>
        /// Inserta o actualiza el registro de precios del Euro FX.
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> updatePrices(Dictionary<String, Int32> datetimemili, Prices _prices) {

            log.Debug("Updating Current EUROFX prices Init");

            try {
                using (MySqlConnection conn = new MySqlConnection(Constants.MARIA_HOST)) {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(Constants.SQL_INSERT_UPDATE_PRICES_EURO_FX, conn);
                    log.Info("Conn opened");

                    Prices_dec prices = (Prices_dec)_prices;

                    cmd.Parameters.AddWithValue("@tickdate", datetimemili["thedate"]);
                    cmd.Parameters.AddWithValue("@ticktime", datetimemili["thetime"]);
                    cmd.Parameters.AddWithValue("@tickmili", datetimemili["milis"]);
                    cmd.Parameters.AddWithValue("@bprice", prices.buyprice);
                    cmd.Parameters.AddWithValue("@sprice", prices.sellprice);
                    log.Info("Params ready");

                    cmd.Prepare();
                    log.Info("cmd prepared");
                    cmd.ExecuteNonQuery();
                    log.Info("query executed");
                }
            }
            catch (Exception ex) {
                log.Debug("ERROR UPDATING FIBEX PRICES IN MARIADB-EUROFX. " + ex.Message);
                log.Error("ERROR UPDATING FIBEX PRICES IN MARIADB-EUROFX. " + ex.Message);
                return false;
            }

            log.Debug("Updating Current EUROFX prices Ends");
            return true;
        }//fin updatePrices
    }//fin class
}//fin