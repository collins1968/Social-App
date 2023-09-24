using MyAppFrontend.Model;
using MyAppFrontend.Model.Comments;
using Newtonsoft.Json;
using System.Text;

namespace MyAppFrontend.Services.comment
{
    public class CommentService : ICommentInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://localhost:7116/api/Comment";

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDto> AddCommentAsync(CommentRequestDto comment)
        {
           var comments = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl, comments);
            var content = await response.Content.ReadAsStringAsync();
            var commentResponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (commentResponse.IsSuccess)
            {
                return commentResponse;
            }
            else
            {
                throw new Exception(commentResponse.Message);
            }
        }

        
    }
}
