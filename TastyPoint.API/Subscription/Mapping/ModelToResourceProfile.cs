﻿using AutoMapper;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Subscription.Resources;

namespace TastyPoint.API.Subscription.Mapping;

public class ModelToResourceProfile : Profile
{
    ModelToResourceProfile()
    {
        CreateMap<BusinessPlan, BusinessPlanResource>();
    }
}