using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogsWebApp.Models
{
    public class Dog
    {
        public int ID { get; set; }

        [Required]
        public string Breed { get; set; }

        public virtual List<BreedType> Types { get; set; }
    }
}