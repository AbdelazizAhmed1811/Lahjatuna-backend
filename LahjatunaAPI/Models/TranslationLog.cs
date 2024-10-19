using System;
using System.Collections.Generic;

namespace LahjatunaAPI.Models;

public partial class TranslationLog
{
    public int TranslationLogId { get; set; }

    public int? SourceLanguageId { get; set; }

    public int? TargetLanguageId { get; set; }

    public string? UserId { get; set; }

    public string SourceText { get; set; } = null!;

    public string TargetText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Language? SourceLanguage { get; set; }

    public virtual Language? TargetLanguage { get; set; }

    public virtual User? User { get; set; }
}
