using MultiValueDictionaryCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class AddRange : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void AddRange_Single_NewKey()
        {
            //Arrange
            List<string> values = new List<string>() { "value01" };
            string key = "key01";

            //Act
            MultiValueDictionary.AddRange(key, values);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(1, all_members.Count);
            Assert.IsTrue(all_members.Contains(values[0]));
        }

        [TestMethod]
        public void AddRange_Multiple_NewKey()
        {
            //Arrange
            List<string> values = new List<string>() { "value01", "value02" };
            string key = "key01";

            //Act
            MultiValueDictionary.AddRange(key, values);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];
            Assert.AreEqual(2, all_members.Count);
            Assert.IsTrue(all_members.Contains(values[0]));
            Assert.IsTrue(all_members.Contains(values[1]));
        }

        [TestMethod]
        public void AddRange_Multiple_ExistingKey()
        {
            //Arrange
            List<string> values = new List<string>() { "value01", "value02" };
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key, new List<string> { "existing_value" } }
            });

            //Act
            MultiValueDictionary.AddRange(key, values);

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(1, all_keys.Count);
            Assert.AreEqual(key, all_keys[0]);

            var all_members = current_state[key];

            Assert.AreEqual(3, all_members.Count);
            Assert.IsTrue(all_members.Contains(values[0]));
            Assert.IsTrue(all_members.Contains(values[1]));
        }

        [TestMethod]
        public void AddRange_Error_AddingExistingMemberValue()
        {
            //Arrange
            List<string> values = new List<string>() { "value01", "value02" };
            string key = "key01";
            SetToState(new Dictionary<string, List<string>>
            {
                { key, new List<string> { "value02" } }
            });

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.AddRange(key, values);
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

        [TestMethod]
        public void AddRange_Error_DuplicateMemberValues()
        {
            //Arrange
            List<string> values = new List<string>() { "value01", "value01" };
            string key = "key01";

            //Act
            CommandException? exception = null;
            try
            {
                MultiValueDictionary.AddRange(key, values);
            }
            catch (CommandException ex)
            {
                exception = ex;
            }

            //Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(CommandException.DUPLICATE_VALUE, exception.Message);
            
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();
            Assert.AreEqual(0, all_keys.Count);
        }
    }
}
