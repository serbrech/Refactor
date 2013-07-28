using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        private TextReader inputReader { get; set; }
        private TextWriter outputWriter { get; set; }

        public SodaMachine(TextReader input, TextWriter output)
        {
            inputReader = input;
            outputWriter = output;
        }

        private static int money;

        private void AddToCredit(int moneyIn)
        {
            money += moneyIn;
            outputWriter.WriteLine("Adding " + moneyIn + " to credit");
        }

        public void Start()
        {
            var inventory = new[] { new Soda { Name = "coke", Nr = 5, Price = 20 }, new Soda { Name = "sprite", Nr = 3, Price = 15 }, new Soda { Name = "fanta", Nr = 3, Price = 15 } };
            var quit = false;
            while (!quit)
            {
                DisplayInfoMessage();                
                var input = GetCommand();
                if (input.StartsWith("insert"))
                {
                    AddToCredit(int.Parse(input.Split(' ')[1]));
                }
                if (input.StartsWith("order"))
                {
                    // split string on space
                    var csoda = input.Split(' ')[1];
                    Soda soda;
                    //Find out witch kind
                    switch (csoda)
                    {
                        case "coke":
                            soda = inventory[0];
                            OrderSoda(soda);
                            break;
                        case "fanta":
                            soda = inventory[2];
                            OrderSoda(soda);
                            break;
                        case "sprite":
                            soda = inventory[1];
                            OrderSoda(soda);
                            break;
                        default:
                            outputWriter.WriteLine("No such soda");
                            break;
                    }
                }
                if (input.StartsWith("sms order"))
                {
                    var csoda = input.Split(' ')[2];
                    //Find out witch kind
                    switch (csoda)
                    {
                        case "coke":
                            if (inventory[0].Nr > 0)
                            {
                                outputWriter.WriteLine("Giving coke out");
                                inventory[0].Nr--;
                            }
                            break;
                        case "sprite":
                            if (inventory[1].Nr > 0)
                            {
                                outputWriter.WriteLine("Giving sprite out");
                                inventory[1].Nr--;
                            }
                            break;
                        case "fanta":
                            if (inventory[2].Nr > 0)
                            {
                                outputWriter.WriteLine("Giving fanta out");
                                inventory[2].Nr--;
                            }
                            break;
                    }
                }
                if (input.Equals("recall"))
                {
                    //Give money back
                    outputWriter.WriteLine("Returning " + money + " to customer");
                    money = 0;
                }
                if(input.Equals("q"))
                {
                    quit = true;
                    outputWriter.WriteLine("quitting");
                }

            }
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

        private string GetCommand()
        {
            return inputReader.ReadLine();
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
