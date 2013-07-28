using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class InsertCommand : Command
    {
        public int Amount { get; set; }
        public InsertCommand(string input) : base(input)
        {
            Amount = int.Parse(input.Split(' ')[1]);
        }
    }
}
