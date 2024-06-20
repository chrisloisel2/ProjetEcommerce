using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exemple.Models;

public class User
{
    [Key]
    public int UserId { get; set; } // Clefs primaire

    [Required]
    public string Name { get; set; } // Champs obligatoire nom

    [Required]
    public string Password { get; set; } // Champs obligatoire nom

	public string Bio { get; set; } // Champs obligatoire nom

	public string Email { get; set; } // Champs obligatoire nom

	public string Phone { get; set; } // Champs obligatoire nom

	public string Address { get; set; } // Champs obligatoire nom

    public ICollection<Purchase> Purchases { get; set; } // Achats de l'utilisateur
	// Relation 1-N avec Purchase


	public UserPartialDTO ToDTO()
	{
		return new UserPartialDTO
		{
			UserId = UserId,
			Name = Name,
			Bio = Bio,
			Purchases = Purchases.Select(p => p.PurchaseId).ToList()
		};
	}
}

public class Purchase
{
    [Key]
    public int PurchaseId { get; set; } // Clefs primaire

    [Required]
    public int UserId { get; set; } // Clefs étrangère

    [ForeignKey("UserId")]
    public User User { get; set; }
	// Représente l'utilisateur qui a fait l'achat
	// le meme que User.UserId

    public ICollection<PurchaseArticle> PurchaseArticles { get; set; }
	// Relation 1-N avec PurchaseArticle
	// Article acheté
}

public class Article
{
    [Key]
    public int ArticleId { get; set; } // Clefs primaire

    [Required]
    public string Name { get; set; } // Champs obligatoire nom

    public ICollection<PurchaseArticle> PurchaseArticles { get; set; }
	// Relation 1-N avec PurchaseArticle
}

public class PurchaseArticle
{
    public int PurchaseId { get; set; }
	// Clefs étrangère
    public int ArticleId { get; set; }
	// Clefs étrangère

    [ForeignKey("PurchaseId")]
    public Purchase Purchase { get; set; }

    [ForeignKey("ArticleId")]
    public Article Article { get; set; }
}
