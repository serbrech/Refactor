using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class QuitCommand : Command
    {
        
        public QuitCommand(string input)
            : base(input)
        {
        }
    }
}
