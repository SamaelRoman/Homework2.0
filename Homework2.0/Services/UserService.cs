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
    public class UserService : IUserService
    {
        private IRepository Repo;
        public UserService()
        {
            NinjectModule registration = new NinjectRegistrations();
            var kernel = new StandardKernel(registration);
            Repo = kernel.Get<IRepository>();
        }

        public User GetUserByPassportId(string PassportId)
        {
            if(Repo.GetUsers().FirstOrDefault(U=>U.PassportID.ToLower() == PassportId.ToLower()) == null)
            {
                throw new Exception($"Пользователь с таким номером паспорта \"{PassportId}\" не найден, проверьте вводимые данные!");
            }
            return Repo.GetUsers().FirstOrDefault(U => U.PassportID.ToLower() == PassportId.ToLower());
        }

        public void RegistrationUser(User user)
        {
            try
            {
                Repo.AddUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
