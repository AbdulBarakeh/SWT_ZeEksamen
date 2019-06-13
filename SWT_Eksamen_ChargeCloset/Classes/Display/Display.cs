using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Display
{
    class Display : IDisplay
    {
        public void displayMsg(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
