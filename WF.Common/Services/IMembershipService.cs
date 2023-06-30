namespace WF.Common.Services
{
    public interface IMembershipService
    {
        Task<FilmDTO>  GetFilmAsync(int id);
        Task<List<FilmDTO>> GetFilmsAsync();
    }
}