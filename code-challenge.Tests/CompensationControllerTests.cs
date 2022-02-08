using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
            FirstName = "George",
            LastName = "Harrison",
            Position = "Developer III",
            Department = "Engineering"
            };
            //Creates a new Compensation object to test 
            var compensation = new Compensation()
            {
                employee = employee,
                salary = "20",
                effectiveDate = "2/1/2022"
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newCompensation = response.DeserializeContent<Compensation>();

            //Checks to if new compensation object matches the predetermined compensation fields
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.employee.FirstName, newCompensation.employee.FirstName);
            Assert.AreEqual(compensation.employee.LastName, newCompensation.employee.LastName);
            Assert.AreEqual(compensation.employee.Position, newCompensation.employee.Position);
            Assert.AreEqual(compensation.salary, newCompensation.salary);
            Assert.AreEqual(compensation.effectiveDate, newCompensation.effectiveDate);

        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var expectedSalary = "20";
            var expectedEffectiveDate = "2/1/2022";

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();

            Assert.AreEqual(employeeId, compensation.EmployeeId);
            Assert.AreEqual(expectedFirstName, compensation.employee.FirstName);
            Assert.AreEqual(expectedLastName, compensation.employee.LastName);
            Assert.AreEqual(expectedSalary, compensation.salary);
            Assert.AreEqual(expectedEffectiveDate, compensation.effectiveDate);
        }
    }
}
