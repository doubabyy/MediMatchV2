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
using System.Text.Json;




namespace MediMatch.Client.Test
{
    public class MessageHttpRepositoryTests
    {
        private MockHttpMessageHandler _mockHttp;
        private HttpClient _httpClient;
        private IMessageHttpRepository _messageHttpRepository;

        [SetUp]
        public void Setup()
        {
            _mockHttp = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttp)
            {
                BaseAddress = new Uri("https://localhost:7031/")
            };
            _messageHttpRepository = new MessageHttpRepository(_httpClient);
        }

        [Test]
        public async Task GetMessagesBetweenUsersAsync_ReturnsExpectedMessages()
        {
            // Arrange
            var userId1 = "Doctor2@example.com";
            var userId2 = "Patient2@example.com";

            var messages = new[]
            {
                new Message { MessageTxt = "Hello", MessageFromID = userId1, MessageToID = userId2, MessageDate = DateTime.UtcNow },
                new Message { MessageTxt = "Hi", MessageFromID = userId2, MessageToID = userId1, MessageDate = DateTime.UtcNow }
            };

            _mockHttp.Expect($"https://localhost:7031/GetMessagesBetweenUsers/{userId1}/{userId2}")
                .Respond("application/json", JsonSerializer.Serialize(messages));

            // Act
            var result = await _messageHttpRepository.GetMessagesBetweenUsersAsync(userId1, userId2);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(messages[0].MessageTxt, result[0].MessageTxt);
            Assert.AreEqual(messages[1].MessageTxt, result[1].MessageTxt);
        }
        private bool IsEquivalentToMessage(HttpContent content, Message expectedMessage)
        {
            var jsonContent = content.ReadAsStringAsync().Result;
            var actualMessage = JsonSerializer.Deserialize<Message>(jsonContent);
            return actualMessage.MessageTxt == expectedMessage.MessageTxt &&
                   actualMessage.MessageFromID == expectedMessage.MessageFromID &&
                   actualMessage.MessageToID == expectedMessage.MessageToID &&
                   actualMessage.MessageDate == expectedMessage.MessageDate;
        }

//        [Test]
//        public async Task SendMessageAsync_SendsExpectedMessage()
//        {
//            // Arrange
//            var message = new Message
//            {
//                MessageTxt = "Test 1000",
//                MessageFromID = "patient2@example.com",
//                MessageToID = "Doctor2@example.com",
//                MessageDate = DateTime.Parse("2023-04-06T21:28:55.765-07:00")
//            };

//            _mockHttp.Expect("https://localhost:7031/api/SendMessage")
//.With(request => IsEquivalentToMessage(request.Content, message))
//.Respond(System.Net.HttpStatusCode.OK);

//            // Act
//            await _messageHttpRepository.SendMessageAsync(message);

//            // Assert
//            _mockHttp.VerifyNoOutstandingExpectation();
//        }

    }
}
