using NUnit.Framework;
using System.Linq;
using CommonProvider.ConfigSources.Xml;
using CommonProvider.ConfigSources.Xml.Configuration;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class XmlConfigSourceTests
    {
        [Category("XmlProviderConfigSource.GetProviderConfiguration")]
        public class GetProviderConfiguration
        {
            [Test]
            public void Should_get_enabled_providers_from_config()
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings.Count, Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_not_get_disabled_providers_from_config()
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.First().ProviderName, Is.EqualTo("Bar"));
                Assert.That(providerConfig.Settings.Count, Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(1));
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings.Count, Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings.Count, Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_get_enabled_providers_when_types_section_not_defined()
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings.Count, Is.EqualTo(1));
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_get_enabled_providers_when_global_settings_section_not_defined()
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings, Is.Null);
                Assert.That(providerConfig.ProviderDescriptors.Sum(x => x.ProviderSettings.Count), Is.EqualTo(2));
            }

            [Test]
            public void Should_get_enabled_providers_when_provider_settings_not_defined()
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

                var xmlProviderConfigSource = new XmlProviderConfigSource(configSection);
                var providerConfig = xmlProviderConfigSource.GetProviderConfiguration();

                Assert.That(providerConfig.ProviderDescriptors, Is.Not.Null);
                Assert.That(providerConfig.ProviderDescriptors.Count(), Is.EqualTo(2));
                Assert.That(providerConfig.Settings, Is.Null);
                Assert.That(providerConfig.ProviderDescriptors.Where(x => x.ProviderSettings != null)
                    .Sum(x => x.ProviderSettings.Count), Is.EqualTo(1));
            }
        }
    }
}
