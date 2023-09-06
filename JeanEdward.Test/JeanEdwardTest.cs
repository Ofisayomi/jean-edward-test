using jean_edwards.Controllers;
using jean_edwards.Interfaces;
using jean_edwards.Model;
using jean_edwards.Services;

namespace jean_edwards.Test;

public class JeanEdwardTest
{
    IMovieServices _movieServices;
    public JeanEdwardTest(IMovieServices movieServices){
        _movieServices = movieServices;
    }

    [Fact]
    public async Task TestSearchMovie()
    {
        var title = "robot";
        var query = new MovieQuery{
            Year = 0,
            Plot = "Short"
        };
        var resp = await _movieServices.SearchMovie(title, query);

        Assert.IsType<MovieModel>(resp);
    }

    [Fact]
    public async Task GetMovieSearch()
    {
        var resp = await _movieServices.GetSearchResults();
        Assert.IsType<List<SearchResultResponse>>(resp);
    }
}