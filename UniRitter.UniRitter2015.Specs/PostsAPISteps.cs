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
   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable"), Binding]
    public class PostsAPISteps
    {
        private readonly HttpClient client;
        private IEnumerable<Post> backgroundData;
        private string path;
        private Post postData;
        private HttpResponseMessage response;
        private Post result;

        public PostsAPISteps()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49556/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Given(@"an API populated with the following posts")]
        public void GivenAnAPIPopulatedWithTheFollowingPosts(Table table)
        {
            backgroundData = table.CreateSet<Post>();
            var repo = new InMemoryRepository<PostModel>();
            foreach (var entry in table.CreateSet<PostModel>())
            {
                repo.Add(entry);
            }
        }

        [Given(@"a post resource as described below:")]
        public void GivenAPostResourceAsDescribedBelow(Table table)
        {
            postData = new Post();
            table.FillInstance(postData);
        }

        [When(@"I post it to the /posts API endpoint: (.+)")]
        public void WhenIPostItToThePostsAPIEndpoint(string jsonData)
        {
            //TODO
            ScenarioContext.Current.Pending();
        }

        [When(@"I post is to the /posts endpoint")]
        public void WhenIPostIsToThePostsEndpoint()
        {
            response = client.PostAsJsonAsync("posts", postData).Result;
        }

        [Then(@"I get a success \(code (.*)\) response code")]
        public void ThenIGetASuccessCodeResponseCode(int code)
        {
            if (!response.IsSuccessStatusCode)
            {
                var msg = String.Format("API error: {0}", response.Content.ReadAsStringAsync().Result);
                Assert.Fail(msg);
            }

            CheckCode(code);
        }

        private void CheckCode(int code)
        {
            Assert.That(response.StatusCode, Is.EqualTo((HttpStatusCode)code));
        }

        private class Post : IEquatable<Post>
        {
            public Guid? id { get; set; }
            public string body { get; set; }
            public string title { get; set; }
            public Guid authorId { get; set; }
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
