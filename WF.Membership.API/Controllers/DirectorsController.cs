using Microsoft.AspNetCore.Mvc;
using WF.Common.DTOs;
using WF.Membership.Database.Contexts;
using WF.Membership.Database.Entities;
using WF.Membership.Database.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WF.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorController : ControllerBase
{
    private readonly IDbService _db;

    public DirectorController(IDbService db)
    {
        this._db = db;
    }
    // GET: api/<DirectorController>
    [HttpGet]
    public async Task<IResult> Get(bool freeOnly)
    {   


        try
        {

            
            


         
                var director = await _db.GetAsync<Director, DirectorDTO>();
                return Results.Ok(director);
           


        }
        catch
        {
            return Results.NotFound();
        }



    }



    // GET api/<CoursesController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            


            var Director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(id));
            return Results.Ok(Director);
        }
        catch
        {
            return Results.NotFound();
        }
    }

    // POST api/<CoursesController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] DirectorCreateDTO dto)
    {
        try
        {
            if (dto is null) return Results.BadRequest();

            var director = await _db.AddAsync<Director, DirectorCreateDTO>(dto);
            var success = await _db.SaveChangesAsync();

            if (success.Equals(false)) return Results.BadRequest();

            return Results.Created(_db.GetURI<Director>(director), director);
        }
        catch
        {
            return Results.BadRequest();
        }
    }

    // PUT api/<CoursesController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] DirectorEditDTO dto)
    {
        try
        {
            if (dto is null || dto.Id != id) return Results.BadRequest();

            //if (!await _db.AnyAsync<Director>(i => i.Id == dto.Id)) return Results.NotFound();

            //if (!await _db.AnyAsync<Director>(f => f.Id == id)) return Results.NotFound();

            _db.Update<Director, DirectorEditDTO>(id, dto);

            var success = await _db.SaveChangesAsync();

            if (success) return Results.NoContent();

            return Results.BadRequest();
        }
        catch
        {
            return Results.BadRequest();
        }
    }

    // DELETE api/<CoursesController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var success = await _db.DeleteAsync<Director>(id);

            

            if (success.Equals(false)) return Results.NotFound();

            success = await _db.SaveChangesAsync();

            if (success) return Results.NoContent();

            return Results.BadRequest();
        }
        catch
        {
            return Results.BadRequest();
        }
    }
}
