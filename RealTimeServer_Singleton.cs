namespace RealTimeDataCapture2.common {


    /// <summary>
    /// This singleton class, contains the reference to the Real Time Server instance.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class RealTimeServer_Singleton {

        private static volatile RealTimeServer_Singleton instance = null;
        private static readonly object padlock = new object();

        /**
         * Instance of Real Time Server
         */
        private VCRealTimeLib.RealTime realtime;



        /**
        * Instance, Sigleton implementation, return an unique instance of this class.
        */
        public static RealTimeServer_Singleton Instance {
            
             get {
                if (null == instance) {
                    lock(padlock) {
                        if (null == instance) { 
                            instance = new RealTimeServer_Singleton();
                        }
                    }
               }
 
               return instance;
            }//get

        }//fin Instance



        /**
         * Constructor
         */
        private RealTimeServer_Singleton() { 
        
        }//fin constructor



        public VCRealTimeLib.RealTime getRealTimeInstance() {

            if (null != realtime) { 
                return realtime;
            }
           
            return null;
        
        }//fin getRealTimeInstance



        public void setRealTimeServerInstance(VCRealTimeLib.RealTime _realtime) {
        
            realtime = _realtime;
        
        }//fin 

    }//fin clase

}//fin 
