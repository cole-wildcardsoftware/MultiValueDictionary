using MultiValueDictionaryCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class GetKeys : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void GetKeys_NoKeys()
        {
            //Arrange

            //Act
            var keys = MultiValueDictionary.GetKeys();

            //Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(0, keys.Count);
        }

        [TestMethod]
        public void GetKeys_ExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value } }
            });

            //Act
            var keys = MultiValueDictionary.GetKeys();

            //Assert
            Assert.IsNotNull(keys);

            Assert.AreEqual(1, keys.Count);
            Assert.AreEqual(key, keys[0]);
        }

        [TestMethod]
        public void GetKeys_None_RemovingExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value } }
            });
            MultiValueDictionary.RemoveValue(key, value);

            //Act
            var keys = MultiValueDictionary.GetKeys();

            //Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(0, keys.Count);
        }
    }
}
