using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BCSH2_Kratky_semestralka_typA.Controllers
{
    public class GuildsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //injection ApplicationDbContext , for working with db
        public GuildsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //akce na zobrazení seznamu gild
        public async Task<IActionResult> Index()
        {
            var guilds = await _context.Guilds.ToListAsync(); //pokud fk na jinou tabulku
            //Console.WriteLine($"Počet načtených gild: {guilds.Count}");
            //foreach (var guild in guilds)
            //{
            //    Console.WriteLine($"Gilda: {guild.Name}, Typ: {guild.Type}, Prestiž: {guild.Prestige}");
            //}
            return View(guilds); //zobrazí do view
        }

        //akce na zobrazení formuláře nové guildy
        public IActionResult Create()
        {
            return View(new Guild());
        }

        //Akce pr zpracování formuláře pro přidání nové gildy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guild guild)
        {
            if (ModelState.IsValid)
            {
                await _context.Guilds.AddAsync(guild);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(guild);
        }

        //Akce pro zobrazení formuláře pro úpravu gildy
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guild = await _context.Guilds.FindAsync(id);
            if (guild == null)
            {
                return NotFound();
            }
            return View(guild);
        }

        //Akce pro zpracování úpravy gildy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guild guild)
        {
            if (id != guild.Id_Guild)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(guild);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guild);
        }
         //Akce pro smazání gildy
         public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var guild = await _context.Guilds.FindAsync(id);
            if (guild == null)
            {
                return NotFound();
            }
            return View(guild);
        }

        //Akce pro zpracování mazání gildy
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guild = await _context.Guilds.FindAsync(id);
            _context.Guilds.Remove(guild);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
