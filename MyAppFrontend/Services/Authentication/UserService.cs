using MyAppFrontend.Model;
using MyAppFrontend.Model.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace MyAppFrontend.Services.Authentication
{
    public class UserService : IUserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl = "https://apigatewayapi.azurewebsites.net";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> getAllUsers()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/User/getAllUsers");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(result.IsSuccess)
            {
                var users = JsonConvert.DeserializeObject<List<UserDto>>(result.Result.ToString());
                return users;
            }
            else
            {
                throw new Exception(result.Message);
              }
        }

        public  async Task<UserDto> GetuserbyId(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/User/getuserbyId?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if(result.IsSuccess)
            {
                var user = JsonConvert.DeserializeObject<UserDto>(result.Result.ToString());
                return user;
            }
            else
            {                
                throw new Exception(result.Message);
              }

        }

        public async Task<ResponseDto> login(LoginRequestDto loginRequestDto)
        {
            var request = JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            return result;

        }

        public async Task<ResponseDto> register(RegisterDto registerRequestDto)
        {
            try
            {
                var request = JsonConvert.SerializeObject(registerRequestDto);
                var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/api/User/register", bodyContent);
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResponseDto>(content);

                if (result.IsSuccess)
                {
                    return result;
                }
                return new ResponseDto();

            }
            catch (Exception ex)
            {
       
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }


    }
}
