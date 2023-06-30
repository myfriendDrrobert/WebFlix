using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WF.Membership.Database.Entities;

namespace WF.Membership.Database.Services;

public interface IDbService
{
    Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity where TDto : class;

    Task<TDto> SingleAsync<Tentity, TDto>(Expression<Func<Tentity, bool>> expression) where Tentity : class, IEntity where TDto : class;

    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    where TEntity : class, IEntity;

    Task<bool> SaveChangesAsync();

    Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class;

    void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity where TDto : class;

    Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
    void Include<TEntity>() where TEntity : class, IEntity;
    Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class;
    string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
    string GetURIRef<TReferenceEntity>(TReferenceEntity entity) where TReferenceEntity : class, IEntity;
    
    void IncludeReference<TEntity>() where TEntity : class, IReferenceEntity;
    
    
    void AddFilmGenre(int filmId, int GenreId);
    Task<bool> DeleteAsyncSimilarFilmReference<TEntity>(int id) where TEntity : class, IReferenceEntity;
   
    Task<bool> DeleteAsyncFilmGenreReference(int id, bool isFilm);
    Task DeleteFilmsSimilarFilms(int id);
    Task<TEntity> AddAsyncReference<TEntity, TDto>(TDto dto)
        where TEntity : class, IReferenceEntity
        where TDto : class;
}
