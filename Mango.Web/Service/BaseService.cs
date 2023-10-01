using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        // inject the http client to make the api call using it
        private readonly IHttpClientFactory _httpClientFactory;
        private object encoding;

        public BaseService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            // Create an HttpClient instance using the factory
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");

            // when we are making the request
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            //token

            // the reqesturi  to specify the url that will invoke to access any api, and i will get the value from the requestDto
            message.RequestUri = new Uri(requestDto.Url);
            // if the request is post or update , we need to declear the message content
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

         // handling the response
            // variable to hold the response
                HttpResponseMessage? apiResponse = null;
            // handling the response for each method type
            switch (requestDto.ApiType)
            {
                case ApiType.POST:
                        message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;    
                    break;
            }
            // final thing is to send this and get ur response back
            apiResponse = await client.SendAsync(message);
            // check the status code that we recive
            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "UnAuthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        // if the status is ok, we need to retrive the content form the api response
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false,
                };
                return dto;
            }
        }
    }
}
