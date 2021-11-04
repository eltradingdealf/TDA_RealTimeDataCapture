using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using RealTimeDataCapture2.dao;

/* En el Explorador de Soluciones, seleccionamos “VCRealTimeLib” dentro de “References”. 
 * En la ventana de propiedades (pulsar Alt+Enter si no la tenéis visible), ponemos la 
 * opción “Incrustar tipos de interoperabilidad” a “False”. Esto último es muy importante o 
 * tendremos luego problemas de compilación.
 * Entre otros problemas no disparara los eventos.
*/
using VCRealTimeLib;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.common;
using RealTimeDataCapture2.workers;
using RealTimeDataCapture2.util;


namespace RealTimeDataCapture2 {


    /// <summary>
    ///  Main Application Form.
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
    public partial class MainForm : Form {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RTSTickList_Interface tickList_Process;

        private Thread threadConnectRTS;
        private System.Timers.Timer timerStoreBBDD;

        Market market = null;
        int idSession;



        /**
         * Constructor
         */
        public MainForm() {

            InitializeComponent();
            Shown += FormShown_event;
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit_event);
        }//fin constructor



        /**
         * +++FORM EVENT
         * 
         * The Implementation of this event initialize 
         * and load all the needed info required by
         * the App, mostly stored in Database.
         */
        private void FormShown_event(object sender, EventArgs e) {

            writeInStatusField(Constants.MENSAJE_NOT_CONNECTED);

            log.Info("Started program VisualChart Realtime Data Capture 2");
            fillSymbolCombos();
            deleteDatabaseData_ticks();
        }//fin FormShown_event



        /**
        * +++APP EVENT
        * 
        * When the application is exiting, close every resource
        * could be open.
        */
        private void OnApplicationExit_event(object sender, EventArgs e) {

            log.Info("Finishing VisualChart Realtime Data Capture 2");
            forceShutdownApp();
        }//fin OnApplicationExit



        /**
         * Fill with data all the Symbol Combos in the Form.
         */
        private void fillSymbolCombos() {

            Queue<Market> qMarkets = null;
            try {
                IMarketsDAO dao = DAOFactory.Instance.getXMarketsDAO();

                qMarkets = dao.selectMarketList();
                log.Debug("markets requested");
            }
            catch (Exception ex) {
                log.Error("Error requesting Markets", ex);
                throw new Exception(ex.Message);
            }

            foreach (Market m in qMarkets) {
                cbSymbol.Items.Add(m);
            }
        }//fin fillSymbolCombos



        private void deleteDatabaseData_ticks() {

            try {
                IDataAccessDAO dao = DAOFactory.Instance.getDAO(Constants.SIMBOLO_CME_EURO_FX);
                dao.deleteTicks();
                log.Debug("EUROFX ticks data deleted");

                IDataAccessDAO dao2 = DAOFactory.Instance.getDAO(Constants.SIMBOLO_CME_MINISP);
                dao2.deleteTicks();
                log.Debug("SP500 ticks data deleted");

                IDataAccessDAO dao3 = DAOFactory.Instance.getDAO(Constants.SIMBOLO_DAX);
                dao3.deleteTicks();
                log.Debug("DAX ticks data deleted");

                IDataAccessDAO dao4 = DAOFactory.Instance.getDAO(Constants.SIMBOLO_IBEX35);
                dao4.deleteTicks();
                log.Debug("IBEX35 ticks data deleted");

                IDataAccessDAO dao5 = DAOFactory.Instance.getDAO(Constants.SIMBOLO_BUND);
                dao5.deleteTicks();
                log.Debug("BUND ticks data deleted");

                IDataAccessDAO dao6 = DAOFactory.Instance.getDAO(Constants.SIMBOLO_STOXX50);
                dao6.deleteTicks();
                log.Debug("STOXX50 ticks data deleted");
            }
            catch (Exception ex) {
                log.Error("Error requesting Markets", ex);
                throw new Exception(ex.Message);
            }
        }


