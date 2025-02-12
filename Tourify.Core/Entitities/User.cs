﻿using System;
using System.Collections.Generic;
using Tourify.Core.Entities;

namespace Tourify.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<PlaceReview> Reviews { get; set; } = new();
        public List<UserFavorite> Favorites { get; set; } = new();
    }
}