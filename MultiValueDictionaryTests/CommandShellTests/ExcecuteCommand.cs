using Moq;
using MultiValueDictionaryCLI;
using MultiValueDictionaryCLI.Models;
using MultiValueDictionaryTests.MultiValueDictionaryTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.CommandShellTests
{
    [TestClass]
    public class ExcecuteCommand : CommandShellTestBase
    {
        public static string arg1 = "arg1";
        public static string arg2 = "arg2";
        public static string arg3 = "arg3";

        public string GetInputWithArgs(string commandName, int argumentCount)
        {
            if (argumentCount == 0)
            {
                return commandName;
            }
            else if (argumentCount == 1)
            {
                return commandName + " " + arg1;
            }
            else if (argumentCount == 2)
            {
                return commandName + " " + arg1 + " " + arg2;
            }
            else
            {
                return commandName + " " + arg1 + " " + arg2 + " " + arg3;
            }
        }

        [DataTestMethod]
        [DataRow(CommandEnum.MEMBERS, "MEMBERS", DisplayName="ExecuteCommand_Error_InvalidArgCount_MEMBERS")]
        [DataRow(CommandEnum.ADD, "ADD key", DisplayName="ExecuteCommand_Error_InvalidArgCount_ADD")]
        [DataRow(CommandEnum.ADDRANGE, "ADDRANGE key", DisplayName="ExecuteCommand_Error_InvalidArgCount_ADDRANGE")]
        [DataRow(CommandEnum.REMOVE, "REMOVE key", DisplayName="ExecuteCommand_Error_InvalidArgCount_REMOVE")]
        [DataRow(CommandEnum.REMOVEALL, "REMOVEALL", DisplayName="ExecuteCommand_Error_InvalidArgCount_REMOVEALL")]
        [DataRow(CommandEnum.KEYEXISTS, "KEYEXISTS", DisplayName="ExecuteCommand_Error_InvalidArgCount_KEYEXISTS")]
        [DataRow(CommandEnum.MEMBEREXISTS, "MEMBEREXISTS key", DisplayName="ExecuteCommand_Error_InvalidArgCount_MEMBEREXISTS")]
        public void ExecuteCommand_Error_InvalidArgCount(CommandEnum commandEnum, string input)
        {
            //Arrange
            string commandName = input.Split(' ')[0];
            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(commandEnum);

            //Act
            CommandException? exception = null;
            try
            {
                string output = CommandShell.ExecuteCommand(input);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);

            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.INVALID_ARGUMENT_COUNT, exception.Message);
        }

        [TestMethod]
        public void ExecuteCommand_KEYS()
        {
            //Arrange
            string commandName = "KEYS";
            string input = GetInputWithArgs(commandName, 0);
            List<string> multiLineOutput = new List<string>() { "line1" };
            string listResult = "result";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.KEYS);
            _MultiValueDictionary.Setup(x => x.GetKeys()).Returns(multiLineOutput);

            _ConsoleIO.Setup(x => x.ToListResult(multiLineOutput)).Returns(listResult);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.GetKeys(), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(multiLineOutput), Times.Once);

            Assert.AreEqual(listResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_MEMBERS()
        {
            //Arrange
            string commandName = "MEMBERS";
            string input = GetInputWithArgs(commandName, 1);
            List<string> multiLineOutput = new List<string>() { "line1" };
            string listResult = "result";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.MEMBERS);
            _MultiValueDictionary.Setup(x => x.GetMembers(arg1)).Returns(multiLineOutput);

            _ConsoleIO.Setup(x => x.ToListResult(multiLineOutput)).Returns(listResult);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.GetMembers(arg1), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(multiLineOutput), Times.Once);

            Assert.AreEqual(listResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_ALLMEMBERS()
        {
            //Arrange
            string commandName = "ALLMEMBERS";
            string input = GetInputWithArgs(commandName, 0);
            List<string> multiLineOutput = new List<string>() { "line1" };
            string listResult = "result";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.ALLMEMBERS);
            _MultiValueDictionary.Setup(x => x.GetAllMembers()).Returns(multiLineOutput);

            _ConsoleIO.Setup(x => x.ToListResult(multiLineOutput)).Returns(listResult);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.GetAllMembers(), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(multiLineOutput), Times.Once);

            Assert.AreEqual(listResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_ITEMS()
        {
            //Arrange
            string commandName = "ITEMS";
            string input = GetInputWithArgs(commandName, 0);
            List<string> multiLineOutput = new List<string>() { "line1" };
            string listResult = "result";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.ITEMS);
            _MultiValueDictionary.Setup(x => x.GetAllItems()).Returns(multiLineOutput);

            _ConsoleIO.Setup(x => x.ToListResult(multiLineOutput)).Returns(listResult);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.GetAllItems(), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(multiLineOutput), Times.Once);

            Assert.AreEqual(listResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_ADD()
        {
            //Arrange
            string commandName = "ADD";
            string input = GetInputWithArgs(commandName, 2);
            string expectedResult = "Added";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.ADD);
            _MultiValueDictionary.Setup(x => x.AddValue(arg1, arg2));

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.AddValue(arg1, arg2), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_ADDRANGE()
        {
            //Arrange
            string commandName = "ADDRANGE";
            string input = GetInputWithArgs(commandName, 3);
            string expectedResult = "Added";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.ADDRANGE);
            _MultiValueDictionary.Setup(x => x.AddRange(arg1, It.Is<List<string>>(x => x.Contains(arg2) && x.Contains(arg3) && x.Count == 2)));

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.AddRange(arg1, It.Is<List<string>>(x => x.Contains(arg2) && x.Contains(arg3) && x.Count == 2)), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_REMOVE()
        {
            //Arrange
            string commandName = "REMOVE";
            string input = GetInputWithArgs(commandName, 2);
            string expectedResult = "Removed";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.REMOVE);
            _MultiValueDictionary.Setup(x => x.RemoveValue(arg1, arg2));

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.RemoveValue(arg1, arg2), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_REMOVEALL()
        {
            //Arrange
            string commandName = "REMOVEALL";
            string input = GetInputWithArgs(commandName, 1);
            string expectedResult = "Removed";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.REMOVEALL);
            _MultiValueDictionary.Setup(x => x.RemoveAll(arg1));

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.RemoveAll(arg1), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_CLEAR()
        {
            //Arrange
            string commandName = "CLEAR";
            string input = GetInputWithArgs(commandName, 0);
            string expectedResult = "Cleared";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.CLEAR);
            _MultiValueDictionary.Setup(x => x.Clear());

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.Clear(), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ExecuteCommand_HELP()
        {
            //Arrange
            string commandName = "HELP";
            string input = GetInputWithArgs(commandName, 0);
            string expectedResult = "Valid Commands: KEYS, MEMBERS, ADD, ADDRANGE, REMOVE, REMOVEALL, CLEAR, KEYEXISTS, MEMBEREXISTS, ALLMEMBERS, ITEMS, HELP";

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.HELP);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow(true, DisplayName = "ExecuteCommand_KEYEXISTS_true")]
        [DataRow(false, DisplayName = "ExecuteCommand_KEYEXISTS_false")]
        public void ExecuteCommand_KEYEXISTS(bool exists)
        {
            //Arrange
            string commandName = "KEYEXISTS";
            string input = GetInputWithArgs(commandName, 1);
            string expectedResult = exists.ToString();

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.KEYEXISTS);
            _MultiValueDictionary.Setup(x => x.KeyExists(arg1)).Returns(exists);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.KeyExists(arg1), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow(true, DisplayName = "ExecuteCommand_MEMBEREXISTS_true")]
        [DataRow(false, DisplayName = "ExecuteCommand_MEMBEREXISTS_false")]
        public void ExecuteCommand_MEMBEREXISTS(bool exists)
        {
            //Arrange
            string commandName = "MEMBEREXISTS";
            string input = GetInputWithArgs(commandName, 2);
            string expectedResult = exists.ToString();

            _ConsoleIO.Setup(x => x.GetCommandFromInput(commandName)).Returns(CommandEnum.MEMBEREXISTS);
            _MultiValueDictionary.Setup(x => x.MemberExists(arg1, arg2)).Returns(exists);

            //Act
            var result = CommandShell.ExecuteCommand(input);

            //Assert
            _ConsoleIO.Verify(x => x.GetCommandFromInput(commandName), Times.Once);
            _MultiValueDictionary.Verify(x => x.MemberExists(arg1, arg2), Times.Once);
            _ConsoleIO.Verify(x => x.ToListResult(It.IsAny<List<string>>()), Times.Never);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
