
namespace Wonderland.Logic.SagePay
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web;

    internal static class SagePaySerializer
    {
        /// <summary>
        /// Serialize request for http posting to Sage Pay
        /// request posts use a query string format
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static string SerializeRequest(object request)
        {
            Type type = request.GetType();
            Dictionary<string, string> dictionary = new Dictionary<string,string>();

            foreach(PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod).Where(x => x.CanRead))
            {
                object rawValue = propertyInfo.GetValue(request, null);
                if(rawValue != null)
                {                   
                    string format = "{0}";

                    if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        // custom formatting for decimal (limit decimal value to two decimal places)
                        format = "{0:f2}";
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        // TODO: convert bools to 1 or 0
                        rawValue = Convert.ToInt32(rawValue);
                    }

                    string convertedValue = string.Format(CultureInfo.InvariantCulture, format, rawValue);

                    // TODO: exclude NotificationUrl from being url encoded ?
                    
                    convertedValue = HttpUtility.UrlEncode(convertedValue, Encoding.GetEncoding("ISO-8859-15"));

                    dictionary.Add(propertyInfo.Name, convertedValue);
                }
            }

            return string.Join("&", dictionary.Select(x => x.Key + "=" + x.Value).ToArray());
        }

        internal static string SerializeResponse(object response)
        {
            Type type = response.GetType();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod).Where(x => x.CanRead))
            {
                object rawValue = propertyInfo.GetValue(response, null);
                if (rawValue != null)
                {
                    string convertedValue = string.Format(CultureInfo.InvariantCulture, "{0}", rawValue);
                    
                    //convertedValue = HttpUtility.UrlEncode(convertedValue, Encoding.GetEncoding("ISO-8859-15"));

                    dictionary.Add(propertyInfo.Name, convertedValue);
                }
            }
            
            return string.Join(Environment.NewLine, dictionary.Select(x => x.Key + "=" + x.Value).ToArray());
        }

        /// <summary>
        /// DeserializeResponse data from Sage Pay back into an request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static T DeserializeResponse<T>(string response) where T : new() // TODO: add interface
        {
            T output = new T();
            Type type = typeof(T);

            string[] nameValuePairs = response.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string nameValuePair in nameValuePairs)
            {
                int index = nameValuePair.IndexOf('=');
                string name = nameValuePair.Substring(0, index);
                string value = nameValuePair.Substring(index + 1);

                PropertyInfo propertyInfo = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    object convertedValue = null;

                    if (propertyInfo.PropertyType == typeof(TransactionRegistrationStatus))
                    {
                        convertedValue = Enum.Parse(typeof(TransactionRegistrationStatus), value.Replace(" ", string.Empty)); // removing spaces to account for "OK REPEATED"
                    }
                    else
                    {                     
                        convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
                    }

                    propertyInfo.SetValue(output, convertedValue, null);
                }
            }

            return output;
        }
    }
}
