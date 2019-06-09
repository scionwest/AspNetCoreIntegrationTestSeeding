using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1;

namespace Janus
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            // Arrange
            var faker = new Faker();
            var users = new UserEntity[]
            {
                new UserEntity { Address = faker.Address.FullAddress(), Email = faker.Internet.Email(), Username = faker.Internet.UserName() },
                new UserEntity { Address = faker.Address.FullAddress(), Email = faker.Internet.Email(), Username = faker.Internet.UserName() }
            };
            var factory = new ApiIntegrationTestFactory<Startup>("DataSource");
            factory.WithDataContext<AppDbContext>("Default")
                .WithSeedData(context =>
                {
                    context.Users.AddRange(users);
                    context.SaveChanges();
                });

            var client = factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("api/users");
            string responseBody = await response.Content.ReadAsStringAsync();
            UserEntity[] responseData = JsonConvert.DeserializeObject<UserEntity[]>(responseBody);


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(users.Length, responseData.Length);
        }
    }
}
