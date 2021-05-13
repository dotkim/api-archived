using Funq;
using ServiceStack;
using NUnit.Framework;
using Api.ServiceInterface;
using Api.ServiceModel;

namespace Api.Tests
{
    public class IntegrationTest
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(ImageService).Assembly) { }

            public override void Configure(Container container)
            {
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Test]
        public void Can_call_Image_Service()
        {
            var client = CreateClient();

            var response = client.Get(new GetImageRandom { GuildId = 1234 });

            //Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}
