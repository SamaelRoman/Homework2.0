using Homework2._0.Repositories;
using System;

namespace Homework2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.Start();


            Console.ReadKey();
            void DisplayDB()
            {
                FirstRepository FR1 = new FirstRepository();

                Console.WriteLine("Categories");
                foreach (var C in FR1.GetRoomCategories())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t" + C.Name);
                    foreach (var R in C.Rooms)
                    {
                        Console.WriteLine("\t\t" + R.Id);
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("Rooms");
                foreach (var R in FR1.GetRooms())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t" + R.Id + " | " + R.IsBooked);
                    foreach (var RC in R.Categories)
                    {
                        Console.WriteLine("\t\t" + RC.Name);
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("Users");
                foreach (var U in FR1.GetUsers())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t" + U.FullName);
                    foreach (var RI in U.ReservationInfos)
                    {
                        Console.WriteLine($"\t\t Id:{RI.Id} | Room:{RI.room.Id} | Data:{RI.StartReservation} - {RI.EndReservation}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("ReservationInfos");
                foreach (var RI in FR1.GetReservationInfos())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\t" + RI.Id + " For user: " + RI.user.FullName + " Room: " + RI.room.Id);
                    Console.ResetColor();
                }
            }
        }
    }
}
