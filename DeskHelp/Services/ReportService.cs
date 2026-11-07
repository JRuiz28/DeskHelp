using DeskHelp.DataSets.HelpDeskDataSetTableAdapters;
using DeskHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeskHelp.Services
{
    public class ReportService
    {
        private ticketTableAdapter ticketAdapter = new ticketTableAdapter();

        public List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            var report = new List<ReportModel>();

            for (int i = 1; i <= 4; i++)
            {
                var tickets = ticketAdapter.GetDataByDateAndState(startDate, endDate, i);

                report.Add(new ReportModel
                {
                    StateID = i,
                    Count = tickets.Count
                });
            }

            return report;
        }
    }
}
