using System;

namespace RandomJokes.Data
{
    public class UserJokeLike
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public User User { get; set; }
        public Joke Joke { get; set; }
        public bool Like { get; set; }
        public DateTime Date { get; set; }
    }
}