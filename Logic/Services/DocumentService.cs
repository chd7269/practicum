using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IDocumentService
    {
        List<DocumentDTO> GetDocuments(int CurrentUserId);
        bool AddDocument(DocumentDTO document);
        bool DeleteDocument(int id, int CurrentUserId);
        DocumentDTO GetFile(int id);
    
        bool UpdateDocument(IdName doc);
    }
    public class DocumentService : IDocumentService
    {
        IDBService dBService;
        private object dbService;

        public DocumentService(IDBService dBService)
        {
            this.dBService = dBService;
        }
        public List<DocumentDTO> GetDocuments(int currentUserId)
        {
            List<DocumentDTO> list = new List<DocumentDTO>();
            list = dBService.entities.Documents.Where(x => x.UserId == currentUserId).Select(x => new DocumentDTO()
            {
                Description = x.Description,
                FileName = x.FileName,
                Id = x.Id,
                Src = "File/ShowFile/" + x.Id
            }).ToList();
            return list;
        }
        public bool AddDocument(DocumentDTO document)
        {
            if (dBService.entities.Documents.Any(l => l.UserId == document.UserId && l.FileName == document.FileName))
            {
                return false;
            }
            dBService.entities.Documents.Add(new Document()
            {
                Content = document.Content,
                Description = document.Description,
                FileName = document.FileName,
                UserId = document.UserId

            });
            dBService.Save();
            return true;
        }
        public bool DeleteDocument(int id, int CurrentUserId)
        {
            var DocumentToDelete = dBService.entities.Documents.FirstOrDefault(x => x.Id == id);
            if (DocumentToDelete != null)
            {
                dBService.entities.Documents.Remove(dBService.entities.Documents.FirstOrDefault(x => x.Id == id));
                dBService.Save();
                return true;
            }
            return false;
        }
       
        public DocumentDTO GetFile(int id)
        {
            var doc = new DocumentDTO();
            var dbFile = dBService.entities.Documents.FirstOrDefault(x => x.Id == id);
            if (dbFile != null)
            {
                doc.Content = dbFile.Content;
                doc.FileName = dbFile.FileName;
            }
            return doc;
        }
        public bool UpdateDocument(IdName doc)
        {
            var dbDescrip = dBService.entities.Documents.FirstOrDefault(x => x.Id == doc.Id );
            if (dbDescrip != null)
            {
                dbDescrip.Description = doc.Name;
                dBService.Save();
                return true;
            }
            return false;
        }

    }
}
