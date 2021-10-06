using Homework2._0.Services;
using Homework2._0.Util;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework2._0
{
    public class Application
    {
        private IReservationInfoService reservationInfoService;
        private IUserService userService;
        private IRoomCategoryService roomCategoryService;
        private IRoomService roomService;
        public Application()
        {
            NinjectModule registrations = new NinjectRegistrations();
            var Kernel = new StandardKernel(registrations);
            reservationInfoService = Kernel.Get<IReservationInfoService>();
            userService = Kernel.Get<IUserService>();
            roomCategoryService = Kernel.Get<IRoomCategoryService>();
            roomService = Kernel.Get<IRoomService>();
        }
        public void Start()
        {
        Start:
            Console.WriteLine("Добрый день, вы хотите:");
            Console.WriteLine("1:Забронировать номер.");
            Console.WriteLine("2:Посмотреть информацию забронированых вами номерах.");
            Console.WriteLine("3:Отменить бронирование номера.");
            string Result = Console.ReadLine();
            int ResultInt = 0;
            if(!int.TryParse(Result,out ResultInt)){
                goto Start;
            }
            switch (ResultInt)
            {
                case 1:
                    ReservationRooms();
                    goto Start;
                case 2:
                    SeeInformationAboutReservation();
                    goto Start;
                case 3:
                    CancelBooking();
                    goto Start;
                default:
                    goto Start;
            }
        }
        private void Separator(ConsoleColor BackToColor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('~', 40));
            Console.ForegroundColor = BackToColor;
        }
        private void SeeInformationAboutReservation()
        {
            Console.Clear();
            Console.WriteLine("Что бы получить информацию о забронированных номерах, пожалуйста введите номер паспорта:");
            var PassportID = Console.ReadLine().ToUpper();
            IEnumerable<ReservationInfo> Result = null;
            try
            {
                Result = reservationInfoService.GetBookingInformationByUserPassportId(PassportID);
            }
            catch (Exception Exc)
            {
                Console.Clear();
                Console.WriteLine(Exc.Message);
                Result = null;
            }
            if (Result != null)
            {
                foreach (var RI in Result)
                {
                    Console.Clear();
                    Console.WriteLine($"Номер {RI.room.Id} забронирован с {RI.StartReservation} по {RI.EndReservation}");
                }
            }
        }
        private void ReservationRooms()
        {
            Console.Clear();
        ReservationStart:
            Console.WriteLine("На какую дату вы хотите забронировать номер?");
            Console.Write("Введите дату в формате \"YYYY.MM.DD\":");
            DateTime StartReservation = new DateTime();
            if (!DateTime.TryParse(Console.ReadLine(), out StartReservation))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте вводимую дату на соответстиве формата!");
                Console.ResetColor();
                goto ReservationStart;
            }
            if (StartReservation.Date <= DateTime.Now)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы не можете забронировать номер в тот же день, либо на прошедшую дату!");
                Console.ResetColor();
                goto ReservationStart;
            }
        ReservationStepOneEnd:
            Console.Clear();
            DateTime EndReservation;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Дата заселения:" + StartReservation);
            Console.ResetColor();
            Console.WriteLine("На какое количество дней вам нужен номер?");
            Console.Write("Введите количество дней от 1го до 30ти:");
            int EndReservationInt;
            if(!int.TryParse(Console.ReadLine(),out EndReservationInt) ||  EndReservationInt > 30 ||  EndReservationInt < 1){
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что бы ввводимые данные были числом от 1го до 30ти!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto ReservationStepOneEnd;
            }
            EndReservation = StartReservation.AddDays(EndReservationInt);
        ReservationStepTwoEnd:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Дата выселения:" + EndReservation);
            Console.ResetColor();
            List<Room> AvailableRooms;
            try
            {
                AvailableRooms = roomService.GetAvailableRoomsByDate(StartReservation, EndReservation);
            }
            catch (Exception exc)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exc.Message);
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto ReservationStepTwoEnd;
                throw;
            }
        ReservationStepThreeEnd:
            Console.WriteLine("На указаный промежуток времени свободны следующие номера:");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var R in AvailableRooms)
            {

                Separator(ConsoleColor.Green);
                Console.WriteLine($"Номер {R.Id}");
                Console.WriteLine("\t Категории номера:");
                foreach (var RC in R.Categories)
                {
                    Console.WriteLine($"\t {RC.Name}");
                }
                Separator(ConsoleColor.Green);

            }
            Console.ResetColor();
            Console.Write("Введите код номера для бронирования:");
            int RoomNumber;
            if(!int.TryParse(Console.ReadLine(),out RoomNumber))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что бы ввводимые данные были числом!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto ReservationStepThreeEnd;
            }
            
            if(!AvailableRooms.Exists(R=>R.Id == RoomNumber))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что вы указываете код номера содержащийся в списке свободных номеров!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto ReservationStepThreeEnd;
            }
            var SelectedRoom = AvailableRooms.Find(R=>R.Id == RoomNumber);
        ReservationStepFourEnd:
            User user = new User();
            Console.WriteLine("Для продолжения бронирования укажите личные данные:");
            Console.WriteLine("1:Я уже являюсь клиентом, ввести номер паспорта");
            Console.WriteLine("2:Ввести личные данные");           
            int ChoisedNumber;
            if (!int.TryParse(Console.ReadLine(), out ChoisedNumber))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что бы ввводимые данные были числом!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto ReservationStepFourEnd;
            }

            switch (ChoisedNumber)
            {
                case 1:
                    Console.Write("Введите номер паспорта:");
                    user = userService.GetUserByPassportId(Console.ReadLine());
                    break;
                case 2:
                    NameInput:
                    Console.Write("Введите ваше имя: ");
                    var Name = Console.ReadLine();
                    SurNameInput:
                    Console.Write("Введите вашу фамилию: ");
                    var SurName = Console.ReadLine();
                    PatronymicInput:
                    Console.Write("Введите ваше отчество: ");
                    var Patronymic = Console.ReadLine();
                    PassportIDInput:
                    Console.Write("Введите ваш номер паспорта: ");
                    var PassportID = Console.ReadLine();
                    DOBInput:
                    Console.Write("Введите дату рождения в формате \"YYYY.MM.DD\": ");
                    DateTime DOB = new DateTime();
                    if (!DateTime.TryParse(Console.ReadLine(), out DOB))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Проверьте вводимую дату на соответстиве формата!");
                        Console.ResetColor();
                        goto DOBInput;
                    }                   
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ваши данные для регистрации:");
                    Console.WriteLine($"ФИО:{Name} {SurName} {Patronymic}");
                    Console.WriteLine($"Дата рождения \"{DOB.ToString("d")}\"");
                    Console.WriteLine($"Номер паспорта:{PassportID}");
                    Console.ResetColor();
                    Console.WriteLine("Для продолжения нажмите любую клавишу!");
                    Console.ReadKey();
                    try
                    {
                        userService.RegistrationUser(new User(Name, SurName, Patronymic, PassportID, DOB));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.WriteLine("Для продолженния нажмите любую клавишу");
                        Console.ReadKey();
                        goto NameInput;
                    }
                    user = userService.GetUserByPassportId(PassportID);
                    break;
                default:
                    goto ReservationStepFourEnd;
            }
            ReservationInfo CurrentReservation = new ReservationInfo(StartReservation, EndReservation, user, SelectedRoom);
        EndReservation:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Separator(ConsoleColor.Yellow);
            Console.WriteLine("Информация о бронировании");
            Console.WriteLine(CurrentReservation.ToString());
            Separator(ConsoleColor.Yellow);
            Console.ResetColor();           
            Console.WriteLine("Для подтверждения заказа нажмите любую клавишу!");
            Console.ReadKey();
            try
            {
                reservationInfoService.AddReservation(CurrentReservation);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("Для продолженния нажмите любую клавишу");
                Console.ReadKey();
                goto EndReservation;
            }
            Console.WriteLine("Вы успешно забронировали номер,для возврата в главное меню нажмите любую клавишу!");
            Console.ReadKey();
            Start();
        }
        private void CancelBooking()
        {
            Console.Clear();
            Console.WriteLine("Что бы получить информацию о забронированных номерах, пожалуйста введите номер паспорта:");
            var PassportID = Console.ReadLine().ToUpper();
            List<ReservationInfo> BookedRooms = null;
            CancelBookingStart:
            try
            {
                BookedRooms = reservationInfoService.GetBookingInformationByUserPassportId(PassportID);
            }
            catch (Exception Exc)
            {
                Console.Clear();
                Console.WriteLine(Exc.Message);
                BookedRooms = null;
            }
            if (BookedRooms != null)
            {
                foreach (var RI in BookedRooms)
                {
                    Console.Clear();
                    Console.WriteLine($"Номер {RI.room.Id} забронирован с {RI.StartReservation} по {RI.EndReservation}");
                }
            }
            CancelBookingStep1:
            Console.WriteLine("Для отменения бронирования выберите и введите номер комнаты!");
            int RoomNumber;
            if (!int.TryParse(Console.ReadLine(), out RoomNumber))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что бы ввводимые данные были числом!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto CancelBookingStep1;
            }

            if (!BookedRooms.Exists(RI => RI.room.Id == RoomNumber))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Проверьте что вы указываете код номера содержащийся в списке Забронированных номеров!");
                Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                Console.ReadKey();
                Console.ResetColor();
                goto CancelBookingStep1;
            }
            var SelectedRoom = BookedRooms.Find(RI => RI.room.Id == RoomNumber);
            CancelBookingStep2:
            Console.WriteLine("Для подтверждения отмены бронирования следующего номера нажмите Y для отмены N");
            Console.WriteLine(SelectedRoom.ToString());
            switch (Console.ReadKey().Key.ToString().ToUpper())
            {
                case "Y":
                    try
                    {
                        reservationInfoService.CancelBooking(SelectedRoom);
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.Write("Для того что бы продолжить нажмите любую клавишу!");
                        Console.ReadKey();
                        Console.ResetColor();
                        goto CancelBookingStart;
                    }
                    break;
                case "N":
                    goto CancelBookingStep1;
                default:
                    goto CancelBookingStep2;
            }
            Console.WriteLine("Бронь снята! для продолжения нажмите любую клавишу!");
            Console.ReadKey();
            Start();
        }
    }
}
