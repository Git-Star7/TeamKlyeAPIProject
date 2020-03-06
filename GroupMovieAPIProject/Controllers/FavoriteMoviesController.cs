using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<ActionResult> GetMovieBySearch(string search)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri($"http://omdbapi.com/");
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "http://omdbapi.com/");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", $"{APIKEYVARIABLE}");
            var response = await client.GetAsync($"?t={search}");
            //ADD NUGET PACKAGE - Microsoft.aspnet.webapi.client
            var searchedMovies = await response.Content.ReadAsAsync<Movies>();
            return View(searchedMovies);
            
        }
    }
}