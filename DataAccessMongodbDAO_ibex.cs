using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace RealTimeDataCapture2.dao {

    /// <summary>
    /// Data Access Obejct for MongoDB Server.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class DataAccessMongodbDAO_ibex  : IDataAccessDAO {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Inserta Un Tick en la Base de datos MongoDB para mercado Ibex-35
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> insertTick(Tick _tick) { 

            Boolean result = false;

            try {
                Tick_ibex tick = (Tick_ibex)_tick;

                //El driver internamente gestiona un Pool de Conexiones
                MongoUrl mongoUrl = MongoUrl.Create(Constants.MONGO_HOST);
                IMongoClient mdbclient = new MongoClient(mongoUrl);

                IMongoDatabase db = mdbclient.GetDatabase(mongoUrl.DatabaseName);
                IMongoCollection<BsonDocument> mongocollection_ticks = db.GetCollection<BsonDocument>(Constants.MONGO_COLLECTION_IBEX_TICKS);

                BsonDocument document = new BsonDocument();
                document.Add("ID", new BsonString(generateID()));
                document.Add("date", new BsonInt32(tick.date));
                document.Add("time", new BsonInt32(tick.time));
                document.Add("ope", new BsonString(tick.operation));
                document.Add("trade_price", new BsonInt32(tick.price));
                document.Add("trade_vol", new BsonInt32(tick.volume));
                document.Add("buy_price", new BsonInt32(tick.buy));
                document.Add("sell_price", new BsonInt32(tick.sell));
                

                await mongocollection_ticks.InsertOneAsync(document);

                result = true;
            }
            catch (Exception ex) {
                log.Error("ERROR INSERTING TICKS DATA IN MONGODB-IBEX35. " + ex.Message);
                throw ex;
            }

            return result;
        }//fin insertTick



        /// <summary>
        /// Inserta o actualiza el registro de precios de otro mercado con precios en decimal.
        /// </summary>
        /// <returns>true si insert ok, false i algun fallo</returns>
        public async Task<Boolean> updatePrices(Dictionary<String, Int32> datetimemili, Prices _prices) {

            return true;
        }//fin updatePrices



        private string generateID() {

            DateTime currentDate = DateTime.Now;
            long ticks = currentDate.Ticks;
            log.Debug("ID generated:     " + ticks);

            string result =  Convert.ToString(ticks);
            log.Debug("ID generated str: " + ticks);

            return result;
        }//fin generateID
    }//fin clase
}//fin
