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
    public class RoomCategoryService : IRoomCategoryService
    {
        private IRepository Repo;
        public RoomCategoryService()
        {
            NinjectModule registrations = new NinjectRegistrations();
            var Kernel = new StandardKernel(registrations);
            Repo = Kernel.Get<IRepository>();
        }
    }
}
