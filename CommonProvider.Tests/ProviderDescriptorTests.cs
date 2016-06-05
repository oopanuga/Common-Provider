
using CommonProvider.Data;
using CommonProvider.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ProviderDescriptorTests
    {
        [Category("ProviderDescriptorTests.Constructors")]
        public class Constructors
        {
            [Test]
            public void Should_initialise_provider_descriptor()
            {
                var settings = MockRepository.GenerateMock<IProviderSettings>();
                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var providerDescriptor = new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        );

                Assert.That(providerDescriptor, Is.Not.Null);
                Assert.That(providerDescriptor.ProviderName, Is.EqualTo(fooProviderName));
                Assert.That(providerDescriptor.ProviderGroup, Is.EqualTo(fooProviderGroup));
                Assert.That(providerDescriptor.ProviderType, Is.EqualTo(fooProviderType));
                Assert.That(providerDescriptor.IsEnabled, Is.EqualTo(isFooProviderEnabled));
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_provider_type_not_set()
            {
                var settings = MockRepository.GenerateMock<IProviderSettings>();
                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                Type fooProviderType = null;
                var isFooProviderEnabled = true;

                var providerDescriptor = new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        );
            }

            [Test]
            public void Should_not_throw_exception_when_provider_name_not_set()
            {
                var settings = MockRepository.GenerateMock<IProviderSettings>();
                var fooProviderName = "";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var providerDescriptor = new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        );

                Assert.That(providerDescriptor, Is.Not.Null);
                Assert.That(providerDescriptor.ProviderName, Is.EqualTo(fooProviderName));
                Assert.That(providerDescriptor.ProviderGroup, Is.EqualTo(fooProviderGroup));
                Assert.That(providerDescriptor.ProviderType, Is.EqualTo(fooProviderType));
                Assert.That(providerDescriptor.IsEnabled, Is.EqualTo(isFooProviderEnabled));
            }

            [Test]
            public void Should_not_throw_exception_when_provider_group_not_set()
            {
                var settings = MockRepository.GenerateMock<IProviderSettings>();
                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var providerDescriptor = new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        );

                Assert.That(providerDescriptor, Is.Not.Null);
                Assert.That(providerDescriptor.ProviderName, Is.EqualTo(fooProviderName));
                Assert.That(providerDescriptor.ProviderGroup, Is.EqualTo(fooProviderGroup));
                Assert.That(providerDescriptor.ProviderType, Is.EqualTo(fooProviderType));
                Assert.That(providerDescriptor.IsEnabled, Is.EqualTo(isFooProviderEnabled));
            }

            [Test]
            public void Should_not_throw_exception_when_settings_not_set()
            {
                IProviderSettings settings = null;
                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var providerDescriptor = new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        );

                Assert.That(providerDescriptor, Is.Not.Null);
                Assert.That(providerDescriptor.ProviderName, Is.EqualTo(fooProviderName));
                Assert.That(providerDescriptor.ProviderGroup, Is.EqualTo(fooProviderGroup));
                Assert.That(providerDescriptor.ProviderType, Is.EqualTo(fooProviderType));
                Assert.That(providerDescriptor.IsEnabled, Is.EqualTo(isFooProviderEnabled));
            }
        }
    }

}
