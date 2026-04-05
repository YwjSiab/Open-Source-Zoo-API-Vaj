using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTC_Zoo_API.Data;
using NTC_Zoo_API.Models;

namespace NTC_Zoo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly ZooContext _context;

        public AnimalsController(ZooContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            List<Animal> animals = await _context.Animals.ToListAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            Animal? animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }
    }
}