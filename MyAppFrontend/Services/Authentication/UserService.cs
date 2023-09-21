using MyAppFrontend.Model;
using MyAppFrontend.Model.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace MyAppFrontend.Services.Authentication
{
    public class UserService : IUserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl = "https://localhost:7036";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
