using MultiValueDictionaryCLI.Functionality;
using MultiValueDictionaryCLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.ConsoleIOTests
{
    public class ConsoleIOTestBase : TestBase
    {
        public IConsoleIO ConsoleIO { get; set; }

        public ConsoleIOTestBase()
        {
            ConsoleIO = new ConsoleIO();
        }
    }
}
