using AutoMapper;
using WF.Common.DTOs;
using Microsoft.EntityFrameworkCore;
using WF.Membership.Database.Contexts;
using WF.Membership.Database.Entities;
using WF.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(policy => {
policy.AddPolicy("CorsAllAccessPolicy", opt =>
opt.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WFContext>(
options => options.UseSqlServer(
builder.Configuration.GetConnectionString("WFConnection")));

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Director, DirectorDTO>().ReverseMap();
    cfg.CreateMap<Film, FilmDTO>().ReverseMap().ForMember(dest => dest.SimilarFilms, src => src.Ignore());
    cfg.CreateMap<Genre, GenreDTO>()./*ForMember(dest => dest.Name, src => src.MapFrom(s => s.Name)).*/ReverseMap();
    cfg.CreateMap<SimilarFilm, SimilarReadFilmsDTO>().ForMember(dest => dest.Similar, src => src
    .MapFrom(s => s.Similar.Title)).ReverseMap();
    cfg.CreateMap<FilmGenre,
    FilmGenreDTO>().ReverseMap();
    cfg.CreateMap<SimilarFilm, SimilarFilmsDTO>().ForMember(dest => dest.SimilarFilm, src => src
    .MapFrom(s => s.Similar)).ForMember(dest => dest.Film, src => src
    .MapFrom(s => s.Film.Title))
    .ReverseMap().ForMember(dest => dest.Similar, src => src.Ignore()).ForMember(dest => dest.Film, src => src.Ignore());


    
    cfg.CreateMap<FilmCreateDTO, Film>().ForMember(dest => dest.Genres, src => src.Ignore()).ForMember(dest => dest.SimilarFilms, src => src.Ignore());
    cfg.CreateMap<FilmEditDTO, Film>().ForMember(dest => dest.Genres, src => src.Ignore());
    cfg.CreateMap<DirectorCreateDTO, Director>();
    cfg.CreateMap<DirectorEditDTO, Director>();
    cfg.CreateMap<SimilarFilm, SimilarFilmsCreateDTO>().ForMember(dest => dest.FilmId, src => src
    .MapFrom(s => s.FilmId)).ForMember(dest => dest.SimilarFilmId, src => src.MapFrom(s => s.SimilarFilmId)).ReverseMap();
    cfg.CreateMap<GenreCreateDTO, Genre>();
    cfg.CreateMap<GenreEditDTO, Genre>();
    cfg.CreateMap<Film, FilmReadDTO>().ReverseMap();




});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
RegisterServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsAllAccessPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IDbService, DbService>();
}
