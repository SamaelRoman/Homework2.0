using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Repositories
{
    interface IRepository
    {
        #region Room CRUD
        /// <summary>
        /// Добавление объекта Room в базу данных.
        /// </summary>
        /// <param name="room">Объект Room для добавления</param>
        void AddRoom(Room room);
        /// <summary>
        /// Метод получения всех гостинечных номеров из базы данных.
        /// </summary>
        /// <returns>Возвращает Ienumerable<Room>, который является списком всех номеров</returns>
        IEnumerable<Room> GetRooms();
        /// <summary>
        /// Возвращает объект Room по его Id.
        /// </summary>
        /// <param name="Id">Id Номера</param>
        /// <returns>Возвращает объект номера по его Id</returns>
        Room GetRoom(int Id);
        /// <summary>
        /// Редактирование объекта Room в базе данных.
        /// </summary>
        /// <param name="room">Объект класса Room с измененными свойствами, и имеющий Id изменяемого элемента.</param>
        void EditRoom(Room room);
        /// <summary>
        /// Удаляет объект класа Room из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для удаления.</param>
        void DeleteRoom(int Id);
        #endregion

        #region User CRUD
        /// <summary>
        /// Добавление объекта User в базу данных.
        /// </summary>
        /// <param name="user">Объект для добавления</param>
        void AddUser(User user);
        /// <summary>
        /// Получение всех объектов User из базы данных. 
        /// </summary>
        /// <returns>Список объектов класса User</returns>
        IEnumerable<User> GetUsers();
        /// <summary>
        /// Получение объекта User из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для получения</param>
        /// <returns>Объект класса User</returns>
        User GetUser(int Id);
        /// <summary>
        /// Редактирование объекта User в базе данных.
        /// </summary>
        /// <param name="user">Объект класса User с измененными свойствами, и имеющий Id изменяемого элемента.</param>
        void EditUser(User user);
        /// <summary>
        /// Удаляет объект класа User из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для удаления.</param>
        void DeleteUser(int Id);
        #endregion

        #region RoomCategory CRUD
        /// <summary>
        /// Добавление объекта RoomCategory в базу данных.
        /// </summary>
        /// <param name="roomCategory">Объект для добавления.</param>
        void AddRoomCategory(RoomCategory roomCategory);
        /// <summary>
        /// Получение всех объектов RoomCategory из базы данных. 
        /// </summary>
        /// <returns>Список объектов класса RoomCategory</returns>
        IEnumerable<RoomCategory> GetRoomCategories();
        /// <summary>
        /// Получение объекта RoomCategory из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для получения<</param>
        /// <returns>Объект класса RoomCategory</returns>
        RoomCategory GetRoomCategory(int Id);
        /// <summary>
        /// Редактирование объекта RoomCategory в базе данных.
        /// </summary>
        /// <param name="roomCategory">Объект класса RoomCategory с измененными свойствами, и имеющий Id изменяемого элемента.</param>
        void EditRoomCategory(RoomCategory roomCategory);
        /// <summary>
        /// Удаляет объект класа RoomCategory из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для удаления.</param>
        void DeleteRoomCategory(int Id);
        #endregion

        #region ReservationInfo CRUD
        /// <summary>
        /// Добавление объекта ReservationInfo в базу данных.
        /// </summary>
        /// <param name="roomCategory">Объект для добавления.</param>
        void AddResirvationInfo(ReservationInfo reservationInfo);
        /// <summary>
        /// Получение всех объектов ReservationInfo из базы данных. 
        /// </summary>
        /// <returns>Список объектов класса ReservationInfo</returns>
        IEnumerable<ReservationInfo> GetReservationInfos();
        /// <summary>
        /// Получение объекта ReservationInfo из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для получения<</param>
        /// <returns>Объект класса ReservationInfo</returns>
        ReservationInfo GetReservationInfo(int Id);
        /// <summary>
        /// Редактирование объекта ReservationInfo в базе данных.
        /// </summary>
        /// <param name="reservationInfo">Объект класса ReservationInfo с измененными свойствами, и имеющий Id изменяемого элемента.</param>
        void EditReservationInfo(ReservationInfo reservationInfo);
        /// <summary>
        /// Удаляет объект класа ReservationInfo из базы данных по его Id.
        /// </summary>
        /// <param name="Id">Id объекта для удаления.</param>
        void DeleteReservationInfo(int Id);
        #endregion
    }
}
