﻿namespace Post_Service.Model.Dto
{
    public class PostResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } 

        public string ImageUrl { get; set; }


        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
