using Newtonsoft.Json;
using RESERVATION_SYSTEM.Application.DTOs;
using RESERVATION_SYSTEM.Application.Feature.reservation.Commands;
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
    public class ReservationControllerTest
    {
        private const string PARAMETER_PATH = "api/Reservation";

        private readonly TestStartup<Program> factory;
        private readonly HttpClient httpClient;

        public ReservationControllerTest(
            TestStartup<Program> factory
        )
        {
            this.factory = factory;
            httpClient = this.factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        [Fact]
        public async Task ObtainReservationAsync_Ok()
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
        public async Task CreateReservationAsync_Ok()
        {
            //Arrange
            CreateReservationCommand createReservationCommand = new(
                new("526f3a54-45e2-4196-bd3f-c936ab336e91"),
                new("f188d658-7d23-40cd-a7e4-0453209a0979"),
                DateTime.Now.AddHours(5),
                DateTime.Now.AddHours(6),
                2,
                1
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createReservationCommand),
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
        public async Task UpdateReservationAsync_Ok()
        {
            //Arrange
            UpdateReservationCommand updateReservationCommand = new(
                new("ea133773-f0c3-4391-9a1f-bd547f5542aa"),
                new("526f3a54-45e2-4196-bd3f-c936ab336e91"),
                new("f188d658-7d23-40cd-a7e4-0453209a0979"),
                DateTime.Now,
                DateTime.Now.AddHours(1),
                2
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updateReservationCommand),
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
        public async Task UpdateReservationByCancelAsync_Ok()
        {
            //Arrange
            CancelReservationCommand cancelReservationCommand = new(
                Guid.Parse("92c2cfe1-610b-47e1-89ac-6f1d08da6831")
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}/cancel", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(cancelReservationCommand),
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
