using Newtonsoft.Json;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Application.Feature.customer.Commands;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RESERVATION_SYSTEM.Api.Tests
{
    [Collection("TestCollection")]
    public class CustomerControllerTest
    {
        private const string PARAMETER_PATH = "api/Customer";

        private readonly TestStartup<Program> factory;
        private readonly HttpClient httpClient;

        public CustomerControllerTest(
            TestStartup<Program> factory
        )
        {
            this.factory = factory;
            httpClient = this.factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        [Fact]
        public async Task ObtainCustomerAsync_Ok()
        {
            //Arrange
            List<FieldFilter> fieldFilters = [];

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}/list", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(fieldFilters),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();

            List<CustomerDto> listCustomerDto = JsonConvert
                .DeserializeObject<List<CustomerDto>>(data);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(listCustomerDto);
            Assert.True(listCustomerDto.Count != 0);
            Assert.NotEmpty(listCustomerDto);
        }

        [Fact]
        public async Task CreateCustomerAsync_Ok()
        {
            //Arrange
            CreateCustomerCommand createCustomerCommand = new(
                "name sd",
                 "email sd",
                 "123456"
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createCustomerCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            
            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);            
        }
        
        [Fact]
        public async Task UpdateCustomerAsync_Ok()
        {
            //Arrange
            UpdateCustomerCommand updateCustomerCommand = new(
                Guid.Parse("526f3a54-45e2-4196-bd3f-c936ab336e91"),
                "name",
                 "email",
                 "123"
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updateCustomerCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            
            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);            
        }
        
        [Fact]
        public async Task DeleteCustomerAsync_Ok()
        {
            //Arrange
            DeleteCustomerCommand deleteCustomerCommand = new(
                Guid.Parse("a2ec74d3-4517-4592-a49e-101808e7481c")
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Delete,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(deleteCustomerCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            
            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);            
        }
    }
}
