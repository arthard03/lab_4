using Microsoft.AspNetCore.Mvc;
using lab4.ControllerAnimal;

namespace lab4.ControllerVisit
{
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private static List<Visits> animalsVisitsList = new()
        {
            new Visits { Date = DateTime.Now, Visistdesc = "Vaccination", Visitprice = 250.50, AnimalShelter = AnimalsController._AnimalShelters[2] },
            new Visits { Date = DateTime.Today, Visistdesc = "Weekly checkup", Visitprice = 150.5, AnimalShelter = AnimalsController._AnimalShelters[0] },
            new Visits { Date = DateTime.Now, Visistdesc = "Grooming", Visitprice = 320, AnimalShelter = AnimalsController._AnimalShelters[1] }
        };
        [HttpGet]
        public IActionResult GetAnimals() => Ok(animalsVisitsList);
        
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var visitToId = animalsVisitsList.FirstOrDefault(a => a.AnimalShelter.IdAnimal == id);
            if (visitToId == null) return NotFound("Animal with that id not found, could not assign visit");
            return Ok(visitToId);
        }
        
        
        [HttpPost]
        public IActionResult Post(Visits visit)
        {
            animalsVisitsList.Add(visit);
            return CreatedAtAction(nameof(Get), new { id = visit.AnimalShelter.IdAnimal }, visit);
        }

    }
    
}