using InnoviApiProxy;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AgentVI.Utils
{
    public static class StringUtils
    {
        private static Dictionary<SensorEvent.eBehaviorType, string> EnumDescriptions = new Dictionary<SensorEvent.eBehaviorType, string>()
        {
            {SensorEvent.eBehaviorType.Anomaly,     "Anomaly" },
            {SensorEvent.eBehaviorType.Crossing,    "Crossing a Line" },
            {SensorEvent.eBehaviorType.Grouping,    "Groupin" },
            {SensorEvent.eBehaviorType.Mask,        "Ignore activity mask" },
            {SensorEvent.eBehaviorType.Moving,      "Moving in an area" },
            {SensorEvent.eBehaviorType.Mrd,         "MRD" },
            {SensorEvent.eBehaviorType.Occupancy,   "Occupancy" },
            {SensorEvent.eBehaviorType.Stopped,     "Stopped Vehicle" },
            {SensorEvent.eBehaviorType.Undefined,   "Underfined" },
            {SensorEvent.eBehaviorType.Vqm,         "VQM" }
        };

        public static string BehaviorToString(this SensorEvent.eBehaviorType i_Enum)
        {
            return EnumDescriptions[i_Enum];
        }

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
    }
}
