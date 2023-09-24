using AuthService.Models.RequestsDto;
using AuthService.Models.ResponseDto;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services.IServices
{
    public interface IUserInterface
    {
        Task<string> RegisterUser(RegistrationDto registrationDto);

        Task<string> LoginUser(LoginDto loginDto);

        Task<List<IdentityUser>> getAllUsers();

        Task<IdentityUser> GetUserById(string id);

        //get all post of this user
        Task<IEnumerable<PostDto>> GetPostsOfThisUser();
    }
}
