using CinemaIS.Models;
using CinemaIS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaIS.Controllers
{
    public class SessionsController : Controller
    {
        public const int TabsCount = 6;

        private readonly CinemaDbContext _context;

        public SessionsController(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var sessions = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .ToListAsync();

            date ??= DateTime.Today;
            ICollection<SessionsByDateTab> tabs = new List<SessionsByDateTab>();

            for (int i = 0; i < TabsCount - 1; i++)
            {
                DateTime nextDate = ((DateTime)date).AddDays(i);
                tabs.Add(new SessionsByDateTab
                {
                    Title = GetDateName(nextDate),
                    HtmlId = "date-" + i,
                    Sessions = sessions.Where(s => s.DateTime.Date == nextDate).ToList()
                });
            }

            ViewBag.DateValue = ((DateTime)date).ToString("yyyy-MM-dd");
            return View(tabs);
        }

        /*[HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult IndexDateChanged(DateTime? date)
        {
            return RedirectToAction(nameof(Index), "Sessions", new { customDate = date });
        }*/

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name");
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,DateTime,HallId")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);

                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        _context.Tickets.Add(new Ticket()
                        {
                            Session = session,
                            Row = i,
                            Seat = j
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", session.MovieId);
            return View(session);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", session.MovieId);
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,DateTime,HallNumber")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", session.MovieId);
            return View(session);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'CinemaDbContext.Sessions'  is null.");
            }

            var session = await _context.Sessions.FindAsync(id);

            if (session != null)
            {
                _context.Sessions.Remove(session);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetDateName(DateTime date)
        {
            if (date == DateTime.Today)
                return "Сегодня";

            if (date == DateTime.Today.AddDays(1))
                return "Завтра";

            string result = "";

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: result += "Понедельник"; break;
                case DayOfWeek.Tuesday: result += "Вторник"; break;
                case DayOfWeek.Wednesday: result += "Среда"; break;
                case DayOfWeek.Thursday: result += "Четверг"; break;
                case DayOfWeek.Friday: result += "Пятница"; break;
                case DayOfWeek.Saturday: result += "Суббота"; break;
                case DayOfWeek.Sunday: result += "Воскресенье"; break;
            }

            result += " " + date.ToString("dd.MM");
            return result;
        }
    }
}
