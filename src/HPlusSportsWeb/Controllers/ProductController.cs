using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HPlusSportsWeb.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client;
        private readonly ILogger _logger;

        public ProductController(HttpClient apiClient, ILogger<ProductController> logger)
        {
            client = apiClient;
            _logger = logger;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //https://medium.com/it-dead-inside/docker-containers-and-localhost-cannot-assign-requested-address-6ac7bc0d042b
            _logger.LogInformation($"API base address: '{client.BaseAddress}'");
            var response = await client.GetStringAsync("product");
            var products = JArray.Parse(response);
            return View(products);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var response = await client.GetStringAsync($"product/{id}" );

            var product = JObject.Parse(response);
            return View(product);
        }
    }
}
