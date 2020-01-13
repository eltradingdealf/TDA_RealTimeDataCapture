using System;
using System.Collections.Generic;


namespace RealTimeDataCapture2.util {
    
    /// <summary>
    /// Class containing util methods for the Application
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class Util {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static String maskDecimalFormat_Int(int num) {

            string result = "";

            if (0 == num) {
                result = "0";
            }
            else if (10 > num) {
                result = String.Format("{0:0}", num);
            }
            else {
                result = String.Format("{0:0,0}", num);
            }

            return result;

        }//fin maskDecimalFormat_Int



        public static String maskDecimalFormat_Double(double num) {

            string result = "";

            if (0.0 == num) {                 
                result = "0.0";
            }
            else if (10.0 > num) {
                result = String.Format("{0:0.0}", num);
            }
            else {
                result = String.Format("{0:0,0.0}", num);
            }

            return result;

        }//fin maskDecimalFormat_Double
        


        public static String maskDecimalFormat_Int64(Int64 num) {

            string result = "";

            if (0 == num) {
                result = "0";
            }
            else if (10 > num) {
                result = String.Format("{0:0}", num);
            }
            else {
                result = String.Format("{0:0,0}", num);
            }

            return result;

        }//fin maskDecimalFormat_Int64



        public static Dictionary<String, Int32> getDatetimeMili_current() {

            Dictionary<String, Int32> result = new Dictionary<String, Int32>();

            Int32 milis = 0;
            Int32 thedate = 0;
            Int32 thetime = 0;

            try {
                DateTime dt = DateTime.Now;

                
                thedate = Int32.Parse(dt.ToString("yyyyMMdd"));
                thetime = Int32.Parse(dt.ToString("HHmmss"));
                milis = Int32.Parse(dt.ToString("FFF"));
            }
            catch (Exception ex) {
                log.Error("Error:", ex);
            }

            result.Add("thedate", thedate);
            result.Add("thetime", thetime);
            result.Add("milis", milis);

            return result;
        }//fin getDatetimeMili_current

    }//fin clase
}//fin
