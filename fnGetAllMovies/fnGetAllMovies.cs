using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;

namespace Company.Function
{
    public class fnGetAllMovies
    {
       private readonly ILogger<fnGetAllMovies> _logger;
        private readonly CosmosClient _cosmosClient;

        public fnGetAllMovies(ILogger<fnGetAllMovies> logger, CosmosClient cosmosClient)
        {
            _logger = logger;
            _cosmosClient = cosmosClient;
        }

        [Function("all")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var container = _cosmosClient.GetContainer("PrjFlixDB","movies");
            var id = req.Query["id"];
            var query = $"SELECT * FROM c";
            var queryDefinition = new QueryDefinition(query);
            var result = container.GetItemQueryIterator<MovieResult>(queryDefinition);
            var results = new List<MovieResult>();

            while (result.HasMoreResults)
            {
            foreach (var item in await result.ReadNextAsync())
            {
                results.Add(item);
            }
            }
             var responseMessage = req.CreateResponse(System.Net.HttpStatusCode.OK);
             await responseMessage.WriteAsJsonAsync(results);
            return responseMessage;
        }
    }
}
