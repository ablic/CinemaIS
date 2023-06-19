using CinemaIS.Models;
using CinemaIS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaIS.Controllers
{
    public class SessionsController : Controller
    {

        private readonly CinemaDbContext _context;

        public SessionsController(CinemaDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(DateTime? date)
        {
            DateTime targetDate = date ?? DateTime.Today;

            var sessions = _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Tickets);

            ViewBag.DateValue = targetDate.ToString("yyyy-MM-dd");
            return View(await SessionsByDateTabUtility.GetTabsAsync(targetDate, sessions));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }


        public async Task<IActionResult> SelectMovie()
        {
            return View(await _context.Movies.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectMovie(int? selectedMovieId)
        {
            if (selectedMovieId == null)
            {
                return View();
            }

            return RedirectToAction(nameof(Create), new { movieId = selectedMovieId });
        }

        public IActionResult Create(int? movieId)
        {
            if (movieId == null)
            {
                return RedirectToAction(nameof(SelectMovie));
            }

            ViewData["MovieName"] = _context.Movies.Find(movieId).Name;
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            return View(new Session { MovieId = (int)movieId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,DateTime,HallId,MinPrice")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);

                session.Hall = await _context.Halls.FindAsync(session.HallId);
                Seat[][] layout = await SeatingUtility.GetSeatsAsync(session);

                for (int i = 0; i < layout.Length; i++)
                {
                    for (int j = 0; j < layout[i].Length; j++)
                    {
                        _context.Tickets.Add(new Ticket()
                        {
                            Session = session,
                            Price = layout[i][j].Price,
                            Place = SeatingUtility.GetPlace(i, j)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,DateTime,HallNumber,MinPrice")] Session session)
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
    }
}
