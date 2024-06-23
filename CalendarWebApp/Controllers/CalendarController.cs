using CalendarWebApp.Data;
using CalendarWebApp.Models;
using CalendarWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebApp.Controllers
{
    public class CalendarController(CalendarWebAppContext context) : Controller
    {
        private readonly CalendarWebAppContext _context = context;

        // GET: Calendar
        [HttpGet("calendar")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calendar
                .OrderBy(date => date.Date).ToListAsync());
        }

        // GET: Calendar/Details/5
        [HttpGet("calendar/{date}")]
        public async Task<IActionResult> Details(DateOnly date)
        {
            var viewModel = new CalendarDetailsViewModel
            {
                Date = date
            };

            var calendar = await _context.Calendar
                .Include(e => e.Events)
                .Where(e => e.Date == date)
                .FirstOrDefaultAsync();

            if (calendar != null)
            {
                viewModel.Events = calendar.Events;
            }
            else
            {
                viewModel.Events = [];
            }

            return View(viewModel);
        }

        // GET: Calendar/Create
        [HttpGet("calendar/create")]
        public IActionResult Create()
        {
            var viewModel = new CalendarCreateViewModel();
            return View(viewModel);
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalendarCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Calendar
                    .FirstOrDefaultAsync(c => c.Date == model.Calendar!.Date);

                if (exists == null)
                {
                    _context.Calendar.Add(model.Calendar!);
                    await _context.SaveChangesAsync();

                    model.Event!.CalendarId = model.Calendar!.Id;
                }
                else
                {
                    model.Event!.CalendarId = exists.Id;
                }

                if (model.AllDay)
                {
                    model.Event.Time = null;
                }
                else 
                {
                    model.Event.Time = new TimeOnly(model.Event.Time!.Value.Hour, model.Event.Time!.Value.Minute);
                }
                
                _context.Events.Add(model.Event);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Calendar/Delete/5
        [HttpGet("calendar/{date}/deleteAll")]
        public async Task<IActionResult> DeleteAll(DateOnly date)
        {
            var viewModel = new CalendarDeleteViewModel
            {
                Date = date
            };

            var calendar = await _context.Calendar
                .Include(e => e.Events)
                .Where(e => e.Date == date)
                .FirstOrDefaultAsync();

            if (calendar != null)
            {
                viewModel.Events = calendar.Events;
            }
            else
            {
                viewModel.Events = [];
            }

            return View(viewModel);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateOnly date)
        {
            var calendar = await _context.Calendar
                .FirstOrDefaultAsync(f => f.Date == date);

            if (calendar != null)
            {
                var events = await _context.Events
                    .Where(f => f.CalendarId == calendar.Id)
                    .ToListAsync();

                if (events != null) 
                {
                    _context.Events.RemoveRange(events);
                }

                _context.Calendar.Remove(calendar);
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendar.Any(e => e.Id == id);
        }
    }
}
