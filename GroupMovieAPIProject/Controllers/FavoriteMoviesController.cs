using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMovieAPIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMovieAPIProject.Controllers
{
    [Authorize]
    public class FavoriteMoviesController : Controller
    {
        private readonly FavoriteMoviesDbContext _context;

        public FavoriteMoviesController(FavoriteMoviesDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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
            var movieList = _context.Movies.ToList();
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