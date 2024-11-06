using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BCSH2_Kratky_semestralka_typA.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.ToListAsync(); // Načte všechny členy asynchronně
            return View(members); // Předá seznam členů do view
        }

        // GET: Zobrazení formuláře pro vytvoření nového člena
        public IActionResult Create()
        {
            var guilds = _context.Guilds.ToList();
            Console.WriteLine($"Počet dostupných gild: {guilds.Count}");

            // Zajistí, že ViewBag.Guilds není null, i když nejsou žádné gildy v databázi
            ViewBag.Guilds = guilds.Any() ? new SelectList(guilds, "Id", "Name") : new SelectList(new List<SelectListItem>());


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Members.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Guilds = new SelectList(_context.Guilds, "Id", "Name"); // Pokud validace selže, znovu naplní ViewBag
            return View(member);
        }
    }
}


