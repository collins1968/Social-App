using Post_Service.Model.Dto;

namespace Post_Service.Services.IServices
{
    public interface CommentsInterface
    {
        Task<IEnumerable<CommentDto>> getcommentsbypostId(Guid id);
    }
}
