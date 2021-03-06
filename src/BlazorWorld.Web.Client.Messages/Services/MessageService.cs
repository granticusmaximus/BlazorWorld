﻿using BlazorWorld.Core.Entities.Content;
using BlazorWorld.Web.Client.Common;
using BlazorWorld.Web.Client.Common.Services;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Messages.Services
{
    public class MessageService : ApiService, IMessageService
    {
        private const string API_URL = "api/Message";
        private readonly IUserApiService _userApiService;

        public MessageService(
            IHttpClientFactory httpClientFactory,
            IUserApiService userApiService
            ) : base(httpClientFactory)
        {
            _userApiService = userApiService;
        }

        public async Task<Message[]> GetAsync(string groupId, int currentPage)
        {
            var request = $"{API_URL}?groupId={groupId}";
            var messages = await AuthorizedHttpClient.GetFromJsonAsync<Message[]>(request);
            messages.ToList().ForEach(async m => m.Username = await _userApiService.GetUserNameAsync(m.CreatedBy));
            return messages;
        }

        public async Task<int> GetCountAsync(string groupId)
        {
            var request = $"{API_URL}/getCount?groupId={groupId}";
            var response = await AuthorizedHttpClient.GetAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            int count;
            int.TryParse(responseString, out count);
            return count;
        }

        public async Task<int> GetPageSizeAsync(string module)
        {
            var request = $"{API_URL}/getPageSize?module={module}";
            var response = await AuthorizedHttpClient.GetAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            int size;
            int.TryParse(responseString, out size);
            return size;
        }
    }
}
