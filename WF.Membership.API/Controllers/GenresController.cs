using Microsoft.AspNetCore.Mvc;
using WF.Common.DTOs;
using WF.Membership.Database.Entities;
using WF.Membership.Database.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WF.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IDbService _db;

        public GenresController(IDbService db)
        {
            this._db = db;
        }
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IResult> Get()
        {


            try
            {

                


                
                    var genres = await _db.GetAsync<Genre, GenreDTO>();
                    return Results.Ok(genres);
               


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
                _db.Include<Film>();
                


                var genre = await _db.SingleAsync<Genre, GenreDTO>(g => g.Id.Equals(id));
                return Results.Ok(genre);
            }
            catch
            {
                return Results.NotFound();
            }
        }

        

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
        {
            try
            {
                if (dto is null) return Results.BadRequest();

                var genre = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
                var success = await _db.SaveChangesAsync();

                if (success.Equals(false)) return Results.BadRequest();

                return Results.Created(_db.GetURI<Genre>(genre), genre);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
        {
            try
            {
                if (dto is null || dto.Id != id) return Results.BadRequest();

                //if (!await _db.AnyAsync<Director>(i => i.Id == dto.DirectorId)) return Results.NotFound();

                //if (!await _db.AnyAsync<Film>(f => f.Id == id)) return Results.NotFound();

                _db.Update<Genre, GenreEditDTO>(id, dto);

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
                var success = await _db.DeleteAsync<Genre>(id);

                await _db.DeleteAsyncFilmGenreReference(id, false);

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
}
