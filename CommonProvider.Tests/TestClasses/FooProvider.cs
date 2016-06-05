using CommonProvider.Data;

namespace CommonProvider.Tests.TestClasses
{
    public class FooProvider : IFooProvider
    {
        public string WriteFoo()
        {
            return "";
        }

        public string Name { get; set; }

        public IProviderSettings Settings { get; set; }

        public string Group { get; set; }
    }
}
