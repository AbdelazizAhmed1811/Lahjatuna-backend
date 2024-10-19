namespace LahjatunaAPI.Interfaces
{
    public interface ITranslationModelService
    {
        public Task<string> GetModelResponseAsync(string sourceText, string sourceLang, string targetLang);
    }
}
