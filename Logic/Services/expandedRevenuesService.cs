using Logic.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IExpandedRevenuesService {
        List<daysRevenuesExpanded> GetDaysRevenuesExpanded();
        List<productsRevenuesExpanded> GetProductsRevenuesExpanded();
    }

    public class expandedRevenuesService : IExpandedRevenuesService
    {
        private IDBService dbService;
        public expandedRevenuesService( IDBService dbService )
        {
            this.dbService = dbService;
        }

        public List<daysRevenuesExpanded> GetDaysRevenuesExpanded() {

            List<daysRevenuesExpanded> list = new List<daysRevenuesExpanded>();

            list=dbService.entities.PresenceSettings.Select(x=>new daysRevenuesExpanded()
            {
                Day=x.Day,
                Hours=x.Hours,
            }).ToList();

            return list;
        }

        public List<productsRevenuesExpanded> GetProductsRevenuesExpanded() {

            List<productsRevenuesExpanded> list = new List<productsRevenuesExpanded>();

            list = dbService.entities.ProductSettingsForDays.Select(x => new productsRevenuesExpanded()
            {
                Day = x.Day,
                Product = x.Product,
                ProductType = x.ProductType,
                ProductValue = x.ProductValue,
                ProductQuantity = x.ProductQuantity,
            }).ToList();

            return list;
        }

           
    }

}
