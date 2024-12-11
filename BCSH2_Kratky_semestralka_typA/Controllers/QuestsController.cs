using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class QuestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Akce pro zobrazení seznamu úkolů
    public async Task<IActionResult> Index()
    {
        var quests = await _context.Quests.ToListAsync();
        return View(quests);
    }

    // Akce pro zobrazení formuláře pro vytvoření nového úkolu
    public IActionResult Create()
    {
        ViewBag.ValidationErrors = new List<string>(); // Prázdný seznam pro chyby validace
        ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name");
        ViewBag.Members = new SelectList(_context.Members.ToList(), "Id_Member", "Name");
        return View(new Quest());
    }

    // Akce pro zpracování formuláře pro vytvoření nového úkolu
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Quest quest)
    {
        if (ModelState.IsValid)
        {
            // Přidej quest do databáze a přesměruj na Index
            _context.Add(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Pokud validace selže, znovu nastav ViewBag a vrátíš chyby
        ViewBag.ValidationErrors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name");
        ViewBag.Members = new SelectList(_context.Members.ToList(), "Id_Member", "Name");

        return View(quest);
    }

    // Akce pro zobrazení formuláře pro úpravu úkolu
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var quest = await _context.Quests
       .Include(q => q.Guild)
       .Include(q => q.AcceptedBy) // Načteme data o členu, který přijal quest
       .FirstOrDefaultAsync(q => q.Id == id);

      
        if (quest == null)
        {
            return NotFound();
        }

        ViewBag.Guilds = new SelectList(_context.Guilds.ToList(), "Id_Guild", "Name", quest.GuildId);
        ViewBag.Members = new SelectList(_context.Members.ToList(), "Id_Member", "Name", quest.AcceptedBy?.Id_Member); // Nastavíme výchozí hodnotu
        return View(quest);
    }

    // Akce pro zpracování úpravy úkolu
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Quest quest)
    {
        if (id != quest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(quest);
    }

    // Akce pro smazání úkolu
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quest = await _context.Quests.FindAsync(id);
        if (quest == null)
        {
            return NotFound();
        }

        return View(quest);
    }

    // Akce pro potvrzení smazání úkolu
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var quest = await _context.Quests.FindAsync(id);
        _context.Quests.Remove(quest);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    //akce na potvrzení ukolu členem gildy
    public async Task<IActionResult> Accept(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quest = await _context.Quests
        .Include(quest => quest.Guild)
        .FirstOrDefaultAsync(quest => quest.Id == id);

        if (quest == null)
        {
            return NotFound();
        }

        ViewBag.Members = new SelectList(
            _context.Members.Where(m => m.GuildId == quest.GuildId).ToList(),
            "Id_Member",
            "Name"
            );

        return View(quest);


    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(int id, int memberId)
    {
        var quest = await _context.Quests.FindAsync(id);

        if (quest == null)
        {
            return NotFound();
        }

        quest.AcceptedById = memberId;

        _context.Update(quest);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}
