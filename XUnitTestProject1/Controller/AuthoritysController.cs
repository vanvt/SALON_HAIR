
using RestSharp;
using Xunit;
using System;
using System.Globalization;

namespace XUnitTestProject1
{   
    public class authorityController
    {
        [Fact]
        public void authority()
        {
            //DateTime myDate = DateTime.ParseExact("2018-12-12T20:00:51.63Z", "yyyy-MM-ddHH:mm:ss.fff",
            //                           System.Globalization.CultureInfo.InvariantCulture);
            DateTime st;
        //  var d=   DateTime.TryParseExact("2018-12-12T20:00:51.63Z", "YYYY-MM-DDTHH:mm:ss.sssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out st);

            DateTime dateTime = DateTime.Parse("2018-12-12T20:00:51.63Z");
            DateTime dateTime1 = DateTime.Parse("2018-12-12T20:00:51.63Z");
            DateTime dateTime2 = DateTime.SpecifyKind(dateTime1, DateTimeKind.Utc);
            DateTime dateTime3 = DateTime.SpecifyKind(dateTime1, DateTimeKind.Local);
            //    var client = new RestClient(ConfigTest.url+"authoritys");
            //    var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            //    request.AddHeader("content-type", "application/json");
            //    request.AddHeader("authorization", ConfigTest.token);
            //    DateTime start;
            //DateTime end;
            //start = DateTime.Now;
            //    IRestResponse response = client.Execute(request);
            //end = DateTime.Now;          
            //    var actual = (int)(end - start).TotalMilliseconds;
            //Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            //    Assert.True(actual <= ConfigTest.expectMiliseconds, $"Expected total milliseconds of less than or equal to {ConfigTest.expectMiliseconds} but was {actual}.");
        }
}
}

