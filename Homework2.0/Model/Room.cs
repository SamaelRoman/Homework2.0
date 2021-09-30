using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0
{
    [Serializable]
    public class Room
    {
        /// <summary>
        /// Уникальный идентификатор, и номер команты в гостинице.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Список категорий опеределенного номера.
        /// </summary>
        public List<RoomCategory> Categories { get; set; }
        /// <summary>
        /// Коллекция объектов ReservationInfos содержащих информацию о бронировании номеров.
        /// </summary>
        public List<ReservationInfo> ReservationInfos { get; set; }
        /// <summary>
        /// Забронирован ли номер, в случаее наличия брони возвращает True.
        /// </summary>
        public bool IsBooked { get { if (ReservationInfos.Count() == 0) { return false; } else { return true; } } }
        /// <summary>
        /// Конструктор класса Room
        /// </summary>
        /// <param name="Id">Уникальный идентификатор, и номер команты в гостинице.</param>
        public Room(int Id)
        {
            this.Id = Id;
            Categories = new List<RoomCategory>();
            ReservationInfos = new List<ReservationInfo>();

        }
        public Room() { }




    }
}
