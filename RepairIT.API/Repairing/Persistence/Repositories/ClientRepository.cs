using Microsoft.EntityFrameworkCore;
using RepairIT.API.Repairing.Domain.Models;
using RepairIT.API.Repairing.Domain.Repositories;
using RepairIT.API.Shared.Persistence;
using RepairIT.API.Shared.Persistence.Repositories;

namespace RepairIT.API.Repairing.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) :base(context)
    {
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients
            .ToListAsync();
    }

    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task<Client> FindByIdAsync(int clientId)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(p => p.Id == clientId);
    }

    public async Task<Client> FindByEmailAsync(string clientEmail)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(p => p.Email == clientEmail);
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }
}