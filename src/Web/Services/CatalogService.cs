using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using LinFx.Application.Models;
using LinFx.Application.Abstractions;
using Mall.Web.Models;
using LinFx.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace Mall.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:53253/api/v1/catalog/");
        }

        public async Task<CatalogItem> GetCatalogItem(int id)
        {
            string uri = "";
            var responseString = await _httpClient.GetStringAsync(uri);

            var catalog = JsonConvert.DeserializeObject<CatalogItem>(responseString);
            return catalog;
        }

        public async Task<IPagedResult<CatalogItem>> GetCatalogItems(int page, int take, int? brand, int? type)
        {
            string uri = "";
            var responseString = await _httpClient.GetStringAsync(uri);

            return JsonUtils.DeserializeWithType<PagedResult<CatalogItem>>(responseString);
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            string uri = "catalogBrands";
            var responseString = await _httpClient.GetStringAsync(uri);
            var brands = JArray.Parse(responseString);

            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = null, Text = "All", Selected = true }
            };

            foreach (var brand in brands.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = brand.Value<string>("id"),
                    Text = brand.Value<string>("brand")
                });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            string uri = "";
            var responseString = await _httpClient.GetStringAsync(uri);

            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = null, Text = "All", Selected = true }
            };

            var brands = JArray.Parse(responseString);
            foreach (var brand in brands.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = brand.Value<string>("id"),
                    Text = brand.Value<string>("type")
                });
            }

            return items;
        }
    }
}
