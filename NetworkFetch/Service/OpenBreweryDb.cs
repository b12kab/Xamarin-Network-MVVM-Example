using NetworkFetch.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetworkFetch.Service
{
    /// <summary>
    /// https://www.openbrewerydb.org/
    /// </summary>
    public class OpenBreweryDb
    {
        public const string BASE_BREWERY_LIST_URL = "https://api.openbrewerydb.org/breweries?by_state={0}&page={1}";

        public static async Task<IList<Brewery>> GetBreweriesAsync(string state, int page)
        {
            IList<Brewery> breweries = new List<Brewery>();

            string url = String.Format(BASE_BREWERY_LIST_URL, state, page);
            using (HttpClient httpClient = new HttpClient())
            {
                System.Diagnostics.Debug.WriteLine("Get data - starting network call");
                var response = await httpClient.GetAsync(url);
                System.Diagnostics.Debug.WriteLine("Get data - network call complete");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var posts = JsonConvert.DeserializeObject<IList<Brewery>>(content);
                    breweries = posts;
                }
                else
                {
                    httpClient.CancelPendingRequests();
                }
            }

            return breweries;
        }

        public static async Task<IList<Brewery>> GetBreweriesPages(string state, int startPage, int endPage)
        {
            List<Task<IList<Brewery>>> taskList = new List<Task<IList<Brewery>>>();

            for (int i = startPage; i <= endPage; i++)
            {
                var task = GetBreweriesAsync(state, i);
                task.Start();
                taskList.Add(task);
            }
            await Task.WhenAll(taskList.ToArray());

            List<Brewery> breweries = new List<Brewery>();
            foreach (var item in taskList)
            {
                breweries.AddRange(item.Result);
            }

            return breweries;
        }
    }
}
