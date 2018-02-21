using System;
using System.Collections.Generic;
using PType = System.Double;

namespace diceexpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            var d1 = new Die(20);
            Console.WriteLine(d1);
            var d2 = new Constant<PType>((PType)1.7);
            Console.WriteLine(d2);
            Console.WriteLine(d1+d1+d1);
            var t2 = 1;
            Console.WriteLine(d1+t2);
            Console.WriteLine(t2+d1);
            var d3=d1+d2;
            Console.WriteLine(d3);
            var d4 = d1.ArithMult(3);
            Console.WriteLine(d4);
            var d5 = d1.ArithMult(d1);
            Console.WriteLine(d5);
            d5.SaveOxyPlotPdf("/home/jonas/test.pdf");
        }
    }
}
