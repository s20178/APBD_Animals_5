using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Animals.Data;
using Animals.Repositories;

namespace Animals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _repository;

        public AnimalsController(IAnimalRepository repository)
        {
            _repository = repository;
        }

        // GET: api/animals
        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy = "name")
        {
            var animals = await _repository.GetAnimals(orderBy);
            return Ok(animals);
        }

        // POST: api/animals
        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAnimal(animal);
            return CreatedAtAction(nameof(GetAnimals), new { id = animal.IdAnimal }, animal);
        }

        // PUT: api/animals/{idAnimal}
        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal(int idAnimal, [FromBody] Animal animal)
        {
            if (animal == null || animal.IdAnimal != idAnimal)
            {
                return BadRequest("Invalid animal data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAnimal(animal);
            return NoContent();
        }

        // DELETE: api/animals/{idAnimal}
        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            await _repository.DeleteAnimal(idAnimal);
            return NoContent();
        }
    }
}
