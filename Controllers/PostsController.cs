using centuras.org.Data;
using centuras.org.Models;
using centuras.org.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace centuras.org.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PostsController : Controller
    {
        private readonly IFileService fileService;
        private readonly ApplicationDbContext context;
        public PostsController(ApplicationDbContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await context.Posts.Include(s => s.Category)
                                                    .ToListAsync();
            return View(posts);
        }
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Title, Content, RawImage, Author, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedDate = DateTime.Now;
                string[] extensions = { ".jpg", ".jpeg", ".png" };
                if (post.RawImage != null)
                    post.CoverImage = await fileService.SaveFile(post.RawImage, "images", extensions);
                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(post);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Content, Author, CreatedDate, UpdatedDate, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UpdatedDate = DateTime.Now;
                context.Update(post);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Post item = await context.Posts.FindAsync(id);
            if (item != null)
            {
                if (item.CoverImage != null)
                    fileService.DeleteFile(item.CoverImage, "images");
                context.Posts.Remove(item);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Post(int id)
        {
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return View(post);
        }

    }
}
