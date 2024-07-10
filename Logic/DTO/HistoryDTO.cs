using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class HistoryDTO
    {
        public int HistoryId { get; set; }
        public int Id { get; set; }
        public DateTime DateofChange { get; set; }
        public string oldDomain { get; set; }
        public string newDomain { get; set; }
        public int oldAmount { get; set; }
        public int NewAmount { get; set; }


    }
}
