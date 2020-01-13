using System;
using System.Threading;

/* En el Explorador de Soluciones, seleccionamos “VCRealTimeLib” dentro de “References”. 
 * En la ventana de propiedades (pulsar Alt+Enter si no la tenéis visible), ponemos la 
 * opción “Incrustar tipos de interoperabilidad” a “False”. Esto último es muy importante o 
 * tendremos luego problemas de compilación.
 * Entre otros problemas no disparara los eventos.
*/
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.common;
using RealTimeDataCapture2.util;

namespace RealTimeDataCapture2.workers {


    /// <summary>
    ///  Implements an execution Thread with the
    ///  purpose of Connect to the Real Time Server.
    ///  After the Connection is realized, the thread finish.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class StartRTSworker  {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Thread myThread;
        private RealTimeDataCapture2.MainForm refForm;
        private Symbol symbol = null;


         /**
         * Called from button click event, executed in a new thread.
         */
        public void DoWork() {

            try { 
                Boolean connected = false;
                try  {
                    refForm.writeInStatusField(Constants.MENSAJE_CONNECTING);
                    connected = connRealTimeServer();
                }
                catch (Exception ex) {
                    log.Error("Error:", ex);
                    refForm.writeInStatusField(Constants.MENSAJE_RETRING_CONNECTED);
                    connected = reconnRealTimeServer();
                }

                if (true == connected) { 
                    refForm.ConnectToRTSdone();                    
                }

                refForm.writeInStatusField(Constants.MENSAJE_CONNECTED);
            }
            finally { 
                myThread.Abort();
            }
                    
        }//fin actionConnectRTS

        

        /**
         * Make the connection to the realtime sever
         */
        protected Boolean connRealTimeServer() {  
           
            try {                 
                VCRealTimeLib.RealTime realtime = new VCRealTimeLib.RealTime();

                RealTimeServer_Singleton.Instance.setRealTimeServerInstance(realtime);
                RealTimeServer_Singleton.Instance.getRealTimeInstance().RequestSymbolFeed(symbol.code, true);
                RealTimeServer_Singleton.Instance.getRealTimeInstance().RequestFieldEx(symbol.code, 1);
               
                RealTimeServer_Singleton.Instance.getRealTimeInstance().TicksBufferSize = 10;
                RealTimeServer_Singleton.Instance.getRealTimeInstance().TimerFrecuency = 1000;
            }
            catch (System.Runtime.InteropServices.COMException ex) { 
                log.Error("RealTimeServer Connection Failure: ", ex);
                throw new Exception("RealTimeServer Connection Failure", ex);
            }

            Console.WriteLine("RealTimeServer connected succesfully");
            return true;
                    
        }//fin startRealTime


       
        /**
         * This method retry the connection to real time server
         */
        protected Boolean reconnRealTimeServer() { 
                               
            int retry = 1;
            Boolean connected = false;

            while (retry <= Constants.MAX_RETRYS_REALTIMESERVER_CONN) { 
                //***WAIT x seconds
                System.Threading.Thread.Sleep(Constants.FIVE_SECONDS); 

                try { 
                    connected = connRealTimeServer();
                }
                catch (Exception ex) {
                    log.Error("Error Retrying to connect to RealTimeServer: " + retry, ex);
                    connected = false;
                }

                if (true == connected) { 
                    break;
                }

                retry++;
            }//while

            return connected;

        }//fin reconnRealTimeServer



        /**
         * @param _t: The thread itself, provided by the form
         * @param _f: The Form reference.
         */
        public void place(ref Thread _t, Symbol _symbol, MainForm _f) { 
        
            myThread = _t;
            refForm = _f;
            symbol = _symbol;
        
        }//fin place

    }//fin clase
}//fin
