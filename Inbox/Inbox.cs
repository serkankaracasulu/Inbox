using Inbox.InboxReponses;
using Inbox.InboxReponses.Requests;
using Inbox.InboxReponses.ResultObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inbox
{
    public class Inbox : IInbox
    {
        private const string TOKEN_API = "/token";
        private const string REFRESH_TOKEN_API = "/token/refresh";
        private const string NEWSLETTER_API = "/inbox/v1/newsletters";
        private const string CONTACT_LIST = "/inbox/v1/contactlists";

        private readonly string _userName;
        private readonly string _password;

        private DateTime TokenExpiredDate { get; set; }
        private Token Token { get; set; }

        private readonly HttpClient _client;

        public Inbox(string userName, string password)
        {
            _userName = userName;
            _password = password;
            _client = HttpClientFactory.Create();
            _client.BaseAddress = new Uri("https://useapi.useinbox.com/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Token = GetToken().GetAwaiter().GetResult();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token.token_type, Token.access_token);
        }

        private async Task<Token> GetToken()
        {
            var tokenBody = new TokenVariables(_userName, _password);
            var tokenJson = new StringContent(
                JsonSerializer.Serialize(tokenBody),
                Encoding.UTF8,
                "application/json");
            var httpResponse = await _client.PostAsync(TOKEN_API, tokenJson);
            httpResponse.EnsureSuccessStatusCode();
            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            var response = await JsonSerializer.DeserializeAsync<TokenResult>(responseStream);
            return response.resultObject;

        }

        public async Task<IEnumerable<Newsletter>> GetNewsletters()
        {
            await CheckTokenExpired();
            var httpResponse = await _client.GetAsync(NEWSLETTER_API);
            httpResponse.EnsureSuccessStatusCode();
            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            var response = await JsonSerializer.DeserializeAsync<NewsLetterResult>(responseStream);
            return response.resultObject.items;
        }

        public async Task<IEnumerable<Contact>> GetContactLists()
        {
            await CheckTokenExpired();
            var httpResponse = await _client.GetAsync(CONTACT_LIST);
            httpResponse.EnsureSuccessStatusCode();
            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            var response = await JsonSerializer.DeserializeAsync<ContactListResult>(responseStream);
            return response.resultObject.items;
        }

        private async Task<RefreshToken> GetRefreshToken()
        {
            var tokenBody = new RefreshTokenVariables(Token.access_token);
            var tokenJson = new StringContent(
                JsonSerializer.Serialize(tokenBody),
                Encoding.UTF8,
                "application/json");
            var httpResponse = await _client.PostAsync(REFRESH_TOKEN_API, tokenJson);
            httpResponse.EnsureSuccessStatusCode();
            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<RefreshToken>(responseStream);
        }
        private bool IsTokenExpired()
        {
            return TokenExpiredDate < DateTime.UtcNow;
        }
        private async Task CheckTokenExpired()
        {
            if (IsTokenExpired())
            {
                var refreshToken = await GetRefreshToken();
                Token = refreshToken;
                SetTokenExpiredDate(Token.expires_in);
            }
        }
        private void SetTokenExpiredDate(int sec)
        {
            TokenExpiredDate = DateTime.UtcNow.AddSeconds(sec);
        }
    }
}
