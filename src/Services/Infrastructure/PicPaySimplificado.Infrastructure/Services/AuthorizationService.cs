using PicPaySimplificado.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;

        public AuthorizationService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<bool> AuthorizaAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("8fafdd68-a090-496f-8c9a-3442cf30dae6");

            if (response.IsSuccessStatusCode) return true;

            return false;
        }
    }
}
