using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LahjatunaAPI.Models;

public partial class User: IdentityUser
{
    //public string? Id { get; set; }

    //public string Username { get; set; } = null!;

    //public string Email { get; set; } = null!;

    //public string Password { get; set; } = null!;

    //public string? Role { get; set; }

    public int? TranslationsCount { get; set; }

    public int? FeedbackCount { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<TranslationLog> TranslationLogs { get; set; } = new List<TranslationLog>();
}
