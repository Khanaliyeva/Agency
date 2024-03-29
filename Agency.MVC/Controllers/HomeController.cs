﻿using Agency.Business.ViewModels;
using Agency.DAL.Context;
using Agency.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agency.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            HomeVm homevm = new HomeVm()
            {
                portfolios = _context.Portfolios.ToList(),
            };
            return View(homevm);
        }
    }
}
