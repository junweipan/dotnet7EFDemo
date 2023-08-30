using System.Text.Json.Serialization;

namespace DatabaseDemo.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CharactrerId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
