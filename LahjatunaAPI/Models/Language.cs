using System;
using System.Collections.Generic;

namespace LahjatunaAPI.Models;

public partial class Language
{
    public int LanguageId { get; set; }

    public string LanguageName { get; set; } = null!;

    public string LanguageCode { get; set; } = null!;

    public string? Script { get; set; }

    public virtual ICollection<TranslationLog> TranslationLogSourceLanguages { get; set; } = new List<TranslationLog>();

    public virtual ICollection<TranslationLog> TranslationLogTargetLanguages { get; set; } = new List<TranslationLog>();
}
