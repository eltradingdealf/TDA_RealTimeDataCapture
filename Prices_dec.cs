using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeDataCapture2.model {

    /// <summary>
    /// Contiene informacion del precio actual.
    /// Formato numerico decimal.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Marzo 2019
    /// </date>
    class Prices_dec : Prices {


        /**
        * Precio de compra
        */
        public decimal buyprice { get; set; }

        /**
         * Precio de venta
         */
        public decimal sellprice { get; set; }



        public override string ToString() {

            StringBuilder result = new StringBuilder();

            result.Append("buy=" + this.buyprice.ToString());
            result.Append(", sell=" + this.sellprice.ToString());

            return result.ToString();
        }//fin ToString
    }//fin clase
}//fin
