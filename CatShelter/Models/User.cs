using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        [Required]
        public required string Email { get; set; }
        public string Phone { get; set; } = "";
        [Required]
        public required string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEmployee { get; set; }

        
        public IList<Adoption> Adoptions { get; set; } = [];
        public IList<Cat> CaredForCats { get; set; } = [];

    }
}
