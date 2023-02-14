using System.Text.Json.Serialization;

namespace EntityRelationship
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Damage { get; set; }
        //Weapon non puo esistere senza Character
        [JsonIgnore]
        public Character Character { get; set; }
        public int CharacterId { get; set; }

    }
}
