using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Post_Service.Data;
using Post_Service.Model;
using Post_Service.Model.Dto;
using Post_Service.Services.IServices;

namespace Post_Service.Services
{
    public class PostService : IPostInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public PostService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
           _clientFactory = httpClientFactory;
        }
        

        public async Task<string> CreatePost(Post Post)
        {
            
           _context.Posts.Add(Post);
            await _context.SaveChangesAsync();
            return "Post Created Successfully";

        }

        public async Task<string> DeletePost(Post post)
        {
            _context.Posts.Remove(post);    
            await _context.SaveChangesAsync();
            return "Post Deleted Successfully";

        }

        public async Task<List<CommentResponse>> getcommentsbypostId(Guid id)
        {
            try
            {
                var client = _clientFactory.CreateClient("Comments");
                var response = await client.GetAsync($"api/Comment/post/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var comments = JsonConvert.DeserializeObject<ResponseDto>(content);
                  if(comments.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<List<CommentResponse>>(comments.Result.ToString());
                }
                  else
                {
                    throw new Exception("Unable to get comments!");
                }

                

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Post> GetPostById(Guid id)
        {
            var post = await  _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            post.Comments = await getcommentsbypostId(id);
            if (post == null)
            {
                throw new Exception("Post Not Found");
            }
            return post;
        }

        public Task<List<CommentDto>> GetPostComments(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<List<Post>> GetPosts()
        {
     
            return _context.Posts.ToListAsync();
        }

        public async Task<string> UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return "Post Updated Successfully";

        }
    }
}
