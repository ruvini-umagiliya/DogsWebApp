namespace DogsWebApp.Models
{
    public class BreedType
    {
        public int ID { get; set; }
        public int DogID { get; set; }
        public string Type { get; set; }

        public virtual Dog Dog { get; set; }
    }
}