using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeDataCapture2.model {


    /// <summary>
    /// Contiene informacion del precio actual.
    /// Formato numerico entero.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Marzo 2019
    /// </date>
    class Prices_int : Prices {

        /**
        * Precio de compra
        */
        public Int32 buyprice { get; set; }

        /**
         * Precio de venta
         */
        public Int32 sellprice { get; set; }



        public override string ToString() {

            StringBuilder result = new StringBuilder();

            result.Append("buy=" + this.buyprice);
            result.Append(", sell=" + this.sellprice);

            return result.ToString();
        }//fin ToString
    }//fin clase
}//fin
