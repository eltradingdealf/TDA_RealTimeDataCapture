using RealTimeDataCapture2.common;
using RealTimeDataCapture2.dao;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using System;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Concurrent;



namespace RealTimeDataCapture2.workers {


    /// <summary>
    ///  Implements an execution Thread with the
    ///  purpose of store the generated Ticks into
    ///  the Database.
    ///  Ibex-market
    ///  
    ///  This thread never ends by itself.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class DatabaseStore_Process_ibex : DatabaseStore_Process_Interface {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private Timer myTimer;
        private MainForm refForm;
        private readonly object pdlock = new object();
        private Boolean working = false;


        /**
        * Constructor
        */
        public DatabaseStore_Process_ibex() {
        }



        /**
         * Init method of the Timer Thread
         */
        public void DoWork(Object source, ElapsedEventArgs e) {

            if (true == working) {
                return;
            }

            lock (pdlock) {

                log.Debug("Executing DatabaseStore_Process_ibex");
                execStore();

                //execStore_TEST();
            }//lock

        }//fin DoWork



        /// <summary>Recorre la lista de Ticks y los almacena en la BBDD</summary>
        /// <returns></returns>
        private void execStore() {

            //If List has no data, ends method
            log.Debug("TicksListSingleton.Instance.getListFiFoTicks_ibex  Count=" + TicksListSingleton.Instance.getListFiFoTicks_ibex().Count);
            if (0 == TicksListSingleton.Instance.getListFiFoTicks_ibex().Count) {
                return;
            }

            log.Debug("Gonna insert ticks: " + TicksListSingleton.Instance.getListFiFoTicks_ibex().Count);

            try {
                working = true;

                int insertsOKnum = 0;
                int insertsKOnum = 0;

                Tick tickBean = null;
                IDataAccessDAO dao = DAOFactory.Instance.getDAO(Constants.DAO_TYPE_IBEX_MARIADB);

                //Data Store
                while (0 != TicksListSingleton.Instance.getListFiFoTicks_ibex().Count) {

                    Boolean hasTick = TicksListSingleton.Instance.getListFiFoTicks_ibex().TryDequeue(out tickBean);
                    log.Debug("hasTick= " + hasTick);
                    if(false == hasTick) {
                        continue;
                    }

                    Tick_ibex tick_ibex = (Tick_ibex)tickBean;
                    tick_ibex.milisecond = getMilis();

                    //INSERT
                    try {
                        Task<Boolean> task = dao.insertTick(tick_ibex);
                        task.Wait();
                        if (true == task.Result) {
                            insertsOKnum++;
                        }
                        else {
                            insertsKOnum++;
                        }

                        log.Debug("Ibex Tick Inserted");
                    }
                    catch(Exception ex) {
                        log.Error("Error inserting Tick", ex);
                        insertsKOnum++;
                    }

                    //Update Form field value
                    refForm.writeFifoNumberField();
                }

                printMessages(insertsOKnum, insertsKOnum);
            }
            finally {
                working = false;
                log.Debug("Ticks array inserted");
            }
        }//fin execStore



        private int getMilis() {

            int milis = 0;
            DateTime dt = DateTime.Now;
            
            try {
                milis = int.Parse(dt.ToString("FFF"));
            }
            catch (Exception ex) {
                log.Error("ERROR getting miliseconds. " + ex.Message);
            }

            return milis;
        }//fin getMilis



        /// TEST Only
        /// <summary>Este metodo es para pruebas de insercion de datos en MongoDB</summary>
        /// <returns></returns>
        private void execStore_TEST() {

            try {
                working = true;

                int insertsOKnum = 0;
                int insertsKOnum = 0;

                Tick_ibex tickBean = null;
                DateTime dt;
                //IDataAccessDAO_ibex dao = DAOFactory.Instance.getDAO(Constants.DAO_TYPE_IBEX_MONGODB);
                IDataAccessDAO dao = DAOFactory.Instance.getDAO(Constants.DAO_TYPE_IBEX_MARIADB);

                int b = 10600;
                int s = 10601;
                int p = 10600;

                //Data Store
                int i = 1;
                int itmp = 1;
                int vol = 0;
                while (10000 > i) {

                    if (4 == itmp) {
                        System.Threading.Thread.Sleep(1000);
                        itmp = 1;
                        continue;
                    }
                    dt = DateTime.Now;

                    tickBean = new Tick_ibex();
                    tickBean.symbol = Constants.SIMBOLO_IBEX35;
                    // @see https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
                    tickBean.date = int.Parse(dt.ToString("yyyyMMdd")); 
                    tickBean.time = int.Parse(dt.ToString("HHmmss"));
                    tickBean.milisecond = 0;
                    try {
                        tickBean.milisecond = int.Parse(dt.ToString("FFF"));
                    }
                    catch (Exception ex) {
                        log.Error("ERROR getting miliseconds. " + ex.Message);
                    }

                    Random rndSigno = new Random();
                    int signo = rndSigno.Next(1, 5);

                    Random rndPrice = new Random();
                    int price = rndPrice.Next(1, 2);

                    if (2 >= signo) {
                        tickBean.operation = Constants.OPERATION_SELL;
                        b -= price;
                        s -= price;
                        p -= price;
                    }
                    else {
                        tickBean.operation = Constants.OPERATION_BUY;
                        b += price;
                        s += price;
                        p += price;
                    }

                    Random rndVol = new Random();
                    vol = rndVol.Next(1, 35);
                    
                    tickBean.buy = Convert.ToInt32(b);
                    tickBean.sell = Convert.ToInt32(s);
                    tickBean.price = Convert.ToInt32(p);
                    tickBean.volume = vol;

                    //INSERT
                    log.Info("Gonna insert: " + tickBean.ToString());
                    log.Info("gonna save tick");
                    Task<Boolean> task = dao.insertTick(tickBean);
                    task.Wait();
                    log.Debug("Inserted tick:  status=" + task.Status + ", result=" + task.Result);

                    if (true == task.Result) {
                        insertsOKnum++;
                    }
                    else {
                        insertsKOnum++;
                    }
                    log.Debug("insertsOKnum=" + insertsOKnum + ", insertsKOnum=" + insertsKOnum);

                    //Update Form field value
                    refForm.writeFifoNumberField();
                    i++;
                    itmp++;
                }

                printMessages(insertsOKnum, insertsKOnum);

            }
            finally {
                working = false;
            }

        }//fin execStore_TEST



        private void printMessages(int insertsOKnum, int insertsKOnum) {

            StringBuilder result = new StringBuilder();

            result.Append(insertsOKnum);
            result.Append(" inserts OK, y ");
            result.Append(insertsKOnum);
            result.Append(" inserts KO");

            refForm.writeDBQuerysResultField(result.ToString());
        }//fin printMessages



        /**
         * @param _t: The thread itself, provided by the form
         * @param _f: The Form reference.
         */
        public void place(ref System.Timers.Timer _t, MainForm _f) {

            myTimer = _t;
            refForm = _f;

        }//fin place

    }//fin clase
}//fin
