using MultiValueDictionaryCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class RemoveAll : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void RemoveAll_Error_RemoveMissingKey()
        {
            //Arrange
            string key = "key01";

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.RemoveAll(key);
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
        public void RemoveAll_ExistingMember()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            string existing_value = "existing_value";
            string existing_key = "existing_key";

            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value, existing_value } },
                { existing_key, new List<string> { value } },
            });

            //Act
            MultiValueDictionary.RemoveAll(key);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(existing_key, all_keys[0]);
        }
    }
}
