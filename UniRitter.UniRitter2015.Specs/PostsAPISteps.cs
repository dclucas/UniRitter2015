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
    public class PostsAPISteps
    {
        private readonly HttpClient postClient;
        private HttpResponseMessage postResponse;
        private IEnumerable<Post> postBackgroundData;
        private string postPath;
        private Post postData;
        private Post postResult;

        public PostsAPISteps()
        {
            postClient = new HttpClient();
            postClient.BaseAddress = new Uri("http://localhost:9000/");
            postClient.DefaultRequestHeaders.Accept.Clear();
            postClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Given(@"an API populated with the following Post")]
        public void GivenAnAPIPopulatedWithTheFollowingPost(Table table)
        {
             /*
            var mongoRepo = new MongoRepository<PostModel>(new ApiConfig());
            mongoRepo.Upsert(table.CreateSet<PostModel>());
            */
            postBackgroundData = table.CreateSet<Post>();
            var repo = new InMemoryRepository<PostModel>();
            foreach (var entry in table.CreateSet<PostModel>()) { repo.Add(entry); }
        }



        //Scenario: Get all entries Post
        [Given(@"the populated API Post")]
        public void GivenThePopulatedAPIPost()
        {
            // This step has been left blank -- data seeding occurs in the backgorund step
        }

        [When(@"I get from the /(.+) API endpoint Post")]
        public void WhenIGetFromTheAPIEndpointPost(string path)
        {
            this.postPath = path;
            postResponse = postClient.GetAsync(path).Result;
        }

        [Then(@"I get a list containing the populated resources Post")]
        public void ThenIGetAListContainingThePopulatedResourcesPost()
        {
            var resourceList = postResponse.Content.ReadAsAsync<IEnumerable<Post>>().Result;
            Assert.That(postBackgroundData, Is.SubsetOf(resourceList));
        }



        //Scenario Outline: Get a specific entry
        //Given the populated API Post
        //When I get from the /Posts/<id> API endpoint Post
        [Then(@"the data matches that id Post")]
        public void ThenTheDataMatchesThatIdPost()
        {
            var id = new Guid(postPath.Substring(postPath.LastIndexOf('/') + 1));
            postResult = postResponse.Content.ReadAsAsync<Post>().Result;
            var expected = postBackgroundData.Single(p => p.id == id);
            Assert.That(postResult, Is.EqualTo(expected));
        }



        //Scenario: Add a Post
        [Given(@"a resource as described below Post:")]
        public void GivenAResourceAsDescribedBelowPost(Table table)
        {
            postData = new Post();
            table.FillInstance(postData);
        }

        [When(@"I post it to the /Posts API endpoint")]
        public void WhenIPostItToThePostsAPIEndpoint()
        {
            postResponse = postClient.PostAsJsonAsync("Posts", postData).Result;
        }

        [Then(@"I receive a success \(code (.*)\) return Post message")]
        public void ThenIReceiveASuccessCodeReturnPostMessage(int code)
        {
            if (!postResponse.IsSuccessStatusCode)
            {
                var msg = String.Format("API error: {0}", postResponse.Content.ReadAsStringAsync().Result);
                Assert.Fail(msg);
            }
            CheckCode(code);
        }

        [Then(@"I receive the Post resource")]
        public void ThenIReceiveThePostedResource()
        {
            postResult = postResponse.Content.ReadAsAsync<Post>().Result;
            Assert.That(postResult.body, Is.EqualTo(postData.body));
        }

        [Then(@"the posted resource now has an Post ID")]
        public void ThenThePostedResourceNowHasAnPostID()
        {
            Assert.That(postResult.id, Is.Not.Null);
        }

        [Then(@"I can fetch /(.+) from the APIPost")]
        public void ThenICanFetchItFromTheAPIPost(string path)
        {
            var id = postResult.id.Value;
            var newEntry = postClient.GetAsync(path + "/" + id).Result;
            Assert.That(newEntry, Is.Not.Null);
        }




        //Scenario Outline: Invalid post data on insertion
        [Given(@"(.+) resource Post")]
        public void GivenAnInvalidResourcePost(string resourceCase)
        {
            // step purposefully left blank
        }

        [When(@"I post the following data to the /Posts API endpoint: (.+)")]
        public void WhenIPostTheFollowingDataToThePostsAPIEndpoint(string jsonData)
        {
            postData = JsonConvert.DeserializeObject<Post>(jsonData);
            postResponse = postClient.PostAsJsonAsync("Posts", postData).Result;
        }

        [Then(@"I receive an error \(code (.*)\) return message Post")]
        public void ThenIReceiveAnErrorCodeReturnMessagePost(int code)
        {
            CheckCode(code);
        }

        [Then(@"I receive a Post message that conforms (.+)")]
        public void ThenIReceiveAPostMessageThatConforms(string pattern)
        {
            var msg = postResponse.Content.ReadAsStringAsync().Result;
            StringAssert.IsMatch(pattern, msg);
        }




        private void CheckCode(int code)
        {
            Assert.That(postResponse.StatusCode, Is.EqualTo((HttpStatusCode)code));
        }

        private class Post : IEquatable<Post>
        {
            public Guid? id { get; set; }
            public string body { get; set; }
            public string title { get; set; }
            public Guid authorId { get; set; }
            public string tags { get; set; }

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
