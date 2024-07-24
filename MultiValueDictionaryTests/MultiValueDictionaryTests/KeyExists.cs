using MultiValueDictionaryCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class KeyExists : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void KeyExists_NoKeys()
        {
            //Arrange
            string key = "key";

            //Act
            bool keyExists = MultiValueDictionary.KeyExists(key);

            //Assert
            Assert.AreEqual(false, keyExists);
        }

        [TestMethod]
        public void KeyExists_ExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value } }
            });

            //Act
            bool keyExists = MultiValueDictionary.KeyExists(key);

            //Assert
            Assert.AreEqual(true, keyExists);
        }
    }
}
