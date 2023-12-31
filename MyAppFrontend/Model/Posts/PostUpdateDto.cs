﻿namespace MyAppFrontend.Model.Posts
{
    public class PostUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
