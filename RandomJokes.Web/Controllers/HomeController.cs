using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RandomJokes.Data;
using RandomJokes.Web.Models;

namespace RandomJokes.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private const int MinutesAllowedToChangeLike = 5;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var joke = JokesApi.GetJoke();
            var jokeRepo = new JokesRepository(_connectionString);
            if (!jokeRepo.JokeExists(joke.OriginId))
            {
                jokeRepo.AddJoke(joke);
            }
            else
            {
                joke = jokeRepo.GetByOriginId(joke.OriginId);
            }
            var viewModel = new IndexViewModel { Joke = joke };
            viewModel.InteractionStatus = GetStatus(joke.Id);
            return View(viewModel);
        }

        public IActionResult ViewAll()
        {
            var repo = new JokesRepository(_connectionString);
            return View(repo.GetAll());
        }

        public IActionResult GetInteractionStatus(int jokeId)
        {
            var vm = new InteractionStatusViewModel
            {
                InteractionStatus = GetStatus(jokeId)
            };
            return Json(vm);
        }

        [HttpPost]
        [Authorize]
        public void InteractWithJoke(int jokeId, bool like)
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            var jokeRepo = new JokesRepository(_connectionString);
            jokeRepo.InteractWithJoke(user.Id, jokeId, like);
        }

        public IActionResult GetLikesCount(int jokeId)
        {
            var repo = new JokesRepository(_connectionString);
            var joke = repo.GetWithLikes(jokeId);
            return Json(new
            {
                likes = joke.UserJokeLikes.Count(u => u.Like),
                dislikes = joke.UserJokeLikes.Count(u => !u.Like)
            });
        }

        private UserJokeInteractionStatus GetStatus(int jokeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return UserJokeInteractionStatus.Unauthenticated;
            }
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            var jokeRepo = new JokesRepository(_connectionString);
            var likeStatus = jokeRepo.GetLike(user.Id, jokeId);
            if (likeStatus == null)
            {
                return UserJokeInteractionStatus.NeverInteracted;
            }
            if (likeStatus.Date.AddMinutes(MinutesAllowedToChangeLike) < DateTime.Now)
            {
                return UserJokeInteractionStatus.CanNoLongerInteract;
            }
            return likeStatus.Like
                   ? UserJokeInteractionStatus.Liked
                   : UserJokeInteractionStatus.Disliked;
        }
    }
}
