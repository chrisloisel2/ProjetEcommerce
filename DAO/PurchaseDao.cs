using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exemple.Models;
using exemple.Data;


namespace exemple.Models;


public class PurchaseDao
{
    private readonly ApplicationDbContext _context;

    public PurchaseDao(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Purchase>> GetPurchases()
    {
        return await _context.Purchases.Include(p => p.PurchaseArticles)
                                       .ThenInclude(pa => pa.Article)
                                       .ToListAsync();
    }

    public async Task<Purchase> GetPurchase(int id)
    {
        return await _context.Purchases.Include(p => p.PurchaseArticles)
                                       .ThenInclude(pa => pa.Article)
                                       .FirstOrDefaultAsync(p => p.PurchaseId == id);
    }

    public async Task<Purchase> AddPurchase(Purchase purchase)
    {
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> UpdatePurchase(Purchase purchase)
    {
        _context.Entry(purchase).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return purchase;
    }

    public async Task<bool> DeletePurchase(int id)
    {
        var purchase = await _context.Purchases.FindAsync(id);
        if (purchase == null) return false;

        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();
        return true;
    }
}
