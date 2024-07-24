using MultiValueDictionaryCLI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class GetMembers : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void GetMembers_ExistingKeyWithMembers()
        {
            //Arrange
            string value1 = "value01";
            string value2 = "value02";
            string key = "key01";

            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value1, value2 } }
            });

            //Act
            var values = MultiValueDictionary.GetMembers(key);

            //Assert
            Assert.IsNotNull(values);

            Assert.AreEqual(2, values.Count);
            Assert.IsTrue(values.Contains(value1));
            Assert.IsTrue(values.Contains(value2));
        }

        [TestMethod]
        public void GetMembers_Error_NoKeys()
        {
            //Arrange
            string key = "key";

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.GetMembers(key);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.KEY_MISSING, exception.Message);
        }

        [TestMethod]
        public void GetMembers_Error_RemovingExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value } }
            });
            MultiValueDictionary.RemoveValue(key, value);

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.GetMembers(key);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.KEY_MISSING, exception.Message);
        }
    }
}
