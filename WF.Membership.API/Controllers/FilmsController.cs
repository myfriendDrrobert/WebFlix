using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using WF.Membership.Database.Services;
using WF.Membership.Database.Entities;
using WF.Common.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WF.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService db)
        {
            this._db = db;
        }
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IResult> Get(bool freeOnly)
        {


            try
            {            
                if (freeOnly)
                {

                    _db.Include<Film>();
                    _db.IncludeReference<FilmGenre>();
                    _db.IncludeReference<SimilarFilm>();


                    var films = await _db.GetAsync<Film, FilmDTO>(f => f.Free.Equals(true));
                    return Results.Ok(films);
                }
                else
                {

                    _db.Include<Film>();
                    _db.IncludeReference<FilmGenre>();
                    _db.Include<Genre>();

                    var films = await _db.GetAsync<Film, FilmDTO>();
                    return Results.Ok(films);
                }


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
                _db.IncludeReference<FilmGenre>();
                _db.IncludeReference<SimilarFilm>();
                


                var film = await _db.SingleAsync<Film, FilmDTO>(c => c.Id.Equals(id));
                return Results.Ok(film);
            }
            catch
            {
                return Results.NotFound();
            }
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
        {
            
            try
            {
                if (dto is null) return Results.BadRequest();

               

                var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);

                
                var success = await _db.SaveChangesAsync();
                
                if (success.Equals(false)) return Results.BadRequest();
                var test = _db.GetURI<Film>(film);
                if (dto.GenreIds is not null && dto.GenreIds.Length > 0)
                {
                    foreach (var id in dto.GenreIds)
                    {
                        FilmGenreDTO genreToAdd = new(film.Id, id);

                        await _db.AddAsync<FilmGenre, FilmGenreDTO>(genreToAdd);
                    }
                   


                }
                if (dto.SimilarIds is not null && dto.SimilarIds.Length > 0)
                {


                    foreach (var similarId in dto.SimilarIds)
                    {
                        SimilarFilmsCreateDTO toAdd = new() { FilmId = film.Id, SimilarFilmId = similarId };

                        await _db.AddAsyncReference<SimilarFilm, SimilarFilmsCreateDTO>(toAdd);
                    }



                }

                await _db.SaveChangesAsync();

                

                return Results.Created(test, film);
                
            }
            catch
            {
                
            }

            return Results.BadRequest();
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
        {
            try
            {
                if (dto is null || dto.Id != id) return Results.BadRequest();

                if (!await _db.AnyAsync<Director>(i => i.Id == dto.DirectorId)) return Results.NotFound();

                if (!await _db.AnyAsync<Film>(f => f.Id == id)) return Results.NotFound();

                if (dto.GenreIds is not null && dto.GenreIds.Length > 0)
                {
                    await _db.DeleteAsyncFilmGenreReference(id, true);

                    foreach (var genreid in dto.GenreIds)
                    {
                        FilmGenreDTO genreToAdd = new(id, genreid);

                        await _db.AddAsync<FilmGenre, FilmGenreDTO>(genreToAdd);
                    }


                }
                if (dto.SimilarIds is not null && dto.SimilarIds.Length > 0)
                {
                    await _db.DeleteFilmsSimilarFilms(id);

                    foreach (var similarId in dto.SimilarIds)
                    {
                        SimilarFilmsDTO toAdd = new() { FilmId = id, SimilarFilmId = similarId };

                        await _db.AddAsyncReference<SimilarFilm, SimilarFilmsDTO>(toAdd);
                    }

                    await _db.SaveChangesAsync();
                }

                _db.Update<Film, FilmEditDTO>(id, dto);

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
                var success = await _db.DeleteAsync<Film>(id);

                await _db.DeleteAsyncFilmGenreReference(id, true);

                await _db.DeleteAsyncSimilarFilmReference<SimilarFilm>(id);

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
