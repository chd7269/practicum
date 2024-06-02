using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    public class ListController : GlobalController
    {

        private IListService listService;

        public ListController(IListService listService)
        {
            this.listService = listService;
        }
        [IsManager]
        public GResult<ListsDTO> GetAllLists()
        {
            return Success(listService.GetAllLists());
        }
        [IsPermission]
        [HttpPost]
        public GResult<List<IdName>> GetList(IdNameDB item)
        {
            return Success(listService.GetList(item));
        }

        [HttpPost]
        [IsManager]
        public Result AddItem(IdNameDB idName)
        {
            var isSuccess = listService.AddItem(idName);
            if (isSuccess)
            {
                return Success();
            }
            return Fail(message: "ארעה שגיאה");
        }

        [HttpPost]
        [IsManager]
        public Result DeleteItem(IdNameDB idName)
        {
            var isSuccess = listService.DeleteItem(idName);
            if (isSuccess)
            {
                return Success();
            }
            return Fail(message: "אין אפשרות למחוק תחומים עם תחום זה");
        }

        [HttpPut]
        [IsManager]
        public Result UpdateItem(IdNameDB idName)
        {
            var isSuccess = listService.UpdateItem(idName);
            if (isSuccess)
            {
                return Success();
            }
            return Fail(message: "אין אפשרות לעדכן תחומים עם תחום זה");
        }
    }
}
