using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WF.Common.DTOs;
using WF.Membership.Database.Entities;

namespace WF.Membership.Database.Contexts;

public class WFContext : DbContext
{
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<SimilarFilm> SimilarFilms => Set<SimilarFilm>();
    public WFContext(DbContextOptions<WFContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e
            => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(builder);

        builder.Entity<FilmGenre>().HasKey(fg => new { fg.FilmId, fg.GenreId });
        builder.Entity<SimilarFilm>().HasKey(sf => new { sf.FilmId, sf.SimilarFilmId });
        //Seedata(builder);

        /* Configuring related tables for the Film table*/
        builder.Entity<Film>(entity =>
        {
            entity
                // For each film in the Film Entity,
                // reference relatred films in the SimilarFilms entity
                // with the ICollection<SimilarFilms>
                .HasMany(d => d.SimilarFilms)
                .WithOne(p => p.Film)
                .HasForeignKey(d => d.FilmId)
                // To prevent cycles or multiple cascade paths.
                // Neded when seeding similar films data.
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure a many-to-many realtionship between genres
            // and films using the FilmGenre connection entity.
            entity.HasMany(d => d.Genres)
                  .WithMany(p => p.Films)
                  .UsingEntity<FilmGenre>()
                  // Specify the table name for the connection table
                  // to avoid duplicate tables (FilmGenre and FilmGenres)
                  // in the database.
                  .ToTable("FilmGenres");
        });
        /* Seed Data */
        builder.Entity<Director>().HasData(
            new { Id = 1, Name = "Director Name" });

        builder.Entity<Film>().HasData(
            new { Id = 1, Title = "Spiderman", DirectorId = 1 },
            new { Id = 2, Title = "Batman", DirectorId = 1 },
            new { Id = 3, Title = "The Hulk", DirectorId = 1 });

        builder.Entity<SimilarFilm>().HasData(
            new SimilarFilm { FilmId = 1, SimilarFilmId = 2 },
            new SimilarFilm { FilmId = 1, SimilarFilmId = 3 });

        builder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Sci-Fi" });

        builder.Entity<FilmGenre>().HasData(
            new FilmGenre { FilmId = 1, GenreId = 1 },
            new FilmGenre { FilmId = 1, GenreId = 2 },
            new FilmGenre { FilmId = 2, GenreId = 1 },
            new FilmGenre { FilmId = 3, GenreId = 1 });
    }
}

