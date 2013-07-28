using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class UnknownCommand : Command
    {
        public UnknownCommand(string input) : base(input)
        {

        }
    }
}
