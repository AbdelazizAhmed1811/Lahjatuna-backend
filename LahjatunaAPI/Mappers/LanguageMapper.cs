using LahjatunaAPI.Dtos.Language;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Mappers
{
    public static class LanguageMapper
    {
        public static LanguageDto ToLanguageDto(this Language language)
        {
            return new LanguageDto
            {
                LanguageId = language.LanguageId,
                LanguageName = language.LanguageName,
                LanguageCode = language.LanguageCode,
                Script = language.Script
            };
        }
    }
}
