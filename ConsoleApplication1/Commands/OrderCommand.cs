using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class OrderCommand : Command
    {
        public string SodaName { get; set; }
 
        public OrderCommand(string input)
            : base(input)
        {
            SodaName = input.Split(' ')[1];
        }
    }
}
