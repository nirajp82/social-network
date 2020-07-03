using System;

namespace SocialNetwork.Util
{
    public class HelperFunc
    {
        #region Constructor
        private HelperFunc() { }
        #endregion

        public static DateTime GetCurrentDateTime() => DateTime.Now;


        public static T ChangeType<T>(object value)
        {
            var toType = typeof(T);

            if (value == null || value == DBNull.Value || value.Equals(default(T)))
                return default;

            if (value is string)
            {
                if (toType == typeof(Guid))
                    return ChangeType<T>(new Guid(Convert.ToString(value)));
                else if ((string)value == string.Empty && toType != typeof(string))
                    return ChangeType<T>(null);
                else if (toType.IsValueType && toType.IsEnum && value is string && !string.IsNullOrWhiteSpace(Convert.ToString(value)))
                {
                    object enumValue;
                    if (Enum.TryParse(toType, Convert.ToString(value), true, out enumValue))
                        return (T)enumValue;
                }
            }
            else
            {
                if (typeof(T) == typeof(string))
                    return ChangeType<T>(Convert.ToString(value));
            }

            if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
                toType = Nullable.GetUnderlyingType(toType);

            bool canConvert = toType is IConvertible || (toType.IsValueType && !toType.IsEnum);
            if (canConvert)
                return (T)Convert.ChangeType(value.ToString(), toType);
            return (T)value;
        }
    }
}
