using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using MediMatch.Client.Pages;
using NUnit.Framework;

namespace MediMatch.Client.Test
{
    internal class ViewRequestTest
    {
        [Test]
        public async Task Test_SendRequest_Should_ReturnNameandId()
        {
            var MockHttp = new MockHttpMessageHandler();

        }
    }// [{"id":"cc289041-eb5c-4c31-948e-3f6abe276f2c","firstName":"Doctor 3","lastName":"I","description":"Many years of treating depression ","availability":"M-W 9am-4PM","specialty":"Depression","rates":300,"acceptsInsurance":true}]
}
