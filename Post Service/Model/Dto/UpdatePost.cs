namespace Post_Service.Model.Dto
{
    public class UpdatePost
    {
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }
    public string ImageUrl { get; set; }
    }
}
