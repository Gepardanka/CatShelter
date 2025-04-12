using CatShelter.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.ViewModels.UserViewModels
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }

        public static User ToUser(CreateViewModel create)
        {
            return new User {
                Id = create.Id,
                Name = create.Name,
                Surname = create.Surname,
                Email = create.Email,
                Phone = create.Phone,
                Password = create.Password,
                IsAdmin = create.IsAdmin,
                IsEmployee = create.IsEmployee
            };
        }
        public static CreateViewModel FromUser(User user)
        {
            return new CreateViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
                IsEmployee = user.IsEmployee
            };
        }
    }
}
