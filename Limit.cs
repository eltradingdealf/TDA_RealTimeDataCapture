using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealTimeDataCapture2.model {

    /// <summary>
    /// Contiene informacion de un tick servido por RealTimeServer
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class Limit {

        public int askPrice { get; set;}

        public int askVolume { get; set;}

        public int bidPrice { get; set;}

        public int bidVolume { get; set;}


         public override string ToString() { 

            StringBuilder result = new StringBuilder();

            result.Append("askPrice=" + this.askPrice);
            result.Append(", askVol=" + this.askVolume);
            result.Append(", bidPrice=" + this.bidPrice);
            result.Append(", bidVol=" + this.bidVolume);

            return result.ToString();

        }//fin ToString

    }//fin clase

}//fin
