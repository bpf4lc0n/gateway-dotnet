using System.Net;
using System.Text.RegularExpressions;
using Abp.Extensions;

namespace Gateway.Validation
{
    public static class ValidationHelper
    {
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static bool IsEmail(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            var regex = new Regex(EmailRegex);
            return regex.IsMatch(value);
        }

        public static bool IsIpv4(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return IPAddress.TryParse(value, out var ip);
        }
    }
}
