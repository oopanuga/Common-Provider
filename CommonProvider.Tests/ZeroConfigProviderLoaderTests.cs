using CommonProvider.ProviderLoaders;
using NUnit.Framework;
using System;
using System.Linq;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ZeroConfigProviderLoaderTests
    {
        [Category("ZeroConfigProviderLoader.Load")]
        public class Load
        {
            [Test]
            public void Should_load_providers_from_classes_of_type_izeroconfigprovider()
            {
                var loader = new ZeroConfigProviderLoader(Environment.CurrentDirectory);
                var providerTypes = loader.Load();

                Assert.That(providerTypes.Count(), Is.EqualTo(2));
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_doesnt_exist()
            {
                var loader = new ZeroConfigProviderLoader("z:\\somefakedirectory\\");
                loader.Load();
            }

            [TestCase("")]
            [TestCase(null)]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_not_set(string assemblyDirectory)
            {
                var loader = new ZeroConfigProviderLoader(assemblyDirectory);
                loader.Load();
            }
        }
    }
}
