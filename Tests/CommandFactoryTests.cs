using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ConsoleApplication1.Commands;

namespace Tests
{
    public class CommandFactoryTests
    {
        CommandFactory factory = new CommandFactory();

        [Fact]
        public void build_insert_command()
        {
            Command command = factory.BuildCommand("insert 100");
            command.Should().BeOfType<InsertCommand>();
            command.As<InsertCommand>().Amount.Should().Be(100);
        }

        [Fact]
        public void build_quit_command()
        {
            Command command = factory.BuildCommand("q");
            command.Should().BeOfType<QuitCommand>();
        }

        [Fact]
        public void build_order_command()
        {
            Command command = factory.BuildCommand("order fanta");
            command.Should().BeOfType<OrderCommand>();
            command.As<OrderCommand>().SodaName.Should().Be("fanta");
        }

        [Fact]
        public void build_recall_command()
        {
            Command command = factory.BuildCommand("recall");
            command.Should().BeOfType<RecallCommand>();
        }

        [Fact]
        public void build_sms_order_command()
        {
            Command command = factory.BuildCommand("sms order fanta");
            command.Should().BeOfType<SmsOrderCommand>();
            command.As<SmsOrderCommand>().SodaName.Should().Be("fanta");
        }

        [Fact]
        public void build_unknown_command()
        {
            Command command = factory.BuildCommand("does not exist");
            command.Should().BeOfType<UnknownCommand>();
        }

    }
}
