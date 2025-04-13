using CatShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace CatShelter.ViewModels.UserViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }

        public static User ToUser(EditViewModel userViewModel)
        {
            return new User
            {
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
        public static EditViewModel FromUser(User user)
        {
            return new EditViewModel
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

    public class EditViewModelValidator : AbstractValidator<EditViewModel>
    {
        public EditViewModelValidator()
        {
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
            When(x => !x.Password.IsNullOrEmpty(), () => {
                RuleFor(x => x.Password).MinimumLength(8);
                RuleFor(x => x.Password).Custom((password, context) => {
                    if (!password.Any(x => char.IsLower(x)))
                    {
                        context.AddFailure("A password must contain a lower letter");
                    }
                    if (!password.Any(x => char.IsUpper(x)))
                    {
                        context.AddFailure("A password must contain an upper letter");
                    }
                    if (!password.Any(x => char.IsAsciiDigit(x)))
                    {
                        context.AddFailure("A password must contain a digit");
                    }
                });
            });

        }

    }
}
