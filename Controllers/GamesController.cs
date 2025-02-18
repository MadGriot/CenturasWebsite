using centuras.org.Data;
using centuras.org.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace centuras.org.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext context;
        public GamesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Game> games = await context.Games.ToListAsync();
            return View(games);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Title, Content, Author, CreatedDate, UpdatedDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.CreatedDate = DateTime.Now;
                context.Games.Add(game);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(game);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Content, Author, CreatedDate, UpdatedDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.UpdatedDate = DateTime.Now;
                context.Update(game);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game game = await context.Games.FindAsync(id);
            if (game != null)
            {
                context.Games.Remove(game);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Game(int id)
        {
            Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }
    }
}
