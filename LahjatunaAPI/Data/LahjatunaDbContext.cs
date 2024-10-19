using LahjatunaAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Data;

public partial class LahjatunaDbContext : IdentityDbContext<User>
{
    public LahjatunaDbContext()
    {
    }

    public LahjatunaDbContext(DbContextOptions<LahjatunaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<TranslationLog> TranslationLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        List<IdentityRole> roles = new List<IdentityRole>
           {
               new IdentityRole
               {
                   Name = "admin",
                   NormalizedName = "ADMIN"
               },
               new IdentityRole
               {
                   Name = "user",
                   NormalizedName = "USER"
               }
           };

        modelBuilder.Entity<IdentityRole>().HasData(roles);


        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("favorites_pkey");

            entity.ToTable("favorites");

            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.TranslationLogId).HasColumnName("translation_log_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.TranslationLog).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.TranslationLogId)
                .HasConstraintName("favorites_translation_log_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("favorites_user_id_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TranslationLogId).HasColumnName("translation_log_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.TranslationLog).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TranslationLogId)
                .HasConstraintName("feedback_translation_log_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("feedback_user_id_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("languages_pkey");

            entity.ToTable("languages");

            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(50)
                .HasColumnName("language_code");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(255)
                .HasColumnName("language_name");
            entity.Property(e => e.Script)
                .HasMaxLength(100)
                .HasColumnName("script");
        });

        modelBuilder.Entity<TranslationLog>(entity =>
        {
            entity.HasKey(e => e.TranslationLogId).HasName("translation_logs_pkey");

            entity.ToTable("translation_logs");

            entity.Property(e => e.TranslationLogId).HasColumnName("translation_log_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SourceLanguageId).HasColumnName("source_language_id");
            entity.Property(e => e.SourceText).HasColumnName("source_text");
            entity.Property(e => e.TargetLanguageId).HasColumnName("target_language_id");
            entity.Property(e => e.TargetText).HasColumnName("target_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SourceLanguage).WithMany(p => p.TranslationLogSourceLanguages)
                .HasForeignKey(d => d.SourceLanguageId)
                .HasConstraintName("translation_logs_source_language_id_fkey");

            entity.HasOne(d => d.TargetLanguage).WithMany(p => p.TranslationLogTargetLanguages)
                .HasForeignKey(d => d.TargetLanguageId)
                .HasConstraintName("translation_logs_target_language_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TranslationLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("translation_logs_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FeedbackCount)
                .HasDefaultValue(0)
                .HasColumnName("feedback_count");
            entity.Property(e => e.TranslationsCount)
                .HasDefaultValue(0)
                .HasColumnName("translations_count");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
