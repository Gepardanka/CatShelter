using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CatShelter.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
            User user = CreateViewModel.ToUser(userViewModel);
            _userService.Insert(user);
            return Redirect($"/user/details/{user.Id}");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) { return NotFound(); }
            return View(CreateViewModel.FromUser(user));
        }

        [HttpPost]
        public IActionResult Edit(CreateViewModel userViewModel)
        {
            User user = CreateViewModel.ToUser(userViewModel);
            _userService.Update(user);
            return Redirect($"/user/details/{user.Id}");
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

}

