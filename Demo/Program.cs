using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilioWithThinq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TwilioWrapper wrapper = new TwilioWrapper("+86 131 3051 1523", "ACa5a21802beff96f147d40bf98c957038", "7852c807435af28d468344ca57a49d2a", "+1 754-333-6811");
            Console.WriteLine("Call sid: " + wrapper.call());
            Console.ReadLine();
        }
    }
}
