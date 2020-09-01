using System.Text;

namespace RealTimeDataCapture2.model {

    /// <summary>
    /// Bean Object containing one Market info.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Sept 2020
    /// </date>
    class Market {

        public string symbol { get; set; }

        public int active { get; set; }

        public string market { get; set; }

        public string short_text { get; set; }



        public override string ToString() {

            StringBuilder result = new StringBuilder();
            result.Append(market);
            result.Append(" (");
            result.Append(symbol);
            result.Append(")");            

            return result.ToString();
        }//fin ToString

    }//fin clase
}//fin
