using JituPost.Services.IService;
using Newtonsoft.Json;
using JituPost.Services.IService;
using JituPost.Models.Dtos;
using Models.Dtos;

namespace JituPost.Services
{
    public class CommentsService : ICommentInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;

        //injecting httpclientfactory in the constructor
        public CommentsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<CommentsDto>> GetCommentsAsync()
        {
            //create a client
            var client = _httpClientFactory.CreateClient("Comments");
            //make a request
            var response = await client.GetAsync("api/Comments");
            //read the response
            var content = await response.Content.ReadAsStringAsync();
            //deserialize the response
            var comments = JsonConvert.DeserializeObject<ResponseDto>(content);
            // check if the response is success
            if(comments.IsSuccess)
            {
                //deserialize the data
                return JsonConvert.DeserializeObject<IEnumerable<CommentsDto>>(Convert.ToString(comments.Data));
            }
            return new List<CommentsDto>();

        }
    }
}