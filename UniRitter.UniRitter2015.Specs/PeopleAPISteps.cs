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
        class Person
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }
        }

        Person personData;
        HttpResponseMessage response;
        Person result;

        private void ExecuteAPI(Action<HttpClient> act)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                act(client);
            }
        }
       
        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            /*
            ExecuteAPI(c =>
            {
                response = c.PostAsJsonAsync("people", personData).Result;
            });
             */
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

        [Then(@"the person is added to the database")]
        public void ThenThePersonIsAddedToTheDatabase()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"an invalid person resource")]
        public void GivenAnInvalidPersonResource()
        {
            personData = new Person
            {
                firstName = null,
                lastName = "de Tal",
                email = "fulano",
                url = "http://fulano.com.br"
            };
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
            ExecuteAPI(c =>
            {
                var person = new Person();
                table.FillInstance(person);
                response = c.PostAsJsonAsync("people", person).Result;
            });
        }

        [Then(@"I can fetch it from the API")]
        public void ThenICanFetchItFromTheAPI()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"a ""(.*)"" resource")]
        public void GivenAResource(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I post ""(.*)"" to the /people API endpoint")]
        public void WhenIPostToThePeopleAPIEndpoint(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I receive a message that conforms @""(.*)""")]
        public void ThenIReceiveAMessageThatConforms(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
