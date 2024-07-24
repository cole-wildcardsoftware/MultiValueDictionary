using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionaryCLI.Models
{
    public enum CommandEnum
    {
        KEYS,
        MEMBERS,
        ADD,
        ADDRANGE,
        REMOVE,
        REMOVEALL,
        CLEAR,
        KEYEXISTS,
        MEMBEREXISTS,
        ALLMEMBERS,
        ITEMS,
        HELP
    }
}
