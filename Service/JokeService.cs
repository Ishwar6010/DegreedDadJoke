using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using DegreedDadJoke.Models;

namespace DadJoke.Service
{
    public class JokeService : IJokeService
    {
        private  readonly HttpClient _httpClient;
        public JokeService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/"),
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<Joke> GetRandomJoke()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var joke = JsonConvert.DeserializeObject<Joke>(json);
                return joke;
            }
            else
            {
                throw new Exception("Failed to fetch a random joke.");
            }
        }

        public async Task<IEnumerable<Joke>> SearchJokes(string term)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"search?term={term}&limit=30");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var joke = JsonConvert.DeserializeObject<JokeResult>(json);
                List<Joke> shortJokes = new List<Joke>();
                List<Joke> mediumJokes = new List<Joke>();
                List<Joke> longJokes = new List<Joke>();

                foreach (var EachJoke in joke.Results)
                {
                    string emphasizedJokeText = EachJoke.joke.Replace(term, $"<strong>{term}</strong>");
                    EachJoke.JokeText = emphasizedJokeText;

                    if (EachJoke.joke.Length < 10)
                    {
                        shortJokes.Add(EachJoke);
                    }
                    else if (EachJoke.joke.Length < 20)
                    {
                        mediumJokes.Add(EachJoke);
                    }
                    else
                    {
                        longJokes.Add(EachJoke);
                    }
                }

                var groupedJokes = shortJokes.Concat(mediumJokes).Concat(longJokes).OrderBy(x => x.joke.Length);
                
                return groupedJokes;
            }
            else
            {
                throw new Exception("Failed to search for jokes.");
            }
        }
    }
}
