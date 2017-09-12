using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coreMvc.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace coreMvc.Controllers
{
    public class MoviesController : Controller
    {
        private readonly coreMvcContext _context;

        public MoviesController(coreMvcContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string SelectList, string SearchString , int page = 1 )
        {
           
            //ViewData["CurrentSort"] = sortOrder;
            /* if (searchString != null)
             {
                 page = 1;
             }
             else
             {
                 searchString = currentFilter;
             }
             ViewData["CurrentFilter"] = searchString;*/

            IQueryable<string> genreQuery = from m in _context.Movies
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movies
                         select m;


            if (!String.IsNullOrEmpty(SearchString))
               
                {
                 //SearchString = null;
                movies = movies.Where(s => s.Title.Contains(SearchString));
                
               
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
                
            }

            var qry =movies.AsNoTracking().OrderBy(p => p.Title);
            var model = await PagingList<Movies>.CreateAsync(qry, 2, page);

            model.RouteValue = new RouteValueDictionary { { "movieGenre", movieGenre } };
            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.movies = model;


            return View(movieGenreVM);
                     
        }

        //public ActionResult IndexViewModel()
        //{
        //   // ViewBag.Message = "Welcome to my demo!";
        //    ViewModel mymodel = new ViewModel();
        //    mymodel.movies = getMovies();
        //    mymodel.genres = getGenres();
        //    return View(mymodel);
        //}

        //private IEnumerable<MovieGenreViewModel> getGenres()
        //{
        //    throw new NotImplementedException();           
        //}

        //private IEnumerable<Movies> getMovies()
        //{
        //    throw new NotImplementedException();
        //}





        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .SingleOrDefaultAsync(m => m.ID == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movies movies)
        {
            if (id != movies.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .SingleOrDefaultAsync(m => m.ID == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
