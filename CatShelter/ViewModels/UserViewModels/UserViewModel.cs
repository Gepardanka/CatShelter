using CatShelter.Models;
using System.ComponentModel;

namespace CatShelter.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }

        public static UserViewModel FromUser(User user) {
            return new UserViewModel {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                IsAdmin = user.IsAdmin,
                IsEmployee = user.IsEmployee
            };
        }
    }
}
