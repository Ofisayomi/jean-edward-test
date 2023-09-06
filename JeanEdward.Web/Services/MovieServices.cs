using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using jean_edwards.Database;
using jean_edwards.Database.Entities;
using jean_edwards.Interfaces;
using jean_edwards.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace jean_edwards.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;
        public MovieServices(HttpClient httpClient, IConfiguration config, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _config = config;
            _dbContext = dbContext;
        }
        public async Task<MovieModel> SearchMovie(string title, MovieQuery query)
        {
            try
            {
                var apiKey = _config.GetSection("ApiKey").Value;
                var year = query.Year == 0 ? string.Empty : query.Year.ToString();
                var plot = string.IsNullOrEmpty(query.Plot) ? "short" : query.Plot;
                var responseString = await _httpClient.GetStringAsync($"?apiKey={apiKey}&t={title}&plot={plot}&y={year}");
                var responseModel = JsonConvert.DeserializeObject<MovieModel>(responseString);
                if (responseModel.Response)
                {
                    var movieResult = await _dbContext.MovieResults.FirstOrDefaultAsync(x => x.ImdbId == responseModel.imdbID);
                    if (movieResult == null)
                    {
                        await _dbContext.MovieResults.AddAsync(new MovieResult
                        {
                            Keyword = title,
                            SearchResult = JsonConvert.SerializeObject(responseModel),
                            ImdbId = responseModel.imdbID
                        });
                    }
                    else
                    {
                        movieResult.DateCreated = DateTime.Now;
                        movieResult.Keyword = title;
                    }

                    await _dbContext.SaveChangesAsync();
                }
                return responseModel;
            }
            catch (WebException webEx)
            {
                throw new WebException($"Error connecting to remote server. {webEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception($"An error occurred. {ex.Message}");
            }
        }

        public async Task<List<SearchResultResponse>> GetSearchResults()
        {
            var searchResults = await _dbContext.MovieResults.OrderByDescending(x => x.DateCreated).Take(5).ToListAsync();
            List<SearchResultResponse> response = new();
            searchResults.ForEach(x =>
            {
                response.Add(new SearchResultResponse
                {
                    Title = x.Keyword,
                    SearchResult = JsonConvert.DeserializeObject<MovieModel>(x.SearchResult)
                });
            });

            return response;
        }
    }
}