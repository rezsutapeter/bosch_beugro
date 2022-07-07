using System;

namespace beugro_projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process();
            p.ReadandFill();
            p.GenerateProductions();
            p.CreatePuffer();
            p.ReadPuffer();
            Console.ReadKey();
        }
    }
}
