using APBD05.DataBase;
using APBD05.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD05.Controllers;

[Route("[controller]")]
[ApiController]
public class VisitsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateVisit(Visit visit)
    {
        StaticDb.Visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("animals/{id:int}")]
    public IActionResult GetVisitsAssociatedWith(int id)
    {
        List<Visit> visits = StaticDb.Visits.Where(v => v.Animal.Id == id).ToList();
        if (visits.Count == 0)
            return NotFound($"Visits for animal with id {id} was not found");
        return Ok(visits);
    }
}