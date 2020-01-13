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
    /// Marzo 2019
    /// </update>
    class DAOFactory {

        private static volatile DAOFactory instance = null;
        private static readonly object padlock = new object();
        private static readonly object pdlock = new object();

        /**
         * Instance of Real Time Server
         */
        private IDataAccessDAO dao = null;      
        

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

            if (null == dao) { 
                lock (pdlock) {
                    if (null == dao) {
                        if (Constants.SIMBOLO_IBEX35_CONTINUOS.Equals(_market)) {
                            dao = new DataAccessMariadbDAO_ibex();
                        }
                        else if (Constants.SIMBOLO_CME_MINISP_500_JUNE2019.Equals(_market)) {
                            dao = new DataAccessMariadbDAO_sp500();
                        }
                        else if (Constants.SIMBOLO_CME_EURO_FX_JUNE2019.Equals(_market)) {
                            dao = new DataAccessMariadbDAO_eurofx();
                        }
                        else if (Constants.SIMBOLO_CME_MINI_NASDAQ_JUNE2019.Equals(_market)) {
                            dao = new DataAccessMariadbDAO_nasdaq();
                        }
                    }
                }//if  
            }//lock
           
            return dao;
        
        }//fin getDAO

    }//fin clase
}//fin
