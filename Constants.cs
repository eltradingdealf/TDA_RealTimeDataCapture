using System;

namespace RealTimeDataCapture2.util {
    
    /// <summary>
    ///  Constants class utility.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class Constants  {

        public const string MONGO_HOST = "mongodb://127.0.0.1:27017/DB-IBEX35";
        public const string MARIA_HOST = "Server=127.0.0.1;Uid=root;Pwd=alfredo;Database=trading_db;IgnorePrepare=true;ConnectionLifeTime=300;";


        public const string SIMBOLO_IBEX35 = "IBEX";
        public const string SIMBOLO_CME_EURO_FX = "EUROFX";
        public const string SIMBOLO_CME_MINISP = "SP500";
        public const string SIMBOLO_DAX = "DAX";


        public const int TICKS_BUFFER_SIZE = 10; 
        public const int TIMER_FREQUENCY = 1000;
        public const int MAX_RETRYS_REALTIMESERVER_CONN = 10; 
        public const int FIVE_SECONDS = 5000;
        public const int ONE_SECOND = 1000;
        public const int HALF_A_SECOND = 500;

        public static readonly System.TimeSpan INIT_SESSION_TIMESTAMP    = new  System.TimeSpan(8, 0, 0);
        public static readonly System.TimeSpan END_SESSION_TIMESTAMP     = new  System.TimeSpan(22, 0, 0);
        public const int INIT_SESSION_TIME = 80000;
        public const int END_SESSION_TIME_FIVE_IN_THE_AFTERNOON = 170000;
        public const int END_SESSION_TIME = 220000;

        public const string DAO_TYPE_IBEX_MONGODB = "IBEX-MONGO";
        public const string DAO_TYPE_IBEX_MARIADB = "IBEX-MARIADB";
        public const string DAO_TYPE_SP500_MARIADB = "SP500-MARIADB";
        public const string DAO_TYPE_EURO_FX_MARIADB = "EUROFX-MARIADB";

        public const string MENSAJE_NOT_CONNECTED = "NOT CONNECTED";
        public const string MENSAJE_CONNECTED = "CONNECTED";
        public const string MENSAJE_CONNECTING = "CONNECTING";
        public const string MENSAJE_RETRING_CONNECTED = "RETRYING THE CONNECTION";
        public const string MENSAJE_ERROR_DISCONNECTING = "ERROR WHILE DISCONNECTING";

        public const string MENSAJE_DDBB_NOT_CONNECTED = "NOT CONNECTED DDBB";
        public const string MENSAJE_DDBB_CONNECTED = "CONNECTED DDBB";
        public const string MENSAJE_DDBB_ERROR_CONNECTING = "ERROR CONNECTING DDBB";

        public const string MENSAJE_QRY_INSRT_EXECUTED = "INSERTS EXECUTED ";
        public const string MENSAJE_QRY_INSRT_EXECUTED_OK_1 = "OK= ";
        public const string MENSAJE_QRY_INSRT_EXECUTED_OK_2 = "KO= ";
        public const string MENSAJE_QRY_INSRT_EXECUTED_KO = "INSERT DIDN'T WORK";        

        public const string OPERATION_BUY      = "BUY";
        public const string OPERATION_SELL     = "SELL";
        public const string OPERATION_PRICE    = "PRICE";


        public const string MONGO_COLLECTION_IBEX_TICKS = "TICKS";

        public const String SQL_INSERT_TICK = "INSERT INTO trading_db.ticks_fibex " +
                                                 "(tickdate, ticktime, tickmili, ope, trade_price, trade_vol, buy_price, sell_price) " +
                                              "VALUES "+
                                                 "(@tickdate, @ticktime, @tickmili, @ope, @tprice, @tvol, @bprice, @sprice)";


        public const String SQL_INSERT_UPDATE_PRICES_IBEX = "INSERT INTO prices_fibex " +
                                                            "VALUES(@tickdate, @ticktime, @tickmili, @bprice, @sprice) " +
                                                            "ON DUPLICATE KEY UPDATE " +
                                                            "ticktime=@ticktime, tickmili=@tickmili, buy_price=@bprice, sell_price=@sprice";


