using Moq;
using MultiValueDictionaryCLI.Functionality;
using MultiValueDictionaryCLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.CommandShellTests
{
    public class CommandShellTestBase : TestBase
    {
        public Mock<IConsoleIO> _ConsoleIO { get; protected set; }
        public Mock<IMultiValueDictionary> _MultiValueDictionary { get; protected set; }
        public ICommandShell CommandShell { get; set; }

        public CommandShellTestBase()
        {
            _ConsoleIO = new Mock<IConsoleIO>();
            _MultiValueDictionary = new Mock<IMultiValueDictionary>();

            CommandShell = new CommandShell(_MultiValueDictionary.Object, _ConsoleIO.Object);
        }
    }
}
