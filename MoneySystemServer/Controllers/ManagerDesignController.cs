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


        //[HttpPost]

        //public Result AddManagerDesign(ManagerDesignDTO mDesign)
        //{

        //    return Success(managerDesignService.AddManagerDesign(mDesign, UserId.Value));
        //}


        [HttpPut]
        public Result UpdateManagerDesign([FromForm] ManagerDesignDTO file)
        {
            var request = Request;
            ManagerDesignDTO mDesign = null;
            if (request.Form != null && request.Form.Files != null && request.Form.Files.Count > 0 && request.Form.Files[0] != null && request.Form.Files[0].Length > 0)
            {
                
                byte[] data = null;

                using (var ms = new MemoryStream())
                {
                    request.Form.Files[0].CopyTo(ms);
                    data = ms.ToArray();
                }

                mDesign = new ManagerDesignDTO()
                {
                    Id = file.Id,
                    ManagerId = UserId.Value,
                    ImageContent = data,
                    Title = file.Title,
                    Slogan = file.Slogan,
                    HeaderColor = file.HeaderColor,
                };


            }
            return Success(managerDesignService.UpdateManagerDesign(mDesign, UserId.Value));
        }
    }
}
