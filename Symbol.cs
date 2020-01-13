using System.Text;

namespace RealTimeDataCapture2.model  {

    /// <summary>
    /// Bean Object containing one Symbol info.
    /// </summary>
    /// <author>
    /// Alfredo Sanz
    /// </author>
    /// <date>
    /// Febrero 2017
    /// </date>
    class Symbol {

        public string code { get; set;}

        public string description { get; set;}



        public override string ToString() {

            StringBuilder result = new StringBuilder();
            result.Append(code);
            result.Append(" (");
            result.Append(description);
            result.Append(")");

            return result.ToString();

        }//fin ToString

    }//fin clase
}//fin
