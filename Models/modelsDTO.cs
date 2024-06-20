using System.Collections.Generic;

namespace exemple.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public List<PurchaseDto> Purchases { get; set; }
    }

public class UserPartialDTO
{
    public int UserId { get; set; } // Clefs primaire

    public string Name { get; set; } // Champs obligatoire nom

	public string Bio { get; set; } // Champs obligatoire nom

    public ICollection<int> Purchases { get; set; } // Achats de l'utilisateur
	// Relation 1-N avec Purchase
}

	//  public class UserLoginDto
    // {
    //     public string Name { get; set; }
	// 	public string Password { get; set; }
    // }

	// Json de l'utilisateur
	// {
	// 	"Name": "John",
	// 	"Purchases": []
	// }

    public class PurchaseDto
    {
        public List<ArticleDto> Articles { get; set; }
    }

    public class ArticleDto
    {
        public string Name { get; set; }
    }
}
