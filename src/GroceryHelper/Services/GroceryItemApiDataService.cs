using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GroceryHelper.Models;

namespace GroceryHelper.Services
{
	public class GroceryItemApiDataService : BaseHttpService, IGroceryItemDataService
    {
		readonly Uri _baseUri;
		readonly IDictionary<string, string> _headers;

		public GroceryItemApiDataService()
        {
			Uri x = new Uri("https://groceryhelper.azurewebsites.net");
            
			_baseUri = x;
			_headers = new Dictionary<string, string>();

			//TODO: Add header with auth-based token in Chp 7
			_headers.Add("zumo-api-version", "2.0.0");
        }
        
        public async Task<IList<Item>> GetItemsAsync()
		{
			try
			{
				var url = new Uri(_baseUri, "/tables/item");
				var response = await SendRequestAsync<Item[]>(url, HttpMethod.Get, _headers);
				return response;
			} catch(Exception e)
			{
				return null;
			}

		}

		public async Task<Item> GetItemAsync(string id)
		{
			var url = new Uri(_baseUri, string.Format("/tables/item/{0}", id));
			var response = await SendRequestAsync<Item>(url, HttpMethod.Get, _headers);

			return response;
		}

		public async Task<Item> AddItemAsync(Item item)
        {
            var url = new Uri(_baseUri, "/tables/item");
            var response = await SendRequestAsync<Item>(url, HttpMethod.Post, _headers, item);

            return response;
        }
        
		public async Task<Item> UpdateItemAsync(Item item)
        {
			var url = new Uri(_baseUri, string.Format("/tables/item/{0}", item.Id));
			var response = await SendRequestAsync<Item>(url, new HttpMethod("PATCH"), _headers, item);
            
            return response;
        }
        
		public async Task RemoveItemAsync(Item item)
        {
			var url = new Uri(_baseUri, string.Format("/tables/item/{0}", item.Id));
			var response = await SendRequestAsync<Item>(url, HttpMethod.Delete, _headers);
        }
    }
}
