using CommonProvider.ProviderLoaders;
using NUnit.Framework;
using System;
using System.Linq;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class DirectoryProviderLoaderTests
    {
        [Category("DirectoryProviderLoaderTests.Load")]
        public class Load
        {
            [Test]
            public void Should_load_providers_from_classes_of_type_iprovider()
            {
                var loader = new DirectoryProviderLoader(Environment.CurrentDirectory);
                var providerData = loader.Load();

                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_doesnt_exist()
            {
                var loader = new DirectoryProviderLoader("z:\\somefakedirectory\\");
                var providerData = loader.Load();
            }

            [TestCase("")]
            [TestCase(null)]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_assembly_directory_not_set(string assemblyDirectory)
            {
                var loader = new DirectoryProviderLoader(assemblyDirectory);
                var providerData = loader.Load();
            }
        }
    }
}
