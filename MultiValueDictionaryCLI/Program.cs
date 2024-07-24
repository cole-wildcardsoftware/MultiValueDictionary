using MultiValueDictionaryCLI.Functionality;
using MultiValueDictionaryCLI.Models;
using System;

namespace MultiValueDictionaryCLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create CommandShell and neccessary dependencies
            var ConsoleIO = new ConsoleIO();
            var MultiValueDictionay = new MultiValueDictionary();
            var CommandShell = new CommandShell(MultiValueDictionay, ConsoleIO);

            // Infinite loop for input
            while (true)
            {
                Console.Write("Enter Command: ");
                var input = Console.ReadLine();
                if (input == null)
                {
                    continue;
                }

                try
                {
                    var output = CommandShell.ExecuteCommand(input);
                    Console.WriteLine(output);
                }
                catch (CommandException ex)
                {
                    Console.WriteLine("ERROR, " + ex.Message);
                }
            }
        }
    }
}
