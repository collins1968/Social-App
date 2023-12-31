﻿using Post_Service.Model.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Service.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

        public string ImageUrl { get; set; }
        [NotMapped]
        public List<CommentResponse> Comments { get; set; } = new List<CommentResponse>();


    }
}
