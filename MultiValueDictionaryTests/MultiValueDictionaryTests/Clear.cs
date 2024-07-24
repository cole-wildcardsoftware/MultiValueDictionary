using MultiValueDictionaryCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class Clear : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void Clear_Empty()
        {
            //Arrange

            //Act
            MultiValueDictionary.Clear();

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(0, all_keys.Count);
        }

        [TestMethod]
        public void Clear_WithValues()
        {
            //Arrange
            SetToState(new Dictionary<string, List<string>>
            {
                { "key1", new List<string> { "value1" } },
                { "key2", new List<string> { "value2", "value3" } },
            });

            //Act
            MultiValueDictionary.Clear();

            //Assert
            var current_state = GetCurrentState();
            var all_keys = current_state.Keys.ToList();

            Assert.AreEqual(0, all_keys.Count);
        }
    }
}
