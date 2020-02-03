using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BTCPrice
{
    public class USD
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }
    public class GBP
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }
    public class Bpi
    {
        public USD USD { get; set; }
        public GBP GBP { get; set; }        
    } 
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Bpi bpi)
        {
            Console.WriteLine($"Symbol: {bpi.GBP.symbol}\tRate: " +
                $"{bpi.GBP.rate}\tDescription: {bpi.GBP.description}");
        }        

        static async Task<Bpi> GetProductAsync(string path)
        {
            Bpi bpi = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("{0}", response.Content.ReadAsStringAsync().Result);
                bpi = await response.Content.ReadAsAsync<Bpi>();

            }
            return bpi;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            //client.BaseAddress = new Uri("https://api.coindesk.com/v1/bpi/currentprice.json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                Bpi bpi = null;
                var url = "http://api.coindesk.com/v1/bpi/currentprice.json";
                Console.WriteLine($"Created at {url}");

                // Get the product
                bpi = await GetProductAsync(url);
                ShowProduct(bpi);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}