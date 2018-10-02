using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AgentVI.Utils
{
    public static class StringUtils
    {
        public static String convertEnumToString(this Enum i_Enum)
        {
            String enumToString = Enum.GetName(i_Enum.GetType(), i_Enum);
            Regex enumToStringRegex = new Regex (@"
                                                (?<=[A-Z])(?=[A-Z][a-z]) |
                                                (?<=[^A-Z])(?=[A-Z]) |
                                                (?<=[A-Za-z])(?=[^A-Za-z])",
                                                RegexOptions.IgnorePatternWhitespace);

            return enumToStringRegex.Replace(enumToString, " ");
        }

        public static string GetStringValue(this Enum i_Enum)
        {
            Type type = i_Enum.GetType();
            FieldInfo fieldInfo = type.GetField(i_Enum.ToString());

            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}
