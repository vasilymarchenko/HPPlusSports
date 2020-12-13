using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace HPlusSportsAPI.Services
{
    /// <summary>
    /// Responsible for saving and loading products to Cosmos DB
    /// </summary>
    public class CosmosDBService : IDocumentDBService
    {
        DocumentClient docClient;

        string dbName, collectionName;
        Uri productCollectionUri;

        public CosmosDBService(IOptions<CosmosDBServiceOptions> options, DocumentClient client)
        {
            dbName = options.Value.DBName;
            collectionName = options.Value.DBCollection;

            docClient = client;
            productCollectionUri = UriFactory.CreateDocumentCollectionUri(dbName, collectionName);

        }

        public async Task<T> AddProductAsync<T>(T product)
        {
            var dbResponse = await docClient.CreateDocumentAsync(productCollectionUri, product);
            return (dynamic)dbResponse.Resource;
        }

        public async Task<ProductBase> GetProductAsync(string id)
        {
            var docUri = UriFactory.CreateDocumentUri(
                dbName, collectionName, id);

            Document doc = await docClient.ReadDocumentAsync(
                docUri,
                new RequestOptions
                {
                    PartitionKey = new PartitionKey(Undefined.Value)
                });

            if (doc.GetPropertyValue<string[]> ("Sizes") != null)
            {
                return (ClothingProduct)(dynamic)doc;
            }

            return (NutritionalProduct)(dynamic)doc;
        }

        public async Task<List<ProductBase>> GetProductsAsync()
        {
            var productList = new List<ProductBase>();

            var products = await docClient.ReadDocumentFeedAsync(
                productCollectionUri);

            foreach (var item in products)
            {
                productList.Add((ProductBase)item);
            }

            return productList;
        }

        public async Task AddImageToProductAsync(string id, string imageUri)
        {
            var docUri = UriFactory.CreateDocumentUri(
                dbName, collectionName, id);

            var doc = await docClient.ReadDocumentAsync(
                docUri, new RequestOptions
                {
                    PartitionKey = new PartitionKey(Undefined.Value)
                });

            doc.Resource.SetPropertyValue("image", imageUri);

            await docClient.ReplaceDocumentAsync(doc);
        }
    }
}
