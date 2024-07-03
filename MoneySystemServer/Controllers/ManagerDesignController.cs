using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;

namespace Api.Controllers
{
    public class ManagerDesignController : GlobalController
    {
        private IManagerDesignService managerDesignService;

        public ManagerDesignController(IManagerDesignService managerDesignService)
        {
            this.managerDesignService = managerDesignService;
        }

        [HttpGet]
        public GResult<ManagerDesignDTO> GetManagerDesign()
        {
            return Success(managerDesignService.GetManagerDesign(UserId.Value));
        }


        [HttpPost]

        public Result AddManagerDesign(ManagerDesignDTO mDesign)
        {

            return Success(managerDesignService.AddManagerDesign(mDesign, UserId.Value));
        }


        [HttpPut]
        public Result UpdateManagerDesign(ManagerDesignDTO mDesign)
        {
            return Success(managerDesignService.UpdateManagerDesign(mDesign, UserId.Value));

            //    if (!success)

            //        return Fail();

            //    return Success();
        }
    }
}
