using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using ApiRestCrossover.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ApiRestCrossover.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private static string _user;
        private static string _userEmail;
        private static string _password;
        private static string _urlBase;
        private static string _token;

        public ApiAuthService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _user= builder.GetSection("ApiAuthSettings:User").Value;
            _userEmail = builder.GetSection("ApiAuthSettings:UserEmail").Value;
            _password = builder.GetSection("ApiAuthSettings:Password").Value;
            _urlBase = builder.GetSection("ApiAuthSettings:UrlBase").Value;
        }
        public async Task Authenticate()
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_urlBase)
                };
                var credentials = new UserAuth() { Email = _userEmail, Password = _password };
                var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/authaccount/login", content);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResult>(jsonResult);
                _token = result.data.Token;
                _user = result.data.Id.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UserModel> User()
        {
            try
            {
                await Authenticate();
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_urlBase)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await client.GetAsync($"/api/users/{_user}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserModel>(jsonResult);
                    return result;
                }
                return new UserModel();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
