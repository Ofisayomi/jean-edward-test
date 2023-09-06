using jean_edwards.Interfaces;
using jean_edwards.Model;
using Microsoft.AspNetCore.Mvc;

namespace jean_edwards.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieServices _movieService;

    public MovieController(ILogger<MovieController> logger, IMovieServices movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    [HttpGet("search/{title}")]
    public async Task<MovieModel> Get(string title, [FromQuery]MovieQuery query) => await _movieService.SearchMovie(title, query);

    [HttpGet("results")]
    public async Task<List<SearchResultResponse>> GetSearchResults() => await _movieService.GetSearchResults();
}
