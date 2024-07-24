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
            //if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    using (MemoryStream ms = new MemoryStream(file.Content))
            //    {
            //        Workbook workbook = new Workbook(ms);
            //        var sheet = workbook.Worksheets[0]; // Process the first worksheet

            //        using (MemoryStream imageStream = new MemoryStream())
            //        {
            //            // Set image options
            //            var options = new ImageOrPrintOptions
            //            {
            //                ImageFormat = ImageFormat.Jpeg, // Set image format
            //                HorizontalResolution = 96,
            //                VerticalResolution = 96
            //            };

            //            // Create a renderer for the worksheet
            //            var sheetRenderer = new SheetRender(sheet, options);

            //            // Render the image of the first worksheet to the MemoryStream
            //            var image = sheetRenderer.ToImage(0); // Render the image of the first page

            //            // Save the image to the MemoryStream
            //            image.Save(imageStream, ImageFormat.Jpeg);

            //            // Return the image file
            //            imageStream.Position = 0; // Reset stream position before returning
            //            return File(imageStream.ToArray(), "image/jpeg");
            //        }
            //    }
            //}
            //else
            //{
            //    // Handle other file types if needed
            //    return File(file.Content, contentType);
            //}
            file.ContentType = GetContentType(file.FileName);
            return File(file.Content, file.ContentType);
        }

        [HttpGet("{idString}")]
        public ActionResult ShowFileDesign(string idString)
        {

            int id =  RemoveTrailingZeros(idString);
            var file = managerDesignService.GetFile(id);
            var contentType = GetContentType(file.FileName);
            return File(file.ImageContent, contentType);
        }

        public static int RemoveTrailingZeros(string str)
        {
            string numberStr = str;
            string sign = "!";
            int index = numberStr.IndexOf(sign);

            if (index != -1)
            {
                return int.Parse(numberStr.Substring(0, index));
            }

            return int.Parse(str); 
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
