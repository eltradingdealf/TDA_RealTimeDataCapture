using System;
using RealTimeDataCapture2.model;
using RealTimeDataCapture2.util;
using RealTimeDataCapture2.common;
using System.Threading.Tasks;
using System.Collections.Generic;
using RealTimeDataCapture2.dao;
using RealTimeDataCapture2.workers;

namespace RealTimeDataCapture2.workers {

    /// <summary>
    ///  This class process a tick list provided by
    ///  the Real Time Server.
    ///  This is an exclusive algorithm for FUTURE-symbols with prices as a decimal numbers.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Sept 2020
    /// </date>
    class RTSTickList_Process_dec : RTSTickList_Interface {


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /**
         * Referencia del Formulario llamador de la clase
         */
        private RealTimeDataCapture2.MainForm refForm;
        private Market market;

        private int totalBuy = 0;
        private int totalSell = 0;
        private int totalVol = 0;
        private Int64 totalTicks = 0;
        private decimal lastBuy = decimal.Zero;
        private decimal lastSell = decimal.Zero;
        private string lastLimitUpdate = "N";

        private readonly object plock = new object();


        /**
         * Constructor
         */
        public RTSTickList_Process_dec(MainForm _f, Market _market) {

            refForm = _f;
            this.market = _market;
        }//constructor



        private RTSTickList_Process_dec() {
        }//constructor



