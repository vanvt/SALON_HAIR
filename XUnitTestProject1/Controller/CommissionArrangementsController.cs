
using RestSharp;
using Xunit;
using System;
namespace XUnitTestProject1
{   
    public class commissionArrangementController
    {
        [Fact]
        public void commissionArrangement()
        {
            var client = new RestClient(ConfigTest.url+"commissionArrangements");
            var request = new RestRequest(Method.GET);
        request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", ConfigTest.token);
            DateTime start;
        DateTime end;
        start = DateTime.Now;
            IRestResponse response = client.Execute(request);
        end = DateTime.Now;          
            var actual = (int)(end - start).TotalMilliseconds;
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(actual <= ConfigTest.expectMiliseconds, $"Expected total milliseconds of less than or equal to {ConfigTest.expectMiliseconds} but was {actual}.");
        }
}
}
