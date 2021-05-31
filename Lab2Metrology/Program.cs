using System;

namespace Lab2Metrology
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 2;
            int z = 2;
            double C = ( Math.Pow( Math.Tan( x), 2) + Math.Cos( y)) / Math.Sqrt( 8 * Math.Pow( z, 5));
            Console.WriteLine("Значение выражения: " + C);
        }
    }
}
