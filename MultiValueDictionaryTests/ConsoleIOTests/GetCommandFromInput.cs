using MultiValueDictionaryCLI;
using MultiValueDictionaryCLI.Models;
using MultiValueDictionaryTests.MultiValueDictionaryTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.ConsoleIOTests
{
    [TestClass]
    public class GetCommandFromInput : ConsoleIOTestBase
    {
        [DataTestMethod]
        [DataRow(CommandEnum.KEYS, "KEYS", DisplayName="GetCommandFromInput_KEYS")]
        [DataRow(CommandEnum.MEMBERS, "MEMBERS", DisplayName="GetCommandFromInput_MEMBERS")]
        [DataRow(CommandEnum.ADD, "ADD", DisplayName="GetCommandFromInput_ADD")]
        [DataRow(CommandEnum.ADDRANGE, "ADDRANGE", DisplayName="GetCommandFromInput_ADDRANGE")]
        [DataRow(CommandEnum.REMOVE, "REMOVE", DisplayName="GetCommandFromInput_REMOVE")]
        [DataRow(CommandEnum.REMOVEALL, "REMOVEALL", DisplayName="GetCommandFromInput_REMOVEALL")]
        [DataRow(CommandEnum.CLEAR, "CLEAR", DisplayName="GetCommandFromInput_CLEAR")]
        [DataRow(CommandEnum.KEYEXISTS, "KEYEXISTS", DisplayName="GetCommandFromInput_KEYEXISTS")]
        [DataRow(CommandEnum.MEMBEREXISTS, "MEMBEREXISTS", DisplayName="GetCommandFromInput_MEMBEREXISTS")]
        [DataRow(CommandEnum.ALLMEMBERS, "ALLMEMBERS", DisplayName="GetCommandFromInput_ALLMEMBERS")]
        [DataRow(CommandEnum.ITEMS, "ITEMS", DisplayName="GetCommandFromInput_ITEMS")]
        [DataRow(CommandEnum.HELP, "HELP", DisplayName="GetCommandFromInput_HELP")]
        public void GetCommandFromInput_Valid(CommandEnum commandEnum, string input)
        {
            //Arrange

            //Act
            CommandEnum result = ConsoleIO.GetCommandFromInput(input);

            //Assert
            Assert.AreEqual(commandEnum, result);
        }

        [TestMethod]
        public void GetCommandFromInput_Invalid()
        {
            //Arrange
            string input = "Uknown command";

            //Act
            CommandException? exception = null;
            try
            {
                CommandEnum result = ConsoleIO.GetCommandFromInput(input);
            }
            catch(CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.UNKNOWN_COMMAND, exception.Message);
        }
    }
}
