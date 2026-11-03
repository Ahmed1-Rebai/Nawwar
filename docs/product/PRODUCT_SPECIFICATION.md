# Product Specification — NAWWAR (نوّار)

## 1. Product Vision

**NAWWAR (نوّار)** — *"Knowledge is Light" (العِلْمُ نُورٌ)* — is an Arabic-first, interactive 3D educational web platform that makes scientific and geometric concepts tangible inside a web browser.

Learners explore binary glTF 2.0 (`.glb`) 3D models alongside structured, fully vowelized (tashkeel) Arabic explanations, with instantaneous switching to French (`fr`, LTR) and English (`en`, LTR).

---

## 2. Core User Personas

| Persona | Primary Language | Goals |
|---|---|---|
| **Arabic-speaking secondary/university student** | Arabic (`ar`, RTL) | Understand spatial physics, astronomy, and geometry in authentic, well-typeset Arabic with interactive 3D visualizations. |
| **Multilingual STEM learner (Maghreb / MENA)** | Arabic (`ar`) + French (`fr`) / English (`en`) | Compare scientific terminology across Arabic, French, and English on the exact same 3D scene checkpoints. |
| **STEM educator / demonstrator** | Arabic (`ar`) / French (`fr`) / English (`en`) | Present interactive `.glb` models in a classroom browser without installing native desktop software. |

---

## 3. Implemented Capabilities (Phase 1 & Phase 2)

1. **Arabic-First (`ar`, RTL) Experience with Instant Language Switching**:
   - Boots in Arabic (`lang="ar" dir="rtl"`) with self-hosted `Noto Sans Arabic Variable` typography.
   - Instant client-side and server-side localization (`Accept-Language`) across `ar` (`rtl`), `fr` (`ltr`), and `en` (`ltr`).
2. **Interactive 3D `.glb` Curriculum Catalog**:
   - Three core scientific modules seeded in PostgreSQL 18:
     - **Optics & Light Physics (`optics-and-light`)**: Ibn al-Haytham's Optical Lantern & Camera Obscura (`/models/lantern.glb`).
     - **Planetary Astronomy (`planetary-astronomy`)**: The Sun, Earth, and Moon System (`/models/solar-system.glb`).
     - **Crystal & Molecular Geometry (`crystal-and-molecular-geometry`)**: Octahedral Crystal Lattice (`/models/crystal-lattice.glb`).
3. **3D Scene Checkpoints & Direct Mesh Picking**:
   - Each `.glb` lesson includes ordered inspection checkpoints bound to named 3D scene-graph nodes (`TargetNodeName`).
   - Selecting a checkpoint tab—or clicking directly on a 3D mesh node inside the WebGL canvas—highlights that node with emissive material feedback and positions a 3D focus indicator ring.
4. **Persistent Learner Checkpoint Progress**:
   - Learners can mark each checkpoint as completed (`POST /api/v1/curriculum/lessons/{slug}/checkpoints/{stepOrder}/complete`), persisted in PostgreSQL (`checkpoint_completions`) and summarized in the UI (`GET /api/v1/curriculum/progress`).
5. **Accessibility & Graceful Degradation**:
   - `prefers-reduced-motion: reduce` disables continuous 3D scene rotation.
   - `<ErrorBoundary>` provides a localized fallback when WebGL is unavailable.
