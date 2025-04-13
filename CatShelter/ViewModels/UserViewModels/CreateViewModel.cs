using CatShelter.Models;
using FluentValidation;
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

        public static User ToUser(CreateViewModel userViewModel)
        {
            return new User {
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Surname = userViewModel.Surname,
                Email = userViewModel.Email,
                Phone = userViewModel.Phone,
                Password = userViewModel.Password,
                IsAdmin = userViewModel.IsAdmin,
                IsEmployee = userViewModel.IsEmployee
            };
        }
    }

    public class CreateViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateViewModelValidator() {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Custom((phone, context) =>
            {
                if (phone.Any(x => !char.IsAsciiDigit(x)))
                {
                    context.AddFailure("A phone number must be digits only");
                }
            });
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Password).Custom((password, context) => {
                if (!password.Any(x => char.IsLower(x)))
                {
                    context.AddFailure("A password must contain a lower letter");
                }
                if(!password.Any(x => char.IsUpper(x)))
                {
                    context.AddFailure("A password must contain an upper letter");
                }
                if (!password.Any(x => char.IsAsciiDigit(x)))
                {
                    context.AddFailure("A password must contain a digit");
                }
            });
        }

    } 
}
