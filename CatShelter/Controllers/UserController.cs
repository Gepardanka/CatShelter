using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.UserViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CatShelter.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateViewModel> _createValidator;
        private readonly IValidator<EditViewModel> _editValidator;
        public UserController(IUserService userService, IValidator<CreateViewModel> createValidator, IValidator<EditViewModel> editValidator)
        {
            _userService = userService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }


        [HttpGet]
        public IActionResult Index(UserFilter userFilter)
        {
            return View(new IndexViewModel
            {
                Users = _userService.GetByName(userFilter.Surname, userFilter.Name).Select(UserViewModel.FromUser).ToList(),
                UserFilter = userFilter
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) { return NotFound(); }
            return View(UserViewModel.FromUser(user!));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel userViewModel)
        {
            ValidationResult result = _createValidator.Validate(userViewModel);
            if (result.IsValid) {
                User user = CreateViewModel.ToUser(userViewModel);
                _userService.Insert(user);
                return Redirect($"/user/details/{user.Id}");
            }
            result.AddToModelState(this.ModelState);
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) { return NotFound(); }
            return View(EditViewModel.FromUser(user));
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel userViewModel)
        {
            ValidationResult result = _editValidator.Validate(userViewModel);
            if (result.IsValid) {
                User user = EditViewModel.ToUser(userViewModel);
                _userService.Update(user);
                return Redirect($"/user/details/{user.Id}");
            }
            result.AddToModelState(this.ModelState);
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Redirect("/user");
        }

        [HttpGet]
        public IActionResult Employees()
        {
            return View(_userService.GetEmployees().Select(UserViewModel.FromUser));
        }

        [HttpGet]
        public IActionResult Admins()
        {
            return View(_userService.GetAdmins().Select(UserViewModel.FromUser));
        }

    }
    public static class Extensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }

}

