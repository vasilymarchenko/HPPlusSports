using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HPlusSportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HPlusSportsAPI.Services.IDocumentDBService docService;

        public ProductController(HPlusSportsAPI.Services.IDocumentDBService docDbService)
        {
            docService = docDbService;
        }

        // GET api/product
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<ProductBase> products = await docService.GetProductsAsync();

            return new JsonResult(products);
        }


        // GET api/product/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id)
        {
            var product = await docService.GetProductAsync(id);

            return new JsonResult(product);
        }

        [HttpPost]
        [Route("/api/[controller]/Nutritional")]
        public async Task<JsonResult> AddNutritional(NutritionalProduct product)
        {
            var newProduct = await docService.AddProductAsync<NutritionalProduct>(product);
            return new JsonResult(newProduct);
        }

        [HttpPost]
        [Route("/api/[controller]/Clothing")]
        public async Task<JsonResult> AddClothing(ClothingProduct product)
        {
            var newProduct = await docService.AddProductAsync<ClothingProduct>(product);
            return new JsonResult(newProduct);
        }
    }
}
