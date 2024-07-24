using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiValueDictionaryCLI.Interfaces;
using MultiValueDictionaryCLI.Models;

namespace MultiValueDictionaryCLI.Functionality
{
    public class ConsoleIO : IConsoleIO
    {
        public ConsoleIO() { }

        // Convert a command name to a CommandEnum
        public CommandEnum GetCommandFromInput(string input)
        {
            var command_found = Enum.TryParse(input, out CommandEnum command);
            if (command_found == false)
            {
                throw new CommandException(CommandException.UNKNOWN_COMMAND);
            }

            return command;
        }

        // Handle multi-line output and format it correctly
        // (empty set) for an empty list
        // numbered list starting at 1
        public string ToListResult(List<string> output)
        {
            if (output.Count == 0)
            {
                return "(empty set)";
            }

            var result_builder = new StringBuilder();
            for (var i = 0; i < output.Count; i++)
            {
                result_builder.AppendLine((i+1).ToString() + ") " + output[i]);
            }

            return result_builder.ToString();
        }
    }
}
