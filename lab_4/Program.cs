using System.Runtime.InteropServices.JavaScript;
using lab_4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var _animals = new List<AnimalShelter>
{
    new AnimalShelter
        { IdAnimal = 1, AnimalName = "Mark", AnimalType = "cat-Maine-Coone", AnimalWeight = 9.4, AnimalColor = "grey" },
    new AnimalShelter
        { IdAnimal = 2, AnimalName = "Boby", AnimalType = "dog-Retrivier", AnimalWeight = 23.53, AnimalColor = "gold" },
    new AnimalShelter
        { IdAnimal = 3, AnimalName = "Jossepa", AnimalType = "cat-Britain", AnimalWeight = 6.7, AnimalColor = "black" }
};

var _visits = new List<Visits>
{
    new Visits { Date = DateTime.Now, Visistdesc = "Vaccinations", Visitprice = 250.50, AnimalShelter = _animals[0] },
    new Visits { Date = DateTime.Today, Visistdesc = "Weeekly checkup", Visitprice = 150.5, AnimalShelter = _animals[1] },
    new Visits { Date = DateTime.Now, Visistdesc = "Grooming", Visitprice = 320, AnimalShelter = _animals[2] }
};
app.MapGet("/api/getAllVisits", () => Results.Ok(_visits)).WithName("GetVisits").WithOpenApi();

app.MapGet("/api/getvisitsofAnimal/{id:int}", (int id) =>
{
    var visit = _visits.FirstOrDefault(s => s.AnimalShelter.IdAnimal == id);
    return visit == null ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(visit);
}).WithName("GetVisitAnimal").WithOpenApi();

app.MapPost("/api/addVisit", (int id ,Visits Visits) =>
{
    var visitToId = _visits.FirstOrDefault(s => s.AnimalShelter.IdAnimal == id);
    if (visitToId == null)
    {
        return Results.NotFound($"Animal with id {id} was not found, could not assign visit");
    }
    _visits.Add(Visits);
    return Results.StatusCode(StatusCodes.Status201Created);
}).WithName("CreateVisit").WithOpenApi();



app.MapGet("/api/getAllAnimals", () => Results.Ok(_animals)).WithName("GetAnimals").WithOpenApi();

app.MapGet("/api/getAnimals/{id:int}", (int id) =>
    {
        var animal = _animals.FirstOrDefault(s => s.IdAnimal == id);
        return animal == null ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(animal);
    }).WithName("GetAnimal").WithOpenApi();


app.MapPost("/api/addAnimal", (AnimalShelter animalShelter) =>
    {
        _animals.Add(animalShelter);
        return Results.StatusCode(StatusCodes.Status201Created);
    }).WithName("CreateAnimal").WithOpenApi();


app.MapPut("/api/updateAnimal/{id:int}", (int id, AnimalShelter animalShelter) =>
    {
        var animalToId = _animals.FirstOrDefault(s => s.IdAnimal == id);
        if (animalToId == null)
        {
            return Results.NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToId);
        _animals.Add(animalShelter);
        return Results.NoContent();
    }).WithName("UpdateAnimal").WithOpenApi();


app.MapDelete("/api/deleteAnimal/{id:int}", (int id) =>
{
    var AnimaltoDelete = _animals.FirstOrDefault(s => s.IdAnimal == id);
    if (AnimaltoDelete == null)
    {
        return Results.NoContent();
    }

    _animals.Remove(AnimaltoDelete);
    return Results.NoContent();
});

app.UseAuthorization();
app.MapControllers();


app.Run();