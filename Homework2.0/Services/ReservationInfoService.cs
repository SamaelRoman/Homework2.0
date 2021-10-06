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
    public class ReservationInfoService : IReservationInfoService
    {
        private IRepository Repo;
        public ReservationInfoService()
        {
            NinjectModule registrations = new NinjectRegistrations();
            var Kernel = new StandardKernel(registrations);
            Repo = Kernel.Get<IRepository>();
        }

        public void AddReservation(ReservationInfo RI)
        {
            try
            {
                Repo.AddResirvationInfo(RI);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CancelBooking(ReservationInfo RI)
        {
            try
            {
                Repo.DeleteReservationInfo(RI.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ReservationInfo> GetBookingInformationByUserPassportId(string PassportID)
        {

            var User = Repo?.GetUsers()?.FirstOrDefault(U => U.PassportID == PassportID);
            if (User == null)
            {
                throw new Exception($"Пользователь с таким номером \"{PassportID}\" паспорта не найден!");
            }
            List<ReservationInfo> Result = Repo.GetReservationInfos().Where(RI => RI.user.PassportID == PassportID).ToList();
            if(Result.Count() == 0 || Result == null)
            {
                throw new Exception($"У пользователя с таким номером пасорта \"{PassportID}\" активные брони не обнаружены!");
            }
            else
            {
                return Result;
            }
        }
    }
}
