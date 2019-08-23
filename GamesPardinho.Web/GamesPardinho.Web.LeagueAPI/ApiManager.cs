using GamesPardinho.Web.LeagueAPI.Extensions;
using GamesPardinho.Web.LeagueAPI.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GamesPardinho.Web.LeagueAPI
{
    public class ApiManager
    {
        public string BaseUrl { get; set; }
        public NVObject ApiKey { get; set; }

        public HttpClient Client { get; }

        public ApiManager(string baseUrl)
        {
            BaseUrl = baseUrl;
            Client = new HttpClient();
            ApiKey = AppSettings.LoadSettings().ApiKey;

            Client.DefaultRequestHeaders.Add(ApiKey.Name, ApiKey.Value);
        }

        public async Task<TEntity> GetAsync<TEntity>(string url) where TEntity : class
        {
            TEntity entity = null;
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<TEntity>();                
            }
            return entity;
        }

        public async Task<TResult> PostAsync<TBody, TResult>(string url, TBody body)
        {
            var bodyJson = JsonConvert.SerializeObject(body);
            var response = await Client.PostAsync(url, new StringContent(bodyJson, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TResult>();
        }

    }
}
