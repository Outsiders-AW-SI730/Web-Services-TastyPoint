﻿using TastyPoint.API.Selling.Domain.Models;

namespace TastyPoint.API.Publishing.Domain.Models;

public class Promotion
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    
    //Not sure about the quantity of packs necessary for a Promotion, possible future edit
    public int PackId { get; set; }
    
    public Pack Pack { get; set; }
}