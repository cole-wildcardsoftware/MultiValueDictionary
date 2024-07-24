using MultiValueDictionaryCLI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class GetAllItems : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void GetAllItems_ExistingKeyWithMembers()
        {
            //Arrange
            string value1 = "value01";
            string value2 = "value02";
            string value3 = "value03";
            string key = "key01";
            string key2 = "key02";
            List<string> expected_result = new List<string>()
            {
                key + ": " + value1,
                key + ": " + value2,
                key2 + ": " + value1,
                key2 + ": " + value3
            };
            expected_result.Sort();

            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value1, value2 } },
                { key2, new List<string> { value1, value3 } }
            });

            //Act
            var values = MultiValueDictionary.GetAllItems();

            //Assert
            Assert.AreEqual(4, values.Count);
            values.Sort();
            expected_result.Sort();

            Assert.IsTrue(values.SequenceEqual(expected_result));
        }

        [TestMethod]
        public void GetAllItems_NoEntries()
        {
            //Arrange

            //Act
            var values = MultiValueDictionary.GetAllItems();

            //Assert
            Assert.AreEqual(0, values.Count);
        }
    }
}
