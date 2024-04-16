using APBD05.DataBase;
using APBD05.Models;

namespace APBD05.Endpoints
{
    public static class AnimalEndpoints
    {
        public static void MapAnimalsEndpoints(this WebApplication app)
        {
            app.MapGet("/animals", GetAllAnimals);
            app.MapGet("/animals/{id:int}", GetAnimalById);
            app.MapPost("/animals", AddAnimal);
            app.MapPut("/animals/{id:int}", UpdateAnimal);
            app.MapDelete("/animals/{id:int}", DeleteAnimal);
        }

        private static IResult GetAllAnimals(HttpContext context)
        {
            return Results.Ok(StaticDb.Animals);
        }

        private static IResult GetAnimalById(HttpContext context)
        {
            int id = int.Parse(context.Request.RouteValues["id"].ToString());
            var animal = StaticDb.Animals.FirstOrDefault(a => a.Id == id);
            return animal == null ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(animal);
        }

        private static IResult AddAnimal(HttpContext context)
        {
            var animal = context.Request.ReadFromJsonAsync<Animal>().Result;
            var animalExist = StaticDb.Animals.FirstOrDefault(a => a.Id == animal.Id);
            if (animalExist != null)
            {
                return Results.Conflict($"Animal with id {animal.Id} already exists");
            }
            StaticDb.Animals.Add(animal);
            return Results.Created("", animal);
        }

        private static IResult UpdateAnimal(HttpContext context)
        {
            int id = int.Parse(context.Request.RouteValues["id"].ToString());
            var animalToEdit = StaticDb.Animals.FirstOrDefault(a => a.Id == id);
            if (animalToEdit == null)
            {
                return Results.NotFound($"Animal with id {id} was not found");
            }

            var animal = context.Request.ReadFromJsonAsync<Animal>().Result;
            StaticDb.Animals.Remove(animalToEdit);
            StaticDb.Animals.Add(animal);
            return Results.NoContent();
        }

        private static IResult DeleteAnimal(HttpContext context)
        {
            int id = int.Parse(context.Request.RouteValues["id"].ToString());
            var animal = StaticDb.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return Results.NotFound($"Animal with id {id} was not found");
            }

            StaticDb.Animals.Remove(animal);
            return Results.NoContent();
        }
    }
}
