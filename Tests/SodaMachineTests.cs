using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class SodaMachineTests
    {
        [Fact]
        public void Command_q_quits()
        {
            var input = new System.IO.StringReader("q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            
            output.ToString().Should().EndWith("quitting\r\n");
        }

        [Fact]
        public void Command_insert_insert_credit()
        {
            var input = GetCommandReader(new[] {
                "insert 100",
                "q"
            });
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            
            output.ToString().Should().Contain("Inserted money: 100");
        }

        private static System.IO.StringReader GetCommandReader(string[] commands)
        {
            return new System.IO.StringReader(string.Join("\r\n",commands));
        }
    }
}
