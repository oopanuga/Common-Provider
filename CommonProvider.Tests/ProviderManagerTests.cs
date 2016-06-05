using CommonProvider.Data;
using CommonProvider.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using CommonProvider.ConfigSources.Xml;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ProviderManagerTests
    {
        [Category("ProviderManager.Constructors")]
        public class Constructors
        {
            [Test]
            public void Should_get_provider_config_using_the_specified_provider_config_source()
            {
                var providerDesccriptors = new List<IProviderDescriptor>
                {
                    MockRepository.GenerateMock<IProviderDescriptor>()
                };

                var settings = MockRepository.GenerateMock<IProviderSettings>();

                var providerConfig = MockRepository.GenerateMock<IProviderConfig>();
                providerConfig.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerConfig.Stub(x => x.Settings).Return(settings);

                var xmlProviderConfigSource = MockRepository.GenerateMock<XmlProviderConfigSource>();
                xmlProviderConfigSource.Stub(x => x.GetProviderConfiguration()).Return(providerConfig);

                var providerManager = new ProviderManager(xmlProviderConfigSource);

                Assert.That(providerManager.Providers, Is.Not.Null);
                Assert.That(providerManager.Settings, Is.Not.Null);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_provider_config_source_is_null()
            {
                var providerDesccriptors = new List<IProviderDescriptor>();
                var settings = MockRepository.GenerateMock<IProviderSettings>();

                var providerConfig = MockRepository.GenerateMock<IProviderConfig>();
                providerConfig.Stub(x => x.ProviderDescriptors).Return(providerDesccriptors);
                providerConfig.Stub(x => x.Settings).Return(settings);

                XmlProviderConfigSource xmlProviderConfigSource = null;
                new ProviderManager(xmlProviderConfigSource);
            }
        }
    }
}
