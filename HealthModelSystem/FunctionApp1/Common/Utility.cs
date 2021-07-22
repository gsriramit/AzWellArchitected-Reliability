using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebComponentHealthSystem.Common
{
    internal class Utility
    {        

        /// <summary>
        /// Utility function to match the entire resourceID string and extract the a) ResourceType and b) ResourceName
        /// </summary>
        /// <param name="resourceString"></param>
        /// <returns></returns>
        internal static List<string> GetResourceDetails(string resourceString)
        {
            List<string> matchedValues = new List<string>();
            Regex regex = new Regex(resourceMatchExpression, RegexOptions.IgnoreCase);
            Match resourceStringMatch = regex.Match(resourceString);
            if (resourceStringMatch.Success && resourceStringMatch.Groups.Count > 0)
            {
                for (var counter = 0; counter < resourceStringMatch.Groups.Count; counter++)
                {
                    matchedValues.Add(resourceStringMatch.Groups[counter].Value);
                }
            }
            return matchedValues;
        }

        private static string resourceMatchExpression = @"^\/subscriptions\/[a-zA-Z\S\d]*\/resourcegroups\/[a-zA-Z\S\d]*\/providers\/microsoft.[a-z]*\/([a-z]*)\/([a-zA-Z\S\d]*)$";

        internal static string ResolvePlatformHealthStatus(string monitorCondition, string alertSeverity)
        {
            if (monitorCondition.ToLower() == GlobalConstants.PLATFORMALERTFIREDSTATUS.ToLower())
            {
                var alertSeverityVal = Utility.GetAlertSeverityLevel(alertSeverity);
                if (alertSeverityVal == GlobalConstants.CRITICALALERTSEVERITY ||
                    alertSeverityVal == GlobalConstants.ERRORALERTSEVERITY ||
                    alertSeverityVal == GlobalConstants.WARNINGALERTSEVERITY)
                    return GlobalConstants.PLATFORMUNHEALTHYSTATUS;

            }
            else if (monitorCondition.ToLower() == GlobalConstants.PLATFORMALERTRESOLVEDSTATUS.ToLower())
                return GlobalConstants.PLATFORMHEALTHYSTATUS;

            return GlobalConstants.PLATFORMHEALTHYSTATUS;

        }


        internal static string GetAlertSeverityLevel(int levelValue)
        {
            return Enum.GetName(typeof(AlertSeverityLevel), levelValue);
        }

        internal static string GetAlertSeverityLevel(string sevLevel)
        {
            char[] severityCharArr = sevLevel.ToCharArray();
            int levelValue = Convert.ToInt16(severityCharArr[severityCharArr.Length-1].ToString());
            return Enum.GetName(typeof(AlertSeverityLevel), levelValue);
        }

        internal static DateTime ParseEventDateTimeString(string eventTime)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime parsedDate;
            bool parseStatus = true;
            parseStatus = DateTime.TryParseExact(eventTime, twelveHourTimeFormat, provider,
                                                    DateTimeStyles.AllowInnerWhite,out parsedDate);
            if(!parseStatus)
                parseStatus = DateTime.TryParseExact(eventTime, twentyHourTimeFormat, provider,
                                                    DateTimeStyles.AllowInnerWhite, out parsedDate);
            if (!parseStatus)
                parsedDate = DateTime.Parse(eventTime);

            return parsedDate;
        }

        private const string twelveHourTimeFormat = "M/dd/yyyy h:mm:ss tt";
        private const string twentyHourTimeFormat = "M/dd/yyyy h:mm:ss";

    }

    

    public enum AlertSeverityLevel
    {
        Critical,
        Error,
        Warning,
        Informational,
        Verbose,
    }
}
