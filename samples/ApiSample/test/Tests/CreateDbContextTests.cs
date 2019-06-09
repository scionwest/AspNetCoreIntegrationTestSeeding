using Bogus;
using Janus.SampleApi.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Janus.SampleApi
{
    [TestClass]
    public class CreateDbContextTests
    {
        private Faker dataFaker;
        private ApiIntegrationTestFactory<Startup> testFactory;

        [TestInitialize]
        public void Initialize()
        {
            this.dataFaker = new Faker();

            // Sqlite connection string uses "DataSource".
            // Replace with "Initial Catalog" for Sql Server or "database" for MySql.
            this.testFactory = new ApiIntegrationTestFactory<Startup>("DataSource");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Destroy the database.
            this.testFactory.Dispose();
        }

        [TestMethod]
        [TestCategory("Samples")]
        public async Task GetUsers_ReturnsOkStatusCode()
        {
            // Arrange - Sets up and creates the database.
            this.testFactory.WithDataContext<AppDbContext>("Default");
            var client = this.testFactory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("api/users");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
