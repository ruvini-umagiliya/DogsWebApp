using DogsWebApp.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DogsWebApp.DAL
{
    public class DogsInitializer : System.Data.Entity.DropCreateDatabaseAlways<DogsContext>
    {
        protected override void Seed(DogsContext context)
        {
            // Look for any dogs.
            if (context.Dogs.Any())
            {
                return;   // DB has been seeded
            }

            var dogs = getDataFromJsonString();
            dogs.ForEach(d => context.Dogs.Add(d));
            context.SaveChanges();
        }

        /// <summary>
        /// Read the list of dogs from the json given and create a list of Dog objects
        /// </summary>
        /// <returns>The list of Dog objects created from the data</returns>
        private List<Dog> getDataFromJsonString()
        {
            // the list sent with the document, " has been replaced with \"
            string json = "{\"affenpinscher\": [],\"african\": [],\"airedale\": [],\"akita\": [],\"appenzeller\": [],\"basenji\": [],\"beagle\": [],\"bluetick\": [],\"borzoi\": [],\"bouvier\": [],\"boxer\": [],\"brabancon\": [],\"briard\": [],\"bulldog\": [\"boston\", \"french\"],\"bullterrier\": [\"staffordshire\"],\"cairn\": [],\"chihuahua\": [],\"chow\": [],\"clumber\": [],\"collie\": [\"border\"],\"coonhound\": [],\"corgi\": [\"cardigan\"],\"dachshund\": [],\"dane\": [\"great\"],\"deerhound\": [\"scottish\"],\"dhole\": [],\"dingo\": [],\"doberman\": [],\"elkhound\": [\"norwegian\"],\"entlebucher\": [],\"eskimo\": [],\"germanshepherd\": [],\"greyhound\": [\"italian\"],\"groenendael\": [],\"hound\": [\"Ibizan\", \"afghan\", \"basset\", \"blood\", \"english\", \"walker\"],\"husky\": [],\"keeshond\": [],\"kelpie\": [],\"komondor\": [],\"kuvasz\": [],\"labrador\": [],\"leonberg\": [],\"lhasa\": [],\"malamute\": [],\"malinois\": [],\"maltese\": [],\"mastiff\": [\"bull\", \"tibetan\"],\"mexicanhairless\": [],\"mountain\": [\"bernese\", \"swiss\"],\"newfoundland\": [],\"otterhound\": [],\"papillon\": [],\"pekinese\": [],\"pembroke\": [],\"pinscher\": [\"miniature\"],\"pointer\": [\"german\"],\"pomeranian\": [],\"poodle\": [\"miniature\", \"standard\", \"toy\"],\"pug\": [],\"pyrenees\": [],\"redbone\": [],\"retriever\": [\"chesapeake\", \"curly\", \"flatcoated\", \"golden\"],\"ridgeback\": [\"rhodesian\"],\"rottweiler\": [],\"saluki\": [],\"samoyed\": [],\"schipperke\": [],\"schnauzer\": [\"giant\", \"miniature\"],\"setter\": [\"english\", \"gordon\", \"irish\"],\"sheepdog\": [\"english\", \"shetland\"],\"shiba\": [],\"shihtzu\": [],\"spaniel\": [\"blenheim\", \"brittany\", \"cocker\", \"irish\", \"japanese\", \"sussex\", \"welsh\"],\"springer\": [\"english\"],\"stbernard\": [],\"terrier\": [  \"american\",  \"australian\",  \"bedlington\",  \"border\",  \"dandie\",  \"fox\",  \"irish\",  \"kerryblue\",  \"lakeland\",  \"norfolk\",  \"norwich\",  \"patterdale\",  \"scottish\",  \"sealyham\",  \"silky\",  \"tibetan\",  \"toy\",  \"westhighland\",  \"wheaten\",  \"yorkshire\"],\"vizsla\": [],\"weimaraner\": [],\"whippet\": [],\"wolfhound\": [\"irish\"] } ";
            JObject jObject = JObject.Parse(json);

            List<Dog> dogs = new List<Dog>();

            // go through all the different dog breeds and add them to the list
            foreach (JToken dogJT in jObject.Children())
            {
                Dog dog = new Dog();

                // gets the dog breed
                dog.Breed = dogJT.Path;
                dog.Types = new List<BreedType>();

                // check if any gos types are given for the specific breed
                if (dogJT.Children().FirstOrDefault().HasValues)
                {
                    // add each sub type to the breed types list
                    foreach (var child in dogJT.Children().FirstOrDefault().Children())
                    {
                        BreedType type = new BreedType();
                        type.Type = child.ToString();
                        dog.Types.Add(type);
                    }
                }

                dogs.Add(dog);
            }

            return dogs;
        }
    }
}