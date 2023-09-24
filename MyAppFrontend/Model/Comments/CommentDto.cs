namespace MyAppFrontend.Model.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
