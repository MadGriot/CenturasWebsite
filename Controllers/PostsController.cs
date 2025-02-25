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
        public async Task<IActionResult> Create([Bind("Id, Title, Description, Content, RawImage, ZipFile, Author, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedDate = DateTime.Now;
                string[] extensions = { ".jpg", ".jpeg", ".png" };
                string[] extensionsZip = { ".zip" };
                if (post.RawImage != null)
                    post.CoverImage = await fileService.SaveFile(post.RawImage, "images", extensions);
                if (post.ZipFile != null)
                    post.ZipPath = await fileService.SaveFile(post.ZipFile, "modules", extensionsZip);
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
        public async Task<IActionResult> Edit([Bind
            ("Id, Title, Description, Content, RawImage, CoverImage, ZipFile, ZipPath, Author, CreatedDate, UpdatedDate, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UpdatedDate = DateTime.Now;
                string[] extensions = { ".jpg", ".jpeg", ".png" };
                string[] extensionsZip = { ".zip" };
                if (post.RawImage != null)
                {
                    if (post.CoverImage != null)
                        fileService.DeleteFile(post.CoverImage, "images");
                    post.CoverImage = await fileService.SaveFile(post.RawImage, "images", extensions);
                }
                if (post.ZipFile != null)
                {
                    if (post.ZipPath != null)
                        fileService.DeleteFile(post.ZipPath, "modules");
                    post.ZipPath = await fileService.SaveFile(post.ZipFile, "modules", extensionsZip);
                }
                context.Update(post);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
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
                if (item.ZipPath != null)
                    fileService.DeleteFile(item.ZipPath, "modules");
                context.Posts.Remove(item);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Post(int id)
        {
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return View(post);
        }


        [HttpGet, ActionName("Download")]
        [AllowAnonymous]
        public async Task<FileResult> DownloadFile(int id)
        {
           
            Post post = await context.Posts.FindAsync(id);
            var result = await fileService.DownloadFile(post.ZipPath, "modules");
            return File(result.Item1, result.Item2, result.Item3);
        }
    }
}