        /*
        * INITIALIZE THREAD.
        * 
        * Button event: Connect to Real Time Server.
        * 
        * Start a new Thread to Start Connection to Real Time Server.
        */
        private void btConnectRTS_Click(object sender, EventArgs e) {

            Market myMarket = getSymbolFromCombo(cbSymbol);
            if (null == myMarket) {
                return;
            }
            market = myMarket;

            StartRTSworker worker = new StartRTSworker(); //Connect VC server

            threadConnectRTS = new Thread(new ThreadStart(worker.DoWork));
            worker.place(ref threadConnectRTS, myMarket, this);
            threadConnectRTS.Start();
        }//fin btConnectRTS_Click



        /**
         * THREAD CALLBACK.
         * 
         * Method Called from StartRTSworker Thread,
         * indicating the completion of the connection
         * to Real time server.
         */
        public void ConnectToRTSdone() {

            tickList_Process = new RTSTickList_Process_dec(this, this.market);

            subscribeEvents();

            initializeThreadStoreDataInDDBB();
        }//fin ConnectToRTSdone



        /**
         * Subscribe the events of Visualchart RealTimeLib
         */
        private void subscribeEvents() {

            //Delegamos el evento hacia un metodo de esta clase "_IRealTimeEvents_OnNewTicks"
            try {
                RealTimeServer_Singleton.Instance.getRealTimeInstance().OnNewTicks += new _IRealTimeEvents_OnNewTicksEventHandler(this._IRealTimeEvents_OnNewTicks);
            }
            catch(Exception ex) {
                log.Error("subscribeEvents", ex);
            }
        }//fin subscribeEvents



        /**
        * Button event: Disconnect from Real Time Server
        *  
        * Disconnect every Conection of our Real Time Server, and disable tha current instance. 
        */
        private void btDisconnectRTS_Click(object sender, EventArgs e) {


            Boolean disconnected = false;
            try {
                disconnected = disconnectRealTimeServer();
            }
            catch (Exception ignore) {
                log.Error("Error: ", ignore);
                writeInStatusField(Constants.MENSAJE_ERROR_DISCONNECTING);
            }

            if (true == disconnected) {
                writeInStatusField(Constants.MENSAJE_NOT_CONNECTED);

                stopThreadStoreDataInDDBB();
            }
        }//fin btDisconnectRTS_Click



        /**
         * Disconnect from RealTimeServer
         */
        private Boolean disconnectRealTimeServer() {

            try {
                if (null != RealTimeServer_Singleton.Instance.getRealTimeInstance()) {
                    RealTimeServer_Singleton.Instance.getRealTimeInstance().CancelSymbolFeed(market.symbol, true);
                    RealTimeServer_Singleton.Instance.setRealTimeServerInstance(null);
                }
            }
            catch (System.Runtime.InteropServices.COMException ex) {
                log.Error("Error:", ex);
                return false;
            }

            return true;
        }//fin disconnectRealTimeServer



        /**
         * INITIALIZE THREAD.
         * 
         * Initialize the thread who has to store the generated ticks
         * into the database
         */
        private Boolean initializeThreadStoreDataInDDBB() {

            if (null == timerStoreBBDD ||
               !timerStoreBBDD.Enabled) {

                DatabaseStore_Process_Interface worker = null;
                worker = new DatabaseStore_Process_Dec(this.market);
                timerStoreBBDD = new System.Timers.Timer(Constants.HALF_A_SECOND);
                timerStoreBBDD.Elapsed += worker.DoWork;
                worker.place(ref timerStoreBBDD, this);
                timerStoreBBDD.Start();
            }

            return true;
        }//fin initializeThreadStoreDataInDDBB



