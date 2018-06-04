using System.Collections.Generic;

namespace DogsWebApp.Models
{
    public class Dog
    {
        public int ID { get; set; }
        public string Breed { get; set; }

        public virtual List<BreedType> Types { get; set; }        
    }
}