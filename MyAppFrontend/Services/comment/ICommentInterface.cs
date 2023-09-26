using MyAppFrontend.Model;
using MyAppFrontend.Model.Comments;

namespace MyAppFrontend.Services.comment
{
    public interface ICommentInterface
    {
        Task<ResponseDto > AddCommentAsync(CommentRequestDto comment);
        Task<ResponseDto> DeleteCommentAsync(Guid id);
    }
}
