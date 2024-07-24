using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiValueDictionaryCLI.Models;

namespace MultiValueDictionaryCLI.Interfaces
{
    public interface IConsoleIO
    {
        CommandEnum GetCommandFromInput(string input);
        string ToListResult(List<string> output);
    }
}
