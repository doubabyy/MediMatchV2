using MediMatch.Client.HttpRepository;
using MediMatch.Shared;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediMatch.Client.Test
{
    public class UserBillsHttpRepositoryTests
    {
        [Test]
        public async Task GetBillsHistory_ShouldReturnBillsHistory()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://localhost:7031/api/bill/get-bills-history")
                .Respond("application/json", "[{ \"Bill_Id\": 1, \"Amount\": 100, \"Paid\": true, \"PatientName\": \"John Doe\" }]");

            var httpClient = new HttpClient(mockHttp) { BaseAddress = new System.Uri("https://localhost:7031") };
            var userBillsRepository = new UserBillsHttpRepository(httpClient);

            // Act
            var billsHistory = await userBillsRepository.GetBillsHistory();

            // Assert
            Assert.AreEqual(1, billsHistory.Count);
        }

        [Test]
        public async Task GetBillsUpcoming_ShouldReturnBillsUpcoming()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://localhost:7031/api/bill/get-bills-upcoming")
                .Respond("application/json", "[{ \"Bill_Id\": 2, \"Amount\": 200, \"Paid\": false, \"PatientName\": \"Jane Doe\" }]");

            var httpClient = new HttpClient(mockHttp) { BaseAddress = new System.Uri("https://localhost:7031") };
            var userBillsRepository = new UserBillsHttpRepository(httpClient);

            // Act
            var billsUpcoming = await userBillsRepository.GetBillsUpcoming();

            // Assert
            Assert.AreEqual(1, billsUpcoming.Count);
        }
    }
}
