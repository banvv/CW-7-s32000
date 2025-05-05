using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlClient.Exceptions;
using SqlClient.Models;
using SqlClient.Models.DTOs;
using SqlClient.Services;

namespace SqlClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAnimals()
    {
        return Ok(await dbService.GetAnimalsDetailsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimalById(
        [FromRoute] int id
    )
    {
        try
        {
            return Ok(await dbService.GetAnimalDetailsByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnimal(
        [FromBody] AnimalCreateDTO body
    )
    {
        var animal = await dbService.CreateAnimalAsync(body);
        return Created($"animals/{animal.Id}", animal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceAnimalById(
        [FromRoute] int id,
        [FromBody] AnimalCreateDTO body
    )
    {
        try
        {
            await dbService.ReplaceAnimalByIdAsync(id, body);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimalById(
        [FromRoute] int id
    )
    {
        try
        {
            await dbService.RemoveAnimalByIdAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}/visits")]
    public async Task<IActionResult> GetVisitsByAnimalId(
        [FromRoute] int id
    )
    {
        try
        {
            return Ok(await dbService.GetVisitsByAnimalIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("{id}/visits")]
    public async Task<IActionResult> AddVisit(
        [FromRoute] int id,
        [FromBody] VisitCreateDTO body
    )
    {
        try
        {
            var visit = await dbService.CreateVisitAsync(id, body);
            return Created($"visits/{visit.Id}", visit);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    //mine
    [HttpGet("trips")]
    public async Task<ActionResult> GetAllTrips()
    {
        try
        {
            return Ok(await dbService.GetTrips());
        }
        catch (System.Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("clients/{id}/trips")]
    public async Task<ActionResult> GetAllTripsByClientId(
                [FromRoute] int id
    )
    {
        try
        {
            return Ok(await dbService.GetTripsByClientId(id));
        }
        catch (System.Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("clients")]
    public async Task<IActionResult> AddClient(
            [FromRoute] int id,
            [FromBody] ClientCreateDTO body
        )
    {
        try
        {
            var client = await dbService.CreateClient(id, body);
            return Created($"clients/{client.Id}", client);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("clients/{id}/trips/{tripId}")]
    public async Task<IActionResult> AddClientToTrip(
            [FromRoute] int id,
            [FromRoute] int tripId
        )
    {
        try
        {
            await dbService.AddClientToTrip(id, tripId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("clients/{id}/trips/{tripId}")]
    public async Task<IActionResult> RemoveClientFromTrip(
            [FromRoute] int id,
            [FromRoute] int tripId
    )
    {
        try
        {
            await dbService.RemoveClientFromTrip(id, tripId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}


