using CinemaIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaIS.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoviesController(
            CinemaDbContext context, 
            UserManager<Visitor> userManager, 
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Movies != null 
                ? View(await _context.Movies.ToListAsync())
                : Problem("Entity set 'CinemaDbContext.Movies'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Duration,ReleaseDate,Rating,Description")] Movie movie,
            IFormFile? poster)
        {
            if (ModelState.IsValid)
            {
                if (poster != null)
                    movie.PosterUrl = await SaveImageAsync(poster);

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,Name,Duration,ReleaseDate,Rating,Description")] Movie movie,
            IFormFile poster)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (poster != null)
                    {
                        if (movie.PosterUrl != null)
                            DeleteImage(movie.PosterUrl);

                        movie.PosterUrl = await SaveImageAsync(poster);
                    }

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'CinemaDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string name = 
                Path.GetFileNameWithoutExtension(image.FileName) +
                DateTime.Now.ToString("yymmssfff") +
                Path.GetExtension(image.FileName);

            using (FileStream fileStream = new FileStream(Path.Combine(wwwRootPath, "images", name), FileMode.Create))
                await image.CopyToAsync(fileStream);

            return name;
        }

        private void DeleteImage(string url)
        {
            System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", url));
        }
    }
}
