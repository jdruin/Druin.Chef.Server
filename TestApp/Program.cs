using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new WebException();

            Console.WriteLine(ex.GetType());

            Console.ReadKey();
        }
    }
}
