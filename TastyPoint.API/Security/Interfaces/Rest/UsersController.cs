﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyPoint.API.Security.Domain.Models;
using TastyPoint.API.Security.Domain.Services;
using TastyPoint.API.Security.Domain.Services.Communication;
using TastyPoint.API.Security.Resources;

namespace TastyPoint.API.Security.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<ActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }
        
}