using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SimpleFramework.Helpers;
using SimpleFramework.Models;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SimpleFramework.StepDefinitions
{
    [Binding]
    public class SampleTestsSteps : BaseSteps
    {
        private string url = null;
        private string username = null;
        private string password = null;

        public SampleTestsSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext) : base(objectContainer, scenarioContext, featureContext)
        {
            var settings = ConfigurationManager.AppSettings;
            url = settings["Url"];
            username = settings["Username"];
            password = settings["Password"];
        }

        [When(@"I get authentication token")]
        public void WhenIGetAuthenticationToken()
        {
            RestHelper rest = new RestHelper();

            var token = rest.GetToken(url, username, password);
            Assert.IsNotNull(token);
        }

        [When(@"I get all booking ID")]
        public void WhenIGetAllBookingID()
        {
            RestHelper rest = new RestHelper();

            IRestResponse response = rest.GetRequest(url + "/booking");
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [When(@"I get a booking by ID")]
        public void WhenIGetABookingByID()
        {
            RestHelper rest = new RestHelper();

            IRestResponse response = rest.GetRequest(url + "/booking/2");
            var booking = rest.Deserialize<Booking>(response);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [When(@"I post booking")]
        public void WhenIPostBooking()
        {
            var booking = new Booking();

            /* Sample */
            booking.firstname = Faker.Name.First();
            booking.lastname = Faker.Name.Last();
            booking.totalprice = Faker.RandomNumber.Next(1, 999);
            booking.depositpaid = Faker.Boolean.Random();
            booking.bookingdates.checkin = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
            booking.bookingdates.checkout = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            booking.additionalneeds = Faker.Company.BS();

            RestHelper rest = new RestHelper();
            IRestResponse response = rest.CreateUpdateRequest(Method.POST, url + "/booking", booking);
            var createBooking = rest.Deserialize<CreateBooking>(response);
            Assert.AreEqual(200, (int)response.StatusCode);

            scenarioContext.Set(createBooking.bookingid, "BookingID");
        }

        [Then(@"I update the booking")]
        public void ThenIUpdateTheBooking()
        {
            var booking = new Booking();

            /* Sample */
            booking.firstname = Faker.Name.First();
            booking.lastname = Faker.Name.Last();
            booking.totalprice = Faker.RandomNumber.Next(1, 999);
            booking.depositpaid = Faker.Boolean.Random();
            booking.bookingdates.checkin = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
            booking.bookingdates.checkout = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            booking.additionalneeds = Faker.Company.BS();

            RestHelper rest = new RestHelper();
            IRestResponse response = rest.CreateUpdateRequest(Method.PUT, url + "/booking/4" /*+ scenarioContext.Get<int>("BookingID").ToString()*/, booking, scenarioContext.Get<string>("AuthenticationToken"));
            var BookingUpdated = rest.Deserialize<Booking>(response);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
    }
}
