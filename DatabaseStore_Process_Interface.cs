using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RealTimeDataCapture2.workers {


    interface DatabaseStore_Process_Interface {

        void DoWork(Object source, ElapsedEventArgs e);

        void place(ref System.Timers.Timer _t, MainForm _f);
    }
}
