using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class SmsOrderCommand : Command
    {
        public string SodaName { get; set; }
        public SmsOrderCommand(string input)
            : base(input)
        {
            SodaName = input.Split(' ')[2];
        }
    }
}
