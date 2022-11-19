﻿using TastyPoint.API.Profiles.Domain.Models;

namespace TastyPoint.API.Social.Domain.Models;

public class FoodStore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int Rate { get; set; }
    public bool Favorite { get; set; }
    public string Image { get; set; }
    
    //Relationships
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
}