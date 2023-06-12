
using DegreedDadJoke.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DadJoke.Service
{
    public interface IJokeService
    {
        public Task<Joke> GetRandomJoke();
        public Task<IEnumerable<Joke>> SearchJokes(string searchTerm);
    }
}
