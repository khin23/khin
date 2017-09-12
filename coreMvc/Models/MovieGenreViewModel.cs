using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

namespace coreMvc.Models
{
    public class MovieGenreViewModel
    {
       // public List<Movies> movies;
        public SelectList Genres { get; set; }

       // public PaginatedList<Movies> paginatedList;
        public string movieGenre { get; set; }
       public PagingList<Movies> movies { get;  set; }
    }
}
