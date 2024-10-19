using System;
using System.Collections.Generic;

namespace LahjatunaAPI.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? TranslationLogId { get; set; }

    public string? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TranslationLog? TranslationLog { get; set; }

    public virtual User? User { get; set; }
}
