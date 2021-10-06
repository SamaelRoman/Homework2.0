using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Repositories
{
    class FirstRepository : IRepository
    {
        #region Add
        public void AddResirvationInfo(ReservationInfo reservationInfo)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if(contextDB1.ReservationInfos.Find(R => R.Id == reservationInfo.Id) != null)
            {
                throw new Exception("Объект класса ReservationInfo c таким Id уже содержится в базе данных!");
            }
            if(reservationInfo.user != null)
            {
                var CurrentUser = contextDB1.Users.Find(U => U.Id == reservationInfo.user.Id && U.PassportID == reservationInfo.user.PassportID);
                if(CurrentUser != null)
                {
                    if(CurrentUser.ReservationInfos.Find(RI=>RI.Id == reservationInfo.Id) == null)
                    {
                        CurrentUser.ReservationInfos.Add(reservationInfo);
                    }
                }
            }
            if(reservationInfo.room != null)
            {
                var CurrentRoom = contextDB1.Rooms.Find(R => R.Id == reservationInfo.room.Id);
                if(CurrentRoom != null)
                {
                    if(CurrentRoom.ReservationInfos.Find(RI=>RI.Id == reservationInfo.Id) == null)
                    {
                        CurrentRoom.ReservationInfos.Add(reservationInfo);
                    }
                }
            }
            contextDB1.ReservationInfos.Add(reservationInfo);
            contextDB1.SaveChanges();
        }

        public void AddRoom(Room room)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Rooms.Find(R => R.Id == room.Id) != null)
            {
                throw new Exception("Объект класса Room c таким Id уже содержится в базе данных!");
            }
            if(room.Categories.Count != 0 || room.Categories != null)
            {
                foreach (var C in room.Categories)
                {
                    var CurrentCategory = contextDB1.RoomCategories.Find(CC => CC.Id == C.Id);
                    if(CurrentCategory != null)
                    {
                        if(CurrentCategory.Rooms.Find(R=>R.Id == room.Id) == null)
                        {
                            CurrentCategory.Rooms.Add(room);
                        }
                    }
                }
            }
            
            contextDB1.Rooms.Add(room);
            contextDB1.SaveChanges();
        }

        public void AddRoomCategory(RoomCategory roomCategory)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.RoomCategories.Find(R => R.Id == roomCategory.Id) != null)
            {
                throw new Exception("Объект класса RoomCategory c таким Id уже содержится в базе данных!");
            }
            contextDB1.RoomCategories.Add(roomCategory);
            contextDB1.SaveChanges();
        }

        public void AddUser(User user)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Users.Find(U => U.Id == user.Id) != null || contextDB1.Users.Find(U=>U.PassportID == user.PassportID) != null)
            {
                throw new Exception("Объект класса User c таким Id или PassportID уже содержится в базе данных!");
            }
            contextDB1.Users.Add(user);
            contextDB1.SaveChanges();
        }
        #endregion

        #region Delete
        public void DeleteReservationInfo(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.ReservationInfos.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            var currentObj = contextDB1.ReservationInfos.Find(R => R.Id == Id);

            var ReservationInfo_User = contextDB1.Users.Find(U=>U.Id == currentObj.user.Id);
            if(ReservationInfo_User != null)
            {
                if(ReservationInfo_User.ReservationInfos.Find(RIU=>RIU.Id  == currentObj.Id)  != null)
                {
                    var DeleteResult = contextDB1.Users.Find(U => U.Id == currentObj.user.Id)?.ReservationInfos?.RemoveAll(R => R.Id == currentObj.Id);
                    if(DeleteResult != 1)
                    {
                        throw new Exception("Что то пошло не так!");
                    }
                    contextDB1.SaveChanges();
                    contextDB1 = contextDB1.RefreshChanges();
                }
            }
            var ReservationInfo_Room = contextDB1.Rooms.Find(R=>R.Id == currentObj.room.Id);
            if(ReservationInfo_Room != null)
            {
                var DeleteResult = contextDB1.Rooms.Find(R => R.Id == currentObj.room.Id)?.ReservationInfos?.RemoveAll(R => R.Id == currentObj.Id);
                if (DeleteResult != 1)
                {
                    throw new Exception("Что то пошло не так!");
                }
                contextDB1.SaveChanges();
                contextDB1 = contextDB1.RefreshChanges();
            }
            contextDB1.ReservationInfos?.RemoveAll(R => R.Id == currentObj.Id);
            contextDB1.SaveChanges();
        }

        public void DeleteRoom(int Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Rooms.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            var currentObj = contextDB1.Rooms.Find(R => R.Id == Id);

            var RoomsCategories = currentObj?.Categories?.ToList();
            if (RoomsCategories != null || RoomsCategories.Count != 0)
            {
                foreach(var RC in RoomsCategories)
                {
                    var DeleteResult = contextDB1.RoomCategories.Find(R => R.Id == RC.Id)?.Rooms?.RemoveAll(R => R.Id == currentObj.Id);
                    if(DeleteResult != 1)
                    {
                        throw new Exception("Что то пошло не так!");
                    }
                    contextDB1.SaveChanges();
                    contextDB1 = contextDB1.RefreshChanges();
                }
            }
            var RoomResInfo = currentObj?.ReservationInfos?.ToList();
            if(RoomResInfo != null || RoomResInfo.Count != 0)
            {
                foreach (var RI in RoomResInfo)
                {
                    DeleteReservationInfo(RI.Id);
                    contextDB1 = contextDB1.RefreshChanges();
                }
            }
            contextDB1?.Rooms?.RemoveAll(R => R.Id == currentObj.Id);
            contextDB1.SaveChanges();
            contextDB1.RefreshChanges();
        }

        public void DeleteRoomCategory(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.RoomCategories.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            var currentObj = contextDB1?.RoomCategories?.Find(R => R.Id == Id);
            var Rooms = currentObj?.Rooms?.ToList();
            if (Rooms.Count != 0 || Rooms != null)
            {
                foreach (var R in Rooms)
                {
                    var DeleteResult = contextDB1?.Rooms?.Find(X => X.Id == R.Id)?.Categories?.RemoveAll(RC => RC.Id == currentObj.Id);
                    if(DeleteResult != 1)
                    {
                        throw new Exception("Что то пошло не так!");
                    }
                    contextDB1.SaveChanges();
                    contextDB1 = contextDB1.RefreshChanges();
                }
            }
            contextDB1?.RoomCategories?.RemoveAll(RC => RC.Id == currentObj.Id);
            contextDB1.SaveChanges();
            contextDB1 = contextDB1.RefreshChanges();
        }

        public void DeleteUser(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Users.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            var currentObj = contextDB1?.Users?.Find(R => R.Id == Id);
            var ResInfos = currentObj?.ReservationInfos?.ToList();
            if(ResInfos.Count != 0 || ResInfos != null)
            {
                foreach (var ResInfo in ResInfos)
                {
                    try
                    {
                        DeleteReservationInfo(ResInfo.Id);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Произошла ошибка при попытке удаления ссылок на User в стороних обьектах класса ReservationInfo");
                    }
                }
            }
            contextDB1 = contextDB1.RefreshChanges();
            contextDB1?.Users?.RemoveAll(U=>U.Id == Id);
            contextDB1.SaveChanges();
        }
        #endregion

        #region Edit
        public void EditReservationInfo(ReservationInfo reservationInfo)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.ReservationInfos.Find(R => R.Id == reservationInfo.Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            DeleteReservationInfo(reservationInfo.Id);
            AddResirvationInfo(reservationInfo);
        }

        public void EditRoom(Room room)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Rooms.Find(R => R.Id == room.Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            DeleteRoom(room.Id);
            AddRoom(room);
        }

        public void EditRoomCategory(RoomCategory roomCategory)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.RoomCategories.Find(R => R.Id == roomCategory.Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            DeleteRoomCategory(roomCategory.Id);
            AddRoomCategory(roomCategory);
        }

        public void EditUser(User user)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Users.Find(R => R.Id == user.Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            DeleteUser(user.Id);
            AddUser(user);
        }
        #endregion

        #region Get
        public ReservationInfo GetReservationInfo(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.ReservationInfos.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            return contextDB1?.ReservationInfos?.Find(R => R.Id == Id);
        }

        public IEnumerable<ReservationInfo> GetReservationInfos()
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            return contextDB1?.ReservationInfos?.ToList();
        }

        public Room GetRoom(int Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Rooms.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            return contextDB1?.Rooms?.Find(R => R.Id == Id);
        }

        public IEnumerable<RoomCategory> GetRoomCategories()
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            return contextDB1?.RoomCategories?.ToList();
        }

        public RoomCategory GetRoomCategory(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.RoomCategories.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            return contextDB1?.RoomCategories?.Find(R => R.Id == Id);
        }

        public IEnumerable<Room> GetRooms()
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            return contextDB1?.Rooms?.ToList();
        }

        public User GetUser(Guid Id)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            if (contextDB1.Users.Find(R => R.Id == Id) == null)
            {
                throw new Exception("Объект с таким Id не найден в базе данных!");
            }
            return contextDB1?.Users?.Find(R => R.Id == Id);
        }

        public IEnumerable<User> GetUsers()
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            contextDB1 = contextDB1.RefreshChanges();
            return contextDB1?.Users?.ToList();
        }
        #endregion
    }
}
