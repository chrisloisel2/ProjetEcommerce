using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exemple.Models;
using exemple.Data;


namespace exemple.Models;


public class UserDao
{
    private readonly ApplicationDbContext _context;

    public UserDao(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.Include(u => u.Purchases)
                                   .ThenInclude(p => p.PurchaseArticles)
                                   .ThenInclude(pa => pa.Article)
                                   .ToListAsync();
    }

    public async Task<User> GetUser(int id)
    {
        return await _context.Users.Include(u => u.Purchases)
                                   .ThenInclude(p => p.PurchaseArticles)
                                   .ThenInclude(pa => pa.Article)
                                   .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<User> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
