using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryCLI.Models
{
    public class CommandException : Exception
    {
        public static string KEY_MISSING = "key does not exist.";
        public static string MEMBER_MISSING = "member does not exist.";
        public static string EXISTING_MEMBER = "member already exists for key.";
        public static string DUPLICATE_VALUE = "multiple members are the same.";
        public static string UNKNOWN_COMMAND = "command does not exist.";
        public static string INVALID_ARGUMENT_COUNT = "not enough arguments for command.";

        public CommandException(string message) : base(message) { }

    }
}
