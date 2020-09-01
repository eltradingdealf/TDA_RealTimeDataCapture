using System.Collections.Generic;
using RealTimeDataCapture2.model;


/// <summary>
/// Interfaz con las operaciones del DAO
/// </summary>
/// <author>
/// Alfredo Sanz
/// </author>
/// <date>
/// Sept 2020
/// </date>
namespace RealTimeDataCapture2 {
    interface IMarketsDAO {
            Queue<Market> selectMarketList();
    }
}
