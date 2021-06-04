using System.Net;
using System.Text.RegularExpressions;
using Abp.Extensions;
using System.Linq;

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
                return false;

            var arr = value.Split('.');
            if (arr.Length != 4)
                return false;

            foreach (var part in arr)
            {
                if (!int.TryParse(part, out var partValue)) 
                    return false;

                if (partValue < 0 || partValue > 255)
                    return false;
            }

            return true;
        }
    }
}
