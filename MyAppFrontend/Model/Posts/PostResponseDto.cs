namespace MyAppFrontend.Model.Posts
{
    public class PostResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
    }
}
