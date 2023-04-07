using RichardSzalay.MockHttp;
using System.Net.Http;
using System.Text.Json;

namespace MediMatch.Client.Test
{
    public static class MockHttpExtensions
    {
        public static MockedRequest WithJsonContent(this MockedRequest mockedRequest, string content)
        {
            var jsonContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return mockedRequest.WithContent(content);
        }
    }
}
