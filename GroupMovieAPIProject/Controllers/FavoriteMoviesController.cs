using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupMovieAPIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GroupMovieAPIProject.Controllers
{
    [Authorize]
    public class FavoriteMoviesController : Controller
    {
        private readonly string APIKEYVARIABLE;
        private readonly FavoriteMoviesDbContext _context;

        public FavoriteMoviesController(FavoriteMoviesDbContext context, IConfiguration configuration)
        {
            _context = context;
            APIKEYVARIABLE = configuration.GetSection("APIKeys")["APIKeyName"];
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetMovieBySearch()
        {
            return View("Search");
        }
        [HttpPost]
        public async Task<ActionResult> GetMovieBySearch(string search)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"http://omdbapi.com/");
            var response = await client.GetAsync($"?t={search}&apikey={APIKEYVARIABLE}");
            //ADD NUGET PACKAGE - Microsoft.aspnet.webapi.client
            var searchedMovies = await response.Content.ReadAsAsync<Movies>();
            ViewBag.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(searchedMovies);
        }
        public IActionResult AddToFavorites(Movies newFav)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(newFav);
                _context.SaveChanges();
            }
            return RedirectToAction("DisplayFavorites");
        }
        public IActionResult DisplayFavorites()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Movies> movieList = _context.Movies.Where(x => x.UserId == id).ToList();
            return View(movieList);
        }
        public IActionResult DeleteFav(int id)
        {
            Movies found = _context.Movies.Find(id);
            if (found != null)
            {
                _context.Movies.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("DisplayFavorites");
        }
    }
}