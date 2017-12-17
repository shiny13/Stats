using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var file = new FileAction();

            //Initially I made it so that it takes the commands in the method arguments
            //But you have to change the command line arguments in Debug properties every time, so it's easier to make a loop of entries when running the app
            //var inputs = args.ToList();

            try
            {
                int x = 1;
                while (x > 0)
                {
                    try
                    {
                        //Take inputs until the user enters exit
                        var input = Console.ReadLine();
                        var inputs = input.Split(' ').ToList();

                        switch (inputs[0])
                        {
                            case "record":
                                var path = inputs[1];
                                inputs.RemoveAt(1);
                                inputs.Remove("record");
                                file.Record(path, string.Join(" ", inputs.ToArray()));
                                break;
                            case "summary":
                                file.Summary(inputs[1]);
                                break;
                            case "exit":
                                x = 0;
                                break;
                            default:
                                file.Help();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        file.Help();
                    }
                }

                Console.WriteLine("Press any key to exit ...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                file.Help();
            }
        }
    }
}
