﻿using Newtonsoft.Json.Linq;
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

        [Given(@"a valid person resource")]
        public void GivenAValidPersonResource()
        {
            personData = new Person {
                firstName = "Fulano",
                lastName = "de Tal",
                email = "fulano@gmail.com",
                url = "http://fulano.com.br"
            };

        }
        
        [When(@"I post it to the /people API endpoint")]
        public void WhenIPostItToThePeopleAPIEndpoint()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49556/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("people", personData).Result;                
            }
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
    }

        [Given(@"an existing person resource")]
        public void GivenAnExistingPersonResource()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"a valid update message to that resource")]
        public void GivenAValidUpdateMessageToThatResource()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I run a PUT command against the /people endpoint")]
        public void WhenIRunAPUTCommandAgainstThePeopleEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive a success \(code (.*)\) status message")]
        public void ThenIReceiveASuccessCodeStatusMessage(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive the updated resource in the body of the message")]
        public void ThenIReceiveTheUpdatedResourceInTheBodyOfTheMessage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"an invalid update message to that resource")]
        public void GivenAnInvalidUpdateMessageToThatResource()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive an error \(code (.*)\) status message")]
        public void ThenIReceiveAnErrorCodeStatusMessage(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I receive a list of validation errors in the body of the message")]
        public void ThenIReceiveAListOfValidationErrorsInTheBodyOfTheMessage()
        {
            ScenarioContext.Current.Pending();
        }







}
