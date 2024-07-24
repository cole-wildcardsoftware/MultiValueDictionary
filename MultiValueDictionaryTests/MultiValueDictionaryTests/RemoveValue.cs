using MultiValueDictionaryCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class RemoveValue : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void RemoveValue_Error_RemoveMissingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.RemoveValue(key, value);
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
        public void RemoveValue_Error_RemoveMissingValue()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key,  new List<string> { "existing_value" } }
            });

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.RemoveValue(key, value);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.MEMBER_MISSING, exception.Message);
        }

        [TestMethod]
        public void RemoveValue_ExistingMember()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key,  new List<string> { value, "existing_value" } }
            });

            //Act
            MultiValueDictionary.RemoveValue(key, value);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(1, all_members.Count);
            Assert.AreNotEqual(value, all_members[0]);
        }

        [TestMethod]
        public void RemoveValue_LastExistingMember()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key, new List<string> { value } }
            });

            //Act
            MultiValueDictionary.RemoveValue(key, value);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(0, all_keys.Count);
        }
    }
}
