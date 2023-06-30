using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WF.Membership.Database.Contexts;
using WF.Membership.Database.Entities;

namespace WF.Membership.Database.Services;

public class DbService : IDbService
{
    private readonly WFContext _db;
    private readonly IMapper _mapper;

    public DbService(WFContext dbContext, IMapper mapper)
    {
        this._db = dbContext;
        this._mapper = mapper;
    }




    public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity where TDto : class
    {   
        
        var entities = await _db.Set<TEntity>().ToListAsync();

        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();

        return _mapper.Map<List<TDto>>(entities);
    }

    private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity =>
    await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entity = await SingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().AnyAsync(expression);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class
        where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        await _db.Set<TEntity>().AddAsync(entity);

        return entity;


    }



    public async Task<TEntity> AddAsyncReference<TEntity, TDto>(TDto dto)
        where TEntity : class, IReferenceEntity
        where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        await _db.Set<TEntity>().AddAsync(entity);

        return entity;


    }

    public void AddFilmGenre(int filmId, int genreId)
    {
       

            _db.FilmGenres.Add(new FilmGenre() { FilmId = filmId, GenreId = genreId });
        

    }

    public void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);

        entity.Id = id;

        _db.Set<TEntity>().Update(entity);
    }

    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await SingleAsync<TEntity>(en => en.Id.Equals(id));
            if (entity is null)
            {
                return false;
            }
            _db.Remove(entity);
        }
        catch
        {
            throw;
        }

        return true;
    }

    public async Task DeleteFilmsSimilarFilms(int id) {

        var similarFilmsEntities = new List<SimilarFilm>();

        try
        {
            similarFilmsEntities = await _db.SimilarFilms.Where(sf => sf.FilmId.Equals(id)).ToListAsync();
            
            foreach (var entity in similarFilmsEntities)
            {
                _db.Remove(entity);
            }            

        }
        catch
        {
            throw;
        }       
    }

    public async Task<bool> DeleteAsyncSimilarFilmReference<TEntity>(int id) where TEntity : class, IReferenceEntity
    {

        
        var similarFilmsEntities = new List<SimilarFilm>();
        var similarFilmsEntitiesReverse = new List<SimilarFilm>();
        try
        {            
                
                similarFilmsEntities = await _db.SimilarFilms.Where(sf => sf.FilmId.Equals(id)).ToListAsync();
                similarFilmsEntitiesReverse = await _db.SimilarFilms.Where(sf => sf.SimilarFilmId.Equals(id)).ToListAsync();
                       
            

            
            foreach (var entity in similarFilmsEntities)
            {
                _db.Remove(entity);
            }
            foreach (var entity in similarFilmsEntitiesReverse)
            {
                _db.Remove(entity);
            }

        }
        catch
        {
            throw;
        }

        return true;
    }

    public async Task<bool> DeleteAsyncFilmGenreReference(int id, bool isFilm) 
    {

        var entities = new List<FilmGenre>();
       
        try
        {
            if (isFilm)
            {
                entities = await _db.FilmGenres.Where(fg => fg.FilmId.Equals(id)).ToListAsync();
                
            }
            else
            {
                entities = await _db.FilmGenres.Where(fg => fg.GenreId.Equals(id)).ToListAsync();
            }



            foreach (var entity in entities)
            {
                _db.Remove(entity);
            }
            

        }
        catch
        {
            throw;
        }

        return true;
    }

    public void Include<TEntity>() where TEntity : class, IEntity
    {

        var propertyNames = _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

        if (propertyNames is null)
        {
            return;
        }

        foreach (var name in propertyNames)
        {
            _db.Set<TEntity>().Include(name).Load();
        }

    }

    public void IncludeReference<TEntity>() where TEntity : class, IReferenceEntity
    {

        var propertyNames = _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

        if (propertyNames is null)
        {
            return;
        }

        foreach (var name in propertyNames)
        {
            _db.Set<TEntity>().Include(name).Load();
        }

    }

    

    public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
        => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";

    public string GetURIRef<TReferenceEntity>(TReferenceEntity entity) where TReferenceEntity : class, IEntity
        => $"/{typeof(TReferenceEntity).Name.ToLower()}s/{entity.Id}";
}
