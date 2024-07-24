using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MultiValueDictionaryCLI.Interfaces;
using MultiValueDictionaryCLI.Models;

namespace MultiValueDictionaryCLI.Functionality
{
    public class MultiValueDictionary : IMultiValueDictionary
    {
        private Dictionary<string, List<string>> _dictionary;

        public MultiValueDictionary()
        {
            _dictionary = new Dictionary<string, List<string>>();
        }

        // Return a list of all keys with non-empty members
        public List<string> GetKeys()
        {
            return _dictionary.Keys.Where(x => _dictionary[x].Count > 0).ToList();
        }

        // Return a list of all members for a given key
        // throws CommandException if the key does not exist
        public List<string> GetMembers(string key)
        {
            if (KeyExists(key) == false)
            {
                throw new CommandException(CommandException.KEY_MISSING);
            }

            return _dictionary[key];
        }

        // Adds a single key value pair to the dictionary
        // throws CommandException if the key value pair already exists
        public void AddValue(string key, string value)
        {
            if (_dictionary.ContainsKey(key) == false)
            {
                _dictionary[key] = new List<string>();
            }

            if (_dictionary[key].Contains(value))
            {
                throw new CommandException(CommandException.EXISTING_MEMBER);
            }

            _dictionary[key].Add(value);
        }

        // Adds multiple key value pair to the dictionary for the same key
        // throws CommandException if a key value pair already exists
        // throws CommandException if duplicate values exist in provided input
        public void AddRange(string key, List<string> values)
        {
            if (_dictionary.ContainsKey(key) == false)
            {
                _dictionary[key] = new List<string>();
            }

            if (values.Distinct().Count() < values.Count())
            {
                throw new CommandException(CommandException.DUPLICATE_VALUE);
            }

            foreach (var value in values)
            {
                if (_dictionary[key].Contains(value))
                {
                    throw new CommandException(CommandException.EXISTING_MEMBER);
                }
            }

            _dictionary[key].AddRange(values);
        }

        // Removes a key value pair
        // throws CommandException if a key or member does not exist
        public void RemoveValue(string key, string value)
        {
            if (KeyExists(key) == false)
            {
                throw new CommandException(CommandException.KEY_MISSING);
            }

            if (_dictionary[key].Contains(value) == false)
            {
                throw new CommandException(CommandException.MEMBER_MISSING);
            }

            _dictionary[key].Remove(value);
        }

        // Removes a key and all its members
        // throws CommandException if a key does not exist
        public void RemoveAll(string key)
        {
            if (KeyExists(key) == false)
            {
                throw new CommandException(CommandException.KEY_MISSING);
            }


            _dictionary[key] = new List<string>();
        }

        // Clear all dictionary entries
        public void Clear()
        {
            _dictionary = new Dictionary<string, List<string>>();
        }

        // Returns whether a key exists and has any valid members
        public bool KeyExists(string key)
        {
            return _dictionary.ContainsKey(key) && _dictionary[key].Count > 0;
        }

        // Returns whether a key value pair exists
        // throws CommandException if key does not exist
        public bool MemberExists(string key, string value)
        {
            if (KeyExists(key) == false)
            {
                return false;
            }

            return _dictionary[key].Contains(value);
        }

        // Returns a list of all members for all keys in the dictionary
        // Order not gaurenteed
        public List<string> GetAllMembers()
        {
            List<string> result = new List<string>();
            foreach (var key in GetKeys())
            {
                result.AddRange(_dictionary[key]);
            }

            return result;
        }

        // Returns a list of all keys and members concatanated into a string
        // Order not gaurenteed
        public List<string> GetAllItems()
        {
            List<string> result = new List<string>();
            foreach (var key in GetKeys())
            {
                result.AddRange(_dictionary[key].Select(x => key + ": " + x));
            }

            return result;
        }
    }
}
