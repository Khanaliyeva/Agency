using Agency.Core.Entities;
using AutoMapper.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.ViewModels
{
    public class HomeVm
    {
        public List<Portfolio> portfolios { get; set; }
    }
}
