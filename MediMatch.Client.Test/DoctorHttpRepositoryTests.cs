using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using NUnit.Framework;
using MediMatch.Client.HttpRepository;
using System.Net.Http;
using MediMatch.Shared;


namespace MediMatch.Client.Test
{
    public class DoctorHttpRepositoryTests
    {
        [Test]
        public async Task GetDoctorsAsync_ShouldReturnDoctorList()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://localhost:7031/api/patient/browse-doctors")
                .Respond("application/json", "[{\"Id\":\"1\",\"FirstName\":\"John\",\"LastName\":\"Doe\"}]");

            var httpClient = new HttpClient(mockHttp) { BaseAddress = new System.Uri("https://localhost:7031/") };
            var doctorRepository = new DoctorHttpRepository(httpClient);

            // Act
            var doctors = await doctorRepository.GetDoctorsAsync();

            // Assert
            Assert.AreEqual(1, doctors.Count);
            Assert.AreEqual("1", doctors[0].Id);
            Assert.AreEqual("John", doctors[0].FirstName);
            Assert.AreEqual("Doe", doctors[0].LastName);
        }

        [Test]
        public async Task SendRequestAsync_ShouldSendRequestToCorrectEndpoint()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://localhost:7031/api/patient/send-request")
                .WithJsonContent("\"1\"")
                .Respond(System.Net.HttpStatusCode.OK);

            var httpClient = new HttpClient(mockHttp) { BaseAddress = new System.Uri("https://localhost:7031/") };
            var doctorRepository = new DoctorHttpRepository(httpClient);

            // Act
            await doctorRepository.SendRequestAsync("1");

            // Assert
            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
