using MultiValueDictionaryCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    [TestClass]
    public class MemberExists : MultiValueDictionaryTestBase
    {
        [TestMethod]
        public void MemberExists_MissingValue()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { "existing_value" } }
            });

            //Act
            bool memberExists = MultiValueDictionary.MemberExists(key, value);

            //Assert
            Assert.AreEqual(false, memberExists);
        }

        [TestMethod]
        public void MemberExists_ExistingKey()
        {
            //Arrange
            string value = "value01";
            string key = "key01";
            SetToState(new Dictionary<string, List<string>> {
                { key, new List<string> { value } }
            });

            //Act
            bool memberExists = MultiValueDictionary.MemberExists(key, value);

            //Assert
            Assert.AreEqual(true, memberExists);
        }

        [TestMethod]
        public void MemberExists_MissingKey()
        {
            //Arrange
            string key = "key";
            string value = "value";

            //Act
            var memberExists = MultiValueDictionary.MemberExists(key, value);

            //Assert
            Assert.AreEqual(false, memberExists);
        }
    }
}
