using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IManagerDesignService
    {
        ManagerDesignDTO GetManagerDesign(int ManagerId);

        bool AddManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId);
        bool UpdateManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId);
    }
    public class ManagerDesignService: IManagerDesignService
    {
        private IDBService dbService;
        public ManagerDesignService(IDBService dbService)
        {
            this.dbService = dbService;
        }
        public ManagerDesignDTO GetManagerDesign(int managerId)
        {
            var mDesign = new ManagerDesignDTO();
            var dbmDesign = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.ManagerId == managerId);
            if (dbmDesign != null)
            {
                mDesign.Id = dbmDesign.Id;
                mDesign.Title = dbmDesign.Title;
                mDesign.Slogan = dbmDesign.Slogan;
                mDesign.HeaderColor = dbmDesign.HeaderColor;
                mDesign.ImageContent = dbmDesign.ImageContent;
            }
            return mDesign;
        }
        public bool AddManagerDesign(ManagerDesignDTO managerDesign,int CurrentUserId)
        {
            var newManagerDesign = new ManagerDesign();
                newManagerDesign.ManagerId = CurrentUserId;
                newManagerDesign.HeaderColor =(managerDesign.HeaderColor);
                newManagerDesign.ImageContent =(managerDesign.ImageContent);
                newManagerDesign.Title =(managerDesign.Title);
                newManagerDesign.Slogan =(managerDesign.Slogan);
            dbService.entities.ManagerDesigns.Add(newManagerDesign);

            dbService.Save();
            return true;
        }

        public bool UpdateManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId)
        {
            var dbUpdateManagerDesign = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.Id == managerDesign.Id);
            if (dbUpdateManagerDesign == null) {
                return(AddManagerDesign(managerDesign, CurrentUserId));
            }
            else
            {
                dbUpdateManagerDesign.ManagerId = CurrentUserId;
                dbUpdateManagerDesign.HeaderColor = (managerDesign.HeaderColor);
                dbUpdateManagerDesign.ImageContent = (managerDesign.ImageContent);
                dbUpdateManagerDesign.Title = (managerDesign.Title);
                dbUpdateManagerDesign.Slogan = (managerDesign.Slogan);
                dbService.Save();
            }
            return true;
        }
    }
}
