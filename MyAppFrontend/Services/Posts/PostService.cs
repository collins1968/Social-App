using MyAppFrontend.Model;
using MyAppFrontend.Model.Posts;
using Newtonsoft.Json;
using System.Text;

namespace MyAppFrontend.Services.Posts
{
    public class PostService : IPostInterface
    {

        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://localhost:7133/api/Post";

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDto> AddPostAsync(PostUpdateDto addpost)
        {
           var post = new StringContent(JsonConvert.SerializeObject(addpost), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl, post);
            var content = await response.Content.ReadAsStringAsync();
            var postResponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (postResponse.IsSuccess)
            {
                return postResponse;
            }
            else
            {
                throw new Exception(postResponse.Message);
            }
        }

        public async Task<ResponseDto> DeletePostAsync(Guid id)
        {
            var deletePost = _httpClient.DeleteAsync($"{baseUrl}/{id}");
            var content = await deletePost.Result.Content.ReadAsStringAsync();
            var deletePostResponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (deletePostResponse.IsSuccess)
            {
                return deletePostResponse;
            }
            else
            {
                throw new Exception(deletePostResponse.Message);
            }
        }

        public async Task<List<PostResponseDto>> GetAllPostsAsync()
        {
            var posts = await _httpClient.GetAsync(baseUrl);
            var content = await posts.Content.ReadAsStringAsync();
            var postsList = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (postsList.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<PostResponseDto>>(postsList.Result.ToString());
            }
            else
            {
                throw new Exception(postsList.Message);
            }

            return new List<PostResponseDto>();
            
        }

        public async Task<PostDto> GetPostByIdAsync(Guid id)
        {
            var singlePost = await _httpClient.GetAsync($"{baseUrl}/{id}");
            var content = await singlePost.Content.ReadAsStringAsync();
            var singlePostResponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (singlePostResponse.IsSuccess)
            {
                return JsonConvert.DeserializeObject<PostDto>(singlePostResponse.Result.ToString());
            }
            else
            {
                throw new Exception(singlePostResponse.Message);
            }

        }

        public async Task<ResponseDto> UpdatePostAsync(Guid id, PostUpdateDto updatepost)
        {
            var post = new StringContent(JsonConvert.SerializeObject(updatepost), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{baseUrl}?id={id}", post);
            var content = await response.Content.ReadAsStringAsync();
            var postResponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (postResponse.IsSuccess)
            {
                return postResponse;
            }
            else
            {
                throw new Exception(postResponse.Message);
            }

        }
    }
}
