using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Animals.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
    }

    public class Animal
    {
        [Key]
        public int IdAnimal { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Area { get; set; }
    }
}
