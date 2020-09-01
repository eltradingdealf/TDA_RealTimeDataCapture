using RealTimeDataCapture2.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Interfaz con las operaciones del DAO
/// </summary>
/// <author>
/// Alfredo Sanz
/// </author>
/// <date>
/// Febrero 2017
/// </date>
namespace RealTimeDataCapture2.dao {

    interface IDataAccessDAO {

        void deleteTicks();
        Task<Boolean> insertTick(Tick tick);
        Task<Boolean> updatePrices(Dictionary<String, Int32> datetimemili, Prices prices);
    }
}
