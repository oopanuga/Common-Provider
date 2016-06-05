using NUnit.Framework;
using System;
using System.Linq;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ProviderTypeLoaderTests
    {
        [Category("ProviderTypeLoader.Load")]
        public class Load
        {
            [Test]
            public void Should_load_provider_types_from_assemblies_in_the_specified_directory()
            {
                var loader = new ProviderTypeLoader(Environment.CurrentDirectory);
                var providerTypes = loader.Load();

                Assert.That(providerTypes.Count(), Is.EqualTo(2));
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_doesnt_exist()
            {
                var loader = new ProviderTypeLoader("z:\\somefakedirectory\\");
                loader.Load();
            }

            [TestCase("")]
            [TestCase(null)]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_not_set(string assemblyDirectory)
            {
                var loader = new ProviderTypeLoader(assemblyDirectory);
                loader.Load();
            }
        }
    }
}
