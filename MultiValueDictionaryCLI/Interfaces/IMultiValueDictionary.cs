using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MultiValueDictionaryCLI.Interfaces
{
    public interface IMultiValueDictionary
    {
        public List<string> GetKeys();
        public List<string> GetMembers(string key);
        public void AddValue(string key, string value);
        public void AddRange(string key, List<string> values);
        public void RemoveValue(string key, string value);
        public void RemoveAll(string key);
        public void Clear();
        public bool KeyExists(string key);
        public bool MemberExists(string key, string value);
        public List<string> GetAllMembers();
        public List<string> GetAllItems();
    }
}
