using System;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class StringUtils
    {
        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return String.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        public static int ToInt(this string value)
        {
            return StringUtils.ToInt(value, 0);
        }

        public static int ToInt(this string s, int defaultValue)
        {
            if (s != null)
            {
                if (Int32.TryParse(s, out int value))
                {
                    return value;
                }
            }

            return defaultValue;
        }

        public static bool ToBool(this string value)
        {
            return StringUtils.ToBool(value, false);
        }

        public static bool ToBool(this string s, bool defaultValue)
        {
            if (s != null)
            {
                if (Boolean.TryParse(s, out bool value))
                {
                    return value;
                }
            }

            return defaultValue;
        }
    }
}
