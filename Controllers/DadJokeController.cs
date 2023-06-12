using DadJoke.Service;
using DegreedDadJoke.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreedDadJoke.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadJokeController : ControllerBase
    {
        private readonly IJokeService _jokeService;

        public DadJokeController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpGet()]
        
        public async Task<ActionResult> GetRandomJoke()
        {
            try
            {
                var joke = await _jokeService.GetRandomJoke();
                return Ok(joke);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        [HttpGet("{term}")]
        public async Task<ActionResult> SearchJoke(string term)
        {
            try
            {
                var jokes = await _jokeService.SearchJokes(term);

                return Ok(jokes);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}