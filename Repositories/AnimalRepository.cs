using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Animals.Data;

namespace Animals.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAnimals(string orderBy);
        Task AddAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);
        Task DeleteAnimal(int idAnimal);
    }

    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalContext _context;

        public AnimalRepository(AnimalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> GetAnimals(string orderBy)
        {
            var validColumns = new List<string> { "name", "description", "category", "area" };
            if (!validColumns.Contains(orderBy.ToLower()))
            {
                orderBy = "name";
            }

            return await _context.Animals
                .OrderBy(a => EF.Property<object>(a, char.ToUpper(orderBy[0]) + orderBy.Substring(1)))
                .ToListAsync();
        }

        public async Task AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimal(int idAnimal)
        {
            var animal = await _context.Animals.FindAsync(idAnimal);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
