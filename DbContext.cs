using Microsoft.EntityFrameworkCore;
using exemple.Models;

namespace exemple.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<PurchaseArticle> PurchaseArticles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:userserverbdd.database.windows.net,1433;Initial Catalog=yoloswag;Persist Security Info=False;User ID=chris;Password=rose230323!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseArticle>()
            .HasKey(pa => new { pa.PurchaseId, pa.ArticleId });

        modelBuilder.Entity<PurchaseArticle>()
            .HasOne(pa => pa.Purchase)
            .WithMany(p => p.PurchaseArticles)
            .HasForeignKey(pa => pa.PurchaseId);

        modelBuilder.Entity<PurchaseArticle>()
            .HasOne(pa => pa.Article)
            .WithMany(a => a.PurchaseArticles)
            .HasForeignKey(pa => pa.ArticleId);
    }
}
