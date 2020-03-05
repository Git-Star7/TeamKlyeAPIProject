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
    }
}