using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Services
{
    public interface IRoomService
    {
        /// <summary>
        /// Возвращаает доступные для бронирования номера на указанную дату
        /// </summary>
        /// <param name="Start">Начальаня дата</param>
        /// <param name="End">Конечная дата</param>
        /// <returns>Возвращаает список IEnumerable<Room> с объектами класса Room доступными для бронирования номера на указанную дату</returns>
        List<Room> GetAvailableRoomsByDate(DateTime Start, DateTime End);
    }
}
