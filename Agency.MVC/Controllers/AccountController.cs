﻿using Agency.Business.Helpers;
using Agency.Business.ViewModels;
using Agency.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registervm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = registervm.Name,
                Email = registervm.Email,
                Surname = registervm.Surname,
                UserName = registervm.Username
            };

            var result = await _userManager.CreateAsync(user, registervm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            return RedirectToAction(nameof(Index), "Home");
        }



        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> LogIn(LoginVM loginvm, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(loginvm.EmailOrUsername);


            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginvm.EmailOrUsername);

                if (user == null)
                {
                    ModelState.AddModelError("", "Sifre ve ya istifadeci adi sehvdi");
                    return View();
                }
            }
            var result = _signInManager.CheckPasswordSignInAsync(user, loginvm.Password, true).Result;



            await _signInManager.SignInAsync(user, loginvm.RememberMe);

            if (ReturnUrl != null && !ReturnUrl.Contains("Login"))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> CreateRole()
        {

            foreach (Roles item in Enum.GetValues(typeof(Roles)))
            {
                if (await _roleManager.FindByNameAsync(item.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = item.ToString(),
                    });
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }

}