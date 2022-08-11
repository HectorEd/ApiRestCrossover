using ApiRestCrossover.Interfaces;
using ApiRestCrossover.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiRestCrossover.Services
{
    public class ApiGitService : IApiGitService
    {
        private static string _urlBase;

        public ApiGitService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlBase = builder.GetSection("ApiGitSettings:UrlBase").Value;
        }
        public async Task<List<UserGitModel>> Repository()
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_urlBase)
                };
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                var response = await client.GetAsync("/repositories");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<UserGitModel>>(jsonResult).OrderBy(x=>x.id).Take(10).ToList();
                    return result;
                }
                return new List<UserGitModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
