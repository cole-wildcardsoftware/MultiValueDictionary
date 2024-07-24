using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiValueDictionaryCLI;
using MultiValueDictionaryCLI.Functionality;
using MultiValueDictionaryCLI.Interfaces;

namespace MultiValueDictionaryTests.MultiValueDictionaryTests
{
    public class MultiValueDictionaryTestBase : TestBase
    {
        public IMultiValueDictionary MultiValueDictionary { get; set; }

        public MultiValueDictionaryTestBase()
        {
            MultiValueDictionary = new MultiValueDictionary();
        }

        public void SetToState(Dictionary<string, List<string>> state)
        {
            MultiValueDictionary.Clear();
            foreach (var key in state.Keys)
            {
                foreach (var value in state[key])
                {
                    MultiValueDictionary.AddValue(key, value);
                }
            }
        }

        public Dictionary<string, List<string>> GetCurrentState()
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            var keys = MultiValueDictionary.GetKeys();
            foreach (var key in keys)
            {
                var members = MultiValueDictionary.GetMembers(key);
                result.Add(key, members);
            }
            return result;
        }
    }
}
