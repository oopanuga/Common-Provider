
using CommonProvider.DependencyManagement;
using NUnit.Framework;
using Rhino.Mocks;
using System;
namespace CommonProvider.Tests
{
    [TestFixture]
    public class DependencyResolverServiceTests
    {
        [Category("DependencyResolverServiceTests.SetResolver")]
        public class SetResolver
        {
            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Should_throw_exception_when_null_dependency_resolver_is_specified()
            {
                DependencyResolver dependencyResolver = null;
                DependencyResolverService.SetResolver(dependencyResolver);
            }

            [Test]
            public void Should_use_default_dependency_resolve_when_dependency_resolver_not_set()
            {
                Assert.That(DependencyResolverService.GetResolver().GetType(), Is.EqualTo(typeof(DependencyResolver)));
            }

            //[Test]
            //public void Should_set_dependency_resolver()
            //{
            //    var dependencyResolver = MockRepository.GenerateMock<IDependencyResolver>();

            //    DependencyResolverService.SetResolver(dependencyResolver);

            //    Assert.That(DependencyResolverService.GetResolver(), Is.EqualTo(dependencyResolver));
            //}
        }
    }
}
