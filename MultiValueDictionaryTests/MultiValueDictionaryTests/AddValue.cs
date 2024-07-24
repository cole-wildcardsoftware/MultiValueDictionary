using MultiValueDictionaryCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class AddValue : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void AddValue_NewKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";

            //Act
            MultiValueDictionary.AddValue(key, value);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(1, all_members.Count);
            Assert.IsTrue(all_members.Contains(value));
        }

        [TestMethod]
        public void AddValue_ExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";

            SetToState(new Dictionary<string, List<string>>
            {
                { key, new List<string> { "existing_value" } }
            });

            //Act
            MultiValueDictionary.AddValue(key, value);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(2, all_members.Count);
            Assert.IsTrue(all_members.Contains(value));
        }

        [TestMethod]
        public void AddValue_Error_AddingExistingMemberValue()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key, new List<string> { value } }
            });

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.AddValue(key, value);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.EXISTING_MEMBER, exception.Message);

            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();
            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(1, all_members.Count);
        }
    }
}
