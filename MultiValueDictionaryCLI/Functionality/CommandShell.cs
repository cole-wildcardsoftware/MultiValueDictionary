using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiValueDictionaryCLI.Interfaces;
using MultiValueDictionaryCLI.Models;

namespace MultiValueDictionaryCLI.Functionality
{
    public class CommandShell : ICommandShell
    {
        private IMultiValueDictionary _MultiValueDictionary { get; set; }
        private IConsoleIO _ConsoleIO { get; set; }

        public CommandShell(IMultiValueDictionary multiValueDictionary, IConsoleIO consoleIO)
        {
            _MultiValueDictionary = multiValueDictionary;
            _ConsoleIO = consoleIO;
        }

        // Validate and Execute the command based on the input the Shell is given
        // throws CommandException on invalid commands
        public string ExecuteCommand(string input)
        {
            var args = input.Split(' ').ToList();
            var command = _ConsoleIO.GetCommandFromInput(args[0]);

            Dictionary<CommandEnum, int> argumentCounts = new Dictionary<CommandEnum, int>()
            {
                { CommandEnum.KEYS, 1 },
                { CommandEnum.MEMBERS, 2 },
                { CommandEnum.ADD, 3 },
                { CommandEnum.ADDRANGE, 3 },
                { CommandEnum.REMOVE, 3 },
                { CommandEnum.REMOVEALL, 2 },
                { CommandEnum.CLEAR, 1 },
                { CommandEnum.KEYEXISTS, 2 },
                { CommandEnum.MEMBEREXISTS, 3 },
                { CommandEnum.ALLMEMBERS, 1 },
                { CommandEnum.ITEMS, 1 },
                { CommandEnum.HELP, 1 },
            };

            if (args.Count < argumentCounts[command])
            {
                throw new CommandException(CommandException.INVALID_ARGUMENT_COUNT);
            }

            string output = "";
            List<string>? multilineOutput = null;
            switch (command)
            {
                case CommandEnum.KEYS:
                    multilineOutput = _MultiValueDictionary.GetKeys();
                    break;
                case CommandEnum.MEMBERS:
                    multilineOutput = _MultiValueDictionary.GetMembers(args[1]);
                    break;
                case CommandEnum.ADD:
                    _MultiValueDictionary.AddValue(args[1], args[2]);
                    output = "Added";
                    break;
                case CommandEnum.ADDRANGE:
                    _MultiValueDictionary.AddRange(args[1], args.Skip(2).ToList());
                    output = "Added";
                    break;
                case CommandEnum.REMOVE:
                    _MultiValueDictionary.RemoveValue(args[1], args[2]);
                    output = "Removed";
                    break;
                case CommandEnum.REMOVEALL:
                    _MultiValueDictionary.RemoveAll(args[1]);
                    output = "Removed";
                    break;
                case CommandEnum.CLEAR:
                    _MultiValueDictionary.Clear();
                    output = "Cleared";
                    break;
                case CommandEnum.KEYEXISTS:
                    output = _MultiValueDictionary.KeyExists(args[1]).ToString();
                    break;
                case CommandEnum.MEMBEREXISTS:
                    output = _MultiValueDictionary.MemberExists(args[1], args[2]).ToString();
                    break;
                case CommandEnum.ALLMEMBERS:
                    multilineOutput = _MultiValueDictionary.GetAllMembers();
                    break;
                case CommandEnum.ITEMS:
                    multilineOutput = _MultiValueDictionary.GetAllItems();
                    break;
                case CommandEnum.HELP:
                    output = "Valid Commands: KEYS, MEMBERS, ADD, ADDRANGE, REMOVE, REMOVEALL, CLEAR, KEYEXISTS, MEMBEREXISTS, ALLMEMBERS, ITEMS, HELP";
                    break;
            }

            if (multilineOutput != null)
            {
                output = _ConsoleIO.ToListResult(multilineOutput);
            }

            return output;
        }
    }
}
