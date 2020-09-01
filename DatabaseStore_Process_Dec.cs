using RealTimeDataCapture2.common;
using RealTimeDataCapture2.util;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.dao;
using System;
using System.Text;
using System.Timers;
using System.Threading.Tasks;


namespace RealTimeDataCapture2.workers {

    /// <summary>
    ///  Implements an execution Thread with the
    ///  purpose of store the generated Ticks into
    ///  the Database.
    ///  mini S&P 500, nasdaq and euro fx - markets
    ///  
    ///  This thread never ends by itself.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Sept 2020
    /// </date>
    class DatabaseStore_Process_Dec : DatabaseStore_Process_Interface {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Timer myTimer;
        private MainForm refForm;
        private readonly object pdlock = new object();
        private Boolean working = false;
        private Market market;

        /**
         * Constructor
         */
        private DatabaseStore_Process_Dec() {
        }


        /**
         * Constructor
         */
        public DatabaseStore_Process_Dec(Market _Market) {
            this.market = _Market;
        }


        /**
         * Init method of the Timer Thread
         */
        public void DoWork(Object source, ElapsedEventArgs e) {

            if (true == working) {
                return;
            }

            lock (pdlock) {

                log.Debug("Executing DatabaseStore_Process_DEC");
                execStore();
            }//lock

        }//fin DoWork



        /**
         * Browse the queue saving its data in the Database
         */
        private void execStore() {

            //If List has no data, ends method
            if (0 == TicksListSingleton.Instance.getListFiFoTicks(this.market.short_text).Count) {
                return;
            }

            log.Debug("Gonna insert ticks Dec: " + TicksListSingleton.Instance.getListFiFoTicks(this.market.short_text).Count);

            try {
                working = true;

                int insertsOKnum = 0;
                int insertsKOnum = 0;

                Tick tickBean = null;
                IDataAccessDAO dao = DAOFactory.Instance.getDAO(this.market.short_text);

                //Data Store
                while (0 != TicksListSingleton.Instance.getListFiFoTicks(this.market.short_text).Count) {

                    Boolean hasTick = TicksListSingleton.Instance.getListFiFoTicks(this.market.short_text).TryDequeue(out tickBean);
                    log.Debug("hasTick= " + hasTick);
                    if (false == hasTick) {
                        continue;
                    }

                    Tick_dec tick_dec = (Tick_dec)tickBean;
                    tick_dec.milisecond = getMilis();

                    //INSERT
                    try {
                        Task<Boolean> task = dao.insertTick(tick_dec);
                        task.Wait();
                        if (true == task.Result) {
                            insertsOKnum++;
                        }
                        else {
                            insertsKOnum++;
                        }

                        log.Debug("Dec Tick Inserted");
                    }
                    catch (Exception ex) {
                        log.Error("Error inserting Tick Dec", ex);
                        insertsKOnum++;
                    }

                    //Update Form field value
                    refForm.writeFifoNumberField();
                }

                printMessages(insertsOKnum, insertsKOnum);
            }
            finally {
                working = false;
                log.Debug("Ticks array Dec inserted");
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
