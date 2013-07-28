using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApplication1.Commands;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine(Console.In, Console.Out);
            sodaMachine.Start();
        }
    }

    public class SodaMachine
    {
        private TextReader inputReader;
        private TextWriter outputWriter;
        private Soda[] inventory;
        CommandFactory commandFactory;

        public SodaMachine(TextReader input, TextWriter output)
        {
            inputReader = input;
            outputWriter = output;
            commandFactory = new CommandFactory();
            inventory = new[] { new Soda { Name = "coke", Nr = 5, Price = 20 }, new Soda { Name = "sprite", Nr = 3, Price = 15 }, new Soda { Name = "fanta", Nr = 3, Price = 15 } };
        }

        private static int money;

        private void AddToCredit(int moneyIn)
        {
            money += moneyIn;
            outputWriter.WriteLine("Adding " + moneyIn + " to credit");
        }

        public void Start()
        {
            var quit = false;
            while (!quit)
            {
                DisplayInfoMessage();
                Command command = GetCommand();
                if (command.Input.StartsWith("insert"))
                {
                    AddToCredit(int.Parse(command.Input.Split(' ')[1]));
                }
                if (command.Input.StartsWith("order"))
                {
                    Soda soda = GetSodaByName(command.Input.Split(' ')[1]);
                    if (soda != null)
                        OrderSoda(soda);
                    else
                        outputWriter.WriteLine("No such soda");
                }
                if (command.Input.StartsWith("sms order"))
                {
                    var csoda = command.Input.Split(' ')[2];
                    Soda soda = GetSodaByName(csoda);
                    outputWriter.WriteLine("Giving " + soda.Name + " out");
                    soda.Nr--;
                }
                if (command.Input.Equals("recall"))
                {
                    //Give money back
                    outputWriter.WriteLine("Returning " + money + " to customer");
                    money = 0;
                }
                if (command.Input.Equals("q"))
                {
                    quit = true;
                    outputWriter.WriteLine("quitting");
                }

            }
        }

        private Command GetCommand()
        {
            return commandFactory.BuildCommand(inputReader.ReadLine());
        }
        

        private Soda GetSodaByName(string csoda)
        {
            return inventory.FirstOrDefault(s => s.Name == csoda);
        }

        private void OrderSoda(Soda soda)
        {
            if (money >= soda.Price && soda.Nr > 0)
            {
                outputWriter.WriteLine("Giving {0} out", soda.Name);
                money -= soda.Price;
                outputWriter.WriteLine("Giving " + money + " out in change");
                money = 0;
                soda.Nr--;
            }
            else if (soda.Nr == 0)
            {
                outputWriter.WriteLine("No {0} left", soda.Name);
            }
            else
            {
                outputWriter.WriteLine("Need " + (soda.Price - money) + " more");
            }
        }

        
        

        private void DisplayInfoMessage()
        {
            outputWriter.WriteLine("\n\nAvailable commands:");
            outputWriter.WriteLine("insert (money) - Money put into money slot");
            outputWriter.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
            outputWriter.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
            outputWriter.WriteLine("recall - gives money back");
            outputWriter.WriteLine("-------");
            outputWriter.WriteLine("Inserted money: " + money);
            outputWriter.WriteLine("-------\n\n");
        }
    }

    public class Soda
    {
        public string Name { get; set; }
        public int Nr { get; set; }


        public int Price { get; set; }
    }
}
