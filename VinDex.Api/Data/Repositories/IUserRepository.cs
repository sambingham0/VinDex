using VinDex.Api.Data.Entities;

namespace VinDex.Api.Data.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetBySubjectIdAsync(string subjectId);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}