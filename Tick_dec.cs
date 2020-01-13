using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeDataCapture2.model {
    /// <summary>
    /// Contiene informacion de un tick servido por RealTimeServer
    /// Datos del FUTURE-Euro
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Marzo 2019
    /// </date>

    class Tick_dec : Tick {


        /**
         * Simbolo de mercado 
         */
        public string symbol { get; set; }

        /**
         * Ultimo precio de compra
         */
        public decimal buy { get; set; }
        /**
         * Ultimo precio de venta
         */
        public decimal sell { get; set; }
        /**
         * Precio de la operacion
         */
        public decimal price { get; set; }
        /**
         * Volumen de contratos de la operacion
         */
        public int volume { get; set; }
        /*
         * Formato yyyMMdd
         */
        public int date { get; set; }
        /*
         * Formato HHmmss
         */
        public int time { get; set; }
        /*
         * Formato int milisegundos de la fecha de tick
        */
        public int milisecond { get; set; }
        /**
         * Indica oepracion de compra o de venta.
         * Valores= COMPRA, VENTA
         */
        public String operation { get; set; }

        /**
         * Contains the list of Limits Price and Volume at the moment of harvest Tick.
         */
        public Queue<Limit> qLimits { get; set; }



        public override string ToString() {

            StringBuilder result = new StringBuilder();

            result.Append("buy=" + this.buy);
            result.Append(", sell=" + this.sell);
            result.Append(", price=" + this.price);
            result.Append(", volume=" + this.volume);
            result.Append(", operation=" + this.operation);
            result.Append(", date=" + this.date);
            result.Append(", time=" + this.time);
            //result.Append(", Limits_length=" + this.qLimits.Count);

            return result.ToString();
        }//fin ToString
    }//fin clase
}//fin
