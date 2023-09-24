using MyAppFrontend.Model;
using MyAppFrontend.Model.Authentication;

namespace MyAppFrontend.Services.Authentication
{
    public interface IUserInterface
    {
        Task<ResponseDto> register(RegisterDto registerRequestDto);

        Task<ResponseDto> login(LoginRequestDto loginRequestDto);

        Task<UserDto> GetuserbyId(string id);

        Task<List<UserDto>> getAllUsers();
    }
}
