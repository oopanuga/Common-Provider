using CommonProvider.Data;
using CommonProvider.Factories;
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
            public void Should_load_providers_and_settings_when_providers_loaders_and_providers_factory_are_specified()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();
                var settings = MockRepository.GenerateMock<ISettings>();
                var providers = MockRepository.GenerateMock<IProviders>();

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                var providerLoader = MockRepository.GenerateMock<ProviderLoaderBase>();
                providerLoader.Stub(x => x.Load()).Return(providerData);

                var providersFactory = MockRepository.GenerateMock<IProvidersFactory>();
                providersFactory.Stub(x => x.Create(providerDesccriptors)).Return(providers);

                var providerManager = new ProviderManager(providerLoader, providersFactory);

                providersFactory.AssertWasCalled(x => x.Create(providerDesccriptors));

                Assert.That(providerManager.Providers, Is.Not.Null);
                Assert.That(providerManager.Settings, Is.Not.Null);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_provider_loaders_is_null()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();
                var settings = MockRepository.GenerateMock<ISettings>();
                var providers = MockRepository.GenerateMock<IProviders>();

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                ProviderLoaderBase providerLoader = null;

                var providersFactory = MockRepository.GenerateMock<IProvidersFactory>();
                providersFactory.Stub(x => x.Create(providerDesccriptors)).Return(providers);

                var providerManager = new ProviderManager(providerLoader, providersFactory);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_providers_factory_is_null()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();
                var settings = MockRepository.GenerateMock<ISettings>();
                var providers = MockRepository.GenerateMock<IProviders>();

                var providerData = MockRepository.GenerateMock<IProviderData>();
                providerData.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerData.Stub(x => x.Settings).Return(settings);

                var providerLoader = MockRepository.GenerateMock<ProviderLoaderBase>();
                providerLoader.Stub(x => x.Load()).Return(providerData);

                IProvidersFactory providersFactory = null;

                var providerManager = new ProviderManager(providerLoader, providersFactory);
            }

            [Test]
            public void Should_load_providers_using_the_default_providers_fatcory_when_only_provider_loaders_is_specified()
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
