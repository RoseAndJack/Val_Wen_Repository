using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            Subscriber s = new Subscriber(p);
            p.Tempchange();
           // Console.ReadKey();
        }
    }
}