        private Boolean stopThreadStoreDataInDDBB() {

            if (null != timerStoreBBDD) {
                if (timerStoreBBDD.Enabled) {

                    try {
                        timerStoreBBDD.Stop();
                        timerStoreBBDD.Dispose();
                        timerStoreBBDD.Close();
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return true;
        }//fin stopThreadStoreDataInDDBB



        /**
         * -REAL TIME SERVER EVENT.
         * 
         * Metodo que captura el Evento delegado de la Instancia de RealTime.
         */
        private void _IRealTimeEvents_OnNewTicks(ref Array arrayTicks) {

            try {
                tickList_Process.processTickList(ref arrayTicks);
            }
            catch(Exception ex) {
                log.Error("_IRealTimeEvents_OnNewTicks ", ex);
            }
        }//fin _IRealTimeEvents_OnNewTicks



        /**
         * Button event: Exit Application
         */
        private void btExitApplication_Click(object sender, EventArgs e) {

            forceShutdownApp();
        }//fin btExitApplication_Click



        /**
         * Force the application close.
         */
        private void forceShutdownApp() {

            Console.WriteLine("Forcing program exit.");

            disconnectRealTimeServer();

            if (null != threadConnectRTS) {
                if (threadConnectRTS.IsAlive) {
                    threadConnectRTS.Abort();
                }
            }

            if (null != timerStoreBBDD) {
                if (timerStoreBBDD.Enabled) {
                    timerStoreBBDD.Stop();
                    timerStoreBBDD.Dispose();
                    timerStoreBBDD.Close();
                }
            }

            try {
                SqlConnection.ClearAllPools();
            }
            catch { }

            Application.Exit();
            //Environment.Exit(-1);
        }//fin forceShutdownApp



        private Market getSymbolFromCombo(ComboBox combo) {

            Market market = null;

            Object m = combo.SelectedItem;
            if (m is Market) {
                market = (Market)m;
            }
            else {
                MessageBox.Show("Select a Market in the combo below", "Info", MessageBoxButtons.OK);
            }

            return market;
        }//fin getSymbolFromCombo



        /*+++WRITE METHODS+++*/



        /**
         * Write a message in the status Field
         */
        public void writeInStatusField(String message) {

            if (InvokeRequired) {
                BeginInvoke((MethodInvoker)delegate {

                    tbStatus.ResetText();
                    tbStatus.Text = message;
                }); ;
            }
            else {
                tbStatus.ResetText();
                tbStatus.Text = message;
            }
        }//fin writeInStatusField



        /**
         * Write values in the Group1 form fields
         */
        public void writeProcessFormFields(string totalticks, string totalvol, string totalbuy, string totalsell) {

            if (InvokeRequired) {
                BeginInvoke((MethodInvoker)delegate {

                    writeProcessFormFields_impl(totalticks, totalvol, totalbuy, totalsell);
                }); ;
            }
            else {
                writeProcessFormFields_impl(totalticks, totalvol, totalbuy, totalsell);
            }
        }//fin writeProcessFormFields



        private void writeProcessFormFields_impl(string totalticks, string totalvol, string totalbuy, string totalsell) {

            tbNumberTicksInCollection.ResetText();
            tbNumTicksCollected.ResetText();
            tbVolumeTotal.ResetText();
            tbAskTotal.ResetText();
            tbBidTotal.ResetText();

            tbNumberTicksInCollection.Text = "Zero";
            tbNumTicksCollected.Text = totalticks;
            tbVolumeTotal.Text = totalvol;
            tbAskTotal.Text = totalsell;
            tbBidTotal.Text = totalbuy;
        }//fin writeProcessFormFields_impl



        public void writeFifoNumberField() {

            //Update Form Fields info
            if (InvokeRequired) {
                BeginInvoke((MethodInvoker)delegate {

                    tbNumberTicksInCollection.ResetText();
                    tbNumberTicksInCollection.Text = "ZERO-";//Util.maskDecimalFormat_Int(TicksListSingleton.Instance.getListFiFoTicks().Count);
                }); ;
            }
            else {
                tbNumberTicksInCollection.ResetText();
                tbNumberTicksInCollection.Text = "ZERO+";//Util.maskDecimalFormat_Int(TicksListSingleton.Instance.getListFiFoTicks().Count);
            }
        }//fin writeFifoNumberField



        public void writeDBQuerysResultField(string message) {

            if (InvokeRequired) {
                BeginInvoke((MethodInvoker)delegate {

                    tbQuerysResult.ResetText();
                    tbQuerysResult.Text = message;
                }); ;
            }
            else {
                tbQuerysResult.ResetText();
                tbQuerysResult.Text = message;
            }
        }//fin writeDBQuerysResultField



        public void setSession(int _idSession) {

            idSession = _idSession;
        }//fin setSession
    }//fin class
}//fin FORM