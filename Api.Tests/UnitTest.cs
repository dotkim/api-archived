using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using Api.ServiceInterface;
using Api.ServiceModel;

namespace Api.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<ImageService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public async void Can_call_ImageService()
        {
            var service = appHost.Container.Resolve<ImageService>();

            var response = await service.GetAsync(new GetImagePage { Page = 1, Filter = true});
            var converted = (GetImagePageResponse) response;

            Assert.That(converted.GetResponseStatus(), Is.GreaterThanOrEqualTo(200));
            Assert.That(converted.GetResponseStatus(), Is.LessThanOrEqualTo(299));
        }
    }
}
