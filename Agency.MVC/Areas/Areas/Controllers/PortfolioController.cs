using Agency.Business.Services.Interfaces;
using Agency.Business.ViewModels;
using Agency.Core.Entities;
using Agency.DAL.Context;
using AutoMapper.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Areas.Areas.Controllers
{

    [Area("Manage")]
    public class PortfolieController : Controller
    {
        private readonly IPortfolioService _service;


        public PortfolieController(IPortfolioService service)
        {
            _service = service;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var features = await _service.GetAllAsync();
            return View(features);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PortfolioCreateVM portfolioVM)
        {
            await _service.CreatePortfolioAsync(portfolioVM);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            Portfolio portfolio = await _service.GetByIdAsync(id);
             PortfolioUpdateVM newportfolio = new PortfolioUpdateVM()
            {
                Title = portfolio.Title,
                Description = portfolio.Description,
                ImageUrl=portfolio.ImageUrl
            };
            return View(newportfolio);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(PortfolioUpdateVM portfolioVM)
        {
            await _service.Update(portfolioVM);

            return RedirectToAction("Index");
        }



        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Portfolio portfolio = await _service.GetByIdAsync(id);
            _service.Delete(portfolio);

            return RedirectToAction("Index");
        }
    }
}
    
