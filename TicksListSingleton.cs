using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;

namespace RealTimeDataCapture2.common {


    /// <summary>
    /// This singlton class, contains Ticks stack exposed to different processes that feed and consume
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Sept 2020
    /// </date>
    sealed class TicksListSingleton  {

        private static volatile TicksListSingleton instance = null;
        private static readonly object padlock = new object();

        /*Queue containing Ticks data*/
        private ConcurrentQueue<Tick> listFiFoTicks_ibex;
        private ConcurrentQueue<Tick> listFiFoTicks_sp500;
        private ConcurrentQueue<Tick> listFiFoTicks_eurofx;
        private ConcurrentQueue<Tick> listFiFoTicks_dax;
        private ConcurrentQueue<Tick> listFiFoTicks_bund;



        /**
        * Instance, Sigleton implementation, return an unique instance of this class.
        */
        public static TicksListSingleton Instance {
            
             get {
                if (null == instance) {
                    lock(padlock) {
                        if (null == instance) { 
                            instance = new TicksListSingleton();
                        }
                    }
               }
 
               return instance;
            }//get
        }//fin Instance



        /**
         * Constructor
         */
        private TicksListSingleton() { 
                 
            listFiFoTicks_ibex = new ConcurrentQueue<Tick>();
            listFiFoTicks_sp500 = new ConcurrentQueue<Tick>();
            listFiFoTicks_eurofx = new ConcurrentQueue<Tick>();
            listFiFoTicks_dax = new ConcurrentQueue<Tick>();
            listFiFoTicks_bund = new ConcurrentQueue<Tick>();
        }//fin constructor



        public ConcurrentQueue<Tick> getListFiFoTicks_ibex() {

            return listFiFoTicks_ibex;
        }//fin getListFiFoTicks_ibex



        public ConcurrentQueue<Tick> getListFiFoTicks_sp500() {

            return listFiFoTicks_sp500;
        }//fin getListFiFoTicks_sp500



        public ConcurrentQueue<Tick> getListFiFoTicks_eurofx() {

            return listFiFoTicks_eurofx;
        }//fin getListFiFoTicks_eurofx



        public ConcurrentQueue<Tick> getListFiFoTicks_dax() {

            return listFiFoTicks_dax;
        }//fin getListFiFoTicks_dax


        public ConcurrentQueue<Tick> getListFiFoTicks_bund() {

            return listFiFoTicks_bund;
        }//fin getListFiFoTicks_bund


        public ConcurrentQueue<Tick> getListFiFoTicks(String _market) {
            
            if (Constants.SIMBOLO_CME_MINISP.Equals(_market)) {
                return listFiFoTicks_sp500;
            }
            else if (Constants.SIMBOLO_CME_EURO_FX.Equals(_market)) {
                return listFiFoTicks_eurofx;
            }
            else if (Constants.SIMBOLO_DAX.Equals(_market)) {
                return listFiFoTicks_dax;
            }
            else if (Constants.SIMBOLO_BUND.Equals(_market)) {
                return listFiFoTicks_bund;
            }
            else {
                return null;
            }
        }
    }//fin clase
}//fin
