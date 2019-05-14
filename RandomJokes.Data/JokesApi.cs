using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace RandomJokes.Data
{
    public static class JokesApi
    {
        public static Joke GetJoke()
        {
            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://official-joke-api.appspot.com/jokes/programming/random")
                    .Result;
                var joke = JsonConvert.DeserializeObject<IEnumerable<Joke>>(json).FirstOrDefault();
                return joke;
            }
        }
    }
}