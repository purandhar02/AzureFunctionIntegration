using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
namespace AzureFunctionIntegration
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("MyFunction");
            logger.LogInformation("HTTP trigger function processed a request.");

            // Extract query parameters from the URL
            var queryParams = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var name = queryParams["name"] ?? "Azure";

            // Create a response
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Hello, {name}");
            await response.WriteStringAsync($"Welcome!!");
            return response;
        }
    }
}
