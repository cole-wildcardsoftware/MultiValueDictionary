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
    public class ToListResult : ConsoleIOTestBase
    {
        [TestMethod]
        public void ToListResult_WithValues()
        {
            //Arrange
            List<string> outputs = new List<string>()
            {
                "line 1",
                "line 2",
                "line 3"
            };
            string exceptedOutput = $"1) line 1{Environment.NewLine}2) line 2{Environment.NewLine}3) line 3{Environment.NewLine}";

            //Act
            string result = ConsoleIO.ToListResult(outputs);

            //Assert
            Assert.AreEqual(exceptedOutput, result);
        }

        [TestMethod]
        public void ToListResult_Empty()
        {
            //Arrange
            List<string> outputs = new List<string>();
            string expectedOutput = "(empty set)";

            //Act
            string result = ConsoleIO.ToListResult(outputs);

            //Assert
            Assert.AreEqual(expectedOutput, result);
        }
    }
}
