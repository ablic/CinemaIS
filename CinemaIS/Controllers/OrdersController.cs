using CinemaIS.Models;
using CinemaIS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CinemaIS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrdersController(CinemaDbContext context, UserManager<Visitor> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> ChooseSeats(int? sessionId)
        {
            if (sessionId == null || _context.Sessions == null)
                return NotFound();

            Session? session = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                return NotFound();

            HttpContext.Session.SetInt32("SessionId", (int)sessionId);

            Seat[][] seats = await SeatingUtility.GetSeatsAsync(session);

            List<string> purchasedTickets = await _context
                .Tickets
                .Where(t => t.SessionId == session.Id && t.OwnerEmail != null)
                .Select(t => t.Place)
                .ToListAsync();

            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[i].Length; j++)
                {
                    seats[i][j].IsOccupied = 
                        purchasedTickets.Contains($"{i+1}-{j+1}") || 
                        session.Tickets.Where(t => t.Place == $"{i + 1}-{j + 1}").FirstOrDefault() == null; 
                }
            }

            return View(seats);
        }

        [HttpPost]
        [ActionName(nameof(ChooseSeats))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseSeatsConfirmed(Seat[][] seats)
        {
            if (NoSeatsSelected(seats))
            {
                return View(seats);
            }

            List<string> selectedSeats = new List<string>();

            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[i].Length; j++)
                {
                    if (seats[i][j].IsSelected)
                    {
                        selectedSeats.Add(SeatingUtility.GetPlace(i, j));
                    }
                }
            }

            HttpContext.Session.SetString("SelectedSeats", JsonConvert.SerializeObject(selectedSeats));
            return RedirectToAction(nameof(EnterEmail));
        }


        public IActionResult EnterEmail()
        {
            return View();
        }

        [HttpPost]
        [ActionName(nameof(EnterEmail))]
        [ValidateAntiForgeryToken]
        public IActionResult EnterEmailConfirmed([Bind("Email")] BuyerEmail buyerEmail)
        {
            HttpContext.Session.SetString("BuyerEmail", buyerEmail.Email);
            return RedirectToAction(nameof(CheckOrder));
        }

        public async Task<IActionResult> CheckOrder()
        {
            List<string> selectedSeats = JsonConvert.DeserializeObject<List<string>>(
                HttpContext.Session.GetString("SelectedSeats"));

            int sessionId = (int)HttpContext.Session.GetInt32("SessionId");

            List<Ticket> selectedTickets = await _context
                .Tickets
                .Where(t => t.SessionId == sessionId && selectedSeats.Contains(t.Place))
                .ToListAsync();

            var session = await _context.Sessions.FindAsync(sessionId);
            session.Movie = await _context.Movies.FindAsync(session.MovieId);

            return View(new Order
            {
                Session = session,
                BuyerEmail = HttpContext.Session.GetString("BuyerEmail"),
                SelectedTickets = selectedTickets
            });
        }

        [HttpPost]
        [ActionName(nameof(CheckOrder))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOrderConfirmed(Order order)
        {
            foreach (var ticket in order.SelectedTickets)
            {
                ticket.OwnerEmail = order.BuyerEmail;
            }

            _context.UpdateRange(order.SelectedTickets);

            Visitor visitor = await _userManager.FindByEmailAsync(order.BuyerEmail);

            if (visitor != null)
            {
                foreach (var ticket in order.SelectedTickets)
                {
                    ticket.VisitorId = visitor.Id;
                }
            }

            await _context.SaveChangesAsync();

            foreach (var ticket in order.SelectedTickets)
            {
                ticket.Session = await _context.Sessions.FindAsync(ticket.SessionId);
                ticket.Session.Movie = await _context.Movies.FindAsync(ticket.Session.MovieId);

                await EmailService.SendTicketAsync(
                    order.BuyerEmail, 
                    _webHostEnvironment.WebRootPath + "/qr/temp.jpeg", 
                    ticket);
            }

            return RedirectToAction("Index", "Sessions");
        }

        private bool NoSeatsSelected(Seat[][] seats)
        {
            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[i].Length; j++)
                {
                    if (seats[i][j].IsOccupied)
                        continue;

                    if (seats[i][j].IsSelected)
                        return false;
                }
            }

            return false;
        }
    }
}
