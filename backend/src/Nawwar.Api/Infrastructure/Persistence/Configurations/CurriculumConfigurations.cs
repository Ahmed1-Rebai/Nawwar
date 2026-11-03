using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Infrastructure.Persistence.Configurations;

public sealed class CurriculumConfigurations :
    IEntityTypeConfiguration<AgeGroup>,
    IEntityTypeConfiguration<LearningDomain>,
    IEntityTypeConfiguration<CourseCollection>,
    IEntityTypeConfiguration<Course>,
    IEntityTypeConfiguration<LearningObjective>,
    IEntityTypeConfiguration<Lesson>,
    IEntityTypeConfiguration<Activity>,
    IEntityTypeConfiguration<ActivityCompletion>
{
    public void Configure(EntityTypeBuilder<AgeGroup> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("age_groups");
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.AgeYears).IsUnique();
        builder.Property(a => a.Slug).HasMaxLength(80).IsRequired();
        builder.HasIndex(a => a.Slug).IsUnique();

        builder.Property(a => a.ThemeKey).HasMaxLength(40).IsRequired();
        builder.Property(a => a.IconEmoji).HasMaxLength(16).IsRequired();

        builder.Property(a => a.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(a => a.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(a => a.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(a => a.SubtitleAr).HasMaxLength(300).IsRequired();
        builder.Property(a => a.SubtitleFr).HasMaxLength(300).IsRequired();
        builder.Property(a => a.SubtitleEn).HasMaxLength(300).IsRequired();

        builder.Property(a => a.DescriptionAr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.DescriptionFr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.DescriptionEn).HasMaxLength(800).IsRequired();

        builder.HasMany(a => a.Domains)
            .WithOne(d => d.AgeGroup)
            .HasForeignKey(d => d.AgeGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<LearningDomain> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("learning_domains");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(d => d.Slug).IsUnique();
        builder.Property(d => d.IconEmoji).HasMaxLength(16).IsRequired();
        builder.Property(d => d.AccentColor).HasMaxLength(32).IsRequired();

        builder.Property(d => d.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(d => d.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(d => d.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(d => d.SummaryAr).HasMaxLength(600).IsRequired();
        builder.Property(d => d.SummaryFr).HasMaxLength(600).IsRequired();
        builder.Property(d => d.SummaryEn).HasMaxLength(600).IsRequired();

        builder.HasMany(d => d.Collections)
            .WithOne(c => c.Domain)
            .HasForeignKey(c => c.DomainId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CourseCollection> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("course_collections");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(c => c.SummaryAr).HasMaxLength(600).IsRequired();
        builder.Property(c => c.SummaryFr).HasMaxLength(600).IsRequired();
        builder.Property(c => c.SummaryEn).HasMaxLength(600).IsRequired();

        builder.HasMany(c => c.Courses)
            .WithOne(co => co.Collection)
            .HasForeignKey(co => co.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("courses");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.Property(c => c.IconEmoji).HasMaxLength(16).IsRequired();

        builder.Property(c => c.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(c => c.DescriptionAr).HasMaxLength(800).IsRequired();
        builder.Property(c => c.DescriptionFr).HasMaxLength(800).IsRequired();
        builder.Property(c => c.DescriptionEn).HasMaxLength(800).IsRequired();

        builder.Property(c => c.CompletionCriteriaAr).HasMaxLength(400).IsRequired();
        builder.Property(c => c.CompletionCriteriaFr).HasMaxLength(400).IsRequired();
        builder.Property(c => c.CompletionCriteriaEn).HasMaxLength(400).IsRequired();

        builder.HasMany(c => c.Objectives)
            .WithOne(o => o.Course)
            .HasForeignKey(o => o.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<LearningObjective> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("learning_objectives");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Code).HasMaxLength(40).IsRequired();
        builder.HasIndex(o => new { o.CourseId, o.Code }).IsUnique();

        builder.Property(o => o.DescriptionAr).HasMaxLength(600).IsRequired();
        builder.Property(o => o.DescriptionFr).HasMaxLength(600).IsRequired();
        builder.Property(o => o.DescriptionEn).HasMaxLength(600).IsRequired();
    }

    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("lessons");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(l => l.Slug).IsUnique();

        builder.Property(l => l.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(l => l.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(l => l.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(l => l.SummaryAr).HasMaxLength(600).IsRequired();
        builder.Property(l => l.SummaryFr).HasMaxLength(600).IsRequired();
        builder.Property(l => l.SummaryEn).HasMaxLength(600).IsRequired();

        builder.HasMany(l => l.Activities)
            .WithOne(a => a.Lesson)
            .HasForeignKey(a => a.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("activities", t =>
            t.HasCheckConstraint(
                "ck_activities_glb_model_url",
                "glb_model_url IS NULL OR glb_model_url LIKE '%.glb'"));
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(a => a.Slug).IsUnique();
        builder.HasIndex(a => new { a.LessonId, a.StepOrder }).IsUnique();

        builder.Property(a => a.ActivityType).HasMaxLength(60).IsRequired();
        builder.Property(a => a.ObjectiveCode).HasMaxLength(40).IsRequired();

        builder.Property(a => a.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(a => a.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(a => a.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(a => a.PromptAr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.PromptFr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.PromptEn).HasMaxLength(800).IsRequired();

        builder.Property(a => a.SpokenTextAr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.SpokenTextFr).HasMaxLength(800).IsRequired();
        builder.Property(a => a.SpokenTextEn).HasMaxLength(800).IsRequired();

        builder.Property(a => a.GlbModelUrl).HasColumnName("glb_model_url").HasMaxLength(300);
        builder.Property(a => a.TargetNodeName).HasMaxLength(100);
        builder.Property(a => a.InteractionPayloadJson).IsRequired();
    }

    public void Configure(EntityTypeBuilder<ActivityCompletion> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("activity_completions");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.LearnerKey).HasColumnName("learner_key").HasMaxLength(80).IsRequired();
        builder.Property(c => c.AgeYears).HasColumnName("age_years").IsRequired();
        builder.Property(c => c.CourseSlug).HasColumnName("course_slug").HasMaxLength(100).IsRequired();
        builder.Property(c => c.LessonSlug).HasColumnName("lesson_slug").HasMaxLength(100).IsRequired();
        builder.Property(c => c.ActivitySlug).HasColumnName("activity_slug").HasMaxLength(100).IsRequired();
        builder.Property(c => c.CompletedAtUtc).HasColumnName("completed_at_utc").IsRequired();

        builder.HasIndex(c => new { c.LearnerKey, c.ActivitySlug })
            .HasDatabaseName("ix_activity_completions_learner_activity")
            .IsUnique();
    }
}
