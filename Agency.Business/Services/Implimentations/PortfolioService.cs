using Agency.Business.Exceptions;
using Agency.Business.Services.Interfaces;
using Agency.Business.ViewModels;
using Agency.Core.Entities;
using Agency.DAL.Repositories.Interfaces;
using AutoMapper;
using AutoMapper.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Implimentations
{
    public class PortfolioService:IPortfolioService
    {
        private readonly IPortfolioRepository _repository;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ICollection<PortfolioListItemVM>> GetAllAsync()
        {
            var portfolies = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<PortfolioListItemVM>>(portfolies);
        }



        public Task<Portfolio> GetByIdAsync(int id)
        {
            var feature = _repository.FindById(id);
            return feature;
        }


        public async Task<Portfolio> CreatePortfolioAsync(PortfolioCreateVM portfolioVM)
        {
            Portfolio portfolio = new Portfolio()
            {
                Title = portfolioVM.Title,
                Description = portfolioVM.Description,
                SubTitle = portfolioVM.SubTitle,
            };
            await _repository.Create(portfolio);
            await _repository.SaveChangesAsync();
            return portfolio;
        }
    

        public async Task<bool> CheckEntity(int id)
            {
                if (id <= 0) throw new NegativeIdException();
                if (await _repository.FindById(id)==null) throw new PortfoleNotFoundException();
                return true;
            }


        public async Task<Portfolio> Update(PortfolioUpdateVM portfolio)
        {
            if (portfolio == null) throw new PortfoleNotFoundException();
            Portfolio newportfolio = await _repository.FindById(portfolio.Id);
            //features.Id = feature.Id;
            newportfolio.Title = portfolio.Title;
            newportfolio.Description = portfolio.Description;
            newportfolio.SubTitle = portfolio.SubTitle;
            newportfolio.ImageUrl = portfolio.ImageUrl;

             _repository.Update(newportfolio);
            await _repository.SaveChangesAsync();
            return newportfolio;
        }




        public void Delete(Portfolio portfolio)
        {
            if (portfolio == null) throw new PortfoleNotFoundException();
            _repository.FindById(portfolio.Id);
            _repository.Delete(portfolio);
            _repository.SaveChangesAsync();

        }


        Task<Portfolio> IPortfolioService.CreatePortfolioAsync(PortfolioCreateVM portfolioVM)
        {
            throw new NotImplementedException();
        }

    }
}
