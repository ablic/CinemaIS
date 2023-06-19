using CinemaIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace CinemaIS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly CinemaDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportsController(CinemaDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public FileResult GetMoneyReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Отчет о выручке фильмов");

                excelPackage.Workbook.Properties.Author = "ИС Кинотеатра";
                excelPackage.Workbook.Properties.Title = "Выручка за фильмы";
                excelPackage.Workbook.Properties.Subject = "Фильмы";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                worksheet.Cells[1, 1].Value = "Фильм";
                worksheet.Cells[1, 2].Value = "Дата";
                worksheet.Cells[1, 3].Value = "Выручка";

                var sessions = _context.Sessions
                    .Include(s => s.Movie)
                    .Include(s => s.Tickets)
                    .ToList();

                int row = 2;

                foreach (var session in sessions)
                {
                    worksheet.Cells[row, 1].Value = session.Movie.Name;
                    worksheet.Cells[row, 2].Value = session.DateTime.ToString("dd.MM.yyyy HH:mm");
                    worksheet.Cells[row, 3].Value = session.Tickets
                        .Where(t => t.OwnerEmail != null)
                        .Sum(t => t.Price);

                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                System.IO.File.WriteAllBytes(
                    _webHostEnvironment.WebRootPath + "/reports/money_report.xlsx", 
                    excelPackage.GetAsByteArray());
            }

            return File(
                "/reports/money_report.xlsx", 
                "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet",
                "Отчет о выручке фильмов.xlsx");
        }

        public FileResult GetHallOccupancyReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Отчет о заполненности залов");

                excelPackage.Workbook.Properties.Author = "ИС Кинотеатра";
                excelPackage.Workbook.Properties.Title = "Заполненность залов";
                excelPackage.Workbook.Properties.Subject = "Залы";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                worksheet.Cells[1, 1].Value = "Фильм";
                worksheet.Cells[1, 2].Value = "Время";
                worksheet.Cells[1, 3].Value = "Мест в зале";
                worksheet.Cells[1, 4].Value = "Куплено билетов";
                worksheet.Cells[1, 5].Value = "Доля заполненности";

                var sessions = _context.Sessions
                    .Include(s => s.Movie)
                    .Include(s => s.Tickets)
                    .ToList();

                int row = 2;

                foreach (var session in sessions)
                {
                    int numberOfTickets = session.Tickets.Count;
                    int numberOfTicketsPurchased = session.Tickets
                        .Where(t => t.OwnerEmail != null)
                        .Count();

                    worksheet.Cells[row, 1].Value = session.Movie.Name;
                    worksheet.Cells[row, 2].Value = session.DateTime.ToString("HH:mm");
                    worksheet.Cells[row, 3].Value = numberOfTickets;
                    worksheet.Cells[row, 4].Value = numberOfTicketsPurchased;
                    worksheet.Cells[row, 5].Value = (double)numberOfTicketsPurchased / numberOfTickets;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                System.IO.File.WriteAllBytes(
                    _webHostEnvironment.WebRootPath + "/reports/hall_occupancy_report.xlsx",
                    excelPackage.GetAsByteArray());
            }

            return File(
                "/reports/hall_occupancy_report.xlsx",
                "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet",
                "Отчет о заполненности залов.xlsx");
        }

        public FileResult GetUserLoyaltyReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Отчет о лояльности пользователей");

                excelPackage.Workbook.Properties.Author = "ИС Кинотеатра";
                excelPackage.Workbook.Properties.Title = "Лояльность пользователей";
                excelPackage.Workbook.Properties.Subject = "Пользователи";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                worksheet.Cells[1, 1].Value = "Пользователь";
                worksheet.Cells[1, 2].Value = "Куплено билетов";

                var emails = _context.Tickets
                    .Where(t => t.OwnerEmail != null)
                    .Select(t => t.OwnerEmail)
                    .Distinct()
                    .ToList();

                int row = 2;

                foreach (var email in emails)
                {
                    worksheet.Cells[row, 1].Value = email;
                    worksheet.Cells[row, 2].Value = _context.Tickets
                        .Where(t => t.OwnerEmail != null && t.OwnerEmail == email)
                        .Count();

                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                System.IO.File.WriteAllBytes(
                    _webHostEnvironment.WebRootPath + "/reports/user_loyalty_report.xlsx",
                    excelPackage.GetAsByteArray());
            }

            return File(
                "/reports/user_loyalty_report.xlsx",
                "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet",
                "Отчет о лояльности клиентов.xlsx");
        }
    }
}
