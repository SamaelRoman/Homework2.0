using Homework2._0.Repositories;
using Homework2._0.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Util
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<FirstRepository>();
            Bind<IReservationInfoService>().To<ReservationInfoService>();
            Bind<IRoomCategoryService>().To<RoomCategoryService>();
            Bind<IRoomService>().To<RoomService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
