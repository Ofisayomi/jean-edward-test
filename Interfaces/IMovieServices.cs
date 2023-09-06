using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jean_edwards.Model;

namespace jean_edwards.Interfaces
{
    public interface IMovieServices
    {
        Task<MovieModel> SearchMovie(string title, MovieQuery query);
        Task<List<SearchResultResponse>> GetSearchResults();
    }
}