using Microsoft.Extensions.Options;
using PosTechFiapWepApp.Models;
using PosTechFiapWepApp.Util;
using System.Text.Json;

#nullable disable

namespace PosTechFiapWepApp.Services
{
    public class ImagensService
    {
        private readonly HttpClient _httpClient;

        public ApiImagensConfig ApiImagensConfig { get; }

        public ImagensService(HttpClient httpClient, IOptions<ApiImagensConfig> apiImagensConfig)
        {
            _httpClient = httpClient;
            ApiImagensConfig = apiImagensConfig.Value;
        }

        private async Task AutenticarJwtAsync()
        {
            HttpResponseMessage resposta = await _httpClient.PostAsync($"{ApiImagensConfig.GerarTokenAsync}{ApiImagensConfig.ChaveSeguranca}", null);

            TokenResponseModel token;

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    token = JsonSerializer.Deserialize<TokenResponseModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    break;
                default:
                    token = new();
                    break;
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        }

        public async Task<ImagemResponseViewModel> EfetuarUploadAsync(ImageRequestViewModel model)
        {
            await AutenticarJwtAsync();

            string json = JsonSerializer.Serialize(model);

            HttpRequestMessage request = new(HttpMethod.Post, ApiImagensConfig.EfetuarUploadAsync)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            HttpResponseMessage resposta = await _httpClient.SendAsync(request);

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json1 = await resposta.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ImagemResponseViewModel>(json1, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                default:
                    return new();
            }
        }

        public async Task<List<ImagemResponseViewModel>> PesquisarTodasImagensAsync()
        {
            await AutenticarJwtAsync();

            HttpResponseMessage resposta = await _httpClient.GetAsync(ApiImagensConfig.PesquisarTodasImagens);

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ImagemResponseViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                default:
                    return new();
            }
        }

        public async Task ApagarImagemPorNomeAsync(string nome)
        {
            await AutenticarJwtAsync();

            string uri = $"{ApiImagensConfig.ApagarImagemPorNome}/{nome}";

            _ = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri));
        }

        public async Task<List<ImagemResponseViewModel>> PesquisarImagensDeletadasAsync()
        {
            await AutenticarJwtAsync();

            HttpResponseMessage resposta = await _httpClient.GetAsync(ApiImagensConfig.PesquisarImagensDeletadas);

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ImagemResponseViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                default:
                    return new();
            }
        }

        public async Task RecuperarImagemPorNomeAsync(string nome)
        {
            await AutenticarJwtAsync();

            string uri = $"{ApiImagensConfig.RecuperarImagemPorNome}/{nome}";

            _ = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, uri));
        }

        public async Task<List<ImagemResponseViewModel>> PesquisarImagemPorNomeAsync(string nome)
        {
            await AutenticarJwtAsync();

            string uri = $"{ApiImagensConfig.PesquisarImagemPorNome}/{nome}";

            HttpResponseMessage resposta = await _httpClient.GetAsync(uri);

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    ImagemResponseViewModel imagem = JsonSerializer.Deserialize<ImagemResponseViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new List<ImagemResponseViewModel> { imagem };
                default:
                    return new();
            }
        }
    }
}
