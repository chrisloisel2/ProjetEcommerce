using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exemple.Models;
using exemple.Data;


namespace exemple.Models;

public class ArticleDao
{
    private readonly ApplicationDbContext _context;

    public ArticleDao(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetArticles()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article> GetArticle(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task<Article> AddArticle(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<Article> UpdateArticle(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<bool> DeleteArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return true;
    }
}
