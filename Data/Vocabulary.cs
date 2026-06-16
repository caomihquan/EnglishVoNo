using Newtonsoft.Json;

namespace EnglishVoNo.Data
{
    public class Vocabulary
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("deckId")]
        public string DeckId { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; } = string.Empty;
        [JsonProperty("word")]
        public string Word { get; set; } = string.Empty;
        [JsonProperty("meaning")]
        public string Meaning { get; set; } = string.Empty;
        [JsonProperty("level")]
        public string Level { get; set; } = string.Empty;
    }
}
