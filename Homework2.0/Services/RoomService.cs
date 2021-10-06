using Homework2._0.Repositories;
using Homework2._0.Util;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Services
{
    public class RoomService : IRoomService
    {
        private IRepository Repo;
        public RoomService()
        {
            NinjectModule registrations = new NinjectRegistrations();
            var Kernel = new StandardKernel(registrations);
            Repo = Kernel.Get<IRepository>();
        }

        public List<Room> GetAvailableRoomsByDate(DateTime Start, DateTime End)
        {
            IEnumerable<Room> AllRooms = Repo.GetRooms();
            List<Room> AvailableRooms = new List<Room>();
            foreach (var Room in AllRooms)
            {
                bool check = true;
                foreach (var RI in Room.ReservationInfos)
                {
                    if (Start >= RI.StartReservation && Start <= RI.EndReservation ||
                       End >= RI.StartReservation && End <= RI.EndReservation ||
                       Start <= RI.StartReservation && End >= RI.EndReservation)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    AvailableRooms.Add(Room);
                }
            }
            if (AvailableRooms == null || AvailableRooms.Count == 0)
            {
                throw new Exception("На указанный промежуток времени свободных номеров нет!");
            }
            return AvailableRooms;
        }
    }
}
