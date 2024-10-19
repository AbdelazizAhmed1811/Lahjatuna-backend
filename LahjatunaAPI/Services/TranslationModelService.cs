using LahjatunaAPI.Interfaces;
using Newtonsoft.Json;

namespace LahjatunaAPI.Services
{
    public class TranslationModelService : ITranslationModelService
    {
        private readonly HttpClient _httpClient;

        public TranslationModelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> GetModelResponseAsync(string sourceText, string sourceLang, string targetLang)
        {
            var requestUrl = $"https://api.mymemory.translated.net/get?q={sourceText}&langpair={sourceLang}|{targetLang}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var translationResult = JsonConvert.DeserializeObject<TranslationApiResponse>(responseContent);
            
                var translatedText = translationResult.responseData.translatedText;

                return translatedText;
            }

            return string.Empty;
        }
    }
}



public class TranslationApiResponse
{
    [JsonProperty("responseStatus")]
    public int ResponseStatus { get; set; }

    [JsonProperty("responseData")]
    public ResponseData responseData { get; set; }

    public string translatedText => responseData.translatedText;


}

public class ResponseData
{
    [JsonProperty("translatedText")]
    public string translatedText { get; set; }
}
