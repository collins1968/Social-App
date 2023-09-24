namespace MyAppFrontend.Model.Comments
{
    public class CommentRequestDto
    {
        public string CommentText { get; set; } = string.Empty;
        public Guid PostId { get; set; }
    }
}
