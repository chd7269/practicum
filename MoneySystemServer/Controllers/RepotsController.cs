using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class RepotsController : GlobalController
    {
        private IReportsServies repotsService;

        public RepotsController(IReportsServies ReportsService)
        {
            this.repotsService = ReportsService;
        }

       
        [HttpGet]
        public GResult<List<MovingReportsDTO>> GetMovingReports()
        {
            return Success(repotsService.GetMovingReports(UserId.Value));
        }


    }
}
