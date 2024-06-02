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
        public FileController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpGet("{id}")]
        public ActionResult ShowFile(int id)
        {
            var file = documentService.GetFile(id);
            file.ContentType = GetContentType(file.FileName);
            return File(file.Content, file.ContentType);
        }

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
            return type;
        }
    }
}
