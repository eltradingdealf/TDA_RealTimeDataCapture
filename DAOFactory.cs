using RealTimeDataCapture2.util;


namespace RealTimeDataCapture2.dao {

    /// <summary>
    /// Factory creator of DAO Objects.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    /// <update>
    /// Sept 2020
    /// </update>
    class DAOFactory {

        private static volatile DAOFactory instance = null;
        private static readonly object padlock = new object();
        private static readonly object pdlock = new object();

        /**
         * Instance of Real Time Server
         */
        private IDataAccessDAO daoEuroFX = null;
        private IDataAccessDAO daoSP500 = null;
        private IDataAccessDAO daoDAX = null;
        private IDataAccessDAO daoIBEX = null;
        private IDataAccessDAO daoBUND = null;
        private IDataAccessDAO daoSTOXX50 = null;
        private IMarketsDAO marketDao = null;


        /**
        * Instance, Sigleton implementation, returns an unique instance of this class.
        */
        public static DAOFactory Instance {
            
             get {
                if (null == instance) {
                    lock(padlock) {
                        if (null == instance) { 
                            instance = new DAOFactory();
                        }
                    }
               }
 
               return instance;
            }//get

        }//fin Instance



        /**
         * Constructor
         */
        private DAOFactory() {         
        }//fin constructor



        /**
         * Returns the Dao object.
         */
        public IDataAccessDAO getDAO(string _market) {

            if (Constants.SIMBOLO_IBEX35.Equals(_market)) {
                if (null == daoIBEX) {
                    lock (pdlock) {
                        if (null == daoIBEX) {
                            daoIBEX = new DataAccessMariadbDAO_ibex();
                        }                        
                    }//lock
                }//if

                return daoIBEX;
            }
            else if (Constants.SIMBOLO_CME_MINISP.Equals(_market)) {
                if (null == daoSP500) {
                    lock (pdlock) {
                        if (null == daoSP500) {
                            daoSP500 = new DataAccessMariadbDAO_sp500();
                        }
                    }//lock
                }//if

                return daoSP500;
            }
            else if (Constants.SIMBOLO_CME_EURO_FX.Equals(_market)) {
                if (null == daoEuroFX) {
                    lock (pdlock) {
                        if (null == daoEuroFX) {
                            daoEuroFX = new DataAccessMariadbDAO_eurofx();
                        }
                    }//lock
                }//if

                return daoEuroFX;
            }
            else if (Constants.SIMBOLO_DAX.Equals(_market)) {
                if (null == daoDAX) {
                    lock (pdlock) {
                        if (null == daoDAX) {
                            daoDAX = new DataAccessMariadbDAO_dax();
                        }
                    }//lock
                }//if

                return daoDAX;
            }
            else if (Constants.SIMBOLO_BUND.Equals(_market)) {
                if (null == daoBUND) {
                    lock (pdlock) {
                        if (null == daoBUND) {
                            daoBUND = new DataAccessMariadbDAO_bund();
                        }
                    }//lock
                }//if

                return daoBUND;
            }
            else if (Constants.SIMBOLO_STOXX50.Equals(_market)) {
                if (null == daoSTOXX50) {
                    lock (pdlock) {
                        if (null == daoSTOXX50) {
                            daoSTOXX50 = new DataAccessMariadbDAO_stoxx50();
                        }
                    }//lock
                }//if

                return daoSTOXX50;
            }
            else return null;
        }//fin getDAO


        public IMarketsDAO getXMarketsDAO() {

            if (null == marketDao) {
                lock (pdlock) {
                    if (null == marketDao) {
                        marketDao = new DataAccessMariadbDAO_x_markets();
                    }
                }//if  
            }//lock

            return marketDao;
        }

    }//fin clase
}//fin
