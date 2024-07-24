using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryCLI.Interfaces
{
    public interface ICommandShell
    {
        string ExecuteCommand(string input);
    }
}