        /**
         * Process the parameter Ticks list
         * 
         * @param arrayTicks: Ticks Array provided by Real Time Server
         */
        public void processTickList(ref Array arrayTicks) {
            if (0 == arrayTicks.Length) {
                return;
            }

            log.Debug("***START processTickList");
            Boolean anyPriceChange = false;
            decimal lastPrice = 0;
            int lastvol = 0;

            Tick_dec tickBean = null;
            Boolean savedTick = true;
            VCRealTimeLib.Limit limitRT = RealTimeServer_Singleton.Instance.getRealTimeInstance().GetLimit(this.market.symbol, 1); ;

            foreach (VCRealTimeLib.Tick rtTick in arrayTicks) {

                log.Debug("**new Iteration");
                if (true == savedTick) {
                    tickBean = new Tick_dec();
                    log.Debug("**new Tick");

                    savedTick = false;
                    lastPrice = 0;
                    lastvol = 0;
                }
                //log.Debug("*--------tick.Field= " + rtTick.Field);
                //log.Debug("*--------tick.Value= " + rtTick.Value);
                //log.Debug("*--------tick.Index= " + rtTick.TickIndex);
                //log.Debug("*--------tick.Text= " + rtTick.Text);
                //log.Debug("*--------tick.String= " + rtTick.ToString());

                if (VCRealTimeLib.enumField.Field_Last == rtTick.Field) {
                    lastPrice = Convert.ToDecimal(rtTick.Value);        log.Debug("***+lastPrice= " + lastPrice);
                }

                if (VCRealTimeLib.enumField.Field_Last_Vol == rtTick.Field) {
                    lastvol = Convert.ToInt32(rtTick.Value);            log.Debug("***$lastvol= " + lastvol);
                }

                if (VCRealTimeLib.enumField.Field_Buy1 == rtTick.Field) {
                    this.lastBuy = Convert.ToDecimal(rtTick.Value);     log.Debug("***lastBuy= " + this.lastBuy);
                    this.lastLimitUpdate = "B";
                    anyPriceChange = true;
                }

                if (VCRealTimeLib.enumField.Field_Sale1 == rtTick.Field) {
                    this.lastSell = Convert.ToDecimal(rtTick.Value);    log.Debug("***lastSell= " + this.lastSell);
                    this.lastLimitUpdate = "S";
                    anyPriceChange = true;
                }


                //---Calculate trade
                if (0 != lastvol && 0 != lastPrice) {
                    tickBean.volume = lastvol;                          log.Debug("***tickBean.lastvol= " + lastvol);
                    tickBean.price = lastPrice;                         log.Debug("***tickBean.lastprice= " + lastPrice);

                    decimal buyPrice = this.lastSell;                   log.Debug("****compra buyPrice=" + buyPrice);
                    decimal sellPrice = this.lastBuy;                   log.Debug("****venta  sellPrice=" + sellPrice);
                                                                        log.Debug("****lastLimitUpdate=" + lastLimitUpdate);

                    if ("S".Equals(lastLimitUpdate)) {
                                                                            log.Debug("++++S == lastLimitUpdate");
                        if (lastPrice.CompareTo(buyPrice) >= 0) {
                                                                            log.Debug("++++lastPrice >= buyPrice");
                            tickBean.operation = Constants.OPERATION_BUY;   log.Debug("***@operation= " + Constants.OPERATION_BUY);
                            this.totalBuy += lastvol;                       log.Debug("***@totalBuy= " + this.totalBuy);
                        }
                        else {
                                                                            log.Debug("++++lastPrice < buyPrice");
                            tickBean.operation = Constants.OPERATION_SELL;  log.Debug("***@operation= " + Constants.OPERATION_SELL);
                            this.totalSell += lastvol;                      log.Debug("***@totalSell= " + this.totalSell);
                        }
                    }
                    else if ("B".Equals(lastLimitUpdate)) {
                                                                            log.Debug("B == lastLimitUpdate");
                        if (lastPrice.CompareTo(sellPrice) <= 0) {
                                                                            log.Debug("++++lastPrice <= sellPrice");
                            tickBean.operation = Constants.OPERATION_SELL;  log.Debug("***@operation= " + Constants.OPERATION_SELL);
                            this.totalSell += lastvol;                      log.Debug("***@totalSell= " + this.totalSell);
                        }
                        else {
                                                                            log.Debug("lastPrice > sellPrice");
                            tickBean.operation = Constants.OPERATION_BUY;   log.Debug("***@operation= " + Constants.OPERATION_SELL);
                            this.totalBuy += lastvol;                       log.Debug("***@totalBuy= " + this.totalBuy);
                        }
                    }
                    else {
                                                                            log.Debug(lastLimitUpdate + "== lastLimitUpdate");
                        if (lastPrice.CompareTo(buyPrice) >= 0) {
                                                                            log.Debug("++++else   lastPrice >= buyPrice");
                            tickBean.operation = Constants.OPERATION_BUY;   log.Debug("***@operation= " + Constants.OPERATION_BUY);
                            this.totalBuy += lastvol;                       log.Debug("***@totalBuy= " + this.totalBuy);
                        }
                        else {
                                                                            log.Debug("++++else    lastPrice < buyPrice");
                            tickBean.operation = Constants.OPERATION_SELL;  log.Debug("***@operation= " + Constants.OPERATION_SELL);
                            this.totalSell += lastvol;                      log.Debug("***@totalSell= " + this.totalSell);
                        }
                    }


                    //Datos de Mercado
                    tickBean.buy = buyPrice;    //bidPrice;
                    tickBean.sell = sellPrice;  //askPrice;

                    //Datos del Tick
                    tickBean.date = int.Parse(rtTick.Date.ToString("yyyyMMdd"));
                    tickBean.time = int.Parse(rtTick.Date.ToString("HHmmss"));
                    tickBean.symbol = rtTick.SymbolCode;

                    //Add TickBean to List
                    TicksListSingleton.Instance.getListFiFoTicks(this.market.short_text).Enqueue(tickBean);      //log.Debug("***Tick Added to List, trade");
                    this.totalTicks++;                                                                      log.Debug("***@totalTicks= " + this.totalTicks);
                    this.totalVol += lastvol;                                                               log.Debug("***@totalVol= " + this.totalVol);

                    lastPrice = 0;
                    lastvol = 0;

                    savedTick = true;                                                           //log.Debug("***savedTick = true");
                }//if
            }//foreach

            //Despues de recorrer los ticks, si hubo un cambio de precios,
            //guardamos en BD el ultimo obtenido.
            if (true == anyPriceChange) {
                updatePricesInDatabase();
            }

            /***UPDATE SCREEN FIELDS**/
            refForm.writeProcessFormFields(Util.maskDecimalFormat_Int64(this.totalTicks),
                                           Util.maskDecimalFormat_Int(int.Parse(this.totalVol.ToString())),
                                           Util.maskDecimalFormat_Int(int.Parse(this.totalBuy.ToString())),
                                           Util.maskDecimalFormat_Int(int.Parse(this.totalSell.ToString())));

            log.Debug("***END processTickList");

        }//fin processTickList



        private void updatePricesInDatabase() {

            Dictionary<String, Int32> fecha_dict = Util.getDatetimeMili_current();

            //INSERTUPDATE
            try {
                IDataAccessDAO dao = DAOFactory.Instance.getDAO(this.market.short_text);

                Prices_dec p = new Prices_dec();
                p.buyprice = this.lastBuy;
                p.sellprice = this.lastSell;

                Task<Boolean> task = dao.updatePrices(fecha_dict, p);
                task.Wait();

                log.Debug("dec prices updated: " + this.market.short_text);
            }
            catch (Exception ex) {
                log.Error("Error updating prices DEC", ex);
            }
        }//fin updatePricesInDatabase
    }//fin clase
}//fin