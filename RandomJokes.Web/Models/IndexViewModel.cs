using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomJokes.Data;

namespace RandomJokes.Web.Models
{
    public class IndexViewModel
    {
        public Joke Joke { get; set; }
        public UserJokeInteractionStatus InteractionStatus { get; set; }
    }
}
