using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Services;
using RepairIT.API.Repairing.Resources;
using RepairIT.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace RepairIT.API.Repairing.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, read, update and delete users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Users of users Table",
        Description = "Get existing users in the users table",
        OperationId = "GetUsers",
        Tags = new[] { "Users" }
    )]
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

        return resources;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create New User",
        Description = "Post New User to users table",
        OperationId = "PostUsers",
        Tags = new[] { "Users" }
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var user = _mapper.Map<SaveUserResource, User>(resource);

        var result = await _userService.SaveAsync(user);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.Resource);

        return Created(nameof(PostAsync), userResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update User",
        Description = "Update user information in users table",
        OperationId = "PutUsers",
        Tags = new[] { "Users" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var user = _mapper.Map<SaveUserResource, User>(resource);

        var result = await _userService.UpdateAsync(id,user);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.Resource);

        return Ok(userResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete User",
        Description = "Delete user in users table",
        OperationId = "DeleteUsers",
        Tags = new[] { "Users" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.Resource);

        return Ok(userResource);
    }



}