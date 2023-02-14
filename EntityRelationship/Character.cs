using System.Text.Json.Serialization;

namespace EntityRelationship
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RpgC { get; set; } = "Spada";
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
        //Character puo esistere senza Weapon
        public Weapon Weapon { get; set; }
        public List<Skills> Skills { get; set; }

    }
}
