using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0.Services
{
    public interface IUserService
    {
        User GetUserByPassportId(string PassportId);
        void RegistrationUser(User user);
    }
}
