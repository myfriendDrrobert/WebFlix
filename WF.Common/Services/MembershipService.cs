


using System;

namespace WF.Common.Services;

public class MembershipService : IMembershipService
{
    private readonly MembershipHttpClient _http;

    public MembershipService(MembershipHttpClient http)
    {
        _http = http;
    }

    public async Task<List<FilmDTO>> GetFilmsAsync()
    {
        try
        {
            bool freeOnly = false;

            using HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<FilmDTO>();

            

        }
        catch
        {
            return new List<FilmDTO>();
        }
    }

    public async Task<FilmDTO> GetFilmAsync(int id)
    {
        try
        {
            if (id.Equals(null)) return new FilmDTO();

            using HttpResponseMessage response = await _http.Client.GetAsync($"Films/{id}"
);

            var courseResponse = response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmDTO>(await
            courseResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result is null) return new FilmDTO();

            return result;

        }
        catch
        {
            return new FilmDTO();
        }
    }
}

