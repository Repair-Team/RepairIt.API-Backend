using System.Net.Mime;
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
[SwaggerTag("Create, read, update and delete clients")]
public class ClientsController: ControllerBase
{
    public ClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    private readonly IClientService _clientService;
    private readonly IMapper _mapper;


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Clients of Clients Table",
        Description = "Get existing clients in the client table",
        OperationId = "GetClients",
        Tags = new[] { "Clients" }
    )]
    public async Task<IEnumerable<ClientResource>> GetAllAsync()
    {
        var clients = await _clientService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);

        return resources;
    }

    [HttpGet("{clientId}")]
    [SwaggerOperation(
        Summary = "Get Client from Clients table by ID",
        Description = "Get existing client with the ID",
        OperationId = "GetClient",
        Tags = new[] { "Clients" }
    )]
    public async Task<ClientResource> GetClientById(int clientId)
    {
        var client = await _clientService.FindIdByAsync(clientId);
        var resource = _mapper.Map<Client, ClientResource>(client);

        return resource;
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create New Client",
        Description = "Post New Client to clients table",
        OperationId = "PostClients",
        Tags = new[] { "Clients" }
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveClientResource, Client>(resource);
        
        var result = await _clientService.SaveAsync(client);

        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Created(nameof(PostAsync), clientResource);
    }
    
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update Client",
        Description = "Update client information in clients table",
        OperationId = "PutClients",
        Tags = new []{"Clients"}
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClientResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveClientResource, Client>(resource);
        var result = await _clientService.UpdateAsync(id, client);
        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Client",
        Description = "Delete client in clients table",
        OperationId = "DeleteClients",
        Tags = new []{"Clients"}
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _clientService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

        return Ok(clientResource);
    }
}