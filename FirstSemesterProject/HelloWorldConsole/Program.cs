using HelloWorldNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorld hw = new HelloWorld();
            Console.WriteLine(hw.SayIt());
            Console.ReadLine();
        }
    }
}
