using BCSH2_Kratky_semestralka_typA.Data;
using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.AspNetCore.Mvc;
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
        return View();
    }

    // Akce pro zpracování formuláře pro vytvoření nového úkolu
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Quest quest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(quest);
    }

    // Akce pro zobrazení formuláře pro úpravu úkolu
    public async Task<IActionResult> Edit(int? id)
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
}
