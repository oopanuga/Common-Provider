using CommonProvider.Data;
using CommonProvider.Tests.TestClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CommonProvider.Tests
{
    [TestFixture]
    public class SettingsTests
    {
        [Category("Settings.Constructors")]
        public class Constructors
        {
            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_settings_count_is_zero()
            {
                var settingsAsDictionary = new Dictionary<string, string>();

                var settings = new Settings(settingsAsDictionary, "");
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_exception_when_settings_is_null()
            {
                Dictionary<string, string> settingsAsDictionary = null;

                var settings = new Settings(settingsAsDictionary, "");
            }

            [Test]
            public void Should_not_throw_exception_when_data_parser_type_not_specified()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings.Get<string>("website"), Is.EqualTo(website));
            }
        }

        [Category("Settings.Get<T>")]
        public class Get_Generic
        {
            [TestCase(typeof(string), "website")]
            [TestCase(typeof(User), "user")]
            public void Should_return_setting_with_specified_type_and_setting_name(Type settingType, string settingName)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                if (settingType == typeof(string))
                {
                    Assert.That(settings.Get<string>(settingName), Is.EqualTo(website));
                }
                else
                {
                    var user = settings.Get<User>(settingName);

                    Assert.That(user.Id, Is.EqualTo(userId));
                    Assert.That(user.Name, Is.EqualTo(userFullname));
                }
            }

            [TestCase(null)]
            [TestCase("")]
            public void Should_return_default_generic_type_when_setting_name_is_null_or_empty(string settingName)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings.Get<string>(settingName), Is.Null);
            }

            [ExpectedException(typeof(InvalidOperationException))]
            public void Should_throw_exception_if_generic_type_is_interface()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                var user = settings.Get<IUser>("user");
            }

            public void Should_for_complex_types_use_default_parser_which_is_the_piped_data_parser_when_data_parser_not_specified()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                var user = settings.Get<User>("user");

                Assert.That(user.Id, Is.EqualTo(userId));
                Assert.That(user.Name, Is.EqualTo(userFullname));
            }
        }

        [Category("Settings[\"settingName\" and type]")]
        public class SettingsIndexerWithType
        {
            [TestCase(typeof(string), "website")]
            [TestCase(typeof(User), "user")]
            public void Should_return_setting_with_specified_type_and_setting_name(Type settingType, string settingName)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                if (settingType == typeof(string))
                {
                    Assert.That(settings[settingName, settingType], Is.EqualTo(website));
                }
                else
                {
                    var user = settings[settingName, settingType];

                    Assert.That(user.Id, Is.EqualTo(userId));
                    Assert.That(user.Name, Is.EqualTo(userFullname));
                }
            }

            [TestCase(null)]
            [TestCase("")]
            public void Should_return_default_type_when_setting_name_is_null_or_empty(string settingName)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings[settingName, typeof(string)], Is.Null);
            }

            [ExpectedException(typeof(InvalidOperationException))]
            public void Should_throw_exception_if_type_is_interface()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                var user = settings["user", typeof(IUser)];
            }

            public void Should_for_complex_types_use_default_parser_which_is_the_piped_data_parser_when_data_parser_not_specified()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                var user = settings["user", typeof(User)];

                Assert.That(user.Id, Is.EqualTo(userId));
                Assert.That(user.Name, Is.EqualTo(userFullname));
            }
        }

        [Category("Settings[\"settingName\"]")]
        public class SettingsIndexer
        {
            [TestCase("website", "http://www.johndoe.com")]
            [TestCase("user", "id:1|name:John Doe")]
            public void Should_return_setting_with_specified_setting_name(string settingName, string settingValue)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings[settingName], Is.EqualTo(settingValue));
            }

            [TestCase(null)]
            [TestCase("")]
            public void Should_return_default_type_when_setting_name_is_null_or_empty(string settingName)
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings[settingName], Is.Null);
            }

            public void Should_return_serialized_complex_type()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                var user = settings["user"];

                Assert.That(user, Is.EqualTo("id:1|name:John Doe"));
            }
        }

        [Category("Settings.Count")]
        public class Count
        {
            public void Should_return_a_count_of_settings()
            {
                string website = "http://www.johndoe.com";
                int userId = 1;
                string userFullname = "John Doe";
                string dataParserType = "CommonProvider.Data.Parsers.PipedDataParser, CommonProvider";

                var settingsAsDictionary = new Dictionary<string, string>();
                settingsAsDictionary.Add("website", website);
                settingsAsDictionary.Add("user", string.Format("id:{0}|name:{1}", userId, userFullname));

                var settings = new Settings(settingsAsDictionary, dataParserType);

                Assert.That(settings.Count, Is.EqualTo(2));
            }
        }
    }
}
