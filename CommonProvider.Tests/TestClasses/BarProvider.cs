
namespace CommonProvider.Tests.TestClasses
{
    public class BarProvider : IBarProvider
    {
        public string WriteBar()
        {
            return "";
        }

        public string Name { get; set; }

        public Data.ISettings Settings { get; set; }

        public string Group { get; set; }
    }
}
