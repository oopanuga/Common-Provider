using CommonProvider.Data;
using CommonProvider.ProviderLoaders;
using CommonProvider.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ProviderManagerTests
    {
        [Category("ProviderManager.Constructors")]
        public class Constructors
        {
            [Test]
            public void Should_load_providers_and_settings_when_a_provider_loader_and_a_provider_factory_is_specified()
            {
                var providerDesccriptors = new List<IProviderDescriptor>
                {
                    MockRepository.GenerateMock<IProviderDescriptor>()
                };

                var settings = MockRepository.GenerateMock<ISettings>();

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                var providerLoader = MockRepository.GenerateMock<ProviderLoaderBase>();
                providerLoader.Stub(x => x.Load()).Return(providerData);

                var providerManager = new ProviderManager(providerLoader);

                Assert.That(providerManager.Providers, Is.Not.Null);
                Assert.That(providerManager.Settings, Is.Not.Null);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_provider_loader_is_null()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();
                var settings = MockRepository.GenerateMock<ISettings>();

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                ProviderLoaderBase providerLoader = null;
                new ProviderManager(providerLoader);
            }

            [Test]
            public void Should_load_providers_using_the_default_provider_fatcory_when_only_a_provider_loader_is_specified()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();

                var settings = MockRepository.GenerateMock<ISettings>();
                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                var providerLoader = MockRepository.GenerateMock<ProviderLoaderBase>();
                providerLoader.Stub(x => x.Load()).Return(providerData);

                var providerManager = new ProviderManager(providerLoader);

                Assert.That(providerManager.Providers, Is.Not.Null);
                Assert.That(providerManager.Settings, Is.Not.Null);
            }
        }
    }
}
