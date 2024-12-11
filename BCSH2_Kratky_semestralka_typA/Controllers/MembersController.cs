using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;

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
            var members = await _context.Members.Include(m=> m.Guild).ToListAsync(); // Načte všechny členy asynchronně
            return View(members); // Předá seznam členů do view
        }

        // GET: Zobrazení formuláře pro vytvoření nového člena
        public IActionResult Create()
        {
            var guilds = _context.Guilds.ToList();
            ViewBag.Guilds = new SelectList(guilds, "Id_Guild", "Name");
            return View(new Member());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            Console.WriteLine($"Name: {member.Name}, Surname: {member.Surname}, Rank: {member.Rank}, Class: {member.Class}, GuildId: {member.GuildId}");

            if (ModelState.IsValid)
            {
                
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                //Console.WriteLine("Validace selhala. Detaily chyb:");
                //foreach (var modelState in ModelState.Values)
                //{
                //    foreach (var error in modelState.Errors)
                //    {
                //        Console.WriteLine($"Validation error: {error.ErrorMessage}");
                //    }
                //}
                ViewBag.ValidationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                //ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name");
                //return View(member);
            }

            // Znovu naplníme ViewBag.Guilds pro případ, že validace selže
            ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name");
            return View(member);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id_Member == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id_Member == id);

            if (member == null)
            {
                return NotFound();
            }

            ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name", member.GuildId);

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.Id_Member)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Members.Any(m => m.Id_Member == member.Id_Member))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name", member.GuildId);

            return View(member);
        }

    }
}


