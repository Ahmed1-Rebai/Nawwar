# Product Specification — NAWWAR (نَوَّر)

## 1. Product Vision

**NAWWAR (نَوَّر)** — *"Knowledge is Light" (العِلْمُ نُورٌ)* — is an Arabic-first, immersive 3D educational platform for **preschool children aged 3, 4, and 5 years** (*عُمْرُ 3 وَ4 وَ5 سَنَوَاتٍ*).

NAWWAR is **not** a general scientific visualization tool, **not** an abstract physics/astronomy simulator, and **not** an authentication-first web app. It is a playful, warmly designed educational universe where young children learn through age-appropriate courses, interactive activities, spoken Arabic audio, illustrations, animations, and supportive 3D exploration.

**Central Product Objective**:
> **Build a complete, structured educational curriculum organized by age (3, 4, and 5 years), with interactive 3D experiences and multi-modal activities supporting each learning objective.**

---

## 2. Target Audience & Personas

| Persona | Role | Needs & Expectations |
|---|---|---|
| **Child — Age 3 (`عَالَمُ الاِكْتِشَافِ`)** | Early Learner (Discovery) | Audio-first prompts, large touch/click targets, familiar animals/colors/shapes/numbers 1–3, instant joyful feedback, zero reading prerequisite. |
| **Child — Age 4 (`عَالَمُ الاِسْتِكْشَافِ`)** | Developing Learner (Exploration) | Arabic alphabet discovery, letter sounds, counting 1–10, sorting/matching, nature/seasons, and playful 3D/2D activities. |
| **Child — Age 5 (`عَالَمُ التَّعَلُّمِ`)** | School-Ready Learner (Consolidation) | Structured Arabic reading pathway (`مَسَارُ تَعَلُّمِ القِرَاءَةِ`: letters, positional forms, short vowels *الفتحة/الكسرة/الضمة*, syllables, words, short vocalized sentences), numbers 1–20, simple addition, and logical sequences. |
| **Parent / Early Childhood Educator** | Guide & Facilitator | Immediate access by age without login friction, clear visibility into learning objectives and course progression, authentic vowelized Arabic (`ar`), and optional French (`fr`) / English (`en`) support. |

---

## 3. Core Product Principles

1. **Age-First Navigation**:
   - The main entry point presents the three age worlds:
     - **عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ**
     - **عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ**
     - **عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ**
2. **Structured 7-Level Curriculum Hierarchy**:
   - `AgeGroup` → `LearningDomain` → `CourseCollection` → `Course` → `Lesson` → `Activity` → `LearningObjective` (specified in detail in [`AGE_BASED_CURRICULUM.md`](AGE_BASED_CURRICULUM.md)).
3. **Decoupled Multi-Modal Activities**:
   - Lessons and activities are decoupled from any single `.glb` file. An activity can combine a 3D scene (`scene_3d_explore`), 2D illustrations, spoken Arabic audio, animations, sorting/matching (`visual_match_sort`), counting (`counting_interactive`), or vocalized Arabic reading builders (`letter_forms_vowels`, `syllable_word_builder`).
4. **Authentication Comes Last**:
   - Children and parents can enter an age world and start learning immediately without account creation barriers. Authentication and multi-profile sync are deferred to `PHASE H`.
