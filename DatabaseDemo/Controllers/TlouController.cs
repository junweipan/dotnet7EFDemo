using DatabaseDemo.Data;
using DatabaseDemo.DTOs;
using DatabaseDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TlouController : ControllerBase
    {
        private readonly DataContext _context;
        public TlouController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            return Ok(character);

        }

        [HttpPost(Name = "junwei")]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDTO request)
        {
            var newCharacter = new Character()
            {
                Name = request.Name,
            };
            var backpack = new Backpack()
            {
                Description = request.Backpack.description,
                Character = newCharacter
            };

            var weapons = request.Weapons.Select(w => new Weapon { Name = w.Name, Character = newCharacter }).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            // return Ok(await _context.Characters.Include(c => c.Backpack).ToListAsync());
            return Ok(await _context.Characters.Include(c => c.Backpack).Include(c =>c.Weapons).ToListAsync());
        }
    }
}
