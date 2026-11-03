# Database Schema — NAWWAR (`PostgreSQL 18`)

## 1. Entity-Relationship Overview

```mermaid
erDiagram
    LEARNING_MODULES ||--o{ LESSONS : contains
    LESSONS ||--o{ LESSON_CHECKPOINTS : defines
    CHECKPOINT_COMPLETIONS }o--|| LESSONS : tracks_slug

    LEARNING_MODULES {
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

    LESSONS {
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

    LESSON_CHECKPOINTS {
        uuid Id PK
        uuid LessonId FK
        int StepOrder "UNIQUE (LessonId, StepOrder)"
        varchar(100) TargetNodeName
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

    CHECKPOINT_COMPLETIONS {
        uuid id PK
        varchar(80) learner_key
        varchar(100) lesson_slug
        int step_order
        timestamptz completed_at_utc
    }
```

## 2. Constraints & Invariants

- **`ck_lessons_model_url_glb`**: Enforces at the PostgreSQL table level that `lessons.model_url` always ends with `.glb` (`model_url LIKE '%.glb'`).
- **Unique Indexes**:
  - `learning_modules ("Slug")`
  - `lessons ("Slug")`
  - `lesson_checkpoints ("LessonId", "StepOrder")`
  - `checkpoint_completions (learner_key, lesson_slug, step_order)` (`ix_checkpoint_completions_learner_lesson_step`)
- **Least-Privilege Access**:
  - Application traffic connects via `NAWWAR_DB_USER` (`nawwar_app`), provisioned on first container startup by `infra/postgres/init/10-create-app-role.sh`.
