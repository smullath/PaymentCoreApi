using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using PaymentCoreApi.Model;

namespace PaymentCoreApi.Services
{
    public class CosmosDbServices : ICosmosDbServices
    {
        private Container _container;
        public CosmosDbServices(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(payment item)
        {
            await this._container.CreateItemAsync<payment>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<payment>(id, new PartitionKey(id));
        }

        public async Task<payment> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<payment> response = await this._container.ReadItemAsync<payment>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<payment>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<payment>(new QueryDefinition(queryString));
            List<payment> results = new List<payment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, payment item)
        {
            await this._container.UpsertItemAsync<payment>(item, new PartitionKey(id));
        }
    }
}


