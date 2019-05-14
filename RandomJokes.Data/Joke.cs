using System.Collections.Generic;
using Newtonsoft.Json;

namespace RandomJokes.Data
{
    public class Joke
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("id")]
        public int OriginId { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }

        public List<UserJokeLike> UserJokeLikes { get; set; }
    }
}
