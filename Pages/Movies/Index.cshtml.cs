using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]          //binds form values and query strings with the same name as the property.
        public string? SearchString { get; set; }  //Contains the text users enter in the search text box. SearchString has the [BindProperty] attribute.
        //Contains the list of genres. Genres allows the user to select a genre from the list. SelectList requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]          // is required for binding on HTTP GET requests.
        public string? MovieGenre { get; set; }     // Contains the specific genre the user selects. For example, "Western".

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;
            if (!string.IsNullOrEmpty(SearchString)) {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            Movie = await movies.ToListAsync();
        }
        /* if (_context.Movie != null)
         {
             Movie = await _context.Movie.ToListAsync();
         }*/
    }
    
}
