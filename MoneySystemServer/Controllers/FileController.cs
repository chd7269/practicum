using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneySystemServer.Controllers
{

    [AllowAnonymous]
    public class FileController : GlobalController
    {
        private IDocumentService documentService;
        private IManagerDesignService managerDesignService;

        public FileController(IDocumentService documentService, IManagerDesignService managerDesignService)
        {
            this.documentService = documentService;
            this.managerDesignService = managerDesignService;
        }

        [HttpGet("{id}")]
        public ActionResult ShowFile(int id)
        {
            var file = documentService.GetFile(id);
            file.ContentType = GetContentType(file.FileName);
            return File(file.Content, file.ContentType);
        }

        [HttpGet("{id}")]
        public ActionResult ShowFileDesign(int id)
        {
            var file = managerDesignService.GetFile(id);
            var contentType = GetContentType(file.FileName);
            return File(file.ImageContent, contentType);
        }

        // אם גט מצליח לקבל שתי נתונים להפוך את הפונקציה לגלובלית בערך ככה
        //[HttpGet("{id}")]
        //public ActionResult ShowFile(int id)
        //{
        //    // string fileName = string.Empty;
        //    // byte[] content = string.Empty;

        //    var file = documentService.GetFile(id);
        //    //var file = managerService.GetFile();//byte[], contentType

        //    var contentType = GetContentType(file.FileName);
        //    return File(file.Content, contentType);
        //}

        private string GetContentType(string fileName)
        {
            string type = "application/pdf";
            var extention = Path.GetExtension(fileName);
            if (extention == ".png" || extention == ".PNG")
            {
                type = "image/png";
            }
            else if (extention == ".jpg" || extention == ".JPG")
            {
                type = "image/jpg";
            }
            else if (extention == ".docx" || extention == ".doc")
            {
                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (extention == ".xlsx" || extention == ".xls")
            {
                type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
         
            return type;
        }
    }
}
