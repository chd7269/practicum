using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;


namespace Api.Controllers
{
    public class RevenuesExpandedController : GlobalController
    {
        private IExpandedRevenuesService expandedRevenuesService;

        public RevenuesExpandedController(IExpandedRevenuesService expandedRevenuesService)
        {
            this.expandedRevenuesService = expandedRevenuesService;
        }

        [HttpGet]
        public GResult<List<daysRevenuesExpanded>> GetDaysRevenuesExpanded() 
        { 
            return Success(expandedRevenuesService.GetDaysRevenuesExpanded());
        }

        [HttpGet]
        public GResult<List<productsRevenuesExpanded>> GetProductsRevenuesExpanded()
        {
            return Success(expandedRevenuesService.GetProductsRevenuesExpanded());
        }

    }
}
