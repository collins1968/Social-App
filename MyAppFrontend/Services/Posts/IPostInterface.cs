using MyAppFrontend.Model;
using MyAppFrontend.Model.Posts;

namespace MyAppFrontend.Services.Posts
{
    public interface IPostInterface
    {
        Task<List<PostResponseDto>> GetAllPostsAsync();
        Task<ResponseDto> AddPostAsync(PostUpdateDto addpost);

        Task<ResponseDto> UpdatePostAsync(Guid id, PostUpdateDto updatepost);

        Task<ResponseDto> DeletePostAsync(Guid id);

        Task<PostDto> GetPostByIdAsync(Guid id);

       
    }
}
