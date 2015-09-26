using CommonProvider.Configuration;
using CommonProvider.ProviderLoaders;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class ConfigProviderLoaderTests
    {
        [Category("ConfigProviderLoader.Load")]
        public class Load
        {
            [Test]
            public void Should_load_enabled_providers_from_config()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                configSection.Types.Add(
                    new TypeElement()
                {
                    Name = "FooProvider",
                    Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests"
                });

                configSection.Types.Add(
                    new TypeElement()
                {
                    Name = "BarProvider",
                    Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests"
                });

                configSection.Settings.Add(new ProviderSettingElement()
                {
                    Key = "GlobalTestSetting",
                    Value = "GlobalTestValue"
                });

                var providerSettingElement =
                    new ProviderSettingElement()
                {
                    Key = "TestSetting",
                    Value = "TestValue"
                };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "FooProvider",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "BarProvider",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings.Count, Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_not_load_disabled_providers_from_config()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "FooProvider",
                        Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests"
                    });

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "BarProvider",
                        Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests"
                    });

                configSection.Settings.Add(new ProviderSettingElement()
                {
                    Key = "GlobalTestSetting",
                    Value = "GlobalTestValue"
                });

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = false,
                    Type = "FooProvider",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "BarProvider",
                };

                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.First().ProviderName, Is.EqualTo("Bar"));
                Assert.That(providerData.Settings.Count, Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(1));
            }

            [Test]
            public void Should_be_able_to_use_type_name_from_types_section_in_plcae_of_full_type_as_provider_type()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "FooProvider",
                        Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests"
                    });

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "BarProvider",
                        Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests"
                    });

                configSection.Settings.Add(new ProviderSettingElement()
                {
                    Key = "GlobalTestSetting",
                    Value = "GlobalTestValue"
                });

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "FooProvider",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "BarProvider",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings.Count, Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_be_able_to_use_full_type_as_provider_type()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "FooProvider",
                        Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests"
                    });

                configSection.Types.Add(
                    new TypeElement()
                    {
                        Name = "BarProvider",
                        Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests"
                    });

                configSection.Settings.Add(new ProviderSettingElement()
                {
                    Key = "GlobalTestSetting",
                    Value = "GlobalTestValue"
                });

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings.Count, Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_load_enabled_providers_when_types_section_not_defined()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                configSection.Settings.Add(new ProviderSettingElement()
                {
                    Key = "GlobalTestSetting",
                    Value = "GlobalTestValue"
                });

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings.Count, Is.EqualTo(1));
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_load_enabled_providers_when_global_settings_section_not_defined()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests",
                };
                fooProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings, Is.Null);
                Assert.That(providerData.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_load_enabled_providers_when_provider_settings_not_defined()
            {
                string configSectionName = "commonProvider";
                var configSection = new ProviderConfigSection();

                var providerSettingElement =
                    new ProviderSettingElement()
                    {
                        Key = "TestSetting",
                        Value = "TestValue"
                    };

                var fooProvider = new ProviderElement()
                {
                    Name = "Foo",
                    Group = "FooGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.FooProvider,CommonProvider.Tests",
                };
                configSection.Providers.Add(fooProvider);

                var barProvider = new ProviderElement()
                {
                    Name = "Bar",
                    Group = "BarGroup",
                    IsEnabled = true,
                    Type = "CommonProvider.Tests.TestClasses.BarProvider,CommonProvider.Tests",
                };
                barProvider.Settings.Add(providerSettingElement);
                configSection.Providers.Add(barProvider);

                var providerConfigurationManager = MockRepository.GenerateMock<IProviderConfigurationManager>();
                providerConfigurationManager.Stub(x => x.GetSection(configSectionName)).Return(configSection);

                var configProviderLoader = new ConfigProviderLoader(providerConfigurationManager);
                var providerData = configProviderLoader.Load();

                Assert.That(providerData.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerData.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerData.Settings, Is.Null);
                Assert.That(providerData.ProviderDescriptors.Where(x => x.ProviderSettings != null)
                    .Sum(x => x.ProviderSettings.Count), Is.EqualTo(1));
            }
        }
    }
}
