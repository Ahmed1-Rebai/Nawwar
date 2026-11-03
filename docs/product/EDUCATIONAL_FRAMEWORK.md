# Educational Framework — NAWWAR (نوّار)

## 1. Pedagogical Principles

1. **Spatial First, Symbolic Second**:
   - Learners first inspect a physical 3D structure (`.glb` scene) by orbiting and selecting parts (`TargetNodeName`) before reading abstract definitions.
2. **Guided Checkpoints (`LessonCheckpoint`)**:
   - Every 3D lesson is decomposed into 3 ordered checkpoints (`StepOrder = 1..3`).
   - Each checkpoint isolates a single physical component (e.g., `Light_Core` → `Aperture_Ring` → `Lantern_Base`) and states a concise, self-contained scientific explanation.
3. **Mother-Tongue Scientific Precision**:
   - Arabic scientific terminology is authored with full diacritics (tashkeel) so pronunciation and grammatical structure are unambiguous.
   - Parallel French and English translations allow learners in bilingual curricula to bridge terminology seamlessly.

---

## 2. Curriculum Matrix

| Module (`Slug`) | Lesson (`Slug`) | `.glb` Asset | Scene Checkpoints (`StepOrder` → `TargetNodeName`) |
|---|---|---|---|
| `optics-and-light` | `alhazen-optical-lantern` | `/models/lantern.glb` | `1` → `Light_Core`<br>`2` → `Aperture_Ring`<br>`3` → `Lantern_Base` |
| `planetary-astronomy` | `sun-earth-moon-system` | `/models/solar-system.glb` | `1` → `Core_Sun`<br>`2` → `Planet_Earth`<br>`3` → `Moon_Satellite` |
| `crystal-and-molecular-geometry` | `octahedral-crystal-lattice` | `/models/crystal-lattice.glb` | `1` → `Central_Atom`<br>`2` → `Bond_Axis`<br>`3` → `Vertex_Ligand` |
