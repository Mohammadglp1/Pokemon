using Microsoft.Extensions.Options;
using RestEase;
using ThePokemonProject.Interfaces;

namespace ThePokemonProject;

public class RestClientConfig
{
    private readonly MyApiSettings _settings;

    public RestClientConfig(IOptions<MyApiSettings> options)
    {
        _settings = options.Value;
    }

   
    public IPokemonApi CreateClient()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_settings.BaseUrl),
            Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds)
        };

        httpClient.DefaultRequestHeaders.Add("User-Agent", _settings.UserAgent);

        return RestClient.For<IPokemonApi>(httpClient);
    }}