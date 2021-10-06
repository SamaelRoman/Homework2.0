using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0
{
    [Serializable]
    public class ReservationInfo
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата начала бронирования.
        /// </summary>
        public DateTime StartReservation { get; set; }
        /// <summary>
        /// Дата окончания бронирования.
        /// </summary>
        public DateTime EndReservation { get; set; }
        /// <summary>
        /// Постоялец зрабронировавший номер.
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// Номер который был забронирован.
        /// </summary>
        public Room room { get; set; }
        /// <summary>
        /// Переопределенный метод ToString() для удобного отображения всей информации о брони.
        /// </summary>
        /// <returns>Возвращает полную информацию о том какой номер кем и насколько был забронирован.</returns>
        public override string ToString()
        {
            return ($"Номер {room.Id}, забронирован постояльцем {user.FullName} c {StartReservation} до {EndReservation}");
        }
        /// <summary>
        /// Конструктор класса ReservationInfo
        /// </summary>
        /// <param name="StartReservation">Дата начала брони.</param>
        /// <param name="EndReservation">Дата оканчания брони.</param>
        /// <param name="user">Постоялец забронировавший номер.</param>
        /// <param name="room">Номер который был забронирован.</param>
        public ReservationInfo(DateTime StartReservation, DateTime EndReservation, User user,Room room)
        {
            this.Id = Guid.NewGuid();
            this.StartReservation = StartReservation;
            this.EndReservation = EndReservation;
            this.user = user;
            this.room = room;
        }
        public ReservationInfo()
        {
        }
    }
}
