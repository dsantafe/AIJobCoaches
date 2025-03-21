namespace AIJobCoaches.Infrastructure.Managers
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public class CosmosDBManager<TEntity> where TEntity : class
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBManager(string databaseName,
            string containerName,
            string connectionString)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _container = _cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<TEntity> AddItemAsync(TEntity entity)
        {
            var result = await _container.CreateItemAsync(entity);
            return result.Resource;
        }

        public async Task<TEntity> UpdateItemAsync(TEntity entity)
        {
            var result = await _container.UpsertItemAsync(entity);
            return result.Resource;
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<object>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<TEntity>> GetItemsAsync(string queryString)
        {
            FeedIterator<TEntity> iterator = GetFeedIterator(queryString);
            List<TEntity> resultsJson = await FetchResultsAsync(iterator);
            return resultsJson;
        }

        public async Task<TEntity> GetItemAsync(string id)
        {
            try
            {
                var query = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
                return query.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<string>> ExecuteQuery(string queryString)
        {
            List<string> results = [];

            FeedIterator<TEntity> iterator = GetFeedIterator(queryString);
            List<TEntity> resultsJson = await FetchResultsAsync(iterator);

            if (resultsJson == null || resultsJson.Count == 0)
            {
                results.Add("No Result Found");
                return results;
            }

            List<string> firstRow = GetPropertyNames(resultsJson);
            results.Add(string.Join(" | ", firstRow));

            foreach (var item in resultsJson)
                results.Add(string.Join(" | ", GetPropertyValues(item, firstRow)));

            return results;
        }

        private FeedIterator<TEntity> GetFeedIterator(string queryString)
        {
            return !string.IsNullOrEmpty(queryString)
                ? _container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString))
                : _container.GetItemLinqQueryable<TEntity>().ToFeedIterator();
        }

        private static async Task<List<TEntity>> FetchResultsAsync(FeedIterator<TEntity> iterator)
        {
            List<TEntity> resultsJson = [];
            while (iterator.HasMoreResults)
            {
                FeedResponse<TEntity> response = await iterator.ReadNextAsync();
                resultsJson.AddRange(response);
            }
            return resultsJson;
        }

        private static List<string> GetPropertyNames(List<TEntity> resultsJson)
        {
            var firstRow = new HashSet<string>();
            foreach (var item in resultsJson)
            {
                var jsonObject = JObject.FromObject(item);
                foreach (var prop in jsonObject.Properties())
                    firstRow.Add(prop.Name);
            }
            return firstRow.ToList();
        }

        private static List<string> GetPropertyValues(dynamic item,
            List<string> propertyNames)
        {
            var jsonObject = JObject.FromObject(item);
            var values = new List<string>();

            foreach (var prop in propertyNames)
            {
                if (jsonObject[prop] != null)
                {
                    if (jsonObject[prop] is JArray array)
                    {
                        // Para cada objeto en el array, combina todas sus propiedades en un solo string
                        var arrayValues = array.Select(arrItem =>
                        {
                            var obj = arrItem as JObject;
                            // Concatena todas las propiedades clave-valor del objeto en un solo string
                            var propertyPairs = obj?.Properties()
                                .Select(p => $"{p.Name}: {p.Value}")
                                .ToList();

                            return propertyPairs != null ? string.Join(", ", propertyPairs) : "NULL";
                        });

                        values.Add(string.Join(", ", arrayValues));
                    }
                    else
                        values.Add(jsonObject[prop].ToString());
                }
                else
                    values.Add("NULL");
            }
            return values;
        }
    }
}
