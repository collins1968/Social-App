using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppFrontend.Model.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ImageUrl { get; set; }
        [NotMapped]
        public List<CommentResponse> Comments { get; set; } = new List<CommentResponse>();
    }

    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
