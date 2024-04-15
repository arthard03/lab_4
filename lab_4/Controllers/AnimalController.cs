using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;

namespace lab4.ControllerAnimal
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
       public static List<AnimalShelter> _AnimalShelters = new()
        {
            new AnimalShelter{IdAnimal =1,AnimalName = "Joseppe", AnimalType= "cat",  AnimalWeight = 9.5,AnimalColor = "Orange"},
            new AnimalShelter{IdAnimal =2,AnimalName = "Penguin",AnimalType = "dog", AnimalWeight= 20,AnimalColor = "Grey"},
            new AnimalShelter{IdAnimal =3,AnimalName = "Armageddon",AnimalType = "dog", AnimalWeight= 50,AnimalColor = "dark"}

        };

        [HttpGet]
        public IActionResult GetAnimals() => Ok(_AnimalShelters);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var animal = _AnimalShelters.FirstOrDefault(a => a.IdAnimal == id);
            if (animal == null) return NotFound("Animal with that id not found");
            return Ok(animal);
        }

        [HttpPost]
        public IActionResult Post(AnimalShelter animal)
        {
            _AnimalShelters.Add(animal);
            return CreatedAtAction(nameof(Get), new { id = animal.IdAnimal }, animal);
        }
  
   

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, AnimalShelter animalShelter)
        {
            var animalToId = _AnimalShelters.FindIndex(a => a.IdAnimal == id);
            if (animalToId==-1 ) return NotFound("Animal with that id not found");
            _AnimalShelters[animalToId] = animalShelter;
            return NoContent();
        }
        
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = _AnimalShelters.FindIndex(a => a.IdAnimal == id);
            if (index == -1) return NotFound("Animal with that id not found");
            _AnimalShelters.RemoveAt(index);
            return NoContent();
        }
    }
}