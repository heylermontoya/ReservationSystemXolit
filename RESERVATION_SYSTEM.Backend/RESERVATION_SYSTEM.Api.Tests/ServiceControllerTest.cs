using Newtonsoft.Json;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Application.Feature.service.Commands;
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
    public class ServiceControllerTest
    {
        private const string PARAMETER_PATH = "api/Service";

        private readonly TestStartup<Program> factory;
        private readonly HttpClient httpClient;

        public ServiceControllerTest(
            TestStartup<Program> factory
        )
        {
            this.factory = factory;
            httpClient = this.factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        [Fact]
        public async Task ObtainServiceAsync_Ok()
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
        public async Task CreateServiceAsync_Ok()
        {
            //Arrange
            CreateServiceCommand createServiceCommand = new(
                "name sd",
                 "email sd",
                 1,
                 2,
                 1,
                 2
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createServiceCommand),
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
        public async Task UpdateServiceAsync_Ok()
        {
            //Arrange
            UpdateServiceCommand UpdateServiceCommand = new(
                Guid.Parse("f188d658-7d23-40cd-a7e4-0453209a0979"),
                "name sde",
                 "email sde",
                 1,
                 2,
                 1,
                 2
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(UpdateServiceCommand),
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
        public async Task DeleteServiceAsync_Ok()
        {
            //Arrange
            DeleteServiceCommand deleteServiceCommand = new(
                Guid.Parse("57e66b0c-0152-4041-aa33-8fc79612889c")
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Delete,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(deleteServiceCommand),
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
