using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Infrastructure.Persistence.Configurations;

public sealed class CurriculumConfigurations :
    IEntityTypeConfiguration<LearningModule>,
    IEntityTypeConfiguration<Lesson>,
    IEntityTypeConfiguration<LessonCheckpoint>,
    IEntityTypeConfiguration<CheckpointCompletion>
{
    public void Configure(EntityTypeBuilder<LearningModule> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("learning_modules");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Slug).HasMaxLength(80).IsRequired();
        builder.HasIndex(m => m.Slug).IsUnique();

        builder.Property(m => m.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(m => m.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(m => m.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(m => m.SummaryAr).HasMaxLength(600).IsRequired();
        builder.Property(m => m.SummaryFr).HasMaxLength(600).IsRequired();
        builder.Property(m => m.SummaryEn).HasMaxLength(600).IsRequired();

        builder.HasMany(m => m.Lessons)
            .WithOne(l => l.Module)
            .HasForeignKey(l => l.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("lessons", t =>
            t.HasCheckConstraint("ck_lessons_model_url_glb", "model_url LIKE '%.glb'"));
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(l => l.Slug).IsUnique();

        builder.Property(l => l.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(l => l.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(l => l.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(l => l.ObjectiveAr).HasMaxLength(800).IsRequired();
        builder.Property(l => l.ObjectiveFr).HasMaxLength(800).IsRequired();
        builder.Property(l => l.ObjectiveEn).HasMaxLength(800).IsRequired();

        builder.Property(l => l.ModelUrl).HasColumnName("model_url").HasMaxLength(300).IsRequired();
        builder.Property(l => l.ModelFormat).HasMaxLength(64).IsRequired();

        builder.HasMany(l => l.Checkpoints)
            .WithOne(c => c.Lesson)
            .HasForeignKey(c => c.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<LessonCheckpoint> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("lesson_checkpoints");
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.LessonId, c.StepOrder }).IsUnique();
        builder.Property(c => c.TargetNodeName).HasMaxLength(100).IsRequired();

        builder.Property(c => c.TitleAr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleFr).HasMaxLength(200).IsRequired();
        builder.Property(c => c.TitleEn).HasMaxLength(200).IsRequired();

        builder.Property(c => c.ExplanationAr).HasMaxLength(1200).IsRequired();
        builder.Property(c => c.ExplanationFr).HasMaxLength(1200).IsRequired();
        builder.Property(c => c.ExplanationEn).HasMaxLength(1200).IsRequired();
    }

    public void Configure(EntityTypeBuilder<CheckpointCompletion> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("checkpoint_completions");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.LearnerKey).HasColumnName("learner_key").HasMaxLength(80).IsRequired();
        builder.Property(c => c.LessonSlug).HasColumnName("lesson_slug").HasMaxLength(100).IsRequired();
        builder.Property(c => c.StepOrder).HasColumnName("step_order").IsRequired();
        builder.Property(c => c.CompletedAtUtc).HasColumnName("completed_at_utc").IsRequired();

        builder.HasIndex(c => new { c.LearnerKey, c.LessonSlug, c.StepOrder })
            .HasDatabaseName("ix_checkpoint_completions_learner_lesson_step")
            .IsUnique();
    }
}

