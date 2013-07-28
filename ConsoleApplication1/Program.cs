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

        public void Start()
        {
            var inventory = new[] { new Soda { Name = "coke", Nr = 5 }, new Soda { Name = "sprite", Nr = 3 }, new Soda { Name = "fanta", Nr = 3 } };
            var quit = false;
            while (!quit)
            {
                DisplayInfoMessage();                
                var input = GetCommand();
                if (input.StartsWith("insert"))
                {
                    //Add to credit
                    money += int.Parse(input.Split(' ')[1]);
                    outputWriter.WriteLine("Adding " + int.Parse(input.Split(' ')[1]) + " to credit");
                }
                if (input.StartsWith("order"))
                {
                    // split string on space
                    var csoda = input.Split(' ')[1];
                    //Find out witch kind
                    switch (csoda)
                    {
                        case "coke":
                            var coke = inventory[0];
                            if (coke.Name == csoda && money > 19 && coke.Nr > 0)
                            {
                                outputWriter.WriteLine("Giving coke out");
                                money -= 20;
                                outputWriter.WriteLine("Giving " + money + " out in change");
                                money = 0;
                                coke.Nr--;
                            }
                            else if (coke.Name == csoda && coke.Nr == 0)
                            {
                                outputWriter.WriteLine("No coke left");
                            }
                            else if (coke.Name == csoda)
                            {
                                outputWriter.WriteLine("Need " + (20 - money) + " more");
                            }

                            break;
                        case "fanta":
                            var fanta = inventory[2];
                            if (fanta.Name == csoda && money > 14 && fanta.Nr > 0)
                            {
                                outputWriter.WriteLine("Giving fanta out");
                                money -= 15;
                                outputWriter.WriteLine("Giving " + money + " out in change");
                                money = 0;
                                fanta.Nr--;
                            }
                            else if (fanta.Name == csoda && fanta.Nr == 0)
                            {
                                outputWriter.WriteLine("No fanta left");
                            }
                            else if (fanta.Name == csoda)
                            {
                                outputWriter.WriteLine("Need " + (15 - money) + " more");
                            }

                            break;
                        case "sprite":
                            var sprite = inventory[1];
                            if (sprite.Name == csoda && money > 14 && sprite.Nr > 0)
                            {
                                outputWriter.WriteLine("Giving sprite out");
                                money -= 15;
                                outputWriter.WriteLine("Giving " + money + " out in change");
                                money = 0;
                                sprite.Nr--;
                            }
                            else if (sprite.Name == csoda && sprite.Nr == 0)
                            {
                                outputWriter.WriteLine("No sprite left");
                            }
                            else if (sprite.Name == csoda)
                            {
                                outputWriter.WriteLine("Need " + (15 - money) + " more");
                            }
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

    }
}
