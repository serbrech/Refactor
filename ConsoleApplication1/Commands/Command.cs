using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class Command
    {
        public string Input { get; set; }   
        public Command(string input)
        {
            Input = input;
        }
    }
}
