using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using centuras.org.Models;
using centuras.org.Data;
using Microsoft.EntityFrameworkCore;

namespace centuras.org.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        this.context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    public async Task<IActionResult> Pathfinder()
    {
        List<Post> posts = await context.Posts.Include(s => s.Category)
                                                    .ToListAsync();
        return View(posts);
    }
    public async Task<IActionResult> Games()
    {
        List<Game> games = await context.Games.ToListAsync();
        return View(games);
    }

    public async Task<IActionResult> Library(int id)
    {
        List<Post> posts = await context.Posts.Include(s => s.Category)
                                                    .ToListAsync();

        return View(posts.Where(s => s.Category?.Id == id).ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
