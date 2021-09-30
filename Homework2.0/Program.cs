using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            ContextDB1 contextDB1 = new ContextDB1(ConfigurationManager.AppSettings.Get("DBFile"));
            

            Console.ReadKey();

        }
    }
}
