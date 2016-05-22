using CommonProvider.Data;
using CommonProvider.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ProvidersTests
    {
        [Category("Providers.Constructors")]
        public class Constructors
        {
            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_provider_descriptors_is_empty()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var providerDesccriptors = new List<ProviderDescriptor>();

                var providers = new ProviderList(providerDesccriptors);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_provider_descriptors_is_null()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                List<ProviderDescriptor> providerDesccriptors = null;

                var providers = new ProviderList(providerDesccriptors);
            }
        }

        [Category("Providers.All<T>")]
        public class All_Generic
        {
            [Test]
            public void Should_get_all_enabled_providers_of_the_specified_type()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProviders = providers.All<IFooProvider>();

                Assert.That(returnedProviders.Count(), Is.EqualTo(1));
                Assert.That(returnedProviders.First().GetType(), Is.EqualTo(fooProviderType));
            }

            [Test]
            public void Should_return_zero_providers_if_type_not_found()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                Assert.That(providers.All<IBarProvider>().Count(), Is.EqualTo(0));
            }
        }

        [Category("Providers.All")]
        public class All
        {
            [Test]
            public void Should_get_all_enabled_providers()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = false;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProviders = providers.All().ToList();

                Assert.That(returnedProviders.Count(), Is.EqualTo(1));
                Assert.That(returnedProviders[0].GetType(), Is.EqualTo(typeof(FooProvider)));
            }
        }

        [Category("Providers.ByGroup")]
        public class ByGroup
        {
            [Test]
            public void Should_get_all_enabled_providers_in_the_specified_group()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProviders = providers.ByGroup(fooProviderGroup).ToList();

                Assert.That(returnedProviders.Count, Is.EqualTo(1));
                Assert.That(returnedProviders[0].GetType(), Is.EqualTo(fooProviderType));
            }

            [Test]
            public void Should_return_zero_providers_when_providers_with_group_name_not_found()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var inexistentGroup = "FakeGroup";

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProviders = providers.ByGroup(inexistentGroup).ToList();

                Assert.That(returnedProviders.Count, Is.EqualTo(0));
            }
        }

        [Category("Providers.ByGroup<T>")]
        public class ByGroup_Generic
        {
            [Test]
            public void Should_get_all_enabled_providers_of_the_specified_type_and_in_the_specified_group()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProviders = providers.ByGroup<IFooProvider>(fooProviderGroup).ToList();

                Assert.That(returnedProviders.Count, Is.EqualTo(1));
                Assert.That(returnedProviders[0].GetType(), Is.EqualTo(fooProviderType));
            }

            [TestCase(typeof(IFooProvider), "FakeGroup")]
            [TestCase(typeof(IBarProvider), "FooProviders")]
            public void Should_return_zero_providers_when_providers_of_specified_type_and_with_specified_group_name_doesnt_exist(Type providerType, string groupName)
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                       barProviderName,
                       barProviderGroup,
                       barProviderType,
                       settings,
                       isBarProviderEnabled
                       ));

                var providers = new ProviderList(providerDesccriptors);

                IList returnedProviders = null;
                if (providerType == typeof(IFooProvider))
                {
                    returnedProviders = providers.ByGroup<IFooProvider>(groupName).ToList();
                }
                else
                {
                    returnedProviders = providers.ByGroup<IBarProvider>(groupName).ToList();
                }

                Assert.That(returnedProviders.Count, Is.EqualTo(0));
            }
        }

        [Category("Providers.ByName")]
        public class ByName
        {
            [Test]
            public void Should_get_an_enabled_provider_with_the_specified_name()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers.ByName(fooProviderName);

                Assert.That(returnedProvider, Is.Not.Null);
                Assert.That(returnedProvider.GetType(), Is.EqualTo(fooProviderType));
            }

            [Test]
            public void Should_return_null_when_provider_with_the_specified_name_not_found()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var inexistentName = "FakeName";

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers.ByName(inexistentName);

                Assert.That(returnedProvider, Is.Null);
            }
        }

        [Category("Providers.ByName<T>")]
        public class ByName_Generic
        {
            [Test]
            public void Should_get_an_enabled_provider_of_the_specified_type_and_with_the_specified_name()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers.ByName<IFooProvider>(fooProviderName);

                Assert.That(returnedProvider, Is.Not.Null);
                Assert.That(returnedProvider.GetType(), Is.EqualTo(fooProviderType));
            }

            [TestCase(typeof(IFooProvider), "FakeName")]
            [TestCase(typeof(IBarProvider), "Foo Provider")]
            public void Should_return_null_provider_when_provider_of_specified_type_and_with_specified_name_doesnt_exist(Type providerType, string providerName)
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                IProvider returnedProvider = null;
                if (providerType == typeof(IFooProvider))
                {
                    returnedProvider = providers.ByName<IFooProvider>(providerName);
                }
                else
                {
                    returnedProvider = providers.ByName<IBarProvider>(providerName);
                }

                Assert.That(returnedProvider, Is.Null);
            }
        }

        [Category("Providers[\"providerName\"]")]
        public class ProvidersIndexer
        {
            [Test]
            public void Should_get_an_enabled_provider_with_the_specified_name()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers[fooProviderName];

                Assert.That(returnedProvider, Is.Not.Null);
                Assert.That(returnedProvider.GetType(), Is.EqualTo(fooProviderType));
            }

            [Test]
            public void Should_return_null_when_provider_with_the_specified_name_not_found()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var inexistentName = "FakeName";

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers[inexistentName];

                Assert.That(returnedProvider, Is.Null);
            }
        }

        [Category("Providers[\"providerName\" and type]")]
        public class ProvidersIndexerWithType
        {
            [Test]
            public void Should_get_an_enabled_provider_of_the_specified_type_and_with_the_specified_name()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers[fooProviderName, typeof(IFooProvider)];

                Assert.That(returnedProvider, Is.Not.Null);
                Assert.That(returnedProvider.GetType(), Is.EqualTo(fooProviderType));
            }

            [TestCase(typeof(IFooProvider), "FakeName")]
            [TestCase(typeof(IBarProvider), "Foo Provider")]
            public void Should_return_null_provider_when_provider_of_specified_type_and_with_specified_name_doesnt_exist(Type providerType, string providerName)
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                var returnedProvider = providers[providerName, providerType];

                Assert.That(returnedProvider, Is.Null);
            }
        }

        [Category("Providers.Count")]
        public class Count
        {
            [Test]
            public void Should_return_a_count_of_enabled_providers()
            {
                var settings = MockRepository.GenerateMock<ISettings>();

                var fooProviderName = "Foo Provider";
                var fooProviderGroup = "FooProviders";
                var fooProviderType = typeof(FooProvider);
                var isFooProviderEnabled = true;

                var barProviderName = "Bar Provider";
                var barProviderGroup = "BarProviders";
                var barProviderType = typeof(BarProvider);
                var isBarProviderEnabled = true;

                var providerDesccriptors = new List<ProviderDescriptor>();

                providerDesccriptors.Add(new ProviderDescriptor(
                        fooProviderName,
                        fooProviderGroup,
                        fooProviderType,
                        settings,
                        isFooProviderEnabled
                        ));

                providerDesccriptors.Add(new ProviderDescriptor(
                        barProviderName,
                        barProviderGroup,
                        barProviderType,
                        settings,
                        isBarProviderEnabled
                        ));

                var providers = new ProviderList(providerDesccriptors);

                Assert.That(providers.Count, Is.EqualTo(2));
            }
        }
    }
}
