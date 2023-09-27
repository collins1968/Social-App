using AuthService.Models.RequestsDto;
using AuthService.Models.ResponseDto;
using AuthService.RabbitMQ;
using AuthService.Services.IServices;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MyMessageBus;
using System.Security.Claims;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserInterface _userInterface;
       private readonly IConfiguration _configuration;
       private readonly responsedto _response;
       private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;


    public UserController(IUserInterface userInterface, IConfiguration configuration, IMessageBus messageBus, IMapper mapper, IRabbitMQPublisher rabbitMQPublisher)
        {
              _userInterface = userInterface;
            _configuration = configuration;
            _messageBus = messageBus;
            _mapper = mapper;
            _response = new responsedto();
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost("register")]
        public async Task<ActionResult<responsedto>> AddUSer(RegistrationDto Registration)
        {
            var message = await _userInterface.RegisterUser(Registration);
            if (message == "User created successfully!")
            { 
                var queueName = _configuration.GetSection("Queues:RegisterUser").Get<string>();
                var messagequeue = new UserMessage()
                {
                    Email = Registration.Email,
                    Name = Registration.UserName
                };
                await _messageBus.PublishMessage(messagequeue, queueName);
                _rabbitMQPublisher.PublishMessage(messagequeue, queueName);
                _response.IsSuccess = true;
                _response.Message = message;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpPost("login")]

        public async Task<ActionResult<responsedto>> LoginUser(LoginDto loginDto)
        {
            var token = await _userInterface.LoginUser(loginDto);
            if (token != null)
            {
                _response.IsSuccess = true;
                _response.Message = "User logged in successfully!";
                _response.Result = token;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid login credentials!";
                return BadRequest(_response);
            }
        }
        //get all user 
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<UserDto>> getAllUsers()
        {
            var response = await _userInterface.getAllUsers();
            var users = _mapper.Map<List<UserDto>>(response);
            if(users != null)
            {
                _response.IsSuccess = true;
                _response.Message = "Users fetched successfully!";
                _response.Result = users;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "No users found!";
                return NotFound(_response);
            }
        }
        //get user by id
        [HttpGet("getUserById")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var response = await _userInterface.GetUserById(id);
            var user = _mapper.Map<UserDto>(response);
            if (user != null)
            {
                _response.IsSuccess = true;
                _response.Message = "User fetched successfully!";
                _response.Result = user;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "No user found!";
                return NotFound(_response);
            }
        }

        //get all post of this user
        [HttpGet("getPostsOfThisUser")]
        [Authorize]
        public async Task<ActionResult<responsedto>> GetPostsOfThisUser()
        {
            //get the user id from the token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var loggedId = userIdClaim.Value;

            //get all posts
            var posts = await _userInterface.GetPostsOfThisUser();

            //get the posts of this user
            var UserPosts = posts.Where(x => x.UserId == loggedId).ToList();
            if (UserPosts != null)
            {
                _response.IsSuccess = true;
                _response.Message = "Posts fetched successfully!";
                _response.Result = UserPosts;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "No posts found!";
                return NotFound(_response);
            }
        }


    }
}
