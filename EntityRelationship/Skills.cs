using System.Text.Json.Serialization;

namespace EntityRelationship
{
    public class Skills
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        public int Damage { get; set; }
        [JsonIgnore]
        public List<Character> CharacterS { get; set; }  

    }
}
