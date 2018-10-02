using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Utils
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }
        public StringValueAttribute(string i_StringValue)
        {
            StringValue = i_StringValue;
        }
    }
}
