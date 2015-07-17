using Newtonsoft.Json.Linq;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PeopleAPISteps
    {
        public PeopleAPISteps()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49556/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        class Person
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }
        }

        private Person personData;
        private HttpResponseMessage response;
        private Person result;
        private HttpClient client;


        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            response = client.PostAsJsonAsync("people", personData).Result;
        }

        private void CheckCode(int code) 
        {
            Assert.That(response.StatusCode, Is.EqualTo((System.Net.HttpStatusCode)code));
        }

        [Then(@"I receive a success \(code (.*)\) return message")]
        public void ThenIReceiveASuccessCodeReturnMessage(int code)
        {
            CheckCode(code);
        }
        
        [Then(@"I receive the posted resource")]
        public void ThenIReceiveThePostedResource()
        {
            result = response.Content.ReadAsAsync<Person>().Result;
            Assert.That(result.firstName, Is.EqualTo(personData.firstName));
        }
        
        [Then(@"the posted resource now has an ID")]
        public void ThenThePostedResourceNowHasAnID()
        {
            Assert.That(result.id, Is.Not.Null);
        }

        [Then(@"I receive an error \(code (.*)\) return message")]
        public void ThenIReceiveAnErrorCodeReturnMessage(int code)
        {
            CheckCode(code);
        }

        [Then(@"I receive a message listing all validation errors")]
        public void ThenIReceiveAMessageListingAllValidationErrors()
        {
            var validationMessage = response.Content.ReadAsStringAsync().Result;
            Assert.That(validationMessage, Contains.Substring("firstName"));
            Assert.That(validationMessage, Contains.Substring("email"));
        }

        [Given(@"the populated API")]
        public void GivenThePopulatedAPI()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I GET from the /people API endpoint")]
        public void WhenIGETFromThePeopleAPIEndpoint()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I get a list containing the populated resources")]
        public void ThenIGetAListContainingThePopulatedResources()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I GET from the /people/""(.*)"" API endpoint")]
        public void WhenIGETFromThePeopleAPIEndpoint(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I get the person record that matches that id")]
        public void ThenIGetThePersonRecordThatMatchesThatId()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"a person resource as described below:")]
        public void GivenAPersonResourceAsDescribedBelow(Table table)
        {
            personData = new Person();
            table.FillInstance(personData);
        }

        [Then(@"I can fetch it from the API")]
        public void ThenICanFetchItFromTheAPI()
        {
            var id = result.id.Value;
            var newEntry = client.GetAsync("people/" + id.ToString()).Result;
            Assert.That(newEntry, Is.Not.Null);
        }

        [Given(@"a ""(.*)"" resource")]
        public void GivenAResource(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I receive a message that conforms @""(.*)""")]
        public void ThenIReceiveAMessageThatConforms(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        /*
        [When(@"I GET from the /people/(.*)d(.*)f(.*)fc(.*)-bb(.*)eb(.*)ea(.*) API endpoint")]
        public void WhenIGETFromThePeopleDffc_BbebeaAPIEndpoint(string p0, int p1, string p2, int p3, string p4, int p5, string p6)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"a missing firstName resource")]
        public void GivenAMissingFirstNameResource()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I post \{""(.*)"":""(.*)"",""(.*)"":""(.*)"",""(.*)"":""(.*)""} to the /people API endpoint")]
        public void WhenIPostToThePeopleAPIEndpoint(string p0, string p1, string p2, string p3, string p4, string p5)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive a message that conforms \.\*firstName\.\*")]
        public void ThenIReceiveAMessageThatConforms_FirstName_()
        {
            ScenarioContext.Current.Pending();
        }
         
         */
        [Given(@"an API populated with the following people")]
        public void GivenAnAPIPopulatedWithTheFollowingPeople(Table table)
        {
            Assert.That(table.RowCount, Is.GreaterThan(0));
        }
    }
}
