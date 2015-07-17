using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services.Implementation;

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

        class Person : IEquatable<Person>
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }

            public override bool Equals(object obj)
            {
                if (obj != null)
                {
                    return Equals(obj as Person);
                }
                return false;
            }

            public bool Equals(Person other)
            {
                if (other == null) return false;

                return
                    this.id == other.id
                    && this.firstName == other.firstName
                    && this.lastName == other.lastName
                    && this.email == other.email
                    && this.url == other.url;

            }
        }

        private Person personData;
        private HttpResponseMessage response;
        private Person result;
        private HttpClient client;
        private IEnumerable<Person> backgroundData;
        
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
            // This step has been left blank -- data seeding occurs in the backgorund step
        }

        [When(@"I GET from the /(.+) API endpoint")]
        public void WhenIGETFromTheAPIEndpoint(string path)
        {
            response = client.GetAsync(path).Result;
        }

        [Then(@"I get a list containing the populated resources")]
        public void ThenIGetAListContainingThePopulatedResources()
        {
            IEnumerable<Person> resourceList = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
            Assert.That(backgroundData, Is.SubsetOf(resourceList));
            foreach (var expectedData in backgroundData)
            {
                //Assert.That(resourceList, Contains(expectedData));
                //CollectionAssert.Contains(resourceList, expectedData);

            }
            
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
            backgroundData = table.CreateSet<Person>();
            /*
            foreach (var row in backgroundData)
            {
                var postRes = client.PostAsJsonAsync("people", row).Result;
                Assert.IsTrue(postRes.IsSuccessStatusCode);
            }
             */
            var mongoRepo = new MongoPersonRepository();
            mongoRepo.Upsert(table.CreateSet<PersonModel>());
        }
    }
}
