using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services.Implementation;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PeopleAPISteps
    {
<<<<<<< HEAD
        class Person
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }
        }

        class Post
        {
            public Guid? id { get; set; }
            public string post { get; set; }
        }

        Person personData;
        Post postData;
        HttpResponseMessage response;
        Person result;

        /// <summary>
        /// itens iniciais
        /// </summary>

        [Given(@"a valid person resource")]
        public void GivenAValidPersonResource()
        {
            personData = new Person
            {
                firstName = "Fulano",
                lastName = "de Tal",
                email = "fulano@gmail.com",
                url = "http://fulano.com.br"
            };

=======
        private readonly HttpClient client;
        private IEnumerable<Person> backgroundData;
        private string path;
        private Person personData;
        private HttpResponseMessage response;
        private Person result;

        public PeopleAPISteps()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49556/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
>>>>>>> 092c5a607f9abc539260bb374deee0650a740538
        }

        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
<<<<<<< HEAD
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("people", personData).Result;
            }
=======
            response = client.PostAsJsonAsync("people", personData).Result;
>>>>>>> 092c5a607f9abc539260bb374deee0650a740538
        }

        private void CheckCode(int code)
        {
            Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode) code));
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

<<<<<<< HEAD
        /// <summary>
        /// 2 item
        /// </summary>

        [Given(@"an existing person resource")]
        public void GivenAnExistingPersonResource()
        {
            
        }

        [Given(@"a valid update message to that resource")]
        public void GivenAValidUpdateMessageToThatResource()
        {
            personData = new Person
            {
                id = Guid.NewGuid(),
                firstName = "Fulano",
                lastName = "de Tal",
                email = "fulano@gmail.com",
                url = "http://fulano.com.br"
            };
        }

        [When(@"I run a PUT command against the /people endpoint")]
        public void WhenIRunAPUTCommandAgainstThePeopleEndpoint()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("people", personData).Result;
            }
        }

        [Then(@"I receive a success \(code (.*)\) status message")]
        public void ThenIReceiveASuccessCodeStatusMessage(int p0)
        {
            CheckCode(p0);
        }

        [Then(@"I receive the updated resource in the body of the message")]
        public void ThenIReceiveTheUpdatedResourceInTheBodyOfTheMessage()
        {
            result = response.Content.ReadAsAsync<Person>().Result;
            Assert.That(result.firstName, Is.EqualTo(personData.firstName));
        }

        [Given(@"an invalid update message to that resource")]
        public void GivenAnInvalidUpdateMessageToThatResource()
        {
            personData = new Person
            {
                id = Guid.NewGuid(),
                firstName = null,
                lastName = "de Tal",
                email = null,
                url = "http://fulano.com.br"
            };
        }

        [Then(@"I receive an error \(code (.*)\) status message")]
        public void ThenIReceiveAnErrorCodeStatusMessage(int p0)
        {
            CheckCode(p0);
        }

        [Then(@"I receive a list of validation errors in the body of the message")]
        public void ThenIReceiveAListOfValidationErrorsInTheBodyOfTheMessage()
        {
            var validationMessage = response.Content.ReadAsStringAsync().Result;
            Assert.That(validationMessage, Contains.Substring("firstName"));
            Assert.That(validationMessage, Contains.Substring("email"));
        }
        
        /// <summary>
        /// 3 item
        /// </summary>

        [Given(@"a valid post resource")]
        public void GivenAValidPostResource()
        {
            //ScenarioContext.Current.Pending();

            postData = new Post
            {
                post = "comentario no blogue" 
            };

        }

        [When(@"I post is to the /posts endpoint")]
        public void WhenIPostIsToThePostsEndpoint()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("post", postData).Result;
            }
        }

        [Then(@"I get a success \(code (.*)\) response code")]
        public void ThenIGetASuccessCodeResponseCode(int p0)
        {
            CheckCode(p0);
        }

        [Then(@"the resource id is populated")]
        public void ThenTheResourceIdIsPopulated()
        {
            //ScenarioContext.Current.Pending();
        }





=======
        [Given(@"the populated API")]
        public void GivenThePopulatedAPI()
        {
            // This step has been left blank -- data seeding occurs in the backgorund step
        }

        [When(@"I GET from the /(.+) API endpoint")]
        public void WhenIGETFromTheAPIEndpoint(string path)
        {
            this.path = path;
            response = client.GetAsync(path).Result;
        }

        [Then(@"I get a list containing the populated resources")]
        public void ThenIGetAListContainingThePopulatedResources()
        {
            var resourceList = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
            Assert.That(backgroundData, Is.SubsetOf(resourceList));
        }

        [Then(@"the data matches that id")]
        public void ThenIGetThePersonRecordThatMatchesThatId()
        {
            var id = new Guid(path.Substring(path.LastIndexOf('/') + 1));
            result = response.Content.ReadAsAsync<Person>().Result;
            var expected = backgroundData.Single(p => p.id == id);
            Assert.That(result, Is.EqualTo(expected));
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
            var newEntry = client.GetAsync("people/" + id).Result;
            Assert.That(newEntry, Is.Not.Null);
        }

        [Given(@"a ""(.*)"" resource")]
        public void GivenAResource(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"(.+) resource")]
        public void GivenAnInvalidResource(string resourceCase)
        {
            // step purposefully left blank
        }

        [Given(@"an API populated with the following people")]
        public void GivenAnAPIPopulatedWithTheFollowingPeople(Table table)
        {
            backgroundData = table.CreateSet<Person>();
            var mongoRepo = new MongoPersonRepository();
            mongoRepo.Upsert(table.CreateSet<PersonModel>());
        }

        [When(@"I post the following data to the /people API endpoint: (.+)")]
        public void WhenIPostTheFollowingDataToThePeopleAPIEndpoint(string jsonData)
        {
            personData = JsonConvert.DeserializeObject<Person>(jsonData);
            response = client.PostAsJsonAsync("people", personData).Result;
        }

        [Then(@"I receive a message that conforms (.+)")]
        public void ThenIReceiveAMessageThatConforms(string pattern)
        {
            var msg = response.Content.ReadAsStringAsync().Result;
            StringAssert.IsMatch(pattern, msg);
        }

        private class Person : IEquatable<Person>
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }

            public bool Equals(Person other)
            {
                if (other == null) return false;

                return
                    id == other.id
                    && firstName == other.firstName
                    && lastName == other.lastName
                    && email == other.email
                    && url == other.url;
            }

            public override bool Equals(object obj)
            {
                if (obj != null)
                {
                    return Equals(obj as Person);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return id.GetHashCode();
            }
        }
>>>>>>> 092c5a607f9abc539260bb374deee0650a740538
    }
}
