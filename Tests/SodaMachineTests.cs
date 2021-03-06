﻿using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Diagnostics;

namespace Tests
{
    public class SodaMachineTests
    {
        public SodaMachineTests()
        {
            //reset money before every test.
            var input = GetCommandReader("recall", "q");
            var output = new System.IO.StringWriter();
            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
        }

        [Fact]
        public void Command_q_quits()
        {
            var input = GetCommandReader("q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            
            output.ToString().Should().EndWith("quitting\r\n");
        }

        [Fact]
        public void Recall_should_give_money_back()
        {
            var input = GetCommandReader("insert 100", "recall", "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            output.ToString().Should().Contain("Returning 100 to customer");
        }

        [Fact]
        public void Order_a_coke_with_no_money_should_warn()
        {
            var input = GetCommandReader("order coke", "q");
            var output = new System.IO.StringWriter();
            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            output.ToString().Should().Contain("Need 20 more");
        }

        [Fact]
        public void Order_a_coke_with_exact_money_should_give_coke()
        {
            var input = GetCommandReader("insert 20", "order coke", "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            output.ToString().Should().Contain("Giving coke out");
        }

        [Fact]
        public void Order_a_coke_with_extra_money_return_change()
        {
            var input = GetCommandReader("insert 21", "order coke", "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            output.ToString().Should().Contain("Giving coke out").And.Contain("Giving 1 out in change");
        }

        [Fact]
        //Haha found you!
        public void Warn_when_no_more_soda()
        {
            var input = GetCommandReader(
                "insert 15", "order fanta", 
                "insert 15", "order fanta", 
                "insert 15", "order fanta",
                "insert 15", "order fanta",
                "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            output.ToString().Should().Contain("No fanta left");
        }

        [Fact]
        public void order_non_existing_soda_should_warn()
        {
            var input = GetCommandReader("order solo", "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();

            output.ToString().Should().Contain("No such soda");
        }

        [Fact]
        public void Command_insert_inserts_credit()
        {
            var input = GetCommandReader("insert 100", "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();
            
            output.ToString().Should().Contain("Inserted money: 100");
        }

        [Fact]
        public void sms_order_checks_for_soda_count()
        {
            var input = GetCommandReader(
                "sms order fanta",
                "sms order fanta",
                "sms order fanta",
                "sms order fanta",
                "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();

            output.ToString().Should().Contain("No fanta left");
        }

        [Fact]
        public void sms_order_returns_soda()
        {
            SmsOrder("sprite");
            SmsOrder("coke");
            SmsOrder("fanta");
        }

        private void SmsOrder(string soda)
        {
            var input = GetCommandReader("sms order " + soda, "q");
            var output = new System.IO.StringWriter();

            var sodamachine = new SodaMachine(input, output);
            sodamachine.Start();

            output.ToString().Should().Contain("Giving " + soda + " out");
        }


        private static System.IO.StringReader GetCommandReader(params string[] commands)
        {
            return new System.IO.StringReader(string.Join("\r\n",commands));
        }
    }
}
