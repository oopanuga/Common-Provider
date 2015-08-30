
namespace CommonProvider.Data.Parsers
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
}