        public const String SQL_INSERT_TICK_SP500 = "INSERT INTO trading_db.ticks_fsp500 " +
                                                        "(tickdate, ticktime, tickmili, ope, trade_price, trade_vol, buy_price, sell_price) " +
                                                    "VALUES " +
                                                        "(@tickdate, @ticktime, @tickmili, @ope, @tprice, @tvol, @bprice, @sprice)";

        public const String SQL_INSERT_UPDATE_PRICES_SP500 = "INSERT INTO prices_fsp500 " +
                                                             "VALUES(@tickdate, @ticktime, @tickmili, @bprice, @sprice) " +
                                                             "ON DUPLICATE KEY UPDATE " +
                                                             "ticktime=@ticktime, tickmili=@tickmili, buy_price=@bprice, sell_price=@sprice";

        public const String SQL_INSERT_TICK_EURO_FX = "INSERT INTO trading_db.ticks_feuro " +
                                                        "(tickdate, ticktime, tickmili, ope, trade_price, trade_vol, buy_price, sell_price) " +
                                                     "VALUES " +
                                                        "(@tickdate, @ticktime, @tickmili, @ope, @tprice, @tvol, @bprice, @sprice)";


        public const String SQL_INSERT_UPDATE_PRICES_EURO_FX =  "INSERT INTO prices_feuro " +
                                                                "VALUES(@tickdate, @ticktime, @tickmili, @bprice, @sprice) " +
                                                                "ON DUPLICATE KEY UPDATE " +
                                                                "ticktime=@ticktime, tickmili=@tickmili, buy_price=@bprice, sell_price=@sprice";

        public const String SQL_INSERT_TICK_DAX = "INSERT INTO trading_db.ticks_dax " +
                                                  "(tickdate, ticktime, tickmili, ope, trade_price, trade_vol, buy_price, sell_price) " +
                                                  "VALUES " +
                                                  "(@tickdate, @ticktime, @tickmili, @ope, @tprice, @tvol, @bprice, @sprice)";


        public const String SQL_INSERT_UPDATE_PRICES_DAX = "INSERT INTO prices_dax " +
                                                                "VALUES(@tickdate, @ticktime, @tickmili, @bprice, @sprice) " +
                                                                "ON DUPLICATE KEY UPDATE " +
                                                                "ticktime=@ticktime, tickmili=@tickmili, buy_price=@bprice, sell_price=@sprice";

        public const String SQL_DELETE_TICKS_ALL_DAX = "DELETE FROM trading_db.ticks_dax";
        public const String SQL_DELETE_TICKS_ALL_EURO_FX = "DELETE FROM trading_db.ticks_feuro";
        public const String SQL_DELETE_TICKS_ALL_SP500 = "DELETE FROM trading_db.ticks_fsp500";
        public const String SQL_DELETE_TICKS_ALL_IBEX = "DELETE FROM trading_db.ticks_fibex";

        public const String SQL_QUERY_SESSION = "SELECT ID, SESSION_DATE, DESCRIPTION " +
                                                "FROM dbo.M_SESSION " +
                                                "WHERE ID = @sesionID"; 

        public const String SQL_INSERT_SESION =  "INSERT INTO dbo.M_SESSION " +
                                                 "(ID, SESSION_DATE) " +
                                                 "VALUES " +
                                                 "(@sesionID, @sesionDate)";

        public const String QUERY_SELECT_X_MARKETS_ACTIVE = "SELECT SYMBOL, ACTIVE, MARKET, SHORT_TEXT " +
                                                   "FROM trading_db.X_MARKETS " +
                                                   "WHERE ACTIVE = 1 " +
                                                   "ORDER BY MARKET ASC";

        public const String QUERY_SELECT_SYMBOLS = "SELECT CODE, OPERATE, ACTIVE, DESCRIPTION " +
                                                   "FROM dbo.M_SYMBOLS " + 
                                                   "WHERE     OPERATE = 'N' " +
                                                        " AND ACTIVE = 'S' " +
                                                   "ORDER BY CODE ASC";


    }//fin clase

}//fin
