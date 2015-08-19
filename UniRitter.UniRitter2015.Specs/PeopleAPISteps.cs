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
using UniRitter.UniRitter2015.Support;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PeopleAPISteps
    {
        private readonly HttpClient client;
        private IEnumerable<Person> backgroundData;
        private IEnumerable<Post> backgroundDataPost;
        private string path;
        private Person personData;
        private HttpResponseMessage response;
        private Person result;
        private Post resultPost;

        public PeopleAPISteps()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49556/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            response = client.PostAsJsonAsync("people", personData).Result;
        }

        private void CheckCode(int code)
        {
            Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode) code));
        }

        [Then(@"I receive a success \(code (.*)\) return message")]
        public void ThenIReceiveASuccessCodeReturnMessage(int code)
        {
            if (! response.IsSuccessStatusCode)
            {
                var msg = String.Format("API error: {0}", response.Content.ReadAsStringAsync().Result);
                Assert.Fail(msg);
            }
            
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
            this.path = path;
            response = client.GetAsync(path).Result;
        }

        [Then(@"I get a list containing the populated resources of the (.+)")]
        public void ThenIGetAListContainingThePopulatedResources(String resource)
        {
            switch(resource)
            {
                case "people":
                    var resourceListPeople = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                    Assert.That(backgroundData, Is.SubsetOf(resourceListPeople));
                    break;
                case "posts":
                    var resourceListPost = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;
                    Assert.That(backgroundDataPost, Is.SubsetOf(resourceListPost));
                    break;
            }
            
        }

        [Then(@"the data of (.+) matches that id")]
        public void ThenIGetTheRecordThatMatchesThatId(String tipo)
        {
            var id = new Guid(path.Substring(path.LastIndexOf('/') + 1));

            switch (tipo)
            {
                case "people":
                    result = response.Content.ReadAsAsync<Person>().Result;
                    var expected = backgroundData.Single(p => p.id == id);
                    Assert.That(result, Is.EqualTo(expected));                    
                    break;
                case "posts":
                    resultPost = response.Content.ReadAsAsync<Post>().Result;
                    var expectedPost = backgroundDataPost.Single(p => p.id == id);
                    Assert.That(resultPost, Is.EqualTo(expectedPost));
                    break;
            }

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

        [Given(@"an API populated with the following (.+)")]
        public void GivenAnAPIPopulatedWithTheFollowing(String tipo, Table table)
        {
            switch(tipo)
            {
                case "people":
                    backgroundData = table.CreateSet<Person>();
                    var repoPeople = new InMemoryRepository<PersonModel>();
                    //var mongoRepo = new MongoRepository<PersonModel>(new ApiConfig());
                    //mongoRepo.Upsert(table.CreateSet<PersonModel>());
                    foreach (var entry in table.CreateSet<PersonModel>()) {
                        repoPeople.Add(entry);
                    }
                    break;

                case "posts":
                    backgroundDataPost = table.CreateSet<Post>();
                    //var mongoRepo = new MongoRepository<PersonModel>(new ApiConfig());
                    //mongoRepo.Upsert(table.CreateSet<PersonModel>());
                    var repoPost = new InMemoryRepository<PostModel>();
                    foreach (var entry in table.CreateSet<PostModel>())
                    {
                        repoPost.Add(entry);
                    }
                    break;
            }

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

        private class Post : IEquatable<Post>
        {
            public Guid? id { get; set; }
            public string body { get; set; }
            public string title { get; set; }
            public Guid? authorId { get; set; }
            public IEnumerable<string> tags { get; set; }

            public bool Equals(Post other)
            {
                if (other == null) return false;

                return
                    id == other.id
                    && body == other.body
                    && title == other.title
                    && authorId == other.authorId
                    && tags == other.tags;
            }

            public override bool Equals(object obj)
            {
                if (obj != null)
                {
                    return Equals(obj as Post);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return id.GetHashCode();
            }
        }

    }
}