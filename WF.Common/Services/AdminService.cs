using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.Common.HttpClients;

namespace WF.Common.Services;

public class AdminService : IAdminService
{
    private readonly MembershipHttpClient _http;

    public AdminService(MembershipHttpClient http)
    {
        this._http = http;
    }
    
    public async Task<List<TDto>> GetAsync<TDto>(string uri)
    {
        try
        {
            using HttpResponseMessage response = await _http.Client.GetAsync(uri);

            var courseResponse = response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<TDto>>(await
            courseResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result is null) return new List<TDto>();

            return result;

        }
        catch
        {
            throw;
        }
    }

    public async Task<TDto?> SingleAsync<TDto>(string uri)
    {
        try
        {
            using HttpResponseMessage response = await _http.Client.GetAsync(uri);

            var filmResponse = response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<TDto>(await
            filmResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result is null) return default;

            return result;

        }
        catch
        {
            throw;
        }
    }

    public async Task CreateAsync<TDto>(string uri, TDto dto)
    {
        try
        {
            using StringContent jsonContent = new(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");
            using HttpResponseMessage response = await _http.Client.PostAsync(uri, jsonContent);

            response.EnsureSuccessStatusCode();


        }
        catch 
        {
            throw;
        }
    }

    public async Task EditAsync<TDto>(string uri, TDto dto)
    {
        try
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await _http.Client.PutAsync(uri, jsonContent); 

            response.EnsureSuccessStatusCode();


        }
        catch(Exception ex)
        {
            throw;
        }
    }

    public async Task DeleteAsync<TDto>(string uri)
    {
        try
        {
            

            using HttpResponseMessage response = await _http.Client.DeleteAsync(uri);

            response.EnsureSuccessStatusCode();


        }
        catch (Exception ex)
        {
            throw;
        }
    }


}
