# Database Schema (`docs/architecture/DATABASE_SCHEMA.md`)

This document describes the PostgreSQL 18 relational schema managed by `NawwarDbContext` ([`NawwarDbContext.cs`](../../backend/src/Nawwar.Api/Infrastructure/Persistence/NawwarDbContext.cs)).

---

## 1. Entity-Relationship Diagram

```mermaid
erDiagram
    learning_modules ||--o{ lessons : "contains"
    lessons ||--o{ lesson_checkpoints : "defines"

    learning_modules {
        uuid Id PK
        varchar(80) Slug UK
        int DisplayOrder
        varchar(200) TitleAr
        varchar(200) TitleFr
        varchar(200) TitleEn
        varchar(600) SummaryAr
        varchar(600) SummaryFr
        varchar(600) SummaryEn
    }

    lessons {
        uuid Id PK
        uuid ModuleId FK
        varchar(100) Slug UK
        int DisplayOrder
        varchar(200) TitleAr
        varchar(200) TitleFr
        varchar(200) TitleEn
        varchar(800) ObjectiveAr
        varchar(800) ObjectiveFr
        varchar(800) ObjectiveEn
        varchar(300) model_url "CHECK (model_url LIKE '%.glb')"
        varchar(64) ModelFormat "model/gltf-binary"
        real CameraPositionX
        real CameraPositionY
        real CameraPositionZ
        real CameraTargetX
        real CameraTargetY
        real CameraTargetZ
    }

    lesson_checkpoints {
        uuid Id PK
        uuid LessonId FK
        int StepOrder "UNIQUE (LessonId, StepOrder)"
        varchar(100) TargetNodeName "Named node inside .glb scene graph"
        real FocusX
        real FocusY
        real FocusZ
        varchar(200) TitleAr
        varchar(200) TitleFr
        varchar(200) TitleEn
        varchar(1200) ExplanationAr
        varchar(1200) ExplanationFr
        varchar(1200) ExplanationEn
    }
```

---

## 2. `.glb` 3D Asset Constraint

Every row in `lessons` must reference a binary glTF 2.0 (`.glb`) 3D model:
- Enforced at the PostgreSQL level by check constraint `ck_lessons_model_url_glb` (`model_url LIKE '%.glb'`) in [`CurriculumConfigurations.cs`](../../backend/src/Nawwar.Api/Infrastructure/Persistence/Configurations/CurriculumConfigurations.cs).
- Enforced in domain logic by `Lesson.IsValidGlbUrl(url)` in [`Lesson.cs`](../../backend/src/Nawwar.Api/Features/Curriculum/Domain/Lesson.cs) and in the frontend by `isValidGlbUrl(url)` in [`api.ts`](../../frontend/src/lib/api.ts).
