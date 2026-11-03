using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Features.Curriculum;

/// <summary>
/// Canonical Phase 2 starter curriculum. Every lesson references a binary glTF 2.0
/// (<c>.glb</c>) 3D model and named scene-graph nodes for interactive checkpoints.
/// </summary>
public static class CurriculumCatalog
{
    public static IReadOnlyList<LearningModule> CreateSeedModules()
    {
        var opticsModuleId = Guid.Parse("11111111-1111-1111-1111-111111111101");
        var astronomyModuleId = Guid.Parse("11111111-1111-1111-1111-111111111102");
        var matterModuleId = Guid.Parse("11111111-1111-1111-1111-111111111103");

        var lanternLessonId = Guid.Parse("22222222-2222-2222-2222-222222222201");
        var solarLessonId = Guid.Parse("22222222-2222-2222-2222-222222222202");
        var crystalLessonId = Guid.Parse("22222222-2222-2222-2222-222222222203");

        return
        [
            new LearningModule
            {
                Id = opticsModuleId,
                Slug = "optics-and-light",
                DisplayOrder = 1,
                TitleAr = "البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ",
                TitleFr = "Optique et physique de la lumière",
                TitleEn = "Optics and Physics of Light",
                SummaryAr = "اِسْتَكْشِفْ كَيْفَ يَنْتَقِلُ الضَّوْءُ فِي خُطُوطٍ مُسْتَقِيمَةٍ وَكَيْفَ فَسَّرَ ابْنُ الهَيْثَمِ الرُّؤْيَةَ.",
                SummaryFr = "Explorez la propagation rectiligne de la lumière et les découvertes optiques d’Ibn al-Haytham.",
                SummaryEn = "Explore how light travels in straight lines and how Ibn al-Haytham explained vision.",
                Lessons =
                [
                    new Lesson
                    {
                        Id = lanternLessonId,
                        ModuleId = opticsModuleId,
                        Slug = "alhazen-optical-lantern",
                        DisplayOrder = 1,
                        TitleAr = "مِصْبَاحُ ابْنِ الهَيْثَمِ وَالحُجْرَةُ المُظْلِمَةُ",
                        TitleFr = "La lanterne optique d’Ibn al-Haytham",
                        TitleEn = "Ibn al-Haytham’s Optical Lantern",
                        ObjectiveAr = "مُلاحَظَةُ مَسَارِ الأَشِعَّةِ الضَّوْئِيَّةِ عَبْرَ الثُّقْبِ الضَّيِّقِ وَتَشَكُّلِ الصُّورَةِ المَقْلُوبَةِ.",
                        ObjectiveFr = "Observer le trajet des rayons lumineux à travers le sténopé et l’inversion de l’image.",
                        ObjectiveEn = "Observe light rays passing through a pinhole aperture to form an inverted projection.",
                        ModelUrl = "/models/lantern.glb",
                        ModelFormat = Lesson.GlbMimeType,
                        CameraPositionX = 3.2f,
                        CameraPositionY = 2.3f,
                        CameraPositionZ = 4.0f,
                        CameraTargetX = 0.0f,
                        CameraTargetY = 1.0f,
                        CameraTargetZ = 0.0f,
                        Checkpoints =
                        [
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333301"),
                                LessonId = lanternLessonId,
                                StepOrder = 1,
                                TargetNodeName = "Light_Core",
                                FocusX = 0.0f,
                                FocusY = 1.25f,
                                FocusZ = 0.0f,
                                TitleAr = "المَصْدَرُ الضَّوْئِيُّ المَرْكَزِيُّ",
                                TitleFr = "Source lumineuse centrale",
                                TitleEn = "Central Light Source",
                                ExplanationAr = "يَنْبَعِثُ الضَّوْءُ مِنْ كُلِّ نُقْطَةٍ فِي المَصْدَرِ المُضِيءِ فِي جَمِيعِ الاتِّجَاهَاتِ بِخُطُوطٍ مُسْتَقِيمَةٍ.",
                                ExplanationFr = "La lumière est émise depuis chaque point de la source dans toutes les directions en ligne droite.",
                                ExplanationEn = "Light radiates from every point on the luminous source in straight lines across all directions.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333302"),
                                LessonId = lanternLessonId,
                                StepOrder = 2,
                                TargetNodeName = "Aperture_Ring",
                                FocusX = 0.0f,
                                FocusY = 1.25f,
                                FocusZ = 0.95f,
                                TitleAr = "حَلْقَةُ الثُّقْبِ الضَّيِّقِ (القُمْرَة)",
                                TitleFr = "Anneau du sténopé (Al-Qumra)",
                                TitleEn = "Pinhole Aperture Ring (Al-Qumra)",
                                ExplanationAr = "يَسْمَحُ الثُّقْبُ الضَّيِّقُ بِمُرُورِ حُزْمَةٍ مُحَدَّدَةٍ مِنَ الأَشِعَّةِ فَيَمْنَعُ تَدَاخُلَ الضَّوْءِ وَيُظْهِرُ صُورَةً وَاضِحَةً.",
                                ExplanationFr = "L’ouverture étroite sélectionne un faisceau précis de rayons et empêche le flou lumineux.",
                                ExplanationEn = "The narrow aperture isolates a tight bundle of rays, preventing overlap and producing a sharp image.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333303"),
                                LessonId = lanternLessonId,
                                StepOrder = 3,
                                TargetNodeName = "Lantern_Base",
                                FocusX = 0.0f,
                                FocusY = 0.25f,
                                FocusZ = 0.0f,
                                TitleAr = "قَاعِدَةُ الاتِّزَانِ وَالمِحْوَرُ البَصَرِيُّ",
                                TitleFr = "Socle et axe optique",
                                TitleEn = "Optical Axis and Base",
                                ExplanationAr = "تُحَافِظُ القَاعِدَةُ عَلَى ثَبَاتِ المِحْوَرِ البَصَرِيِّ لِقِيَاسِ زَوَايَا السُّقُوطِ وَالانْعِكَاسِ بِدِقَّةٍ.",
                                ExplanationFr = "Le socle stabilise l’axe optique afin de mesurer avec précision les angles d’incidence et de réflexion.",
                                ExplanationEn = "The base stabilizes the optical axis so angles of incidence and reflection can be measured accurately.",
                            },
                        ],
                    },
                ],
            },
            new LearningModule
            {
                Id = astronomyModuleId,
                Slug = "planetary-astronomy",
                DisplayOrder = 2,
                TitleAr = "عِلْمُ الفَلَكِ وَحَرَكَةُ الكَوَاكِبِ",
                TitleFr = "Astronomie et mécanique céleste",
                TitleEn = "Planetary Astronomy and Orbits",
                SummaryAr = "تَعَرَّفْ عَلَى المَدَارَاتِ الكَوْكَبِيَّةِ وَتَعَاقُبِ اللَّيْلِ وَالنَّهَارِ فِي نَمُوذَجٍ ثُلاثِيِّ الأَبْعَادِ.",
                SummaryFr = "Comprenez les orbites planétaires et le cycle jour-nuit grâce à un modèle 3D interactif.",
                SummaryEn = "Understand planetary orbits and the day-night cycle through an interactive 3D model.",
                Lessons =
                [
                    new Lesson
                    {
                        Id = solarLessonId,
                        ModuleId = astronomyModuleId,
                        Slug = "sun-earth-moon-system",
                        DisplayOrder = 1,
                        TitleAr = "مَنْظُومَةُ الشَّمْسِ وَالأَرْضِ وَالقَمَرِ",
                        TitleFr = "Le système Soleil-Terre-Lune",
                        TitleEn = "The Sun, Earth, and Moon System",
                        ObjectiveAr = "تَتَبُّعُ دَوَرَانِ الأَرْضِ حَوْلَ الشَّمْسِ وَحَرَكَةِ القَمَرِ فِي مَدَارِهِ الإِهْلِيلَجِيِّ.",
                        ObjectiveFr = "Suivre la révolution de la Terre autour du Soleil et l’orbite de la Lune.",
                        ObjectiveEn = "Track Earth’s revolution around the Sun and the Moon’s orbital path.",
                        ModelUrl = "/models/solar-system.glb",
                        ModelFormat = Lesson.GlbMimeType,
                        CameraPositionX = 4.2f,
                        CameraPositionY = 3.0f,
                        CameraPositionZ = 4.6f,
                        CameraTargetX = 0.0f,
                        CameraTargetY = 0.8f,
                        CameraTargetZ = 0.0f,
                        Checkpoints =
                        [
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333304"),
                                LessonId = solarLessonId,
                                StepOrder = 1,
                                TargetNodeName = "Core_Sun",
                                FocusX = 0.0f,
                                FocusY = 1.0f,
                                FocusZ = 0.0f,
                                TitleAr = "الشَّمْسُ: مَرْكَزُ الجَاذِبِيَّةِ وَالضَّوْءِ",
                                TitleFr = "Le Soleil : centre gravitationnel",
                                TitleEn = "The Sun: Gravitational and Luminous Center",
                                ExplanationAr = "تُمَثِّلُ الشَّمْسُ المَرْكَزَ الجَاذِبِيَّ لِلْمَجْمُوعَةِ الشَّمْسِيَّةِ وَمَصْدَرَ الطَّاقَةِ الحَرَارِيَّةِ وَالضَّوْئِيَّةِ.",
                                ExplanationFr = "Le Soleil concentre l’essentiel de la masse du système et fournit l’énergie lumineuse et thermique.",
                                ExplanationEn = "The Sun holds the gravitational center of the system and supplies radiant light and thermal energy.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333305"),
                                LessonId = solarLessonId,
                                StepOrder = 2,
                                TargetNodeName = "Planet_Earth",
                                FocusX = 1.75f,
                                FocusY = 1.0f,
                                FocusZ = 0.0f,
                                TitleAr = "كَوْكَبُ الأَرْضِ وَمَيْلُ المِحْوَرِ",
                                TitleFr = "La Terre et l’inclinaison axiale",
                                TitleEn = "Planet Earth and Axial Tilt",
                                ExplanationAr = "يُؤَدِّي دَوَرَانُ الأَرْضِ حَوْلَ نَفْسِهَا إِلَى تَعَاقُبِ اللَّيْلِ وَالنَّهَارِ، بَيْنَمَا يُسَبِّبُ مَيْلُ مِحْوَرِهَا الفُصُولَ الأَرْبَعَةَ.",
                                ExplanationFr = "La rotation de la Terre produit l’alternance jour-nuit, tandis que son inclinaison crée les saisons.",
                                ExplanationEn = "Earth’s rotation creates day and night, while its axial tilt drives the four seasons.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333306"),
                                LessonId = solarLessonId,
                                StepOrder = 3,
                                TargetNodeName = "Moon_Satellite",
                                FocusX = 2.35f,
                                FocusY = 1.15f,
                                FocusZ = 0.0f,
                                TitleAr = "القَمَرُ وَأَطْوَارُهُ الشَّهْرِيَّةُ",
                                TitleFr = "La Lune et ses phases",
                                TitleEn = "The Moon and Its Lunar Phases",
                                ExplanationAr = "تَتَغَيَّرُ أَطْوَارُ القَمَرِ تَبَعاً لِزَاوِيَةِ سُقُوطِ ضَوْءِ الشَّمْسِ عَلَيْهِ أَثْنَاءَ دَوَرَانِهِ حَوْلَ الأَرْضِ.",
                                ExplanationFr = "Les phases lunaires varient selon l’angle d’éclairement solaire au cours de son orbite terrestre.",
                                ExplanationEn = "Lunar phases change as the sunlit portion of the Moon is viewed from different orbital angles.",
                            },
                        ],
                    },
                ],
            },
            new LearningModule
            {
                Id = matterModuleId,
                Slug = "molecular-geometry",
                DisplayOrder = 3,
                TitleAr = "البِنْيَةُ الذَّرِّيَّةُ وَالهَنْدَسَةُ الجُزَيْئِيَّةُ",
                TitleFr = "Structure atomique et géométrie moléculaire",
                TitleEn = "Atomic Structure and Molecular Geometry",
                SummaryAr = "تَفَحَّصْ تَرْتِيبَ الذَّرَّاتِ وَالرَّوَابِطِ الكِيمْيَائِيَّةِ فِي الشَّبَكَاتِ البِلَّوْرِيَّةِ ثُلاثِيَّةِ الأَبْعَادِ.",
                SummaryFr = "Inspectez l’arrangement tridimensionnel des atomes et des liaisons dans un réseau cristallin.",
                SummaryEn = "Inspect the 3D arrangement of atoms and chemical bonds inside a crystal lattice.",
                Lessons =
                [
                    new Lesson
                    {
                        Id = crystalLessonId,
                        ModuleId = matterModuleId,
                        Slug = "octahedral-crystal-lattice",
                        DisplayOrder = 1,
                        TitleAr = "الشَّبَكَةُ البِلَّوْرِيَّةُ ثُمَانِيَّةُ الأَوْجُهِ",
                        TitleFr = "Le réseau cristallin octaédrique",
                        TitleEn = "Octahedral Crystal Lattice",
                        ObjectiveAr = "فَهْمُ التَّنَاسُقِ الهَنْدَسِيِّ بَيْنَ الذَّرَّةِ المَرْكَزِيَّةِ وَالذَّرَّاتِ المُحِيطَةِ بِزَوَايَا قَائِمَةٍ.",
                        ObjectiveFr = "Comprendre la coordination géométrique entre l’atome central et ses six voisins.",
                        ObjectiveEn = "Understand the geometric coordination between a central atom and six surrounding ligands.",
                        ModelUrl = "/models/crystal-lattice.glb",
                        ModelFormat = Lesson.GlbMimeType,
                        CameraPositionX = 3.5f,
                        CameraPositionY = 2.6f,
                        CameraPositionZ = 3.8f,
                        CameraTargetX = 0.0f,
                        CameraTargetY = 1.1f,
                        CameraTargetZ = 0.0f,
                        Checkpoints =
                        [
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333307"),
                                LessonId = crystalLessonId,
                                StepOrder = 1,
                                TargetNodeName = "Central_Atom",
                                FocusX = 0.0f,
                                FocusY = 1.2f,
                                FocusZ = 0.0f,
                                TitleAr = "الذَّرَّةُ المَرْكَزِيَّةُ (مَرْكَزُ التَّنَاسُقِ)",
                                TitleFr = "Atome central de coordination",
                                TitleEn = "Central Coordination Atom",
                                ExplanationAr = "تَرْتَبِطُ الذَّرَّةُ المَرْكَزِيَّةُ بِسِتِّ ذَرَّاتٍ مُتَكَافِئَةٍ تُشَكِّلُ رُؤُوسَ مُجَسَّمٍ ثُمَانِيِّ الأَوْجُهِ.",
                                ExplanationFr = "L’atome central est lié à six atomes équivalents formant les sommets d’un octaèdre régulier.",
                                ExplanationEn = "The central atom bonds equally to six surrounding atoms positioned at the vertices of an octahedron.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333308"),
                                LessonId = crystalLessonId,
                                StepOrder = 2,
                                TargetNodeName = "Bond_Axis",
                                FocusX = 0.55f,
                                FocusY = 1.2f,
                                FocusZ = 0.0f,
                                TitleAr = "مِحْوَرُ الرَّابِطَةِ الكِيمْيَائِيَّةِ",
                                TitleFr = "Axe de liaison chimique",
                                TitleEn = "Chemical Bond Axis",
                                ExplanationAr = "تَتَعَامَدُ مَحَاوِرُ الرَّوَابِطِ بَزَوَايَا قَدْرُهَا 90 دَرَجَةً لِتَقْلِيلِ التَّنَافُرِ الإِلِكْتُرُونِيِّ.",
                                ExplanationFr = "Les axes de liaison sont perpendiculaires (90°) afin de minimiser la répulsion électronique.",
                                ExplanationEn = "Bond axes meet at 90-degree angles, minimizing electron-pair repulsion across the lattice.",
                            },
                            new LessonCheckpoint
                            {
                                Id = Guid.Parse("33333333-3333-3333-3333-333333333309"),
                                LessonId = crystalLessonId,
                                StepOrder = 3,
                                TargetNodeName = "Vertex_Ligand",
                                FocusX = 0.0f,
                                FocusY = 2.15f,
                                FocusZ = 0.0f,
                                TitleAr = "ذَرَّةُ الرَّأْسِ الطَّرَفِيَّةُ",
                                TitleFr = "Atome sommet du réseau",
                                TitleEn = "Apical Vertex Atom",
                                ExplanationAr = "تُحَدِّدُ الذَّرَّاتُ الطَّرَفِيَّةُ أَبْعَادَ الخَلِيَّةِ البِلَّوْرِيَّةِ وَخَصَائِصَهَا الفِيزْيَائِيَّةَ وَالضَّوْئِيَّةَ.",
                                ExplanationFr = "Les atomes situés aux sommets définissent la maille cristalline et ses propriétés optiques.",
                                ExplanationEn = "Vertex atoms define the unit-cell geometry and govern the crystal’s physical and optical behavior.",
                            },
                        ],
                    },
                ],
            },
        ];
    }
}

