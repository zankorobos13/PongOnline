using System.Text.Json.Serialization;

namespace Server.Models
{
    struct PostGameDataStruct
    {
        [JsonPropertyName("player_id")]
        public string player_id { get; set; }
        [JsonPropertyName("game_id")]
        public string game_id { get; set; }
        [JsonPropertyName("move_x")]
        public float move_x { get; set; }
        [JsonPropertyName("move_y")]
        public float move_y { get; set; }
    }
}
