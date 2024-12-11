using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BCSH2_Kratky_semestralka_typA.Controllers
{
    public class TreasureVaultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TreasureVaultsController> _logger;

        public TreasureVaultsController(ApplicationDbContext context, ILogger<TreasureVaultsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //return View();
            var treasureVaults = await _context.TreasureVaults
            .Include(tv => tv.Guild)
            .ToListAsync();
            return View(treasureVaults);
        }

        // GET: TreasureVault/Create
        public IActionResult Create()
        {
          
                var guilds = _context.Guilds.ToList();
            Console.WriteLine($"Počet gild v databázi: {guilds.Count}");
            ViewBag.Guilds = new SelectList(guilds, "Id_Guild", "Name");
                return View(new TreasureVault());
            
          
        }

		// POST: TreasureVault/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TreasureVault treasureVault)
		{
            Console.WriteLine($"GuildId: {treasureVault.GuildId}");
            Console.WriteLine($"GoldAmount: {treasureVault.GoldAmount}");
            Console.WriteLine($"Guild: {treasureVault.Guild}");

            if (!ModelState.IsValid)
			{
                Console.WriteLine($"GuildId: {treasureVault.GuildId}");
                Console.WriteLine($"GoldAmount: {treasureVault.GoldAmount}");
				Console.WriteLine("ModelState není validní:");
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					Console.WriteLine($"- {error.ErrorMessage}");
				}

				ViewBag.ValidationErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name");
				return View(treasureVault);
			}

			Console.WriteLine("ModelState je validní. Přidávám pokladnici do databáze.");
			await _context.TreasureVaults.AddAsync(treasureVault);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

        // GET: TreasureVaults/Edit/5
        public IActionResult Edit(int id)
        {
            var treasureVault = _context.TreasureVaults.Find(id);
            if (treasureVault == null)
            {
                return NotFound();
            }

            ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name", treasureVault.GuildId);
            return View(treasureVault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TreasureVault treasureVault)
        {
            if (ModelState.IsValid)
            {
                _context.Update(treasureVault);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name", treasureVault.GuildId);
            return View(treasureVault);
        }

        // GET: TreasureVaults/Delete/5
        public IActionResult Delete(int id)
        {
            var treasureVault = _context.TreasureVaults.Include(tv => tv.Guild).FirstOrDefault(tv => tv.Id == id);
            if (treasureVault == null)
            {
                return NotFound();
            }

            return View(treasureVault);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var treasureVault = _context.TreasureVaults.Find(id);
            if (treasureVault != null)
            {
                _context.TreasureVaults.Remove(treasureVault);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
