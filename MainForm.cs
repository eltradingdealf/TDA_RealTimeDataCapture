using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

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
    public partial class MainForm : Form {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RTSTickList_Interface tickList_Process;

        private Thread threadConnectRTS;
        private System.Timers.Timer timerStoreBBDD;

        Symbol symbol = null;
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

            Queue<Symbol> symbolsQu = new Queue<Symbol>();

            Symbol s1 = new model.Symbol();
            s1.code = Constants.SIMBOLO_IBEX35_CONTINUOS;
            s1.description = "IBEX-35 CONTINUOUS";

            Symbol s2 = new model.Symbol();
            s2.code = Constants.SIMBOLO_CME_EURO_FX_JUNE2019;
            s2.description = "EURO-FX *MARCH 2020";

            Symbol s3 = new model.Symbol();
            s3.code = Constants.SIMBOLO_CME_MINISP_500_JUNE2019;
            s3.description = "MINI S&P 500 *MARCH 2020";

            Symbol s4 = new model.Symbol();
            s4.code = Constants.SIMBOLO_CME_MINI_NASDAQ_JUNE2019;
            s4.description = "MINI-NASDAQ 100 *JUNE 2019";


            symbolsQu.Enqueue(s1);
            symbolsQu.Enqueue(s2);
            symbolsQu.Enqueue(s3);
            symbolsQu.Enqueue(s4);

            foreach (Symbol s in symbolsQu) {
                cbSymbol.Items.Add(s);
            }
        }//fin fillSymbolCombos



        /*
        * INITIALIZE THREAD.
        * 
        * Button event: Connect to Real Time Server.
        * 
        * Start a new Thread to Start Connection to Real Time Server.
        */
        private void btConnectRTS_Click(object sender, EventArgs e) {

            Symbol mysymbol = getSymbolFromCombo(cbSymbol);
            if (null == mysymbol) {
                return;
            }
            symbol = mysymbol;

            StartRTSworker worker = new StartRTSworker(); //Connect VC server

            threadConnectRTS = new Thread(new ThreadStart(worker.DoWork));
            worker.place(ref threadConnectRTS, mysymbol, this);
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

            if (Constants.SIMBOLO_IBEX35_CONTINUOS.Equals(this.symbol.code)) {
                tickList_Process = new RTSTickList_Process_ibex(this, this.symbol);
            }
            else if(Constants.SIMBOLO_CME_MINISP_500_JUNE2019.Equals(this.symbol.code)) {
                tickList_Process = new RTSTickList_Process_dec(this, this.symbol);
            }
            else if(Constants.SIMBOLO_CME_EURO_FX_JUNE2019.Equals(this.symbol.code)) {
                tickList_Process = new RTSTickList_Process_dec(this, this.symbol);
            }
            else if(Constants.SIMBOLO_CME_MINI_NASDAQ_JUNE2019.Equals(this.symbol.code)) {
                tickList_Process = new RTSTickList_Process_dec(this, this.symbol);
            }

            subscribeEvents();

            initializeThreadStoreDataInDDBB();
        }//fin ConnectToRTSdone



        /**
         * Subscribe the events of Visualchart RealTimeLib
         */
        private void subscribeEvents() {

            //Delegamos el evento hacia un metodo de esta clase "_IRealTimeEvents_OnNewTicks"
            RealTimeServer_Singleton.Instance.getRealTimeInstance().OnNewTicks += new _IRealTimeEvents_OnNewTicksEventHandler(_IRealTimeEvents_OnNewTicks);
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
                    RealTimeServer_Singleton.Instance.getRealTimeInstance().CancelSymbolFeed(symbol.code, true);
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
                if (Constants.SIMBOLO_IBEX35_CONTINUOS.Equals(this.symbol.code)) {
                    worker = new DatabaseStore_Process_ibex();
                }
                else if(Constants.SIMBOLO_CME_MINISP_500_JUNE2019.Equals(this.symbol.code)) {
                    worker = new DatabaseStore_Process_Dec(Constants.SIMBOLO_CME_MINISP_500_JUNE2019);
                }
                else if(Constants.SIMBOLO_CME_EURO_FX_JUNE2019.Equals(this.symbol.code)) {
                    worker = new DatabaseStore_Process_Dec(Constants.SIMBOLO_CME_EURO_FX_JUNE2019);
                }
                else if(Constants.SIMBOLO_CME_MINI_NASDAQ_JUNE2019.Equals(this.symbol.code)) {
                    worker = new DatabaseStore_Process_Dec(Constants.SIMBOLO_CME_MINI_NASDAQ_JUNE2019);
                }
                else {
                    return false;
                }

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

            tickList_Process.processTickList(ref arrayTicks);
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



        private Symbol getSymbolFromCombo(ComboBox combo) {

            Symbol symbol = null;

            Object s = combo.SelectedItem;
            if (s is Symbol) {
                symbol = (Symbol)s;
            }
            else {
                MessageBox.Show("Select a Symbol in the combo below", "Info", MessageBoxButtons.OK);
            }

            return symbol;
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