using Microsoft.EntityFrameworkCore;
using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly VinDexDbContext _context;

    public UserRepository(VinDexDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetBySubjectIdAsync(string subjectId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.GoogleSubjectId == subjectId);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}