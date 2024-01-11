using Agency.Business.ViewModels;
using Agency.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<Portfolio> GetByIdAsync(int id);
        void Delete(Portfolio portfolio);
        Task<Portfolio> Update(PortfolioUpdateVM portfolioVM);
        Task<ICollection<PortfolioListItemVM>> GetAllAsync();
        Task<Portfolio> CreatePortfolioAsync(PortfolioCreateVM portfolioVM);
    }
}
