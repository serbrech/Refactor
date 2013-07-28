using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Commands
{
    public class CommandFactory
    {
        public dynamic BuildCommand(string p)
        {
            if (p.StartsWith("insert"))
            {
                return new InsertCommand(p);
            }
            if (p.StartsWith("sms"))
            {
                return new SmsOrderCommand(p);
            }
            if (p.StartsWith("order"))
            {
                return new OrderCommand(p);
            }
            if (p.Equals("recall"))
            {
                return new RecallCommand(p);
            }
            if (p.Equals("q"))
            {
                return new QuitCommand(p);
            }
            else return new UnknownCommand(p);
        }
    }
}
