using centuras.org.Data;
using centuras.org.Models;
using centuras.org.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace centuras.org.Controllers
{
    public class GamesController : Controller
    {
		private readonly IFileService fileService;
		private readonly ApplicationDbContext context;
        public GamesController(ApplicationDbContext context, IFileService fileService)
        {
            this.context = context;
			this.fileService = fileService;

		}

		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Index()
        {
            List<Game> games = await context.Games.ToListAsync();
            return View(games);
        }

		[Authorize(Roles = Roles.Administrator)]
		public IActionResult Create()
        {
			ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");
			return View();
        }

        [HttpPost]
		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Create([Bind("Id, Title, Description, Content, RawImage, ZipFile, Author")] Game game)
        {
            if (game.RawImage == null)
                throw new Exception();
            if (ModelState.IsValid)
            {
                game.CreatedDate = DateTime.Now;
				string[] extensions = { ".jpg", ".jpeg", ".png" };
				string[] extensionsZip = { ".zip" };
                if (game.RawImage != null)
                    game.CoverImage = await fileService.SaveFile(game.RawImage, "images", extensions);
                if (game.ZipFile != null)
                    game.ZipPath = await fileService.SaveFile(game.ZipFile, "games", extensionsZip);
				context.Games.Add(game);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(game);
        }

		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Edit(int id)
        {
			ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");
			Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }

        [HttpPost]
		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Edit(int id, 
            [Bind("Id, Title, Description, Content, RawImage, CoverImage, ZipFile, ZipPath, Author, CreatedDate, UpdatedDate")] Game game)
        {
			if (ModelState.IsValid)
			{
				game.UpdatedDate = DateTime.Now;
				string[] extensions = { ".jpg", ".jpeg", ".png" };
				string[] extensionsZip = { ".zip" };
				if (game.RawImage != null)
				{
					if (game.CoverImage != null)
						fileService.DeleteFile(game.CoverImage, "images");
					game.CoverImage = await fileService.SaveFile(game.RawImage, "images", extensions);
				}
				if (game.ZipFile != null)
				{
					if (game.ZipPath != null)
						fileService.DeleteFile(game.ZipPath, "games");
					game.ZipPath = await fileService.SaveFile(game.ZipFile, "games", extensionsZip);
				}
				context.Update(game);
				await context.SaveChangesAsync();
				return RedirectToAction("index");
			}
			return View(game);
        }

		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> Delete(int id)
        {
            Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
		[Authorize(Roles = Roles.Administrator)]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
			Game item = await context.Games.FindAsync(id);
			if (item != null)
			{
				if (item.CoverImage != null)
					fileService.DeleteFile(item.CoverImage, "images");
				if (item.ZipPath != null)
					fileService.DeleteFile(item.ZipPath, "games");
				context.Games.Remove(item);
				await context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Game(int id)
        {
            Game game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            return View(game);
        }

		[HttpGet, ActionName("Download")]
		public async Task<FileResult> DownloadFile(int id)
		{

			Game game = await context.Games.FindAsync(id);
			var result = await fileService.DownloadFile(game.ZipPath, "games");
			return File(result.Item1, result.Item2, result.Item3);
		}
	}
}
