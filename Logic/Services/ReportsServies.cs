using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
  

    public interface IReportsServies
    {
        List<MovingReportsDTO> GetMovingReports(int current);
        List<HistoryDTO> GetHistory(int current);
        bool AddHistory(int Id, User2Area oldDetails, AreaDTO newDetails);

    }



    public class ReportsService : IReportsServies
    {
        private IDBService dbService;

        public ReportsService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        public List<MovingReportsDTO> GetMovingReports(int current)
        {

            var reports = new List<MovingReportsDTO>();

            var oldMove = dbService.entities.Movings.Where(x => x.User2Area.UserId == current).OrderBy(x => x.Date).FirstOrDefault();
            if (oldMove != null)
            {
                DateTime firstDate = oldMove.Date;

                DateTime oldMonth = new DateTime(firstDate.Year, firstDate.Month, 1);
                DateTime lastMonth = DateTime.Now;
                DateTime finalMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);


                for (DateTime date = oldMonth; date < lastMonth; date = date.AddMonths(1))
                {
                    int? sumOfExpenses = 0;
                    int? sumOfRevenues = 0;

                    var monings = dbService.entities.Movings.Where(x => x.User2Area.UserId == current && x.Date.Year == date.Year && x.Date.Month == date.Month).ToList();
                    if (monings != null)
                    {
                        var expenses = monings.Where(l => l.User2Area.Type == 2);
                        var revenues = monings.Where(l => l.User2Area.Type == 1);

                        // var expenses = monings.Where(l => l.Subject.Type == 2);
                        // var revenues = monings.Where(l => l.Subject.Type == 1);

                        if (expenses != null)
                        {
                            sumOfExpenses = expenses.Sum(l => l.Sum);
                        }
                        if (revenues != null)
                        {
                            sumOfRevenues = revenues.Sum(l => l.Sum);
                        }
                    }

                    reports.Add(new MovingReportsDTO()
                    {
                        Month = date.Month.ToString(),
                        Year = date.Year.ToString(),
                        Expenses = sumOfExpenses,
                        Revenues = sumOfRevenues
                    });

                    // logic here

                }
            }

            return reports;
        }


        public List<HistoryDTO> GetHistory(int current)
        {
            var history = new List<HistoryDTO>();
            history.Add(new HistoryDTO()
            {
        
            });
            return history;
        }


        public bool AddHistory(int Id, User2Area oldDetails, AreaDTO newDetails)
        {
            History newHistory = new History();
            newHistory.Id = Id;
            newHistory.DateofChange = DateTime.Now;
            newHistory.OldDomain = oldDetails.Description;
            newHistory.NewDomain = newDetails.Description;
            newHistory.OldAmount = oldDetails.Sum;
            newHistory.NewAmount = newDetails.Sum;
            dbService.entities.Histories.Add(newHistory);
            try
            {
                //dbService.Save();
                dbService.entities.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                return false;
                
            }
          
        }
    }

}

