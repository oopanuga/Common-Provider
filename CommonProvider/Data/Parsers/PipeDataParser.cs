using CommonProvider.Exceptions;
using System;
using System.Reflection;

namespace CommonProvider.Data.Parsers
{
	/// <summary>
	/// Represents the default data parser and parses a piped delimited string of data.
	/// </summary>
	public class PipeDataParser : IDataParser
	{
        /// <summary>
        /// Parses a string of data to the specified type.
        /// </summary>
        /// <typeparam name="T">The generic type to parse the data to.</typeparam>
        /// <param name="data">The string of data to be parsed.</param>
        /// <returns>The resultant object after parsing.</returns>
        public T Parse<T>(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return default(T);
                }

                Type type = typeof(T);

                var instance = System.Activator.CreateInstance(type);
                PropertyInfo[] properties = type.GetProperties();

                string[] splitted = data.Split('|');
                foreach (var property in properties)
                {
                    foreach (var s in splitted)
                    {
                        string[] valuePair = s.Split(new char[] { ':' }, 
                            StringSplitOptions.RemoveEmptyEntries);

                        string fieldName = valuePair[0].Trim();
                        string fieldValue = valuePair[1].Trim();

                        if (fieldName.Equals(property.Name, 
                            StringComparison.OrdinalIgnoreCase))
                        {
                            property.SetValue(
                                instance,
                                Convert.ChangeType(fieldValue, 
                                property.PropertyType),
                                null);

                            break;
                        }
                    }
                }

                return (T)instance;
            }
            catch (Exception ex)
            {
                throw new DataParseException("Error parsing data", ex);
            }
        }
    }
}
