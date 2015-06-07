using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PeopleAPISteps
    {
        Object validPerson;

        [Given(@"a valid person resource")]
        public void GivenAValidPersonResource()
        {
            var obj = new { 
                firstName = "Fulano",
                lastName = "de Tal",
                email = "fulano@gmail.com",
                url = "http://fulano.com.br"
            };
            //new JavaScriptSerializer().Serialize
            //PersonJson = "{  }";
            validPerson = obj;
        }
        
        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync("people", validPerson).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    dynamic result = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    Assert.That(result.id, Is.Not.Null);
                    //response.Content
                    //Product product = await response.Content.ReadAsAsync>Product>();
                    //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                }
            }
        }
        
        [Then(@"I receive a success \(code (.*)\) return message")]
        public void ThenIReceiveASuccessCodeReturnMessage(int p0)
        {
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive the posted resource")]
        public void ThenIReceiveThePostedResource()
        {
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"the posted resource now has an ID")]
        public void ThenThePostedResourceNowHasAnID()
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
