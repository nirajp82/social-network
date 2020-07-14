using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace SocialNetwork.Util
{
    public class HelperFunc
    {
        #region Constructor
        private HelperFunc() { }
        #endregion


        #region Public Methods
        public static DateTime GetCurrentDateTime() => DateTime.Now;

        public static T FindValueFromJson<T>(string jsonString, string propertyName)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default;
            else if (!jsonString.Contains(propertyName))
                return default;

            using JsonDocument jsonDoc = JsonDocument.Parse(jsonString);
            if (jsonDoc.RootElement.TryGetProperty(propertyName, out JsonElement jsonElement))
                return HelperFunc.ChangeType<T>(jsonElement.GetString());
            return default;
        }

        public static T ChangeType<T>(object value)
        {
            var toType = typeof(T);

            if (value == null || value == DBNull.Value || value.Equals(default(T)))
                return default;

            if (value is string)
            {
                if (toType == typeof(Guid))
                {
                    if (!string.IsNullOrWhiteSpace(value.ToString()))
                        return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.ToString());
                    return default;
                }
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

        public static void SetValue(object objModel, string propertyName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && objModel != null && value != double.NaN.ToString())
            {
                var propertyInfo = objModel.GetType().GetProperty(propertyName, BindingFlags.GetProperty |
                                                              BindingFlags.IgnoreCase |
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public);
                if (propertyInfo != null)
                {
                    Type toType = propertyInfo.PropertyType;
                    if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        toType = Nullable.GetUnderlyingType(toType);
                    }
                    propertyInfo.SetValue(objModel, Convert.ChangeType(value, toType));
                }
            }
        }

        public static bool IsEqualString(string string1, string string2)
        {
            if (string1 == default && string2 == default)
                return true;
            if (string1 == string.Empty && string2 == string.Empty)
                return true;
            if (!string.IsNullOrWhiteSpace(string1) && !string.IsNullOrWhiteSpace(string2))
                return string.Compare(TrimString(string1), TrimString(string2), true) == 0;
            return false;
        }

        public static string ReplaceValue(string value, string oldValue, string newValue)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.Replace(oldValue, newValue) : value;
        }

        public static string TrimString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.Trim() : value;
        }

        public static IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> source, int size)
        {
            var partition = new List<T>(size);
            var counter = 0;
            if (source != null)
            {
                using var enumerator = source?.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    partition.Add(enumerator.Current);
                    counter++;
                    if (counter % size == 0)
                    {
                        yield return partition.ToList();
                        partition.Clear();
                        counter = 0;
                    }
                }

                if (counter != 0)
                    yield return partition;
            }
        }
        #endregion
    }
}
