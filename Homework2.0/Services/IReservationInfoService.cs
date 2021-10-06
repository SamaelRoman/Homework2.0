using System.Collections.Generic;

namespace Homework2._0.Services
{
    public interface IReservationInfoService
    {
        List<ReservationInfo> GetBookingInformationByUserPassportId(string PassportID);
        void AddReservation(ReservationInfo RI);
        void CancelBooking(ReservationInfo RI);
    }
}
