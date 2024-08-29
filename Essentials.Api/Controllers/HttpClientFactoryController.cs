using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Essentials.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class HttpClientFactoryController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [HttpGet]
        public async Task<ActionResult<IList<object>>> Get()
        {
            using HttpClient client = _httpClientFactory.CreateClient("jsonPlaceHolder");

            var list = await client.GetFromJsonAsync<object[]>("todos", new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return Ok(list);
        }

        [HttpPost]
        public async Task Post(object item)
        {
            using HttpClient client = _httpClientFactory.CreateClient("jsonPlaceHolder");

            using StringContent json = new(
                JsonSerializer.Serialize(item, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using HttpResponseMessage httpResponse =
                await client.PostAsync("/posts", json);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
