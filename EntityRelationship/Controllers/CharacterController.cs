using EntityRelationship.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityRelationship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;
        public CharacterController(DataContext dataContext) 
        {
            _context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _context.Characters
                                .Where(x => x.UserId == userId)
                                .Include(c => c.Weapon)
                                .Include(c => c.Skills)
                                .ToListAsync(); 
            return Ok(characters);
        }

        [HttpGet("skill")]
        public async Task<ActionResult<List<Character>>> GetSkill(int skillId)
        {   //Take single skill with all characters and in character weapon
            var skill = await _context.Skill.Where(x => x.Id == skillId)
                                .Include(c => c.CharacterS)
                                .ThenInclude(c => c.Weapon)
                                .ToListAsync();

            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterDTO request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var characterCreated = new Character
            {
                   Name = request.Name,
                   RpgC = request.RpgC,
                   User = user,
            };

            _context.Characters.Add(characterCreated);
            await _context.SaveChangesAsync();

           
            return await Get(characterCreated.UserId);
        }
        //Aggiunge una nuova skill e lo attribuisce ai vari character
        [HttpPost("skill")]
        public async Task<ActionResult<Skills>> CreateSkill(SkillDto request)
        {
            List<Character> characters = new();
            foreach (var characterId in request.CharactersId) 
            {
                var character = await _context.Characters.FindAsync(characterId);

                if(character == null)
                {
                    break;
                }

                characters.Add(character);

            }

            var newSkill = new Skills
            {
                Name = request.Name,
                Damage = request.Damage,
                CharacterS = characters
            };

            _context.Skill.Add(newSkill);
            await _context.SaveChangesAsync();


            return newSkill;
        }
        [HttpPost("character")]
        public async Task<ActionResult<Character>> CreateCharacterSkill(CharacterSkillDto request)
        {
            var character =  await _context.Characters.Where(c => c.Id == request.CharacterId)
                                    .Include(c => c.Skills)
                                    .Include(c => c.Weapon)
                                    .FirstOrDefaultAsync();
            var skill = await _context.Skill.FindAsync(request.SkillId);

            if(character == null || skill == null)
            {
                return NotFound();
            }

            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;

        }
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> CreateSkill(WeaponDto request)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);
            if (character == null)
            {
                return NotFound();
            }

            var weaponCreated = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character,
            };

            _context.Weapon.Add(weaponCreated);
            await _context.SaveChangesAsync();


            return character;
        }
    }
}
