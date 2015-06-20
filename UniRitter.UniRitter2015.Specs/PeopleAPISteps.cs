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
        class Person
        {
            public Guid? id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string url { get; set; }
        }

        class FullMessage
        {
            public Guid? id { get; set; }
            public string message { get; set; }
        }

        Person personData;
        FullMessage messageData;
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


        //Scenario Invalid person data on insertion
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
            Assert.That(validationMessage, Contains.Substring("The firstName field is required."));
            Assert.That(validationMessage, Contains.Substring("The email field is not a valid e-mail address."));
        }


        //Scenario: Valid update
        [Given(@"an existing person resource")]
        public void GivenAnExistingPersonResource()
        {
            personData = new Person
            {
                firstName = "Diego",
                lastName = "Ritzel",
                email = "diego@sistemasd.com.br",
                url = "http://fulano.com.br"
            };
        }

        [Given(@"a valid update message to that resource")]
        public void GivenAValidUpdateMessageToThatResource()
        {
            messageData = new FullMessage
            {
                message = "Cadastrado com sucesso"
            };
        }

        //TODO
        [When(@"I run a PUT command against the /people endpoint")]
        public void WhenIRunAPutCommandAgainstThePeopleEndpoint()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I receive a success \(code (.*)\) status message")]
        public void ThenIReceiveASuccessCodeStatusMessage(int code)
        {
            CheckCode(code);
        }

        [Then(@"I receive the updated resource in the body of the message")]
        public void ThenIReceiveTheUpdatedResourceInTheBodyOfTheMessage()
        {
            result = response.Content.ReadAsAsync<Person>().Result;
            Assert.That("Diego", Is.EqualTo(personData.firstName));
        }

        //Scenario: Invalid update
        //TODO
        [Given(@"an invalid update message to that resource")]
        public void ThenAnInvalidUpdateMessageToThatResource()
        {
            ScenarioContext.Current.Pending();
        }

        //TODO
        [Then(@"I receive an error \(code (.*)\) status message")]
        public void ThenIReceiveAnErrorCodeStatusMessage(int code)
        {
            ScenarioContext.Current.Pending();
        }

        //TODO
        [Then(@"I receive a list of validation errors in the body of the message")]
        public void ThenIReceiveAListOfValidationErrorsInTheBodyOfTheMessage()
        {
            ScenarioContext.Current.Pending();
        }

        //Scenario: Add a valid post
	    //Given a valid post resource
        [Given(@"a valid post resource")]
        public void GivenAValidPostResource()
        {
            result = response.Content.ReadAsAsync<Person>().Result;
            Assert.That(result.firstName, Is.EqualTo(personData.firstName));
        }


	    //When I post is to the /posts endpoint

	    //Then I get a success (code 201) response code

	    //And I receive the posted resource

	    //And the resource id is populated

    }
}
