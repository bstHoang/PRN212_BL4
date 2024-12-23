
using System;

namespace Slot_3_4
{
    internal partial class Program
    {
        int x=3; int y=5;
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Add(1,2);
        }

        void Add(int a, int b) { 
            Console.WriteLine($"{a+b}");
            Console.WriteLine($"{a+b+x+y}");
        }

    }
}