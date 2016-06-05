using System;
using System.Reflection;
using CommonProvider.Exceptions;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a data parser.
    /// </summary>
    public interface IDataParser
    {
        /// <summary>
        /// Parses a string of data to the specified generic type.
        /// </summary>
        /// <typeparam name="T">The generic type to parse the data to.</typeparam>
        /// <param name="data">The string of data to be parsed.</param>
        /// <returns>The resultant object after parsing.</returns>
        T Parse<T>(string data);
    }

	/// <summary>
	/// Represents the default data parser. Its used to parse a pipe delimited string of data.
	/// </summary>
    public class PipeDelimitedDataParser : IDataParser
	{
        /// <summary>
        /// Parses a string of data to the specified type.
        /// </summary>
        /// <typeparam name="T">The generic type to parse the data to.</typeparam>
        /// <param name="settings">The string of data to be parsed.</param>
        /// <returns>The resultant object after parsing.</returns>
        public T Parse<T>(string settings)
        {
            try
            {
                if (string.IsNullOrEmpty(settings))
                {
                    return default(T);
                }

                Type type = typeof(T);

                var instance = Activator.CreateInstance(type);
                PropertyInfo[] properties = type.GetProperties();

                string[] splitted = settings.Split('|');
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
