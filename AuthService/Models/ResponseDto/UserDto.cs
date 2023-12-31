﻿using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.ResponseDto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
