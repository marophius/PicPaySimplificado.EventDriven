using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces.Services;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(10, onRetry: (resonse, retryCount) =>
                {
                    Console.WriteLine($"Trying to connect to notification service {retryCount}");
                });
        }
        public async Task SendNotificationAsync(User user, string message)
        {
            try
            {
                string email = user.Email.Address;

                HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await _httpClient.PostAsJsonAsync("notify",
                        new { Email = email, Message = message });
                });
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
