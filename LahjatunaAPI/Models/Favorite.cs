using System;
using System.Collections.Generic;

namespace LahjatunaAPI.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int? TranslationLogId { get; set; }

    public string? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TranslationLog? TranslationLog { get; set; }

    public virtual User? User { get; set; }
}
