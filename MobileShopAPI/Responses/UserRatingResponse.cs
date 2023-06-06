﻿namespace MobileShopAPI.Responses
{
    public class UserRatingResponse
    {
        public string? Message { get; set; }

        public bool isSuccess { get; set; }

        public IEnumerable<String>? Errors { get; set; }
    }
}
