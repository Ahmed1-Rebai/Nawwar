using Nawwar.Api.Features.Curriculum.Domain;

namespace Nawwar.Api.Features.Curriculum;

/// <summary>
/// Canonical NAWWAR preschool curriculum catalog organized around Ages 3, 4, and 5
/// (<c>عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ</c>, <c>عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ</c>,
/// <c>عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ</c>), including Age 5's dedicated 6-course Arabic reading
/// pathway (<c>مَسَارُ تَعَلُّمِ القِرَاءَةِ</c>).
/// </summary>
public static class CurriculumCatalog
{
    public static IReadOnlyList<AgeGroup> CreateSeedAgeGroups() =>
    [
        BuildAge3DiscoveryWorld(),
        BuildAge4ExplorationWorld(),
        BuildAge5LearningWorld(),
    ];

    private static AgeGroup BuildAge3DiscoveryWorld()
    {
        var age3Id = Guid.Parse("10000000-0000-0000-0000-000000000003");
        var colorsDomainId = Guid.Parse("20000000-0000-0000-0003-000000000001");
        var animalsDomainId = Guid.Parse("20000000-0000-0000-0003-000000000002");
        var numbersDomainId = Guid.Parse("20000000-0000-0000-0003-000000000003");
        var listeningDomainId = Guid.Parse("20000000-0000-0000-0003-000000000004");
        var bodyEmotionsDomainId = Guid.Parse("20000000-0000-0000-0003-000000000005");

        var gardenCollectionId = Guid.Parse("30000000-0000-0000-0003-000000000001");
        var farmCollectionId = Guid.Parse("30000000-0000-0000-0003-000000000002");
        var countCollectionId = Guid.Parse("30000000-0000-0000-0003-000000000003");
        var listeningCollectionId = Guid.Parse("30000000-0000-0000-0003-000000000004");
        var bodyCollectionId = Guid.Parse("30000000-0000-0000-0003-000000000005");

        var colorsCourseId = Guid.Parse("40000000-0000-0000-0003-000000000001");
        var shapesCourseId = Guid.Parse("40000000-0000-0000-0003-000000000002");
        var farmCourseId = Guid.Parse("40000000-0000-0000-0003-000000000003");
        var count1To3CourseId = Guid.Parse("40000000-0000-0000-0003-000000000004");
        var kindWordsCourseId = Guid.Parse("40000000-0000-0000-0003-000000000005");
        var bodyEmotionsCourseId = Guid.Parse("40000000-0000-0000-0003-000000000006");

        var colorsLessonId = Guid.Parse("50000000-0000-0000-0003-000000000001");
        var shapesLessonId = Guid.Parse("50000000-0000-0000-0003-000000000002");
        var farmLessonId = Guid.Parse("50000000-0000-0000-0003-000000000003");
        var count1To3LessonId = Guid.Parse("50000000-0000-0000-0003-000000000004");
        var kindWordsLessonId = Guid.Parse("50000000-0000-0000-0003-000000000005");
        var bodyEmotionsLessonId = Guid.Parse("50000000-0000-0000-0003-000000000006");

        return new AgeGroup
        {
            Id = age3Id,
            AgeYears = 3,
            Slug = "age-3-discovery",
            ThemeKey = "discovery",
            IconEmoji = "🌱",
            DisplayOrder = 1,
            TitleAr = "عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ",
            TitleFr = "Monde de la Découverte — 3 ans",
            TitleEn = "World of Discovery — Age 3",
            SubtitleAr = "الاِكْتِشَافُ الحِسِّيُّ وَالتَّعَرُّفُ الأَسَاسِيُّ بِالصَّوْتِ وَاللَّعِبِ",
            SubtitleFr = "Découverte sensorielle, écoute et reconnaissance ludique",
            SubtitleEn = "Sensory discovery, listening, and joyful recognition",
            DescriptionAr = "أَنْشِطَةٌ قَصِيرَةٌ وَمَرِحَةٌ تَعْتَمِدُ عَلَى الصَّوْتِ وَالصُّورَةِ وَالمَشَاهِدِ ثُلَاثِيَّةِ الأَبْعَادِ لِلتَّعَرُّفِ عَلَى الأَلْوَانِ وَالأَشْكَالِ وَالحَيَوَانَاتِ وَالأَعْدَادِ (1–3).",
            DescriptionFr = "Activités courtes, visuelles et sonores pour découvrir les couleurs, les formes, les animaux familiers et les nombres 1 à 3.",
            DescriptionEn = "Short visual, auditory, and 3D activities introducing basic colors, shapes, familiar animals, and numbers 1 to 3.",
            Domains =
            [
                new LearningDomain
                {
                    Id = colorsDomainId,
                    AgeGroupId = age3Id,
                    Slug = "age-3-colors-and-shapes",
                    DisplayOrder = 1,
                    IconEmoji = "🎨",
                    AccentColor = "#ff7a70",
                    TitleAr = "الأَلْوَانُ وَالأَشْكَالُ الأَسَاسِيَّةُ",
                    TitleFr = "Couleurs et formes de base",
                    TitleEn = "Basic Colors and Shapes",
                    SummaryAr = "تَعَرَّفْ عَلَى الأَحْمَرِ وَالأَصْفَرِ وَالأَزْرَقِ وَالدَّائِرَةِ وَالمُرَبَّعِ فِي حَدِيقَةٍ ثُلَاثِيَّةِ الأَبْعَادِ.",
                    SummaryFr = "Découvrir le rouge, le jaune, le bleu, le cercle et le carré dans un jardin 3D.",
                    SummaryEn = "Discover red, yellow, blue, circles, and squares inside a playful 3D garden.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = gardenCollectionId,
                            DomainId = colorsDomainId,
                            Slug = "garden-of-colors-and-shapes",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ حَدِيقَةِ الأَلْوَانِ وَالأَشْكَالِ",
                            TitleFr = "Collection Le Jardin des Couleurs et des Formes",
                            TitleEn = "Garden of Colors and Shapes Collection",
                            SummaryAr = "رِحْلَةٌ بَصَرِيَّةٌ وَصَوْتِيَّةٌ مَعَ الفَوَاكِهِ وَالأَزْهَارِ وَالمُجَسَّمَاتِ المُلَوَّنَةِ.",
                            SummaryFr = "Parcours visuel et sonore avec des fruits, des fleurs et des blocs colorés.",
                            SummaryEn = "A visual and audio journey with colorful fruits, flowers, and toy blocks.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = colorsCourseId,
                                    CollectionId = gardenCollectionId,
                                    Slug = "my-first-four-colors",
                                    TargetAge = 3,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🍎",
                                    TitleAr = "أَلْوَانِي الأَرْبَعَةُ الأُولَى",
                                    TitleFr = "Mes quatre premières couleurs",
                                    TitleEn = "My First Four Colors",
                                    DescriptionAr = "نَكْتَشِفُ الأَحْمَرَ وَالأَصْفَرَ وَالأَزْرَقَ وَالأَخْضَرَ فِي حَدِيقَةِ نَوَّر.",
                                    DescriptionFr = "Découvrons le rouge, le jaune, le bleu et le vert dans le jardin de Nawwar.",
                                    DescriptionEn = "Discover red, yellow, blue, and green in Nawwar's sunny garden.",
                                    CompletionCriteriaAr = "إِتْمَامُ جَمِيعِ أَنْشِطَةِ الاِسْتِمَاعِ وَالاِسْتِكْشَافِ اللَّوْنِيِّ.",
                                    CompletionCriteriaFr = "Terminer les activités d’écoute et d’exploration des couleurs.",
                                    CompletionCriteriaEn = "Complete all color listening and 3D garden exploration activities.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = colorsCourseId,
                                            Code = "L3-CL-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "التَّعَرُّفُ عَلَى الأَلْوَانِ الأَسَاسِيَّةِ (الأَحْمَرُ، الأَصْفَرُ، الأَزْرَقُ، الأَخْضَرُ) وَتَسْمِيَتُهَا.",
                                            DescriptionFr = "Reconnaître et nommer les couleurs de base (rouge, jaune, bleu, vert).",
                                            DescriptionEn = "Recognize and name basic colors (red, yellow, blue, green).",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = colorsLessonId,
                                            CourseId = colorsCourseId,
                                            Slug = "red-apple-yellow-sun-garden",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "التُّفَّاحَةُ الحَمْرَاءُ وَالزَّهْرَةُ الصَّفْرَاءُ فِي الحَدِيقَةِ",
                                            TitleFr = "La pomme rouge et la fleur jaune dans le jardin",
                                            TitleEn = "The Red Apple and Yellow Flower in the Garden",
                                            SummaryAr = "نَسْتَكْشِفُ الأَلْوَانَ فِي المَشْهَدِ ثُلَاثِيِّ الأَبْعَادِ ثُمَّ نَلْعَبُ لُعْبَةَ اخْتِيَارِ اللَّوْنِ.",
                                            SummaryFr = "Explorons les couleurs dans la scène 3D puis jouons à reconnaître chaque couleur.",
                                            SummaryEn = "Explore colors in the 3D garden scene and play a joyful color recognition game.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = colorsLessonId,
                                                    Slug = "age3-colors-3d-garden",
                                                    StepOrder = 1,
                                                    ActivityType = "scene_3d_explore",
                                                    ObjectiveCode = "L3-CL-01",
                                                    TitleAr = "اِكْتَشِفْ حَدِيقَةَ الأَلْوَانِ ثُلَاثِيَّةَ الأَبْعَادِ",
                                                    TitleFr = "Explore le jardin des couleurs en 3D",
                                                    TitleEn = "Explore the 3D Color Garden",
                                                    PromptAr = "اِضْغَطْ عَلَى التُّفَّاحَةِ الحَمْرَاءِ أَوِ الزَّهْرَةِ الصَّفْرَاءِ أَوِ الكُرَةِ الزَّرْقَاءِ فِي الحَدِيقَةِ!",
                                                    PromptFr = "Touche la pomme rouge, la fleur jaune ou le ballon bleu dans le jardin !",
                                                    PromptEn = "Tap the red apple, yellow flower, or blue ball inside the 3D garden!",
                                                    SpokenTextAr = "هَذِهِ حَدِيقَةُ الأَلْوَانِ! تُفَّاحَةٌ حَمْرَاءُ، زَهْرَةٌ صَفْرَاءُ، وَكُرَةٌ زَرْقَاءُ.",
                                                    SpokenTextFr = "Voici le jardin des couleurs : une pomme rouge, une fleur jaune et un ballon bleu.",
                                                    SpokenTextEn = "Welcome to the color garden: a red apple, a yellow flower, and a blue ball.",
                                                    GlbModelUrl = "/models/garden-colors.glb",
                                                    TargetNodeName = "Red_Apple",
                                                    FocusX = -1.1f,
                                                    FocusY = 0.8f,
                                                    FocusZ = 0.2f,
                                                    InteractionPayloadJson = """{"sceneObjects":[{"nodeName":"Red_Apple","emoji":"🍎","labelAr":"تُفَّاحَةٌ حَمْرَاءُ","labelFr":"Pomme rouge","labelEn":"Red apple","color":"#ef4444"},{"nodeName":"Yellow_Flower","emoji":"🌻","labelAr":"زَهْرَةٌ صَفْرَاءُ","labelFr":"Fleur jaune","labelEn":"Yellow flower","color":"#facc15"},{"nodeName":"Blue_Ball","emoji":"🔵","labelAr":"كُرَةٌ زَرْقَاءُ","labelFr":"Ballon bleu","labelEn":"Blue ball","color":"#3b82f6"}]}""",
                                                },
                                                new Activity
                                                {
                                                    LessonId = colorsLessonId,
                                                    Slug = "age3-colors-audio-pick",
                                                    StepOrder = 2,
                                                    ActivityType = "audio_listen_pick",
                                                    ObjectiveCode = "L3-CL-01",
                                                    TitleAr = "اِسْتَمِعْ وَاخْتَرِ اللَّوْنَ الأَحْمَرَ",
                                                    TitleFr = "Écoute et choisis la couleur rouge",
                                                    TitleEn = "Listen and Pick the Red Object",
                                                    PromptAr = "أَيْنَ التُّفَّاحَةُ الحَمْرَاءُ؟ اِضْغَطْ عَلَيْهَا!",
                                                    PromptFr = "Où est la pomme rouge ? Touche-la !",
                                                    PromptEn = "Where is the red apple? Tap on it!",
                                                    SpokenTextAr = "أَيْنَ التُّفَّاحَةُ الحَمْرَاءُ؟",
                                                    SpokenTextFr = "Où est la pomme rouge ?",
                                                    SpokenTextEn = "Where is the red apple?",
                                                    InteractionPayloadJson = """{"correctOptionId":"red-apple","options":[{"id":"red-apple","emoji":"🍎","labelAr":"تُفَّاحَةٌ حَمْرَاءُ","labelFr":"Pomme rouge","labelEn":"Red apple","accentColor":"#ef4444"},{"id":"yellow-banana","emoji":"🍌","labelAr":"مَوْزَةٌ صَفْرَاءُ","labelFr":"Banane jaune","labelEn":"Yellow banana","accentColor":"#facc15"},{"id":"blue-whale","emoji":"🐳","labelAr":"حُوتٌ أَزْرَقُ","labelFr":"Baleine bleue","labelEn":"Blue whale","accentColor":"#3b82f6"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = shapesCourseId,
                                    CollectionId = gardenCollectionId,
                                    Slug = "my-shape-friends",
                                    TargetAge = 3,
                                    DisplayOrder = 2,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🟡",
                                    TitleAr = "أَصْدِقَائِي الأَشْكَالُ: الدَّائِرَةُ وَالمُرَبَّعُ وَالمُثَلَّثُ",
                                    TitleFr = "Mes amis les formes : cercle, carré et triangle",
                                    TitleEn = "My Shape Friends: Circle, Square, and Triangle",
                                    DescriptionAr = "نَتَعَرَّفُ عَلَى الدَّائِرَةِ وَالمُرَبَّعِ وَالمُثَلَّثِ مِنْ خِلَالِ الأَلْعَابِ المَرِحَةِ.",
                                    DescriptionFr = "Découvrons le cercle, le carré et le triangle à travers le jeu.",
                                    DescriptionEn = "Meet the circle, square, and triangle through playful visual matching.",
                                    CompletionCriteriaAr = "التَّعَرُّفُ الصَّحِيحُ عَلَى الدَّائِرَةِ وَالمُرَبَّعِ وَالمُثَلَّثِ.",
                                    CompletionCriteriaFr = "Identifier correctement le cercle, le carré et le triangle.",
                                    CompletionCriteriaEn = "Identify the circle, square, and triangle accurately.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = shapesCourseId,
                                            Code = "L3-SH-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "التَّمْيِيزُ البَصَرِيُّ بَيْنَ الدَّائِرَةِ وَالمُرَبَّعِ وَالمُثَلَّثِ.",
                                            DescriptionFr = "Distinguer visuellement le cercle, le carré et le triangle.",
                                            DescriptionEn = "Visually distinguish between a circle, a square, and a triangle.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = shapesLessonId,
                                            CourseId = shapesCourseId,
                                            Slug = "circle-square-triangle-play",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "الدَّائِرَةُ تَتَدَحْرَجُ كَالكُرَةِ",
                                            TitleFr = "Le cercle roule comme un ballon",
                                            TitleEn = "The Circle Rolls Like a Ball",
                                            SummaryAr = "نُطَابِقُ الأَشْكَالَ الأَسَاسِيَّةَ مَعَ الأَشْيَاءِ المَأْلُوفَةِ مِنْ حَوْلِنَا.",
                                            SummaryFr = "Associons les formes de base aux objets du quotidien.",
                                            SummaryEn = "Match basic shapes to familiar everyday objects.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = shapesLessonId,
                                                    Slug = "age3-shapes-match",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L3-SH-01",
                                                    TitleAr = "طَابِقْ شَكْلَ الدَّائِرَةِ",
                                                    TitleFr = "Trouve l’objet en forme de cercle",
                                                    TitleEn = "Match the Circle Shape",
                                                    PromptAr = "الشَّمْسُ مُسْتَدِيرَةٌ كَالدَّائِرَةِ! اِخْتَرِ الشَّكْلَ الدَّائِرِيَّ:",
                                                    PromptFr = "Le soleil est rond comme un cercle ! Choisis le cercle :",
                                                    PromptEn = "The sun is round like a circle! Pick the circle shape:",
                                                    SpokenTextAr = "الشَّمْسُ مُسْتَدِيرَةٌ كَالدَّائِرَةِ! اِخْتَرِ الدَّائِرَةَ.",
                                                    SpokenTextFr = "Le soleil est rond comme un cercle ! Choisis le cercle.",
                                                    SpokenTextEn = "The sun is round like a circle! Choose the circle.",
                                                    InteractionPayloadJson = """{"correctOptionId":"shape-circle","options":[{"id":"shape-circle","emoji":"🟠","labelAr":"دَائِرَةٌ","labelFr":"Cercle","labelEn":"Circle","accentColor":"#fb923c"},{"id":"shape-square","emoji":"🟦","labelAr":"مُرَبَّعٌ","labelFr":"Carré","labelEn":"Square","accentColor":"#3b82f6"},{"id":"shape-triangle","emoji":"🔺","labelAr":"مُثَلَّثٌ","labelFr":"Triangle","labelEn":"Triangle","accentColor":"#ef4444"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = animalsDomainId,
                    AgeGroupId = age3Id,
                    Slug = "age-3-animals-and-sounds",
                    DisplayOrder = 2,
                    IconEmoji = "🐑",
                    AccentColor = "#4cc38a",
                    TitleAr = "الحَيَوَانَاتُ وَالأَصْوَاتُ المَأْلُوفَةُ",
                    TitleFr = "Animaux familiers et sons",
                    TitleEn = "Familiar Animals and Sounds",
                    SummaryAr = "نَسْتَمِعُ إِلَى أَصْوَاتِ الحَيَوَانَاتِ اللَّطِيفَةِ فِي مَزْرَعَةِ نَوَّر ثُلَاثِيَّةِ الأَبْعَادِ.",
                    SummaryFr = "Écoutons les animaux de la ferme 3D de Nawwar.",
                    SummaryEn = "Listen to friendly animals inside Nawwar's 3D farm.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = farmCollectionId,
                            DomainId = animalsDomainId,
                            Slug = "my-little-animal-world",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ عَالَمِي الصَّغِيرِ وَأَصْوَاتِ المَزْرَعَةِ",
                            TitleFr = "Collection Mon petit monde à la ferme",
                            TitleEn = "My Little Farm World Collection",
                            SummaryAr = "تَعَرَّفْ عَلَى الخَرُوفِ وَالبَقَرَةِ وَالبَطَّةِ وَأَصْوَاتِهَا.",
                            SummaryFr = "Découvre le mouton, la vache, le canard et leurs cris.",
                            SummaryEn = "Meet the sheep, cow, and duck and hear their sounds.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = farmCourseId,
                                    CollectionId = farmCollectionId,
                                    Slug = "friendly-farm-animals",
                                    TargetAge = 3,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🐮",
                                    TitleAr = "حَيَوَانَاتُ المَزْرَعَةِ اللَّطِيفَةُ",
                                    TitleFr = "Les gentils animaux de la ferme",
                                    TitleEn = "Friendly Farm Animals",
                                    DescriptionAr = "نَزُورُ المَزْرَعَةَ ثُلَاثِيَّةَ الأَبْعَادِ وَنَسْتَمِعُ إِلَى الخَرُوفِ وَالبَقَرَةِ وَالبَطَّةِ.",
                                    DescriptionFr = "Visitons la ferme 3D et écoutons le mouton, la vache et le canard.",
                                    DescriptionEn = "Visit the 3D farm and listen to the sheep, cow, and little duck.",
                                    CompletionCriteriaAr = "التَّعَرُّفُ عَلَى حَيَوَانَاتِ المَزْرَعَةِ وَأَصْوَاتِهَا.",
                                    CompletionCriteriaFr = "Reconnaître les animaux de la ferme et leurs sons.",
                                    CompletionCriteriaEn = "Recognize farm animals and their sounds.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = farmCourseId,
                                            Code = "L3-AN-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "التَّعَرُّفُ عَلَى الحَيَوَانَاتِ المَأْلُوفَةِ بِالشَّكْلِ وَالصَّوْتِ.",
                                            DescriptionFr = "Identifier les animaux familiers par leur forme et leur son.",
                                            DescriptionEn = "Identify familiar animals by sight and sound.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = farmLessonId,
                                            CourseId = farmCourseId,
                                            Slug = "sheep-cow-duck-3d-farm",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "الخَرُوفُ وَالبَقَرَةُ وَالبَطَّةُ فِي المَزْرَعَةِ",
                                            TitleFr = "Le mouton, la vache et le canard à la ferme",
                                            TitleEn = "Sheep, Cow, and Duck on the Farm",
                                            SummaryAr = "اِضْغَطْ عَلَى الحَيَوَانَاتِ فِي المَزْرَعَةِ ثُلَاثِيَّةِ الأَبْعَادِ لِسَمَاعِ أَسْمَائِهَا.",
                                            SummaryFr = "Touche les animaux dans la ferme 3D pour entendre leurs noms.",
                                            SummaryEn = "Tap the animals in the 3D farm to hear their names and sounds.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = farmLessonId,
                                                    Slug = "age3-farm-3d-explore",
                                                    StepOrder = 1,
                                                    ActivityType = "scene_3d_explore",
                                                    ObjectiveCode = "L3-AN-01",
                                                    TitleAr = "زِيَارَةُ المَزْرَعَةِ ثُلَاثِيَّةِ الأَبْعَادِ",
                                                    TitleFr = "Visite de la ferme en 3D",
                                                    TitleEn = "Visit the 3D Friendly Farm",
                                                    PromptAr = "اِضْغَطْ عَلَى الخَرُوفِ الأَبْيَضِ أَوِ البَقَرَةِ أَوِ البَطَّةِ الصَّفْرَاءِ فِي المَزْرَعَةِ!",
                                                    PromptFr = "Touche le mouton blanc, la vache ou le petit canard jaune !",
                                                    PromptEn = "Tap the white sheep, the cow, or the yellow duck on the farm!",
                                                    SpokenTextAr = "مَرْحَباً بِكَ فِي المَزْرَعَةِ! هُنَا الخَرُوفُ الأَبْيَضُ، وَالبَقَرَةُ اللَّطِيفَةُ، وَالبَطَّةُ الصَّغِيرَةُ.",
                                                    SpokenTextFr = "Bienvenue à la ferme ! Voici le mouton blanc, la vache et le petit canard.",
                                                    SpokenTextEn = "Welcome to the farm! Here are the white sheep, the friendly cow, and the little duck.",
                                                    GlbModelUrl = "/models/friendly-farm.glb",
                                                    TargetNodeName = "Friendly_Sheep",
                                                    FocusX = -1.0f,
                                                    FocusY = 0.8f,
                                                    FocusZ = 0.2f,
                                                    InteractionPayloadJson = """{"sceneObjects":[{"nodeName":"Friendly_Sheep","emoji":"🐑","labelAr":"خَرُوفٌ أَبْيَضُ (مَاعْ!)","labelFr":"Mouton blanc (Bêê !)","labelEn":"White sheep (Baa!)","color":"#f8fafc"},{"nodeName":"Friendly_Cow","emoji":"🐮","labelAr":"بَقَرَةٌ لَطِيفَةٌ (مُوو!)","labelFr":"Vache (Meuh !)","labelEn":"Friendly cow (Moo!)","color":"#d97706"},{"nodeName":"Little_Duck","emoji":"🦆","labelAr":"بَطَّةٌ صَفْرَاءُ (وَاقْ!)","labelFr":"Petit canard (Coin !)","labelEn":"Little duck (Quack!)","color":"#facc15"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = numbersDomainId,
                    AgeGroupId = age3Id,
                    Slug = "age-3-numbers-1-to-3",
                    DisplayOrder = 3,
                    IconEmoji = "🔢",
                    AccentColor = "#f4c76b",
                    TitleAr = "الأَعْدَادُ الأُولَى (1–3) وَالتَّرَابُطُ البَصَرِيُّ",
                    TitleFr = "Premiers nombres (1 à 3)",
                    TitleEn = "First Numbers (1 to 3)",
                    SummaryAr = "نَعُدُّ الأَلْعَابَ وَالمُكَعَّبَاتِ: وَاحِدٌ، اِثْنَانِ، ثَلَاثَةٌ!",
                    SummaryFr = "Comptons les cubes ensemble : un, deux, trois !",
                    SummaryEn = "Count toy blocks together: one, two, three!",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = countCollectionId,
                            DomainId = numbersDomainId,
                            Slug = "count-and-play-1-to-3",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ نَعُدُّ مَعاً (1–3)",
                            TitleFr = "Collection Comptons de 1 à 3",
                            TitleEn = "Count Together 1 to 3 Collection",
                            SummaryAr = "العَدُّ الحِسِّيُّ لِلأَشْيَاءِ المَلْمُوسَةِ مِنْ 1 إِلَى 3.",
                            SummaryFr = "Dénombrement visuel de 1 à 3.",
                            SummaryEn = "Visual and 3D counting from 1 to 3.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = count1To3CourseId,
                                    CollectionId = countCollectionId,
                                    Slug = "counting-1-2-3-blocks",
                                    TargetAge = 3,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🧸",
                                    TitleAr = "نَعُدُّ مَعاً: وَاحِدٌ، اِثْنَانِ، ثَلَاثَةٌ",
                                    TitleFr = "Comptons : un, deux, trois",
                                    TitleEn = "Count Together: One, Two, Three",
                                    DescriptionAr = "نَعُدُّ ثَلَاثَةَ مُكَعَّبَاتٍ مُلَوَّنَةٍ فِي المَشْهَدِ ثُلَاثِيِّ الأَبْعَادِ.",
                                    DescriptionFr = "Comptons trois cubes colorés dans la scène 3D.",
                                    DescriptionEn = "Count three colorful blocks inside the 3D play scene.",
                                    CompletionCriteriaAr = "العَدُّ الصَّحِيحُ مِنْ 1 إِلَى 3.",
                                    CompletionCriteriaFr = "Compter correctement de 1 à 3.",
                                    CompletionCriteriaEn = "Count accurately from 1 to 3.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = count1To3CourseId,
                                            Code = "L3-NM-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "إِدْرَاكُ وَعَدُّ الكَمِّيَّاتِ الصَّغِيرَةِ مِنْ 1 إِلَى 3.",
                                            DescriptionFr = "Reconnaître et compter de petites quantités de 1 à 3.",
                                            DescriptionEn = "Perceive and count small quantities from 1 to 3.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = count1To3LessonId,
                                            CourseId = count1To3CourseId,
                                            Slug = "three-blocks-on-the-table",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "ثَلَاثَةُ مُكَعَّبَاتٍ عَلَى الطَّاوِلَةِ",
                                            TitleFr = "Trois cubes sur la table",
                                            TitleEn = "Three Blocks on the Table",
                                            SummaryAr = "اِضْغَطْ عَلَى المُكَعَّبَاتِ وَاحِداً تِلْوَ الآخَرِ: ١، ٢، ٣!",
                                            SummaryFr = "Touche les cubes un par un : 1, 2, 3 !",
                                            SummaryEn = "Tap the blocks one by one: 1, 2, 3!",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = count1To3LessonId,
                                                    Slug = "age3-count-3d-blocks",
                                                    StepOrder = 1,
                                                    ActivityType = "scene_3d_explore",
                                                    ObjectiveCode = "L3-NM-01",
                                                    TitleAr = "عُدَّ المُكَعَّبَاتِ ثُلَاثِيَّةَ الأَبْعَادِ (1، 2، 3)",
                                                    TitleFr = "Compte les cubes 3D (1, 2, 3)",
                                                    TitleEn = "Count the 3D Blocks (1, 2, 3)",
                                                    PromptAr = "اِضْغَطْ عَلَى المُكَعَّبِ الأَوَّلِ (١)، ثُمَّ الثَّانِي (٢)، ثُمَّ الثَّالِثِ (٣)!",
                                                    PromptFr = "Touche le premier cube (1), puis le deuxième (2), puis le troisième (3) !",
                                                    PromptEn = "Tap block one (1), block two (2), and block three (3)!",
                                                    SpokenTextAr = "هَيَّا نَعُدُّ مَعاً: وَاحِدٌ، اِثْنَانِ، ثَلَاثَةٌ!",
                                                    SpokenTextFr = "Comptons ensemble : un, deux, trois !",
                                                    SpokenTextEn = "Let's count together: one, two, three!",
                                                    GlbModelUrl = "/models/counting-blocks.glb",
                                                    TargetNodeName = "Block_One",
                                                    FocusX = -1.1f,
                                                    FocusY = 0.7f,
                                                    FocusZ = 0.0f,
                                                    InteractionPayloadJson = """{"sceneObjects":[{"nodeName":"Block_One","emoji":"1️⃣","labelAr":"وَاحِدٌ (١)","labelFr":"Un (1)","labelEn":"One (1)","color":"#ef4444"},{"nodeName":"Block_Two","emoji":"2️⃣","labelAr":"اِثْنَانِ (٢)","labelFr":"Deux (2)","labelEn":"Two (2)","color":"#facc15"},{"nodeName":"Block_Three","emoji":"3️⃣","labelAr":"ثَلَاثَةٌ (٣)","labelFr":"Trois (3)","labelEn":"Three (3)","color":"#3b82f6"}]}""",
                                                },
                                                new Activity
                                                {
                                                    LessonId = count1To3LessonId,
                                                    Slug = "age3-count-stars-interactive",
                                                    StepOrder = 2,
                                                    ActivityType = "counting_interactive",
                                                    ObjectiveCode = "L3-NM-01",
                                                    TitleAr = "اِضْغَطْ عَلَى النُّجُومِ لِنَعُدَّهَا مَعاً: ١، ٢، ٣!",
                                                    TitleFr = "Touche les étoiles pour compter : 1, 2, 3 !",
                                                    TitleEn = "Tap the Stars to Count Together: 1, 2, 3!",
                                                    PromptAr = "اِضْغَطْ عَلَى كُلِّ نَجْمَةٍ لِتُضِيءَ، ثُمَّ اخْتَرْ عَدَدَ النُّجُومِ:",
                                                    PromptFr = "Touche chaque étoile pour l’allumer, puis choisis le nombre 3 :",
                                                    PromptEn = "Tap each star to light it up, then choose number 3:",
                                                    SpokenTextAr = "نَجْمَةٌ وَاحِدَةٌ، نَجْمَتَانِ، ثَلَاثُ نُجُومٍ لَامِعَةٍ!",
                                                    SpokenTextFr = "Une étoile, deux étoiles, trois étoiles brillantes !",
                                                    SpokenTextEn = "One star, two stars, three shining stars!",
                                                    InteractionPayloadJson = """{"countingItems":{"emoji":"⭐","count":3,"itemNameAr":"نُجُومٌ لَامِعَةٌ","itemNameFr":"Étoiles","itemNameEn":"Shining Stars"},"correctOptionId":"count-3","options":[{"id":"count-1","emoji":"1️⃣","labelAr":"١ — نَجْمَةٌ وَاحِدَةٌ","labelFr":"1 — Une étoile","labelEn":"1 — One star","accentColor":"#38bdf8"},{"id":"count-2","emoji":"2️⃣","labelAr":"٢ — نَجْمَتَانِ","labelFr":"2 — Deux étoiles","labelEn":"2 — Two stars","accentColor":"#f59e0b"},{"id":"count-3","emoji":"3️⃣","labelAr":"٣ — ثَلَاثُ نُجُومٍ","labelFr":"3 — Trois étoiles","labelEn":"3 — Three stars","accentColor":"#22c55e"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = listeningDomainId,
                    AgeGroupId = age3Id,
                    Slug = "age-3-arabic-listening-and-sounds",
                    DisplayOrder = 4,
                    IconEmoji = "🎵",
                    AccentColor = "#a855f7",
                    TitleAr = "الإِصْغَاءُ وَالكَلِمَاتُ اللَّطِيفَةُ",
                    TitleFr = "Écoute arabe et premiers mots doux",
                    TitleEn = "Arabic Listening and Kind Words",
                    SummaryAr = "نَسْتَمِعُ إِلَى أَصْوَاتِ الطَّبِيعَةِ وَنَتَعَلَّمُ الكَلِمَاتِ الطَّيِّبَةَ (مَرْحَباً، شُكْراً، بِسْمِ اللهِ).",
                    SummaryFr = "Écouter les sons de la nature et apprendre les mots gentils en arabe.",
                    SummaryEn = "Listen to nature sounds and learn kind everyday Arabic words.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = listeningCollectionId,
                            DomainId = listeningDomainId,
                            Slug = "sounds-and-kind-words",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ أَصْوَاتٍ مِنْ حَوْلِي وَكَلِمَاتِي اللَّطِيفَةُ",
                            TitleFr = "Collection Sons autour de moi et mots doux",
                            TitleEn = "Sounds Around Me & Kind Words Collection",
                            SummaryAr = "تَمْيِيزُ الأَصْوَاتِ المَأْلُوفَةِ وَالتَّحِيَّاتِ العَرَبِيَّةِ الجَمِيلَةِ.",
                            SummaryFr = "Reconnaître les sons familiers et les salutations arabes.",
                            SummaryEn = "Recognize familiar sounds and cheerful Arabic greetings.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = kindWordsCourseId,
                                    CollectionId = listeningCollectionId,
                                    Slug = "my-first-kind-words",
                                    TargetAge = 3,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "💛",
                                    TitleAr = "كَلِمَاتِي الأُولَى اللَّطِيفَةُ وَأَصْوَاتُ الطَّبِيعَةِ",
                                    TitleFr = "Mes premiers mots gentils et sons de la nature",
                                    TitleEn = "My First Kind Words and Nature Sounds",
                                    DescriptionAr = "نَسْتَمِعُ إِلَى صَوْتِ المَطَرِ وَالعُصْفُورِ وَنَتَعَلَّمُ قَوْلَ: مَرْحَباً وَشُكْراً.",
                                    DescriptionFr = "Écoutons la pluie et l’oiseau et apprenons à dire bonjour et merci.",
                                    DescriptionEn = "Listen to rain and birdsong and learn to say Marhaban and Shukran.",
                                    CompletionCriteriaAr = "الاسْتِمَاعُ إِلَى البطَاقَاتِ الصَّوْتِيَّةِ وَاخْتِيَارُ الصُّورَةِ المُنَاسِبَةِ.",
                                    CompletionCriteriaFr = "Écouter les cartes sonores et choisir la bonne image.",
                                    CompletionCriteriaEn = "Listen to the audio cards and pick the matching illustration.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = kindWordsCourseId,
                                            Code = "L3-AR-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "الاسْتِمَاعُ بِانْتِبَاهٍ إِلَى الكَلِمَاتِ العَرَبِيَّةِ المَأْلُوفَةِ وَأَصْوَاتِ الطَّبِيعَةِ.",
                                            DescriptionFr = "Écouter attentivement les mots arabes familiers et les sons de la nature.",
                                            DescriptionEn = "Listen attentively to familiar Arabic words and nature sounds.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = kindWordsLessonId,
                                            CourseId = kindWordsCourseId,
                                            Slug = "birdsong-rain-and-kind-greetings",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "صَوْتُ العُصْفُورِ وَالمَطَرِ وَكَلِمَةُ «شُكْراً»",
                                            TitleFr = "Le chant de l’oiseau, la pluie et le mot « شُكْراً »",
                                            TitleEn = "Birdsong, Rain, and Saying 'شُكْراً'",
                                            SummaryAr = "اِكْتَشِفْ أَصْوَاتَ الطَّبِيعَةِ وَالكَلِمَاتِ اللَّطِيفَةَ بِالصَّوْتِ وَالصُّورَةِ.",
                                            SummaryFr = "Découvre les sons de la nature et les mots gentils en audio et en images.",
                                            SummaryEn = "Explore nature sounds and kind Arabic words with audio and pictures.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = kindWordsLessonId,
                                                    Slug = "age3-listening-bird-sound",
                                                    StepOrder = 1,
                                                    ActivityType = "audio_listen_pick",
                                                    ObjectiveCode = "L3-AR-01",
                                                    TitleAr = "مَنْ يُغَرِّدُ فَوْقَ الشَّجَرَةِ؟ (زَقْزَقَةُ العُصْفُورِ 🐦)",
                                                    TitleFr = "Qui chante sur l’arbre ? (L’oiseau 🐦)",
                                                    TitleEn = "Who Sings in the Tree? (The Little Bird 🐦)",
                                                    PromptAr = "اِسْتَمِعْ إِلَى بِطَاقَاتِ التَّعَلُّمِ، ثُمَّ اخْتَرِ العُصْفُورَ الَّذِي يُغَرِّدُ فَوْقَ الغُصْنِ:",
                                                    PromptFr = "Écoute les cartes, puis choisis le petit oiseau qui chante :",
                                                    PromptEn = "Listen to the learning cards, then pick the singing bird:",
                                                    SpokenTextAr = "العُصْفُورُ الصَّغِيرُ يُغَرِّدُ فَوْقَ الشَّجَرَةِ: تْشِيب تْشِيب! وَالمَطَرُ يَنْزِلُ مِنَ الغَيْمَةِ!",
                                                    SpokenTextFr = "Le petit oiseau chante sur l’arbre, et la pluie tombe du nuage !",
                                                    SpokenTextEn = "The little bird sings in the tree, and rain falls from the cloud!",
                                                    InteractionPayloadJson = """{"learningCards":[{"emoji":"🐦","titleAr":"عُصْفُورٌ يُغَرِّدُ","titleFr":"Oiseau qui chante","titleEn":"Singing Bird","factAr":"العُصْفُورُ يَطِيرُ فِي السَّمَاءِ وَيُغَرِّدُ بِصَوْتٍ جَمِيلٍ.","factFr":"L’oiseau vole et chante une jolie mélodie.","factEn":"The bird flies in the sky and sings a cheerful song.","accentColor":"#38bdf8"},{"emoji":"🌧️","titleAr":"مَطَرٌ لَطِيفٌ","titleFr":"Pluie douce","titleEn":"Gentle Rain","factAr":"المَطَرُ يَسْقِي الأَزْهَارَ وَالأَشْجَارَ لِتَكْبُرَ.","factFr":"La pluie arrose les fleurs et les arbres.","factEn":"Rain waters the flowers and trees so they grow.","accentColor":"#60a5fa"},{"emoji":"💐","titleAr":"شُكْراً لَكَ!","titleFr":"Merci (شُكْراً)","titleEn":"Thank You (شُكْراً)","factAr":"نَقُولُ «شُكْراً» بِابْتِسَامَةٍ لِكُلِّ مَنْ يُسَاعِدُنَا.","factFr":"Nous disons « شُكْراً » avec un grand sourire.","factEn":"We say 'شُكْراً (Shukran)' with a warm smile.","accentColor":"#f43f5e"}],"correctOptionId":"sound-bird","options":[{"id":"sound-bird","emoji":"🐦","labelAr":"العُصْفُورُ المُغَرِّدُ","labelFr":"L’oiseau qui chante","labelEn":"The Singing Bird","accentColor":"#38bdf8"},{"id":"sound-rain","emoji":"🌧️","labelAr":"غَيْمَةُ المَطَرِ","labelFr":"Le nuage de pluie","labelEn":"The Rain Cloud","accentColor":"#60a5fa"},{"id":"sound-bell","emoji":"🔔","labelAr":"جَرَسُ البَيْتِ","labelFr":"La cloche","labelEn":"The Doorbell","accentColor":"#f59e0b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = bodyEmotionsDomainId,
                    AgeGroupId = age3Id,
                    Slug = "age-3-my-body-and-emotions",
                    DisplayOrder = 5,
                    IconEmoji = "😊",
                    AccentColor = "#f43f5e",
                    TitleAr = "جِسْمِي وَمَشَاعِرِي",
                    TitleFr = "Mon corps et mes émotions",
                    TitleEn = "My Body and Basic Emotions",
                    SummaryAr = "أَتَعَرَّفُ عَلَى عَيْنِي وَيَدِي وَأُمَيِّزُ الوَجْهَ السَّعِيدَ وَالوَجْهَ المُتَفَاجِئَ.",
                    SummaryFr = "Découvrir les parties du corps et reconnaître les émotions de base.",
                    SummaryEn = "Recognize body parts and friendly facial emotions.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = bodyCollectionId,
                            DomainId = bodyEmotionsDomainId,
                            Slug = "me-and-my-feelings",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ أَنَا وَمَشَاعِرِي الجَمِيلَةُ",
                            TitleFr = "Collection Moi et mes émotions",
                            TitleEn = "Me and My Feelings Collection",
                            SummaryAr = "نُشِيرُ إِلَى أَجْزَاءِ الجِسْمِ وَنُعَبِّرُ عَنْ مَشَاعِرِنَا بِفَرَحٍ.",
                            SummaryFr = "Montrer les parties du corps et exprimer ses émotions.",
                            SummaryEn = "Point to body parts and express happy feelings.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = bodyEmotionsCourseId,
                                    CollectionId = bodyCollectionId,
                                    Slug = "my-body-and-emotions",
                                    TargetAge = 3,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🖐️",
                                    TitleAr = "أَجْزَاءُ جِسْمِي وَكَيْفَ أَشْعُرُ اليَوْمَ؟",
                                    TitleFr = "Les parties de mon corps et mes émotions",
                                    TitleEn = "My Body Parts and How I Feel Today",
                                    DescriptionAr = "نَتَعَرَّفُ عَلَى العَيْنِ وَاليَدِ وَالقَدَمِ، وَنُمَيِّزُ الابْتِسَامَةَ السَّعِيدَةَ.",
                                    DescriptionFr = "Découvrons les yeux, la main et le sourire joyeux.",
                                    DescriptionEn = "Discover our eyes, hands, and happy smiling faces.",
                                    CompletionCriteriaAr = "التَّعَرُّفُ عَلَى أَجْزَاءِ الجِسْمِ وَالوَجْهِ السَّعِيدِ.",
                                    CompletionCriteriaFr = "Reconnaître les parties du corps et le visage joyeux.",
                                    CompletionCriteriaEn = "Recognize body parts and the happy face.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = bodyEmotionsCourseId,
                                            Code = "L3-BD-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "الإِشَارَةُ إِلَى أَجْزَاءِ الجِسْمِ الظَّاهِرَةِ وَتَمْيِيزُ المَشَاعِرِ الأَسَاسِيَّةِ.",
                                            DescriptionFr = "Nommer les parties du corps et identifier les émotions simples.",
                                            DescriptionEn = "Point to visible body parts and identify basic emotions.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = bodyEmotionsLessonId,
                                            CourseId = bodyEmotionsCourseId,
                                            Slug = "my-eyes-my-hands-and-happy-smile",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 3,
                                            TitleAr = "عَيْنِي، يَدِي، وَابْتِسَامَتِي السَّعِيدَةُ",
                                            TitleFr = "Mes yeux, ma main et mon grand sourire",
                                            TitleEn = "My Eyes, My Hand, and My Happy Smile",
                                            SummaryAr = "نَتَعَلَّمُ أَسْمَاءَ أَجْزَاءِ جِسْمِنَا وَكَيْفَ نَبْتَسِمُ بِسَعَادَةٍ.",
                                            SummaryFr = "Apprenons les parties du corps et le sourire joyeux.",
                                            SummaryEn = "Learn body part names and identify a cheerful smile.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = bodyEmotionsLessonId,
                                                    Slug = "age3-body-happy-emotion",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L3-BD-01",
                                                    TitleAr = "أَيُّ وَجْهٍ يَشْعُرُ بِالسَّعَادَةِ وَالفَرَحِ (😊)؟",
                                                    TitleFr = "Quel visage est joyeux et souriant (😊) ?",
                                                    TitleEn = "Which Face Feels Happy and Joyful (😊)?",
                                                    PromptAr = "اِسْتَكْشِفِ البِطَاقَاتِ ثُمَّ اخْتَرِ الوَجْهَ السَّعِيدَ المُبْتَسِمَ:",
                                                    PromptFr = "Explore les cartes puis choisis le visage heureux :",
                                                    PromptEn = "Explore the cards, then choose the happy smiling face:",
                                                    SpokenTextAr = "أَنَا سَعِيدٌ وَأَبْتَسِمُ! وَأُلَوِّحُ بِيَدِي الجَمِيلَةِ، وَأَنْظُرُ بِعَيْنِي!",
                                                    SpokenTextFr = "Je suis heureux et je souris ! Je salue avec ma main.",
                                                    SpokenTextEn = "I am happy and smiling! I wave with my hand and see with my eyes.",
                                                    InteractionPayloadJson = """{"learningCards":[{"emoji":"👀","titleAr":"عَيْنِي أَرَى بِهَا","titleFr":"Mes yeux","titleEn":"My Eyes","factAr":"أَنْظُرُ بِعَيْنِي إِلَى الأَلْوَانِ وَالأَزْهَارِ الجَمِيلَةِ.","factFr":"Je vois les couleurs et les fleurs avec mes yeux.","factEn":"I look at colorful flowers with my eyes.","accentColor":"#38bdf8"},{"emoji":"🖐️","titleAr":"يَدِي أُلَوِّحُ بِهَا","titleFr":"Ma main","titleEn":"My Hand","factAr":"أَرْسُمُ وَأُلَوِّحُ لِأَصْدِقَائِي بِيَدِي.","factFr":"Je dessine et je salue mes amis avec ma main.","factEn":"I draw and wave to my friends with my hand.","accentColor":"#f59e0b"},{"emoji":"😊","titleAr":"وَجْهٌ سَعِيدٌ","titleFr":"Visage joyeux","titleEn":"Happy Face","factAr":"عِنْدَمَا أَفْرَحُ تَظْهَرُ ابْتِسَامَةٌ جَمِيلَةٌ عَلَى وَجْهِي!","factFr":"Quand je suis content, je fais un grand sourire !","factEn":"When I feel joyful, a bright smile appears on my face!","accentColor":"#22c55e"}],"correctOptionId":"emotion-happy","options":[{"id":"emotion-happy","emoji":"😊","labelAr":"سَعِيدٌ وَمُبْتَسِمٌ","labelFr":"Joyeux et souriant","labelEn":"Happy and Smiling","accentColor":"#22c55e"},{"id":"emotion-surprised","emoji":"😮","labelAr":"مُتَفَاجِئٌ","labelFr":"Surpris","labelEn":"Surprised","accentColor":"#f59e0b"},{"id":"emotion-sleepy","emoji":"😴","labelAr":"نَعْسَانُ وَهَادِئٌ","labelFr":"Endormi","labelEn":"Sleepy and Calm","accentColor":"#60a5fa"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
            ],
        };
    }

    private static AgeGroup BuildAge4ExplorationWorld()
    {
        var age4Id = Guid.Parse("10000000-0000-0000-0000-000000000004");
        var alphabetDomainId = Guid.Parse("20000000-0000-0000-0004-000000000001");
        var countingDomainId = Guid.Parse("20000000-0000-0000-0004-000000000002");
        var natureLogicDomainId = Guid.Parse("20000000-0000-0000-0004-000000000003");
        var patternsDomainId = Guid.Parse("20000000-0000-0000-0004-000000000004");

        var alphabetCollectionId = Guid.Parse("30000000-0000-0000-0004-000000000001");
        var countingCollectionId = Guid.Parse("30000000-0000-0000-0004-000000000002");
        var natureCollectionId = Guid.Parse("30000000-0000-0000-0004-000000000003");
        var patternsCollectionId = Guid.Parse("30000000-0000-0000-0004-000000000004");

        var lettersGroup1CourseId = Guid.Parse("40000000-0000-0000-0004-000000000001");
        var letterSoundsCourseId = Guid.Parse("40000000-0000-0000-0004-000000000002");
        var numbers1To10CourseId = Guid.Parse("40000000-0000-0000-0004-000000000003");
        var habitatsSeasonsCourseId = Guid.Parse("40000000-0000-0000-0004-000000000004");
        var patternsCourseId = Guid.Parse("40000000-0000-0000-0004-000000000005");

        var alifBaTaLessonId = Guid.Parse("50000000-0000-0000-0004-000000000001");
        var letterSoundsLessonId = Guid.Parse("50000000-0000-0000-0004-000000000002");
        var countTo10LessonId = Guid.Parse("50000000-0000-0000-0004-000000000003");
        var habitatsLessonId = Guid.Parse("50000000-0000-0000-0004-000000000004");
        var patternsLessonId = Guid.Parse("50000000-0000-0000-0004-000000000005");

        return new AgeGroup
        {
            Id = age4Id,
            AgeYears = 4,
            Slug = "age-4-exploration",
            ThemeKey = "exploration",
            IconEmoji = "🧭",
            DisplayOrder = 2,
            TitleAr = "عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ",
            TitleFr = "Monde de l’Exploration — 4 ans",
            TitleEn = "World of Exploration — Age 4",
            SubtitleAr = "اِكْتِشَافُ الحُرُوفِ العَرَبِيَّةِ، العَدُّ (1–10)، الأَنْمَاطُ، وَالطَّبِيعَةُ",
            SubtitleFr = "Découverte de l’alphabet arabe, comptage 1 à 10, formes et nature",
            SubtitleEn = "Arabic alphabet discovery, counting 1–10, patterns, and nature",
            DescriptionAr = "رِحْلَةٌ مُمْتِعَةٌ لِلتَّعَرُّفِ عَلَى الحُرُوفِ الهِجَائِيَّةِ وَأَصْوَاتِهَا، وَعَدِّ الأَشْيَاءِ حَتَّى 10، وَتَصْنِيفِ الحَيَوَانَاتِ وَالفُصُولِ الأَرْبَعَةِ.",
            DescriptionFr = "Un parcours ludique pour découvrir les lettres arabes et leurs sons, compter jusqu’à 10 et explorer la nature.",
            DescriptionEn = "An engaging journey to discover Arabic letters and their sounds, count 1 to 10, and sort animals and seasons.",
            Domains =
            [
                new LearningDomain
                {
                    Id = alphabetDomainId,
                    AgeGroupId = age4Id,
                    Slug = "age-4-arabic-alphabet-discovery",
                    DisplayOrder = 1,
                    IconEmoji = "🔤",
                    AccentColor = "#f4c76b",
                    TitleAr = "اِكْتِشَافُ الحُرُوفِ العَرَبِيَّةِ وَأَصْوَاتِهَا",
                    TitleFr = "Découverte de l’alphabet arabe et des sons",
                    TitleEn = "Arabic Alphabet and Letter Sounds",
                    SummaryAr = "تَعَرَّفْ عَلَى شَكْلِ الحَرْفِ العَرَبِيِّ وَصَوْتِهِ وَكَلِمَتِهِ المُمَيَّزَةِ.",
                    SummaryFr = "Découvrir la forme de chaque lettre arabe, son son et un mot illustré.",
                    SummaryEn = "Discover Arabic letter shapes, phonetic sounds, and anchor words.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = alphabetCollectionId,
                            DomainId = alphabetDomainId,
                            Slug = "alphabet-garden-age-4",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ حَدِيقَةِ الحُرُوفِ الهِجَائِيَّةِ",
                            TitleFr = "Collection Le Jardin de l’Alphabet Arabe",
                            TitleEn = "Arabic Alphabet Garden Collection",
                            SummaryAr = "نَكْتَشِفُ الحُرُوفَ العَرَبِيَّةَ مَعَ الكَلِمَاتِ المُصَوَّرَةِ وَالمَسْرَحِ ثُلَاثِيِّ الأَبْعَادِ.",
                            SummaryFr = "Découvrons les lettres arabes avec des mots illustrés et la scène 3D.",
                            SummaryEn = "Explore Arabic letters with illustrated words and the 3D letter stage.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = lettersGroup1CourseId,
                                    CollectionId = alphabetCollectionId,
                                    Slug = "arabic-letters-alif-to-kha",
                                    TargetAge = 4,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🐰",
                                    TitleAr = "مَجْمُوعَةُ الحُرُوفِ الأُولَى (أ، ب، ت، ث، ج، ح، خ)",
                                    TitleFr = "Premier groupe de lettres (أ، ب، ت، ث، ج، ح، خ)",
                                    TitleEn = "First Letter Group (أ, ب, ت, ث, ج, ح, خ)",
                                    DescriptionAr = "نَتَعَرَّفُ عَلَى حُرُوفِ (أ – أَرْنَبٌ، ب – بَطَّةٌ، ت – تُفَّاحَةٌ) فِي مَسْرَحِ الحُرُوفِ ثُلَاثِيِّ الأَبْعَادِ.",
                                    DescriptionFr = "Découvrons les lettres (أ – أَرْنَبٌ, ب – بَطَّةٌ, ت – تُفَّاحَةٌ) sur la scène 3D.",
                                    DescriptionEn = "Meet the letters أ (Arnab), ب (Batta), and ت (Tuffaha) on the 3D stage.",
                                    CompletionCriteriaAr = "تَمْيِيزُ شَكْلِ وَاسْمِ الحُرُوفِ الأُولَى وَرَبْطُهَا بِصُوَرِهَا.",
                                    CompletionCriteriaFr = "Reconnaître la forme et le nom des premières lettres arabes.",
                                    CompletionCriteriaEn = "Recognize the shape and name of the first Arabic letter group.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = lettersGroup1CourseId,
                                            Code = "L4-AR-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "التَّعَرُّفُ البَصَرِيُّ وَالصَّوْتِيُّ عَلَى الحُرُوفِ الهِجَائِيَّةِ المُنْفَرِدَةِ.",
                                            DescriptionFr = "Reconnaissance visuelle et sonore des lettres arabes isolées.",
                                            DescriptionEn = "Visual and auditory recognition of isolated Arabic letters.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = alifBaTaLessonId,
                                            CourseId = lettersGroup1CourseId,
                                            Slug = "alif-arnab-ba-batta-ta-tuffaha",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 4,
                                            TitleAr = "حُرُوفُ: أ (أَرْنَبٌ) — ب (بَطَّةٌ) — ت (تُفَّاحَةٌ)",
                                            TitleFr = "Lettres : أ (أَرْنَبٌ) — ب (بَطَّةٌ) — ت (تُفَّاحَةٌ)",
                                            TitleEn = "Letters: أ (Arnab) — ب (Batta) — ت (Tuffaha)",
                                            SummaryAr = "نَسْتَكْشِفُ مُجَسَّمَ الحَرْفِ فِي المَسْرَحِ ثُلَاثِيِّ الأَبْعَادِ ثُمَّ نَرْبِطُ كُلَّ حَرْفٍ بِكَلِمَتِهِ.",
                                            SummaryFr = "Explorons la lettre en 3D puis associons chaque lettre à son mot illustré.",
                                            SummaryEn = "Explore the 3D letter stage and match each Arabic letter to its anchor word.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = alifBaTaLessonId,
                                                    Slug = "age4-letters-3d-stage",
                                                    StepOrder = 1,
                                                    ActivityType = "scene_3d_explore",
                                                    ObjectiveCode = "L4-AR-01",
                                                    TitleAr = "مَسْرَحُ الحُرُوفِ العَرَبِيَّةِ ثُلَاثِيُّ الأَبْعَادِ",
                                                    TitleFr = "Théâtre 3D des lettres arabes",
                                                    TitleEn = "3D Arabic Letter Stage",
                                                    PromptAr = "اِضْغَطْ عَلَى مُجَسَّمِ الحَرْفِ أَوْ تَاجِ الفَتْحَةِ أَوْ نَمُوذَجِ الكَلِمَةِ فِي المَشْهَدِ!",
                                                    PromptFr = "Touche la lettre 3D, le signe vocalique ou l’objet illustré !",
                                                    PromptEn = "Tap the 3D letter pillar, the vowel crown, or the word model!",
                                                    SpokenTextAr = "أَلِفٌ: أَرْنَبٌ! بَاءٌ: بَطَّةٌ! تَاءٌ: تُفَّاحَةٌ!",
                                                    SpokenTextFr = "Alif : Arnab (lapin) ! Baa : Batta (canard) ! Taa : Tuffaha (pomme) !",
                                                    SpokenTextEn = "Alif for Arnab! Baa for Batta! Taa for Tuffaha!",
                                                    GlbModelUrl = "/models/arabic-letters-stage.glb",
                                                    TargetNodeName = "Letter_Pillar",
                                                    FocusX = 0.0f,
                                                    FocusY = 1.1f,
                                                    FocusZ = 0.0f,
                                                    InteractionPayloadJson = """{"sceneObjects":[{"nodeName":"Letter_Pillar","emoji":"أ","labelAr":"حَرْفُ الأَلِفِ (أ — أَرْنَبٌ)","labelFr":"Lettre Alif (أ — أَرْنَبٌ)","labelEn":"Letter Alif (أ — أَرْنَبٌ)","color":"#f4c76b"},{"nodeName":"Vowel_Fatha","emoji":"بَ","labelAr":"حَرْفُ البَاءِ (ب — بَطَّةٌ)","labelFr":"Lettre Baa (ب — بَطَّةٌ)","labelEn":"Letter Baa (ب — بَطَّةٌ)","color":"#38bdf8"},{"nodeName":"Word_Model","emoji":"🍎","labelAr":"حَرْفُ التَّاءِ (ت — تُفَّاحَةٌ)","labelFr":"Lettre Taa (ت — تُفَّاحَةٌ)","labelEn":"Letter Taa (ت — تُفَّاحَةٌ)","color":"#ef4444"}]}""",
                                                },
                                                new Activity
                                                {
                                                    LessonId = alifBaTaLessonId,
                                                    Slug = "age4-letters-pick-ba",
                                                    StepOrder = 2,
                                                    ActivityType = "audio_listen_pick",
                                                    ObjectiveCode = "L4-AR-01",
                                                    TitleAr = "أَيُّ كَلِمَةٍ تَبْدَأُ بِحَرْفِ البَاءِ (ب)؟",
                                                    TitleFr = "Quel mot commence par la lettre Baa (ب) ?",
                                                    TitleEn = "Which Word Starts with the Letter Baa (ب)?",
                                                    PromptAr = "اِسْتَمِعْ إِلَى الصَّوْتِ: «ب — بَطَّةٌ». اِخْتَرْ بِطَاقَةَ حَرْفِ البَاءِ:",
                                                    PromptFr = "Écoute : « ب — بَطَّةٌ ». Choisis la carte de la lettre ب :",
                                                    PromptEn = "Listen: 'ب — بَطَّةٌ (Batta)'. Pick the card for letter ب:",
                                                    SpokenTextAr = "بَاءٌ: بَطَّةٌ! اِخْتَرْ حَرْفَ البَاءِ.",
                                                    SpokenTextFr = "Baa : Batta ! Choisis la lettre Baa.",
                                                    SpokenTextEn = "Baa: Batta! Choose the letter Baa.",
                                                    InteractionPayloadJson = """{"correctOptionId":"letter-ba","options":[{"id":"letter-alif","emoji":"🐰","labelAr":"أ — أَرْنَبٌ","labelFr":"أ — أَرْنَبٌ (Lapin)","labelEn":"أ — أَرْنَبٌ (Rabbit)","accentColor":"#f4c76b"},{"id":"letter-ba","emoji":"🦆","labelAr":"ب — بَطَّةٌ","labelFr":"ب — بَطَّةٌ (Canard)","labelEn":"ب — بَطَّةٌ (Duck)","accentColor":"#38bdf8"},{"id":"letter-ta","emoji":"🍎","labelAr":"ت — تُفَّاحَةٌ","labelFr":"ت — تُفَّاحَةٌ (Pomme)","labelEn":"ت — تُفَّاحَةٌ (Apple)","accentColor":"#ef4444"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = letterSoundsCourseId,
                                    CollectionId = alphabetCollectionId,
                                    Slug = "arabic-initial-letter-sounds",
                                    TargetAge = 4,
                                    DisplayOrder = 2,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🔊",
                                    TitleAr = "أَصْوَاتُ الحُرُوفِ فِي أَوَّلِ الكَلِمَةِ",
                                    TitleFr = "Les sons des lettres au début des mots",
                                    TitleEn = "Initial Letter Sounds in Words",
                                    DescriptionAr = "نَسْتَمِعُ إِلَى الكَلِمَةِ وَنَكْتَشِفُ الحَرْفَ الأَوَّلَ الَّذِي تَبْدَأُ بِهِ.",
                                    DescriptionFr = "Écoutons le mot et trouvons sa première lettre.",
                                    DescriptionEn = "Listen to the word and identify its starting Arabic letter.",
                                    CompletionCriteriaAr = "رَبْطُ الصَّوْتِ الأَوَّلِ بِالحَرْفِ الصَّحِيحِ.",
                                    CompletionCriteriaFr = "Associer le son initial à la bonne lettre.",
                                    CompletionCriteriaEn = "Match the initial sound to the correct Arabic letter.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = letterSoundsCourseId,
                                            Code = "L4-AR-02",
                                            DisplayOrder = 1,
                                            DescriptionAr = "رَبْطُ الحَرْفِ العَرَبِيِّ بِصَوْتِهِ فِي بِدَايَةِ الكَلِمَةِ المَأْلُوفَةِ.",
                                            DescriptionFr = "Associer chaque lettre arabe à son son initial dans un mot familier.",
                                            DescriptionEn = "Associate each Arabic letter with its initial phonetic sound in a word.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = letterSoundsLessonId,
                                            CourseId = letterSoundsCourseId,
                                            Slug = "jim-jamal-dal-dubb-sounds",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 4,
                                            TitleAr = "صَوْتُ الجِيمِ (جَمَلٌ) وَصَوْتُ السِّينِ (سَمَكَةٌ)",
                                            TitleFr = "Le son Jim (جَمَلٌ) et le son Sin (سَمَكَةٌ)",
                                            TitleEn = "Sound of Jim (جَمَلٌ) and Sound of Sin (سَمَكَةٌ)",
                                            SummaryAr = "مَيِّزِ الحَرْفَ الأَوَّلَ عِنْدَ سَمَاعِ الكَلِمَةِ.",
                                            SummaryFr = "Identifie la première lettre en écoutant le mot.",
                                            SummaryEn = "Identify the starting letter when hearing the spoken Arabic word.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = letterSoundsLessonId,
                                                    Slug = "age4-sound-match-jamal",
                                                    StepOrder = 1,
                                                    ActivityType = "audio_listen_pick",
                                                    ObjectiveCode = "L4-AR-02",
                                                    TitleAr = "بِأَيِّ حَرْفٍ تَبْدَأُ كَلِمَةُ «جَمَلٌ»؟",
                                                    TitleFr = "Par quelle lettre commence « جَمَلٌ » ?",
                                                    TitleEn = "Which Letter Starts the Word 'جَمَلٌ'?",
                                                    PromptAr = "كَلِمَةُ «جَمَلٌ» تَبْدَأُ بِصَوْتِ (جَ)! اِخْتَرْ حَرْفَ الجِيمِ:",
                                                    PromptFr = "Le mot « جَمَلٌ » commence par (ج) ! Choisis la lettre Jim :",
                                                    PromptEn = "The word 'جَمَلٌ (Jamal)' starts with ج! Choose the letter Jim:",
                                                    SpokenTextAr = "جَمَلٌ! جِيمٌ: جَمَلٌ!",
                                                    SpokenTextFr = "Jamal ! Lettre Jim : Jamal !",
                                                    SpokenTextEn = "Jamal! Letter Jim: Jamal!",
                                                    InteractionPayloadJson = """{"correctOptionId":"jim-jamal","options":[{"id":"jim-jamal","emoji":"🐪","labelAr":"ج — جَمَلٌ","labelFr":"ج — جَمَلٌ","labelEn":"ج — جَمَلٌ","accentColor":"#f59e0b"},{"id":"sin-samaka","emoji":"🐟","labelAr":"س — سَمَكَةٌ","labelFr":"س — سَمَكَةٌ","labelEn":"س — سَمَكَةٌ","accentColor":"#38bdf8"},{"id":"qaf-qamar","emoji":"🌙","labelAr":"ق — قَمَرٌ","labelFr":"ق — قَمَرٌ","labelEn":"ق — قَمَرٌ","accentColor":"#a855f7"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = countingDomainId,
                    AgeGroupId = age4Id,
                    Slug = "age-4-counting-1-to-10",
                    DisplayOrder = 2,
                    IconEmoji = "🔟",
                    AccentColor = "#38bdf8",
                    TitleAr = "العَدُّ (1–10) وَالمُقَارَنَاتُ",
                    TitleFr = "Comptage de 1 à 10 et quantités",
                    TitleEn = "Counting 1 to 10 and Quantities",
                    SummaryAr = "نَعُدُّ الأَشْيَاءَ مِنْ 1 إِلَى 10 وَنُقَارِنُ بَيْنَ الأَكْثَرِ وَالأَقَلِّ.",
                    SummaryFr = "Compter les objets de 1 à 10 et comparer les quantités.",
                    SummaryEn = "Count objects from 1 to 10 and compare more vs. less.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = countingCollectionId,
                            DomainId = countingDomainId,
                            Slug = "playful-numbers-1-to-10",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ أَرْقَامِي المَرِحَةِ (1–10)",
                            TitleFr = "Collection Mes nombres joyeux (1 à 10)",
                            TitleEn = "Playful Numbers 1 to 10 Collection",
                            SummaryAr = "رَبْطُ الكَمِّيَّاتِ بِرُمُوزِ الأَعْدَادِ مِنْ 1 إِلَى 10.",
                            SummaryFr = "Relier les quantités aux chiffres de 1 à 10.",
                            SummaryEn = "Connect quantities to numerals from 1 to 10.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = numbers1To10CourseId,
                                    CollectionId = countingCollectionId,
                                    Slug = "counting-fruits-1-to-10",
                                    TargetAge = 4,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🍓",
                                    TitleAr = "نَعُدُّ الفَوَاكِهَ وَالنُّجُومَ مِنْ 1 إِلَى 10",
                                    TitleFr = "Comptons les fruits et les étoiles de 1 à 10",
                                    TitleEn = "Counting Fruits and Stars 1 to 10",
                                    DescriptionAr = "نَعُدُّ المَجْمُوعَاتِ وَنَخْتَارُ الرَّقْمَ الصَّحِيحَ.",
                                    DescriptionFr = "Comptons les groupes d’objets et choisissons le bon nombre.",
                                    DescriptionEn = "Count groups of objects and select the matching number.",
                                    CompletionCriteriaAr = "عَدُّ المَجْمُوعَاتِ حَتَّى 10 بِدِقَّةٍ.",
                                    CompletionCriteriaFr = "Compter avec précision jusqu’à 10.",
                                    CompletionCriteriaEn = "Accurately count groups up to 10.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = numbers1To10CourseId,
                                            Code = "L4-NM-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "عَدُّ الأَشْيَاءِ مِنْ 1 إِلَى 10 وَرَبْطُهَا بِرَمْزِ العَدَدِ.",
                                            DescriptionFr = "Compter de 1 à 10 et associer la quantité au chiffre.",
                                            DescriptionEn = "Count objects from 1 to 10 and match them to their numeral.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = countTo10LessonId,
                                            CourseId = numbers1To10CourseId,
                                            Slug = "five-apples-and-seven-stars",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 4,
                                            TitleAr = "خَمْسُ تُفَّاحَاتٍ (٥) وَسَبْعُ نُجُومٍ (٧)",
                                            TitleFr = "Cinq pommes (5) et sept étoiles (7)",
                                            TitleEn = "Five Apples (5) and Seven Stars (7)",
                                            SummaryAr = "عُدَّ التُّفَّاحَاتِ وَاخْتَرِ الرَّقْمَ الصَّحِيحَ.",
                                            SummaryFr = "Compte les pommes et choisis le bon chiffre.",
                                            SummaryEn = "Count the apples and choose the matching number.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = countTo10LessonId,
                                                    Slug = "age4-count-five-apples",
                                                    StepOrder = 1,
                                                    ActivityType = "counting_interactive",
                                                    ObjectiveCode = "L4-NM-01",
                                                    TitleAr = "كَمْ تُفَّاحَةً فِي السَّلَّةِ؟ (🍎🍎🍎🍎🍎)",
                                                    TitleFr = "Combien de pommes dans le panier ? (🍎🍎🍎🍎🍎)",
                                                    TitleEn = "How Many Apples Are in the Basket? (🍎🍎🍎🍎🍎)",
                                                    PromptAr = "لِنَعُدَّ التُّفَّاحَاتِ مَعاً: ١، ٢، ٣، ٤، ٥! اِخْتَرِ العَدَدَ خَمْسَةً (٥):",
                                                    PromptFr = "Comptons les pommes : 1, 2, 3, 4, 5 ! Choisis le nombre 5 :",
                                                    PromptEn = "Let's count the apples: 1, 2, 3, 4, 5! Pick number 5:",
                                                    SpokenTextAr = "وَاحِدٌ، اِثْنَانِ، ثَلَاثَةٌ، أَرْبَعَةٌ، خَمْسَةٌ! خَمْسُ تُفَّاحَاتٍ!",
                                                    SpokenTextFr = "Un, deux, trois, quatre, cinq ! Cinq pommes !",
                                                    SpokenTextEn = "One, two, three, four, five! Five apples!",
                                                    InteractionPayloadJson = """{"countingItems":{"emoji":"🍎","count":5,"itemNameAr":"تُفَّاحَاتٍ حَمْرَاءَ","itemNameFr":"Pommes rouges","itemNameEn":"Red Apples"},"correctOptionId":"num-5","options":[{"id":"num-3","emoji":"3️⃣","labelAr":"٣ — ثَلَاثَةٌ","labelFr":"3 — Trois","labelEn":"3 — Three","accentColor":"#38bdf8"},{"id":"num-5","emoji":"5️⃣","labelAr":"٥ — خَمْسَةٌ","labelFr":"5 — Cinq","labelEn":"5 — Five","accentColor":"#4cc38a"},{"id":"num-8","emoji":"8️⃣","labelAr":"٨ — ثَمَانِيَةٌ","labelFr":"8 — Huit","labelEn":"8 — Eight","accentColor":"#f59e0b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = natureLogicDomainId,
                    AgeGroupId = age4Id,
                    Slug = "age-4-nature-habitats-seasons",
                    DisplayOrder = 3,
                    IconEmoji = "🌳",
                    AccentColor = "#4cc38a",
                    TitleAr = "الطَّبِيعَةُ، الفُصُولُ، وَالتَّصْنِيفُ المَنْطِقِيُّ",
                    TitleFr = "Nature, saisons et tri logique",
                    TitleEn = "Nature, Seasons, and Logical Sorting",
                    SummaryAr = "نَكْتَشِفُ بُيُوتَ الحَيَوَانَاتِ وَالفُصُولَ الأَرْبَعَةَ وَالأَنْمَاطَ البَصَرِيَّةَ.",
                    SummaryFr = "Découvrons les habitats des animaux, les quatre saisons et les suites logiques.",
                    SummaryEn = "Explore animal habitats, the four seasons, and visual patterns.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = natureCollectionId,
                            DomainId = natureLogicDomainId,
                            Slug = "exploring-nature-and-seasons",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ نَسْتَكْشِفُ الطَّبِيعَةَ وَالفُصُولَ",
                            TitleFr = "Collection Explorons la nature et les saisons",
                            TitleEn = "Exploring Nature and Seasons Collection",
                            SummaryAr = "أَيْنَ تَعِيشُ الحَيَوَانَاتُ وَكَيْفَ تَتَغَيَّرُ الطَّبِيعَةُ فِي الرَّبِيعِ وَالشِّتَاءِ؟",
                            SummaryFr = "Où vivent les animaux et comment change la nature au fil des saisons ?",
                            SummaryEn = "Where do animals live and how does nature change across seasons?",
                            Courses =
                            [
                                new Course
                                {
                                    Id = habitatsSeasonsCourseId,
                                    CollectionId = natureCollectionId,
                                    Slug = "animal-homes-and-four-seasons",
                                    TargetAge = 4,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🐬",
                                    TitleAr = "بُيُوتُ الحَيَوَانَاتِ وَالفُصُولُ الأَرْبَعَةُ",
                                    TitleFr = "Les habitats des animaux et les quatre saisons",
                                    TitleEn = "Animal Habitats and the Four Seasons",
                                    DescriptionAr = "نَصِلُ كُلَّ حَيَوَانٍ بِبِيئَتِهِ الطَّبِيعِيَّةِ (البَحْرُ، الغَابَةُ، المَزْرَعَةُ).",
                                    DescriptionFr = "Associons chaque animal à son habitat naturel (mer, forêt, ferme).",
                                    DescriptionEn = "Match each animal to its natural habitat (sea, forest, farm).",
                                    CompletionCriteriaAr = "تَصْنِيفُ الحَيَوَانَاتِ حَسَبَ بِيئَتِهَا الصَّحِيحَةِ.",
                                    CompletionCriteriaFr = "Classer les animaux selon leur habitat.",
                                    CompletionCriteriaEn = "Sort animals into their correct natural habitats.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = habitatsSeasonsCourseId,
                                            Code = "L4-AN-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "تَصْنِيفُ الحَيَوَانَاتِ حَسَبَ بِيئَاتِهَا الطَّبِيعِيَّةِ (المَزْرَعَةُ، الغَابَةُ، البَحْرُ).",
                                            DescriptionFr = "Classer les animaux selon leur habitat naturel.",
                                            DescriptionEn = "Group animals by their natural habitats (farm, forest, sea).",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = habitatsLessonId,
                                            CourseId = habitatsSeasonsCourseId,
                                            Slug = "where-does-the-fish-live",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 4,
                                            TitleAr = "أَيْنَ تَعِيشُ السَّمَكَةُ وَالأَسَدُ؟",
                                            TitleFr = "Où vivent le poisson et le lion ?",
                                            TitleEn = "Where Do the Fish and Lion Live?",
                                            SummaryAr = "طَابِقِ الحَيَوَانَ مَعَ المَكَانِ الَّذِي يَعِيشُ فِيهِ.",
                                            SummaryFr = "Associe l’animal à l’endroit où il vit.",
                                            SummaryEn = "Match the animal with its natural home.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = habitatsLessonId,
                                                    Slug = "age4-habitat-fish-sea",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L4-AN-01",
                                                    TitleAr = "أَيْنَ تَسْبَحُ السَّمَكَةُ المُلَوَّنَةُ (🐠)؟",
                                                    TitleFr = "Où nage le poisson coloré (🐠) ?",
                                                    TitleEn = "Where Does the Colorful Fish (🐠) Swim?",
                                                    PromptAr = "السَّمَكَةُ تَسْبَحُ فِي المَاءِ! اِخْتَرْ بِيئَةَ البَحْرِ:",
                                                    PromptFr = "Le poisson nage dans l’eau ! Choisis la mer :",
                                                    PromptEn = "The fish swims in the water! Pick the sea habitat:",
                                                    SpokenTextAr = "السَّمَكَةُ تَعِيشُ وَتَسْبَحُ فِي البَحْرِ الأَزْرَقِ!",
                                                    SpokenTextFr = "Le poisson vit et nage dans la mer bleue !",
                                                    SpokenTextEn = "The fish lives and swims in the blue sea!",
                                                    InteractionPayloadJson = """{"correctOptionId":"habitat-sea","options":[{"id":"habitat-sea","emoji":"🌊","labelAr":"البَحْرُ الأَزْرَقُ","labelFr":"La mer bleue","labelEn":"The Blue Sea","accentColor":"#38bdf8"},{"id":"habitat-desert","emoji":"🏜️","labelAr":"الصَّحْرَاءُ الرَّمْلِيَّةُ","labelFr":"Le désert","labelEn":"The Sandy Desert","accentColor":"#f59e0b"},{"id":"habitat-forest","emoji":"🌲","labelAr":"الغَابَةُ الخَضْرَاءُ","labelFr":"La forêt","labelEn":"The Green Forest","accentColor":"#4cc38a"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = patternsDomainId,
                    AgeGroupId = age4Id,
                    Slug = "age-4-colors-shapes-and-patterns",
                    DisplayOrder = 4,
                    IconEmoji = "🎨",
                    AccentColor = "#a855f7",
                    TitleAr = "الأَلْوَانُ، الأَشْكَالُ، وَالأَنْمَاطُ المَنْطِقِيَّةُ",
                    TitleFr = "Mélange de couleurs, formes et suites logiques",
                    TitleEn = "Color Mixing, Shapes, and Visual Patterns",
                    SummaryAr = "نَمْزِجُ الأَلْوَانَ لِنَكْتَشِفَ أَلْوَاناً جَدِيدَةً وَنُكْمِلُ الأَنْمَاطَ المُتَكَرِّرَةَ.",
                    SummaryFr = "Mélanger les couleurs et compléter des suites visuelles.",
                    SummaryEn = "Mix primary colors to discover new colors and complete visual patterns.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = patternsCollectionId,
                            DomainId = patternsDomainId,
                            Slug = "patterns-and-visual-logic",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ الأَنْمَاطِ وَالذَّكَاءِ البَصَرِيِّ",
                            TitleFr = "Collection Motifs et logique visuelle",
                            TitleEn = "Patterns and Visual Logic Collection",
                            SummaryAr = "مَزْجُ الأَلْوَانِ الأَسَاسِيَّةِ وَإِكْمَالُ الأَنْمَاطِ المُتَنَاوِبَةِ.",
                            SummaryFr = "Mélange de couleurs et suites logiques alternées.",
                            SummaryEn = "Primary color mixing and alternating visual patterns.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = patternsCourseId,
                                    CollectionId = patternsCollectionId,
                                    Slug = "color-mixing-and-patterns",
                                    TargetAge = 4,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🌈",
                                    TitleAr = "مَزْجُ الأَلْوَانِ وَالأَنْمَاطُ المُتَكَرِّرَةُ",
                                    TitleFr = "Mélange de couleurs et suites répétitives",
                                    TitleEn = "Color Mixing and Repeating Patterns",
                                    DescriptionAr = "نَكْتَشِفُ مَاذَا يَحْدُثُ عِنْدَ مَزْجِ الأَصْفَرِ وَالأَزْرَقِ، وَنُكْمِلُ نَمَطَ الأَشْكَالِ.",
                                    DescriptionFr = "Découvrons ce que donnent le jaune et le bleu mélangés.",
                                    DescriptionEn = "Discover what happens when yellow and blue mix together.",
                                    CompletionCriteriaAr = "مَعْرِفَةُ نَاتِجِ مَزْجِ الأَلْوَانِ وَإِكْمَالُ النَّمَطِ البَصَرِيِّ.",
                                    CompletionCriteriaFr = "Connaître le résultat du mélange des couleurs.",
                                    CompletionCriteriaEn = "Identify the result of mixing primary colors.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = patternsCourseId,
                                            Code = "L4-CL-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "مُلَاحَظَةُ مَزْجِ الأَلْوَانِ البَسِيطَةِ وَإِكْمَالُ الأَنْمَاطِ البَصَرِيَّةِ.",
                                            DescriptionFr = "Observer le mélange simple des couleurs et compléter un motif.",
                                            DescriptionEn = "Observe simple color mixing and complete visual patterns.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = patternsLessonId,
                                            CourseId = patternsCourseId,
                                            Slug = "yellow-plus-blue-makes-green",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 4,
                                            TitleAr = "الأَصْفَرُ (💛) + الأَزْرَقُ (💙) = الأَخْضَرُ (💚)",
                                            TitleFr = "Jaune (💛) + Bleu (💙) = Vert (💚)",
                                            TitleEn = "Yellow (💛) + Blue (💙) = Green (💚)",
                                            SummaryAr = "اِكْتَشِفْ سِحْرَ مَزْجِ الأَلْوَانِ مَعَ بِطَاقَاتِ التَّعَلُّمِ!",
                                            SummaryFr = "Découvre la magie du mélange des couleurs !",
                                            SummaryEn = "Discover the magic of mixing colors with interactive cards!",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = patternsLessonId,
                                                    Slug = "age4-color-mixing-green",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L4-CL-01",
                                                    TitleAr = "مَا اللَّوْنُ النَّاتِجُ عِنْدَ مَزْجِ الأَصْفَرِ (💛) وَالأَزْرَقِ (💙)؟",
                                                    TitleFr = "Quelle couleur obtient-on en mélangeant le jaune (💛) et le bleu (💙) ?",
                                                    TitleEn = "What Color Do We Get When We Mix Yellow (💛) and Blue (💙)?",
                                                    PromptAr = "عِنْدَمَا نَمْزِجُ اللَّوْنَ الأَصْفَرَ مَعَ اللَّوْنِ الأَزْرَقِ يَظْهَرُ لَوْنُ أَوْرَاقِ الشَّجَرِ! اِخْتَرِ اللَّوْنَ الصَّحِيحَ:",
                                                    PromptFr = "Quand on mélange le jaune et le bleu, on obtient la couleur des feuilles ! Choisis-la :",
                                                    PromptEn = "When we mix yellow and blue, we get the color of tree leaves! Pick it:",
                                                    SpokenTextAr = "اللَّوْنُ الأَصْفَرُ مَعَ اللَّوْنِ الأَزْرَقِ يُعْطِينَا اللَّوْنَ الأَخْضَرَ الجَمِيلَ!",
                                                    SpokenTextFr = "Le jaune mélangé au bleu donne une belle couleur verte !",
                                                    SpokenTextEn = "Yellow mixed with blue gives us a bright green color!",
                                                    InteractionPayloadJson = """{"learningCards":[{"emoji":"💛","titleAr":"اللَّوْنُ الأَصْفَرُ","titleFr":"Jaune","titleEn":"Yellow","factAr":"كَالشَّمْسِ المُشْرِقَةِ وَالمَوْزِ اللَّذِيذِ.","factFr":"Comme le soleil brillant.","factEn":"Bright like the sun and bananas.","accentColor":"#facc15"},{"emoji":"💙","titleAr":"اللَّوْنُ الأَزْرَقُ","titleFr":"Bleu","titleEn":"Blue","factAr":"كَالسَّمَاءِ الصَّافِيَةِ وَمِيَاهِ البَحْرِ.","factFr":"Comme le ciel et la mer.","factEn":"Clear like the sky and ocean.","accentColor":"#3b82f6"},{"emoji":"💚","titleAr":"أَصْفَرُ + أَزْرَقُ = أَخْضَرُ","titleFr":"Jaune + Bleu = Vert","titleEn":"Yellow + Blue = Green","factAr":"يَظْهَرُ اللَّوْنُ الأَخْضَرُ كَأَوْرَاقِ الأَشْجَارِ وَالعُشْبِ!","factFr":"Le vert apparaît comme l’herbe et les feuilles !","factEn":"Green appears just like grass and leaves!","accentColor":"#22c55e"}],"correctOptionId":"mix-green","options":[{"id":"mix-green","emoji":"💚","labelAr":"الأَخْضَرُ (أَصْفَرُ + أَزْرَقُ)","labelFr":"Le vert (Jaune + Bleu)","labelEn":"Green (Yellow + Blue)","accentColor":"#22c55e"},{"id":"mix-purple","emoji":"💜","labelAr":"البَنَفْسَجِيُّ","labelFr":"Le violet","labelEn":"Purple","accentColor":"#a855f7"},{"id":"mix-orange","emoji":"🧡","labelAr":"البُرْتُقَالِيُّ","labelFr":"L’orange","labelEn":"Orange","accentColor":"#f97316"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
            ],
        };
    }

    private static AgeGroup BuildAge5LearningWorld()
    {
        var age5Id = Guid.Parse("10000000-0000-0000-0000-000000000005");
        var arabicDomainId = Guid.Parse("20000000-0000-0000-0005-000000000001");
        var mathDomainId = Guid.Parse("20000000-0000-0000-0005-000000000002");
        var logicNatureDomainId = Guid.Parse("20000000-0000-0000-0005-000000000003");
        var scienceNatureDomainId = Guid.Parse("20000000-0000-0000-0005-000000000004");

        var readingPathwayCollectionId = Guid.Parse("30000000-0000-0000-0005-000000000001");
        var mathCollectionId = Guid.Parse("30000000-0000-0000-0005-000000000002");
        var logicCollectionId = Guid.Parse("30000000-0000-0000-0005-000000000003");
        var scienceCollectionId = Guid.Parse("30000000-0000-0000-0005-000000000004");

        // The 6 sequential courses in Age 5's dedicated reading pathway (مَسَارُ تَعَلُّمِ القِرَاءَةِ)
        var c1LetterDiscoveryId = Guid.Parse("40000000-0000-0000-0005-000000000001");
        var c2LetterFormsId = Guid.Parse("40000000-0000-0000-0005-000000000002");
        var c3ShortVowelsId = Guid.Parse("40000000-0000-0000-0005-000000000003");
        var c4SyllablesId = Guid.Parse("40000000-0000-0000-0005-000000000004");
        var c5WordReadingId = Guid.Parse("40000000-0000-0000-0005-000000000005");
        var c6SentenceReadingId = Guid.Parse("40000000-0000-0000-0005-000000000006");

        var mathAdditionCourseId = Guid.Parse("40000000-0000-0000-0005-000000000007");
        var plantLogicCourseId = Guid.Parse("40000000-0000-0000-0005-000000000008");
        var dayNightCourseId = Guid.Parse("40000000-0000-0000-0005-000000000009");

        var l1LetterDiscoveryId = Guid.Parse("50000000-0000-0000-0005-000000000001");
        var l2LetterFormsId = Guid.Parse("50000000-0000-0000-0005-000000000002");
        var l3ShortVowelsId = Guid.Parse("50000000-0000-0000-0005-000000000003");
        var l4SyllablesId = Guid.Parse("50000000-0000-0000-0005-000000000004");
        var l5WordReadingId = Guid.Parse("50000000-0000-0000-0005-000000000005");
        var l6SentenceReadingId = Guid.Parse("50000000-0000-0000-0005-000000000006");
        var l7AdditionId = Guid.Parse("50000000-0000-0000-0005-000000000007");
        var l8PlantGrowthId = Guid.Parse("50000000-0000-0000-0005-000000000008");
        var l9DayNightId = Guid.Parse("50000000-0000-0000-0005-000000000009");

        return new AgeGroup
        {
            Id = age5Id,
            AgeYears = 5,
            Slug = "age-5-learning",
            ThemeKey = "learning",
            IconEmoji = "🌟",
            DisplayOrder = 3,
            TitleAr = "عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ",
            TitleFr = "Monde de l’Apprentissage — 5 ans",
            TitleEn = "World of Learning — Age 5",
            SubtitleAr = "مَسَارُ تَعَلُّمِ القِرَاءَةِ العَرَبِيَّةِ، الحِسَابُ المُبَسَّطُ، وَالإِعْدَادُ المَدْرَسِيُّ",
            SubtitleFr = "Parcours de lecture arabe, calcul simple et préparation à l’école",
            SubtitleEn = "Arabic reading pathway, early addition, and school readiness",
            DescriptionAr = "مَنْهَجٌ مُتَكَامِلٌ لِتَرْسِيخِ الحُرُوفِ وَأَشْكَالِهَا وَالحَرَكَاتِ القَصِيرَةِ (الفَتْحَةُ، الكَسْرَةُ، الضَّمَّةُ) وَبِنَاءِ المَقَاطِعِ وَقِرَاءَةِ الكَلِمَاتِ وَالجُمَلِ البَسِيطَةِ، مَعَ الجَمْعِ وَالتَّفْكِيرِ المَنْطِقِيِّ.",
            DescriptionFr = "Programme complet pour maîtriser les lettres arabes, leurs formes, les voyelles courtes, les syllabes, la lecture de mots et de phrases simples, ainsi que l’addition.",
            DescriptionEn = "A comprehensive school-readiness curriculum featuring the 6-step Arabic Reading Pathway (letters, forms, short vowels, syllables, words, and sentences), simple addition, and logic.",
            Domains =
            [
                new LearningDomain
                {
                    Id = arabicDomainId,
                    AgeGroupId = age5Id,
                    Slug = "age-5-arabic-language-and-reading",
                    DisplayOrder = 1,
                    IconEmoji = "📖",
                    AccentColor = "#f4c76b",
                    TitleAr = "اللُّغَةُ العَرَبِيَّةُ",
                    TitleFr = "Langue arabe et lecture (اللُّغَةُ العَرَبِيَّةُ)",
                    TitleEn = "Arabic Language and Reading (اللُّغَةُ العَرَبِيَّةُ)",
                    SummaryAr = "مَسَارُ تَعَلُّمِ القِرَاءَةِ المُتَدَرِّجُ مِنْ إِتْقَانِ الحُرُوفِ وَأَشْكَالِهَا وَالحَرَكَاتِ القَصِيرَةِ إِلَى قِرَاءَةِ الكَلِمَاتِ وَالجُمَلِ المُشَكَّلَةِ.",
                    SummaryFr = "Parcours progressif d’apprentissage de la lecture arabe : des lettres et voyelles courtes jusqu’aux mots et phrases vocalisées.",
                    SummaryEn = "Step-by-step Arabic Reading Pathway from letter forms and short vowels to reading vocalized words and sentences.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = readingPathwayCollectionId,
                            DomainId = arabicDomainId,
                            Slug = "arabic-reading-pathway",
                            DisplayOrder = 1,
                            IsReadingPathway = true,
                            TitleAr = "مَسَارُ تَعَلُّمِ القِرَاءَةِ",
                            TitleFr = "Parcours d’apprentissage de la lecture (مَسَارُ تَعَلُّمِ القِرَاءَةِ)",
                            TitleEn = "Arabic Reading Pathway (مَسَارُ تَعَلُّمِ القِرَاءَةِ)",
                            SummaryAr = "سِتُّ دَوْرَاتٍ مُتَسَلْسِلَةٍ: اِكْتِشَافُ الحُرُوفِ ← أَشْكَالُ الحُرُوفِ ← الحَرَكَاتُ القَصِيرَةُ ← بِنَاءُ المَقَاطِعِ ← قِرَاءَةُ الكَلِمَاتِ ← قِرَاءَةُ الجُمَلِ البَسِيطَةِ.",
                            SummaryFr = "Six cours progressifs : découverte des lettres → formes des lettres → voyelles courtes → syllabes → lecture de mots → lecture de phrases simples.",
                            SummaryEn = "Six sequential courses: Letter Discovery → Letter Forms → Short Vowels → Syllable Construction → Word Reading → Simple Sentence Reading.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = c1LetterDiscoveryId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "letter-discovery-mastery",
                                    TargetAge = 5,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 1,
                                    IconEmoji = "🔤",
                                    TitleAr = "اِكْتِشَافُ الحُرُوفِ",
                                    TitleFr = "1. Découverte et maîtrise des lettres (اِكْتِشَافُ الحُرُوفِ)",
                                    TitleEn = "1. Letter Discovery & Mastery (اِكْتِشَافُ الحُرُوفِ)",
                                    DescriptionAr = "إِتْقَانُ الحُرُوفِ الهِجَائِيَّةِ وَالتَّفْرِيقُ بَيْنَ الحُرُوفِ المُتَشَابِهَةِ رَسْماً وَنُطْقاً.",
                                    DescriptionFr = "Maîtriser les 28 lettres arabes et distinguer les lettres proches par leurs points.",
                                    DescriptionEn = "Master all Arabic letters and distinguish visually similar letter families.",
                                    CompletionCriteriaAr = "التَّمْيِيزُ الدَّقِيقُ بَيْنَ الحُرُوفِ المُتَشَابِهَةِ (ب، ت، ث / ج، ح، خ).",
                                    CompletionCriteriaFr = "Distinguer sans erreur les familles de lettres arabes.",
                                    CompletionCriteriaEn = "Accurately distinguish Arabic letter families.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c1LetterDiscoveryId,
                                            Code = "L5-RD-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "إِتْقَانُ الحُرُوفِ الهِجَائِيَّةِ العَرَبِيَّةِ وَتَمْيِيزُ نِقَاطِهَا وَمَخَارِجِهَا.",
                                            DescriptionFr = "Maîtriser les lettres de l’alphabet arabe et leurs points diacritiques.",
                                            DescriptionEn = "Master the Arabic alphabet letters and their distinguishing dots.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l1LetterDiscoveryId,
                                            CourseId = c1LetterDiscoveryId,
                                            Slug = "distinguishing-ba-ta-tha-families",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "عَائِلَةُ الحُرُوفِ المُتَآخِيَةِ: (ب — ت — ث) وَ (ج — ح — خ)",
                                            TitleFr = "Familles de lettres : (ب — ت — ث) et (ج — ح — خ)",
                                            TitleEn = "Sibling Letter Families: (ب — ت — ث) and (ج — ح — خ)",
                                            SummaryAr = "نُلَاحِظُ مَكَانَ النُّقَاطِ وَنُمَيِّزُ صَوْتَ كُلِّ حَرْفٍ.",
                                            SummaryFr = "Observons la position des points et le son de chaque lettre.",
                                            SummaryEn = "Observe dot placement and distinguish the sound of each letter.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l1LetterDiscoveryId,
                                                    Slug = "age5-rd1-pick-tha",
                                                    StepOrder = 1,
                                                    ActivityType = "audio_listen_pick",
                                                    ObjectiveCode = "L5-RD-01",
                                                    TitleAr = "مَيِّزِ الحَرْفَ ذَا النِّقَاطِ الثَّلَاثِ: حَرْفُ الثَّاءِ (ث)",
                                                    TitleFr = "Trouve la lettre aux trois points : الثَّاء (ث)",
                                                    TitleEn = "Find the Letter with Three Dots: الثَّاء (ث)",
                                                    PromptAr = "حَرْفُ الثَّاءِ (ث) يَحْمِلُ ثَلَاثَ نِقَاطٍ فَوْقَهُ كَمَا فِي كَلِمَةِ «ثَعْلَبٌ». اِخْتَرْ حَرْفَ (ث):",
                                                    PromptFr = "La lettre الثَّاء (ث) porte trois points au-dessus, comme dans « ثَعْلَبٌ ». Choisis (ث) :",
                                                    PromptEn = "The letter الثَّاء (ث) has three dots above it, as in 'ثَعْلَبٌ'. Select (ث):",
                                                    SpokenTextAr = "ثَاءٌ: ثَعْلَبٌ! حَرْفُ الثَّاءِ فَوْقَهُ ثَلَاثُ نِقَاطٍ.",
                                                    SpokenTextFr = "Thaa : Tha'lab ! La lettre Thaa porte trois points.",
                                                    SpokenTextEn = "Thaa: Tha'lab! The letter Thaa has three dots on top.",
                                                    InteractionPayloadJson = """{"correctOptionId":"char-tha","options":[{"id":"char-ba","emoji":"🦆","labelAr":"ب — نُقْطَةٌ وَاحِدَةٌ تَحْتَ الحَرْفِ (بَطَّةٌ)","labelFr":"ب — Un point en dessous (بَطَّةٌ)","labelEn":"ب — One dot below (بَطَّةٌ)","accentColor":"#38bdf8"},{"id":"char-ta","emoji":"🍎","labelAr":"ت — نُقْطَتَانِ فَوْقَ الحَرْفِ (تُفَّاحَةٌ)","labelFr":"ت — Deux points au-dessus (تُفَّاحَةٌ)","labelEn":"ت — Two dots above (تُفَّاحَةٌ)","accentColor":"#4cc38a"},{"id":"char-tha","emoji":"🦊","labelAr":"ث — ثَلَاثُ نِقَاطٍ فَوْقَ الحَرْفِ (ثَعْلَبٌ)","labelFr":"ث — Trois points au-dessus (ثَعْلَبٌ)","labelEn":"ث — Three dots above (ثَعْلَبٌ)","accentColor":"#f4c76b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = c2LetterFormsId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "arabic-letter-forms",
                                    TargetAge = 5,
                                    DisplayOrder = 2,
                                    DifficultyLevel = 1,
                                    IconEmoji = "✍️",
                                    TitleAr = "أَشْكَالُ الحُرُوفِ",
                                    TitleFr = "2. Formes des lettres dans le mot (أَشْكَالُ الحُرُوفِ)",
                                    TitleEn = "2. Arabic Letter Forms (أَشْكَالُ الحُرُوفِ)",
                                    DescriptionAr = "تَعَلُّمُ شَكْلِ الحَرْفِ فِي أَوَّلِ الكَلِمَةِ وَوَسَطِهَا وَآخِرِهَا (مِثْلَ: بـ ، ـبـ ، ـب).",
                                    DescriptionFr = "Apprendre la forme de la lettre au début, au milieu et à la fin du mot (بـ ، ـبـ ، ـب).",
                                    DescriptionEn = "Learn how Arabic letters connect at the beginning, middle, and end of a word (بـ, ـبـ, ـب).",
                                    CompletionCriteriaAr = "اختِيَارُ شَكْلِ الحَرْفِ المُنَاسِبِ لِمَوْقِعِهِ فِي الكَلِمَةِ.",
                                    CompletionCriteriaFr = "Choisir la bonne forme de la lettre selon sa position dans le mot.",
                                    CompletionCriteriaEn = "Select the correct positional letter form within a word.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c2LetterFormsId,
                                            Code = "L5-RD-02",
                                            DisplayOrder = 1,
                                            DescriptionAr = "تَمْيِيزُ أَشْكَالِ الحَرْفِ العَرَبِيِّ فِي أَوَّلِ الكَلِمَةِ وَوَسَطِهَا وَآخِرِهَا.",
                                            DescriptionFr = "Reconnaître les formes initiale, médiane et finale des lettres arabes.",
                                            DescriptionEn = "Recognize initial, medial, and final Arabic letter forms.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l2LetterFormsId,
                                            CourseId = c2LetterFormsId,
                                            Slug = "letter-ba-and-sin-positions",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "حَرْفُ البَاءِ فِي أَوَّلِ الكَلِمَةِ وَوَسَطِهَا وَآخِرِهَا (بـ / ـبـ / ـب)",
                                            TitleFr = "La lettre Baa au début, au milieu et à la fin (بـ / ـبـ / ـب)",
                                            TitleEn = "Letter Baa at the Start, Middle, and End (بـ / ـبـ / ـب)",
                                            SummaryAr = "كَيْفَ يَمُدُّ الحَرْفُ يَدَهُ لِيَتَّصِلَ بِالحُرُوفِ الأُخْرَى فِي الكَلِمَةِ؟",
                                            SummaryFr = "Comment la lettre tend la main pour se relier aux autres lettres du mot ?",
                                            SummaryEn = "Discover how Arabic letters stretch their hands to connect inside words.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l2LetterFormsId,
                                                    Slug = "age5-rd2-ba-initial-form",
                                                    StepOrder = 1,
                                                    ActivityType = "letter_forms_vowels",
                                                    ObjectiveCode = "L5-RD-02",
                                                    TitleAr = "شَكْلُ حَرْفِ البَاءِ فِي أَوَّلِ كَلِمَةِ «بَيْتٌ»",
                                                    TitleFr = "Forme de la lettre Baa au début du mot « بَيْتٌ »",
                                                    TitleEn = "Form of Letter Baa at the Beginning of 'بَيْتٌ'",
                                                    PromptAr = "فِي بِدَايَةِ كَلِمَةِ «بَيْتٌ»، يَمُدُّ حَرْفُ البَاءِ يَدَهُ لِمَا بَعْدَهُ. اِخْتَرْ شَكْلَ البَاءِ فِي أَوَّلِ الكَلِمَةِ:",
                                                    PromptFr = "Au début du mot « بَيْتٌ », la lettre Baa se relie vers la gauche. Choisis sa forme initiale :",
                                                    PromptEn = "At the start of 'بَيْتٌ (Baytun)', letter Baa connects to the next letter. Pick the initial form:",
                                                    SpokenTextAr = "فِي أَوَّلِ الكَلِمَةِ نَكْتُبُ: بـ ، كَمَا فِي كَلِمَةِ: بَيْتٌ!",
                                                    SpokenTextFr = "Au début du mot, on écrit بـ comme dans Baytun (maison) !",
                                                    SpokenTextEn = "At the beginning of a word we write بـ, as in Baytun!",
                                                    InteractionPayloadJson = """{"letterStudio":{"baseLetter":"ب","forms":[{"form":"بـ","positionAr":"فِي أَوَّلِ الكَلِمَةِ","positionFr":"Début du mot","positionEn":"Initial Form","wordAr":"بَيْتٌ","wordFr":"بَيْتٌ (Maison)","wordEn":"بَيْتٌ (House)","emoji":"🏠","color":"#22c55e"},{"form":"ـبـ","positionAr":"فِي وَسَطِ الكَلِمَةِ","positionFr":"Milieu du mot","positionEn":"Medial Form","wordAr":"خُبْزٌ","wordFr":"خُبْزٌ (Pain)","wordEn":"خُبْزٌ (Bread)","emoji":"🍞","color":"#38bdf8"},{"form":"ـب","positionAr":"فِي آخِرِ الكَلِمَةِ","positionFr":"Fin du mot","positionEn":"Final Form","wordAr":"كِتَابٌ","wordFr":"كِتَابٌ (Livre)","wordEn":"كِتَابٌ (Book)","emoji":"📖","color":"#f59e0b"}]},"correctOptionId":"form-initial","options":[{"id":"form-initial","emoji":"🏠","labelAr":"بـ  (فِي أَوَّلِ الكَلِمَةِ: بَيْتٌ)","labelFr":"بـ  (Début du mot : بَيْتٌ)","labelEn":"بـ  (Initial form: بَيْتٌ)","accentColor":"#4cc38a"},{"id":"form-medial","emoji":"🍞","labelAr":"ـبـ (فِي وَسَطِ الكَلِمَةِ: خُبْزٌ)","labelFr":"ـبـ (Milieu du mot : خُبْزٌ)","labelEn":"ـبـ (Medial form: خُبْزٌ)","accentColor":"#38bdf8"},{"id":"form-final","emoji":"📖","labelAr":"ـب  (فِي آخِرِ الكَلِمَةِ: كِتَابٌ)","labelFr":"ـب  (Fin du mot : كِتَابٌ)","labelEn":"ـب  (Final form: كِتَابٌ)","accentColor":"#f4c76b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = c3ShortVowelsId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "short-vowels-harakat",
                                    TargetAge = 5,
                                    DisplayOrder = 3,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🎵",
                                    TitleAr = "الحَرَكَاتُ القَصِيرَةُ",
                                    TitleFr = "3. Les voyelles courtes : الفتحة، الكسرة، الضمة (الحَرَكَاتُ القَصِيرَةُ)",
                                    TitleEn = "3. Short Vowels: Fatha, Kasra, Damma (الحَرَكَاتُ القَصِيرَةُ)",
                                    DescriptionAr = "نَتَعَلَّمُ نُطْقَ الحُرُوفِ مَعَ الحَرَكَاتِ الثَّلَاثِ: الفَتْحَةِ (ـَ)، الكَسْرَةِ (ـِ)، وَالضَّمَّةِ (ـُ).",
                                    DescriptionFr = "Apprenons à prononcer les lettres avec Al-Fatha (ـَ), Al-Kasra (ـِ) et Ad-Damma (ـُ).",
                                    DescriptionEn = "Learn how short vowels — Fatha (ـَ), Kasra (ـِ), and Damma (ـُ) — give voice to every Arabic letter.",
                                    CompletionCriteriaAr = "التَّمْيِيزُ الصَّوْتِيُّ وَالبَصَرِيُّ بَيْنَ الفَتْحَةِ وَالكَسْرَةِ وَالضَّمَّةِ.",
                                    CompletionCriteriaFr = "Distinguer le son et le signe de la Fatha, de la Kasra et de la Damma.",
                                    CompletionCriteriaEn = "Distinguish the sound and mark of Fatha, Kasra, and Damma.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c3ShortVowelsId,
                                            Code = "L5-RD-03",
                                            DisplayOrder = 1,
                                            DescriptionAr = "قِرَاءَةُ الحُرُوفِ الهِجَائِيَّةِ مَعَ الحَرَكَاتِ القَصِيرَةِ الثَّلَاثِ (الفَتْحَةُ، الكَسْرَةُ، الضَّمَّةُ).",
                                            DescriptionFr = "Lire les lettres arabes avec les trois voyelles courtes (الفتحة، الكسرة، الضمة).",
                                            DescriptionEn = "Read Arabic letters with the three short vowels (الفتحة، الكسرة، الضمة).",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l3ShortVowelsId,
                                            CourseId = c3ShortVowelsId,
                                            Slug = "fatha-kasra-damma-with-ba-and-mim",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "الفَتْحَةُ (بَ) — الكَسْرَةُ (بِ) — الضَّمَّةُ (بُ)",
                                            TitleFr = "Al-Fatha (بَ) — Al-Kasra (بِ) — Ad-Damma (بُ)",
                                            TitleEn = "Al-Fatha (بَ) — Al-Kasra (بِ) — Ad-Damma (بُ)",
                                            SummaryAr = "نَفْتَحُ فَمَنَا مَعَ الفَتْحَةِ (بَ)، وَنَبْتَسِمُ مَعَ الكَسْرَةِ (بِ)، وَنَضُمُّ شَفَتَيْنَا مَعَ الضَّمَّةِ (بُ)!",
                                            SummaryFr = "Ouvrons la bouche avec la Fatha (بَ), sourions avec la Kasra (بِ) et arrondissons les lèvres avec la Damma (بُ) !",
                                            SummaryEn = "Open wide for Fatha (بَ), smile for Kasra (بِ), and round your lips for Damma (بُ)!",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l3ShortVowelsId,
                                                    Slug = "age5-rd3-harakat-3d-stage",
                                                    StepOrder = 1,
                                                    ActivityType = "scene_3d_explore",
                                                    ObjectiveCode = "L5-RD-03",
                                                    TitleAr = "تَاجُ الحَرَكَاتِ القَصِيرَةِ فِي المَشْهَدِ ثُلَاثِيِّ الأَبْعَادِ",
                                                    TitleFr = "Les voyelles courtes sur la scène 3D",
                                                    TitleEn = "Short Vowels on the 3D Letter Stage",
                                                    PromptAr = "اِضْغَطْ عَلَى تَاجِ الفَتْحَةِ وَمُجَسَّمِ الحَرْفِ فِي المَشْهَدِ لِسَمَاعِ النُّطْقِ الصَّحِيحِ!",
                                                    PromptFr = "Touche la couronne de la Fatha et la lettre 3D pour écouter la prononciation !",
                                                    PromptEn = "Tap the vowel crown and the 3D letter to hear the vocalized sound!",
                                                    SpokenTextAr = "بَ بِالفَتْحَةِ: بَطَّةٌ! بِ بِالكَسْرَةِ: بِطِّيخٌ! بُ بِالضَّمَّةِ: بُرْتُقَالَةٌ!",
                                                    SpokenTextFr = "Ba avec Fatha : Batta ! Bi avec Kasra : Bittikh ! Bu avec Damma : Burtuqala !",
                                                    SpokenTextEn = "Ba with Fatha: Batta! Bi with Kasra: Bittikh! Bu with Damma: Burtuqala!",
                                                    GlbModelUrl = "/models/arabic-letters-stage.glb",
                                                    TargetNodeName = "Vowel_Fatha",
                                                    FocusX = 0.0f,
                                                    FocusY = 1.6f,
                                                    FocusZ = 0.0f,
                                                    InteractionPayloadJson = """{"sceneObjects":[{"nodeName":"Vowel_Fatha","emoji":"🦆","labelAr":"بَ — الفَتْحَةُ فَوْقَ الحَرْفِ (بَطَّةٌ)","labelFr":"بَ — Al-Fatha (بَطَّةٌ)","labelEn":"بَ — Al-Fatha (بَطَّةٌ)","color":"#f4c76b"},{"nodeName":"Letter_Pillar","emoji":"🍉","labelAr":"بِ — الكَسْرَةُ تَحْتَ الحَرْفِ (بِطِّيخٌ)","labelFr":"بِ — Al-Kasra (بِطِّيخٌ)","labelEn":"بِ — Al-Kasra (بِطِّيخٌ)","color":"#4cc38a"},{"nodeName":"Word_Model","emoji":"🍊","labelAr":"بُ — الضَّمَّةُ فَوْقَ الحَرْفِ (بُرْتُقَالَةٌ)","labelFr":"بُ — Ad-Damma (بُرْتُقَالَةٌ)","labelEn":"بُ — Ad-Damma (بُرْتُقَالَةٌ)","color":"#fb923c"}]}""",
                                                },
                                                new Activity
                                                {
                                                    LessonId = l3ShortVowelsId,
                                                    Slug = "age5-rd3-pick-damma",
                                                    StepOrder = 2,
                                                    ActivityType = "letter_forms_vowels",
                                                    ObjectiveCode = "L5-RD-03",
                                                    TitleAr = "اِسْتَمِعْ وَاخْتَرِ الحَرْفَ المَضْمُومَ (بُ)",
                                                    TitleFr = "Écoute et choisis la lettre avec الضَّمَّة (بُ)",
                                                    TitleEn = "Listen and Pick the Letter with Damma (بُ)",
                                                    PromptAr = "كَلِمَةُ «بُرْتُقَالَةٌ» تَبْدَأُ بِالصَّوْتِ المَضْمُومِ «بُ». اِخْتَرْ (بُ):",
                                                    PromptFr = "Le mot « بُرْتُقَالَةٌ » commence par le son « بُ ». Choisis (بُ) :",
                                                    PromptEn = "The word 'بُرْتُقَالَةٌ' begins with 'بُ' (Damma). Select (بُ):",
                                                    SpokenTextAr = "بُ! بَاءٌ مَعَ الضَّمَّةِ: بُ — بُرْتُقَالَةٌ!",
                                                    SpokenTextFr = "Bu ! Baa avec Damma : Bu — Burtuqala !",
                                                    SpokenTextEn = "Bu! Baa with Damma: Bu — Burtuqala!",
                                                    InteractionPayloadJson = """{"letterStudio":{"baseLetter":"ب","forms":[{"form":"بَ","positionAr":"الفَتْحَةُ (ـَ)","positionFr":"Al-Fatha (ـَ)","positionEn":"Fatha (ـَ)","wordAr":"بَطَّةٌ","wordFr":"بَطَّةٌ (Canard)","wordEn":"بَطَّةٌ (Duck)","emoji":"🦆","color":"#f59e0b"},{"form":"بِ","positionAr":"الكَسْرَةُ (ـِ)","positionFr":"Al-Kasra (ـِ)","positionEn":"Kasra (ـِ)","wordAr":"بِطِّيخٌ","wordFr":"بِطِّيخٌ (Pastèque)","wordEn":"بِطِّيخٌ (Watermelon)","emoji":"🍉","color":"#22c55e"},{"form":"بُ","positionAr":"الضَّمَّةُ (ـُ)","positionFr":"Ad-Damma (ـُ)","positionEn":"Damma (ـُ)","wordAr":"بُرْتُقَالَةٌ","wordFr":"بُرْتُقَالَةٌ (Orange)","wordEn":"بُرْتُقَالَةٌ (Orange)","emoji":"🍊","color":"#f97316"}]},"correctOptionId":"vowel-bu","options":[{"id":"vowel-ba","emoji":"🦆","labelAr":"بَ — فَتْحَةٌ (بَطَّةٌ)","labelFr":"بَ — الفتحة (بَطَّةٌ)","labelEn":"بَ — Fatha (بَطَّةٌ)","accentColor":"#f4c76b"},{"id":"vowel-bi","emoji":"🍉","labelAr":"بِ — كَسْرَةٌ (بِطِّيخٌ)","labelFr":"بِ — الكسرة (بِطِّيخٌ)","labelEn":"بِ — Kasra (بِطِّيخٌ)","accentColor":"#4cc38a"},{"id":"vowel-bu","emoji":"🍊","labelAr":"بُ — ضَمَّةٌ (بُرْتُقَالَةٌ)","labelFr":"بُ — الضمة (بُرْتُقَالَةٌ)","labelEn":"بُ — Damma (بُرْتُقَالَةٌ)","accentColor":"#fb923c"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = c4SyllablesId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "syllable-construction",
                                    TargetAge = 5,
                                    DisplayOrder = 4,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🧩",
                                    TitleAr = "بِنَاءُ المَقَاطِعِ",
                                    TitleFr = "4. Construction de syllabes (بِنَاءُ المَقَاطِعِ)",
                                    TitleEn = "4. Syllable Construction (بِنَاءُ المَقَاطِعِ)",
                                    DescriptionAr = "نَجْمَعُ الحُرُوفَ المُتَحَرِّكَةَ وَالمُدُودَ لِنَبْنِيَ مَقَاطِعَ صَوْتِيَّةً وَاضِحَةً.",
                                    DescriptionFr = "Combinons les lettres vocalisées pour construire des syllabes.",
                                    DescriptionEn = "Combine vocalized letters and long vowels into clear Arabic syllables.",
                                    CompletionCriteriaAr = "دَمْجُ المَقَاطِعِ الصَّوْتِيَّةِ وَقِرَاءَتُهَا بِطَلَاقَةٍ.",
                                    CompletionCriteriaFr = "Assembler et lire correctement les syllabes arabes.",
                                    CompletionCriteriaEn = "Blend and read Arabic syllables accurately.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c4SyllablesId,
                                            Code = "L5-RD-04",
                                            DisplayOrder = 1,
                                            DescriptionAr = "تَرْكِيبُ المَقَاطِعِ الصَّوْتِيَّةِ مِنْ حُرُوفٍ وَحَرَكَاتٍ قَصِيرَةٍ وَطَوِيلَةٍ.",
                                            DescriptionFr = "Construire des syllabes à partir de lettres et de voyelles.",
                                            DescriptionEn = "Blend letters and vowels into spoken and written syllables.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l4SyllablesId,
                                            CourseId = c4SyllablesId,
                                            Slug = "blending-syllables-ka-ta-ba",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "نَبْنِي المَقَاطِعَ: (كَـ + تَـ + بَ) وَ (بَا / بِي / بُو)",
                                            TitleFr = "Construisons les syllabes : (كَـ + تَـ + بَ)",
                                            TitleEn = "Building Syllables: (كَـ + تَـ + بَ)",
                                            SummaryAr = "اِدمِجِ المَقَاطِعَ الصَّوْتِيَّةَ خُطْوَةً بِخُطْوَةٍ لِتَكْوِينِ كَلِمَةٍ كَامِلَةٍ.",
                                            SummaryFr = "Assemble les syllabes étape par étape pour former un mot.",
                                            SummaryEn = "Blend the syllables step by step to build a complete Arabic word.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l4SyllablesId,
                                                    Slug = "age5-rd4-build-kataba",
                                                    StepOrder = 1,
                                                    ActivityType = "syllable_word_builder",
                                                    ObjectiveCode = "L5-RD-04",
                                                    TitleAr = "اِدمِجِ المَقَاطِعَ الثَّلَاثَةَ: كَـ + تَـ + بَ",
                                                    TitleFr = "Assemble les trois syllabes : كَـ + تَـ + بَ",
                                                    TitleEn = "Blend the Three Syllables: كَـ + تَـ + بَ",
                                                    PromptAr = "عِنْدَمَا نَجْمَعُ المَقَاطِعَ: (كَـ) + (تَـ) + (بَ)، مَا الكَلِمَةُ الَّتِي نَحْصُلُ عَلَيْهَا؟",
                                                    PromptFr = "Quand on assemble (كَـ) + (تَـ) + (بَ), quel mot obtient-on ?",
                                                    PromptEn = "When we blend (كَـ) + (تَـ) + (بَ), which word do we build?",
                                                    SpokenTextAr = "كَـ .. تَـ .. بَ : كَتَبَ!",
                                                    SpokenTextFr = "Ka .. ta .. ba : Kataba !",
                                                    SpokenTextEn = "Ka .. ta .. ba: Kataba!",
                                                    InteractionPayloadJson = """{"syllables":["كَـ","تَـ","بَ"],"correctOptionId":"word-kataba","options":[{"id":"word-kataba","emoji":"✍️","labelAr":"كَتَبَ","labelFr":"كَتَبَ (Il a écrit)","labelEn":"كَتَبَ (Kataba — wrote)","accentColor":"#4cc38a"},{"id":"word-qaraa","emoji":"📖","labelAr":"قَرَأَ","labelFr":"قَرَأَ (Il a lu)","labelEn":"قَرَأَ (Qara'a — read)","accentColor":"#38bdf8"},{"id":"word-rasama","emoji":"🎨","labelAr":"رَسَمَ","labelFr":"رَسَمَ (Il a dessiné)","labelEn":"رَسَمَ (Rasama — drew)","accentColor":"#f4c76b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = c5WordReadingId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "simple-word-reading",
                                    TargetAge = 5,
                                    DisplayOrder = 5,
                                    DifficultyLevel = 3,
                                    IconEmoji = "🦁",
                                    TitleAr = "قِرَاءَةُ الكَلِمَاتِ",
                                    TitleFr = "5. Lecture de mots simples (قِرَاءَةُ الكَلِمَاتِ)",
                                    TitleEn = "5. Simple Word Reading (قِرَاءَةُ الكَلِمَاتِ)",
                                    DescriptionAr = "قِرَاءَةُ كَلِمَاتٍ عَرَبِيَّةٍ ثُلَاثِيَّةٍ مُشَكَّلَةٍ وَرَبْطُهَا بِالصُّوَرِ الدَّالَّةِ عَلَيْهَا.",
                                    DescriptionFr = "Lire des mots arabes vocalisés et les associer à leurs illustrations.",
                                    DescriptionEn = "Read vocalized 3-letter Arabic words and match them with illustrations.",
                                    CompletionCriteriaAr = "قِرَاءَةُ الكَلِمَاتِ البَسِيطَةِ وَمُطَابَقَتُهَا مَعَ الصُّورَةِ الصَّحِيحَةِ.",
                                    CompletionCriteriaFr = "Lire les mots simples et les relier à la bonne illustration.",
                                    CompletionCriteriaEn = "Read simple words and match them to the right illustration.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c5WordReadingId,
                                            Code = "L5-RD-05",
                                            DisplayOrder = 1,
                                            DescriptionAr = "قِرَاءَةُ كَلِمَاتٍ بَسِيطَةٍ مُشَكَّلَةٍ وَرَبْطُهَا بِمَعْنَاهَا البَصَرِيِّ.",
                                            DescriptionFr = "Décoder et lire des mots arabes simples vocalisés.",
                                            DescriptionEn = "Decode and read simple vocalized Arabic words and match to pictures.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l5WordReadingId,
                                            CourseId = c5WordReadingId,
                                            Slug = "read-words-asad-shams-qalam",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "أَقْرَأُ وَأُطَابِقُ: أَسَدٌ — شَمْسٌ — قَلَمٌ",
                                            TitleFr = "Je lis et j’associe : أَسَدٌ — شَمْسٌ — قَلَمٌ",
                                            TitleEn = "Read and Match: أَسَدٌ — شَمْسٌ — قَلَمٌ",
                                            SummaryAr = "تَهَجَّ حُرُوفَ الكَلِمَةِ المُشَكَّلَةِ وَاخْتَرِ الصُّورَةَ المُنَاسِبَةَ.",
                                            SummaryFr = "Lis le mot vocalisé et choisis l’illustration correspondante.",
                                            SummaryEn = "Read the vocalized word and choose the matching picture.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l5WordReadingId,
                                                    Slug = "age5-rd5-read-shams",
                                                    StepOrder = 1,
                                                    ActivityType = "syllable_word_builder",
                                                    ObjectiveCode = "L5-RD-05",
                                                    TitleAr = "اِقْرَأِ الكَلِمَةَ: «شَمْسٌ» وَاخْتَرْ صُورَتَهَا",
                                                    TitleFr = "Lis le mot « شَمْسٌ » et choisis son image",
                                                    TitleEn = "Read the Word 'شَمْسٌ' and Pick Its Picture",
                                                    PromptAr = "تَهَجَّ المَقَاطِعَ: (شَمْـ) + (سٌ) = «شَمْسٌ». أَيُّ صُورَةٍ تُمَثِّلُ كَلِمَةَ «شَمْسٌ»؟",
                                                    PromptFr = "Lis : (شَمْـ) + (سٌ) = « شَمْسٌ ». Quelle image correspond à « شَمْسٌ » ?",
                                                    PromptEn = "Read: (شَمْـ) + (سٌ) = 'شَمْسٌ (Shamsun)'. Which picture matches 'شَمْسٌ'?",
                                                    SpokenTextAr = "شَمْـ .. سٌ : شَمْسٌ! الشَّمْسُ تُضِيءُ السَّمَاءَ.",
                                                    SpokenTextFr = "Sham .. sun : Shamsun (Soleil) !",
                                                    SpokenTextEn = "Sham .. sun: Shamsun (Sun)!",
                                                    InteractionPayloadJson = """{"syllables":["شَمْـ","سٌ"],"correctOptionId":"pic-sun","options":[{"id":"pic-sun","emoji":"☀️","labelAr":"شَمْسٌ مُشْرِقَةٌ","labelFr":"شَمْسٌ (Soleil)","labelEn":"شَمْسٌ (Bright Sun)","accentColor":"#facc15"},{"id":"pic-lion","emoji":"🦁","labelAr":"أَسَدٌ قَوِيٌّ","labelFr":"أَسَدٌ (Lion)","labelEn":"أَسَدٌ (Lion)","accentColor":"#fb923c"},{"id":"pic-pen","emoji":"✏️","labelAr":"قَلَمٌ جَمِيلٌ","labelFr":"قَلَمٌ (Crayon)","labelEn":"قَلَمٌ (Pencil)","accentColor":"#38bdf8"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                                new Course
                                {
                                    Id = c6SentenceReadingId,
                                    CollectionId = readingPathwayCollectionId,
                                    Slug = "short-sentence-reading",
                                    TargetAge = 5,
                                    DisplayOrder = 6,
                                    DifficultyLevel = 3,
                                    IconEmoji = "🌟",
                                    TitleAr = "قِرَاءَةُ الجُمَلِ البَسِيطَةِ",
                                    TitleFr = "6. Lecture de phrases simples (قِرَاءَةُ الجُمَلِ البَسِيطَةِ)",
                                    TitleEn = "6. Reading Short Vocalized Sentences (قِرَاءَةُ الجُمَلِ البَسِيطَةِ)",
                                    DescriptionAr = "قِرَاءَةُ جُمَلٍ قَصِيرَةٍ مُشَكَّلَةٍ مِنْ كَلِمَتَيْنِ أَوْ ثَلَاثِ كَلِمَاتٍ وَفَهْمُ مَعْنَاهَا.",
                                    DescriptionFr = "Lire de courtes phrases arabes entièrement vocalisées et comprendre leur sens.",
                                    DescriptionEn = "Read short 2- to 3-word fully vocalized Arabic sentences and understand their meaning.",
                                    CompletionCriteriaAr = "قِرَاءَةُ الجُمْلَةِ البَسِيطَةِ المُشَكَّلَةِ وَرَبْطُهَا بِالمَشْهَدِ الصَّحِيحِ.",
                                    CompletionCriteriaFr = "Lire la phrase vocalisée et l’associer à la bonne scène.",
                                    CompletionCriteriaEn = "Read the vocalized sentence and match it to the correct scene.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = c6SentenceReadingId,
                                            Code = "L5-RD-06",
                                            DisplayOrder = 1,
                                            DescriptionAr = "قِرَاءَةُ جُمَلٍ عَرَبِيَّةٍ قَصِيرَةٍ مُشَكَّلَةٍ بِطَلَاقَةٍ وَفَهْمٍ.",
                                            DescriptionFr = "Lire et comprendre de courtes phrases arabes vocalisées.",
                                            DescriptionEn = "Read and comprehend short vocalized Arabic sentences.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l6SentenceReadingId,
                                            CourseId = c6SentenceReadingId,
                                            Slug = "read-short-vocalized-sentences",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 6,
                                            TitleAr = "أَقْرَأُ جُمْلَتِي الأُولَى: «أَشْرَقَتِ الشَّمْسُ» وَ «قَرَأَ سَامِي كِتَاباً»",
                                            TitleFr = "Je lis ma première phrase : « أَشْرَقَتِ الشَّمْسُ »",
                                            TitleEn = "Reading My First Sentence: 'أَشْرَقَتِ الشَّمْسُ'",
                                            SummaryAr = "نَقْرَأُ الكَلِمَاتِ مَعاً لِنُكَوِّنَ جُمْلَةً مُفِيدَةً وَجَمِيلَةً.",
                                            SummaryFr = "Lisons les mots ensemble pour former une phrase complète.",
                                            SummaryEn = "Read the words together to form a meaningful Arabic sentence.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l6SentenceReadingId,
                                                    Slug = "age5-rd6-sentence-sun-rose",
                                                    StepOrder = 1,
                                                    ActivityType = "syllable_word_builder",
                                                    ObjectiveCode = "L5-RD-06",
                                                    TitleAr = "اِقْرَأِ الجُمْلَةَ البَسِيطَةَ: «أَشْرَقَتِ الشَّمْسُ فِي الصَّبَاحِ»",
                                                    TitleFr = "Lis la phrase : « أَشْرَقَتِ الشَّمْسُ فِي الصَّبَاحِ »",
                                                    TitleEn = "Read the Sentence: 'أَشْرَقَتِ الشَّمْسُ فِي الصَّبَاحِ'",
                                                    PromptAr = "اِقْرَأِ الجُمْلَةَ كَلِمَةً كَلِمَةً: «أَشْرَقَتِ — الشَّمْسُ — فِي — الصَّبَاحِ». مَاذَا حَدَثَ فِي الصَّبَاحِ؟",
                                                    PromptFr = "Lis mot à mot : « أَشْرَقَتِ — الشَّمْسُ — فِي — الصَّبَاحِ ». Que s’est-il passé le matin ?",
                                                    PromptEn = "Read word by word: 'أَشْرَقَتِ — الشَّمْسُ — فِي — الصَّبَاحِ'. What happened in the morning?",
                                                    SpokenTextAr = "أَشْرَقَتِ الشَّمْسُ فِي الصَّبَاحِ! أَضَاءَتِ الشَّمْسُ السَّمَاءَ.",
                                                    SpokenTextFr = "Ashraqati ash-shamsu fi as-sabah ! Le soleil s’est levé le matin.",
                                                    SpokenTextEn = "Ashraqati ash-shamsu fi as-sabah! The sun rose in the morning.",
                                                    InteractionPayloadJson = """{"syllables":["أَشْرَقَتِ","الشَّمْسُ","فِي","الصَّبَاحِ"],"correctOptionId":"sent-sun","options":[{"id":"sent-sun","emoji":"🌅","labelAr":"أَشْرَقَتِ الشَّمْسُ وَأَضَاءَتِ السَّمَاءَ","labelFr":"أَشْرَقَتِ الشَّمْسُ (Le soleil s’est levé)","labelEn":"أَشْرَقَتِ الشَّمْسُ (The sun rose brightly)","accentColor":"#facc15"},{"id":"sent-rain","emoji":"🌧️","labelAr":"نَزَلَ المَطَرُ فِي اللَّيْلِ","labelFr":"نَزَلَ المَطَرُ (La pluie est tombée)","labelEn":"نَزَلَ المَطَرُ (Rain fell at night)","accentColor":"#38bdf8"},{"id":"sent-sleep","emoji":"🌙","labelAr":"ظَهَرَ القَمَرُ وَالنُّجُومُ","labelFr":"ظَهَرَ القَمَرُ (La lune est apparue)","labelEn":"ظَهَرَ القَمَرُ (The moon appeared)","accentColor":"#a855f7"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = mathDomainId,
                    AgeGroupId = age5Id,
                    Slug = "age-5-early-mathematics",
                    DisplayOrder = 2,
                    IconEmoji = "➕",
                    AccentColor = "#4cc38a",
                    TitleAr = "الرِّيَاضِيَّاتُ المُبَكِّرَةُ وَالحِسَابُ",
                    TitleFr = "Mathématiques et addition simple",
                    TitleEn = "Early Mathematics and Addition",
                    SummaryAr = "الأَعْدَادُ حَتَّى 20 وَمَفْهُومُ الجَمْعِ البَسِيطِ بِاسْتِخْدَامِ المُجَسَّمَاتِ وَالصُّوَرِ.",
                    SummaryFr = "Les nombres jusqu’à 20 et l’addition simple avec des objets visuels.",
                    SummaryEn = "Numbers up to 20 and concrete visual addition.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = mathCollectionId,
                            DomainId = mathDomainId,
                            Slug = "numbers-and-simple-addition",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ الحِسَابِ وَالجَمْعِ المَرِحِ",
                            TitleFr = "Collection Calcul et addition joyeuse",
                            TitleEn = "Playful Counting and Addition Collection",
                            SummaryAr = "نَجْمَعُ الأَشْيَاءَ مَعاً لِنَكْتَشِفَ مَفْهُومَ الجَمْعِ.",
                            SummaryFr = "Réunissons des groupes d’objets pour comprendre l’addition.",
                            SummaryEn = "Combine groups of objects to understand simple addition.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = mathAdditionCourseId,
                                    CollectionId = mathCollectionId,
                                    Slug = "simple-addition-with-objects",
                                    TargetAge = 5,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🧮",
                                    TitleAr = "مَفْهُومُ الجَمْعِ البَسِيطِ",
                                    TitleFr = "Concepts d’addition simple",
                                    TitleEn = "Simple Addition Concepts",
                                    DescriptionAr = "نَجْمَعُ مَجْمُوعَتَيْنِ صَغِيرَتَيْنِ مِنَ المُكَعَّبَاتِ وَالتُّفَّاحِ (مِثْلَ: ٢ + ١ = ٣ وَ ٣ + ٢ = ٥).",
                                    DescriptionFr = "Additionnons de petits groupes de cubes et de pommes (2 + 1 = 3 et 3 + 2 = 5).",
                                    DescriptionEn = "Combine small groups of blocks and apples (such as 2 + 1 = 3 and 3 + 2 = 5).",
                                    CompletionCriteriaAr = "حَلُّ مَسَائِلِ الجَمْعِ البَصَرِيَّةِ البَسِيطَةِ.",
                                    CompletionCriteriaFr = "Résoudre de petites additions visuelles.",
                                    CompletionCriteriaEn = "Solve simple visual addition stories.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = mathAdditionCourseId,
                                            Code = "L5-NM-02",
                                            DisplayOrder = 1,
                                            DescriptionAr = "فَهْمُ مَفْهُومِ الجَمْعِ البَسِيطِ بِضَمِّ مَجْمُوعَتَيْنِ مِنَ الأَشْيَاءِ.",
                                            DescriptionFr = "Comprendre l’addition simple en réunissant deux groupes d’objets.",
                                            DescriptionEn = "Understand simple addition by combining two concrete object groups.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l7AdditionId,
                                            CourseId = mathAdditionCourseId,
                                            Slug = "adding-apples-and-blocks",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "نَجْمَعُ مَعاً: ٢ + ١ = ٣ وَ ٣ + ٢ = ٥",
                                            TitleFr = "Additionnons : 2 + 1 = 3 et 3 + 2 = 5",
                                            TitleEn = "Adding Together: 2 + 1 = 3 and 3 + 2 = 5",
                                            SummaryAr = "ضُمَّ التُّفَّاحَاتِ مَعاً وَاحْسِبِ المَجْمُوعَ الكُلِّيَّ.",
                                            SummaryFr = "Réunis les pommes et trouve le total.",
                                            SummaryEn = "Combine the apples together and find the total.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l7AdditionId,
                                                    Slug = "age5-math-add-3-plus-2",
                                                    StepOrder = 1,
                                                    ActivityType = "counting_interactive",
                                                    ObjectiveCode = "L5-NM-02",
                                                    TitleAr = "مَعَنَا ٣ تُفَّاحَاتٍ (🍎🍎🍎) وَأَضَفْنَا تُفَّاحَتَيْنِ (🍎🍎): كَمْ أَصْبَحَ المَجْمُوعُ؟",
                                                    TitleFr = "3 pommes (🍎🍎🍎) + 2 pommes (🍎🍎) : combien au total ?",
                                                    TitleEn = "3 Apples (🍎🍎🍎) + 2 Apples (🍎🍎): How Many in Total?",
                                                    PromptAr = "احْسِبِ المَجْمُوعَ: ٣ + ٢ = ؟ اِخْتَرِ الإِجَابَةَ الصَّحِيحَةَ:",
                                                    PromptFr = "Calcule le total : 3 + 2 = ? Choisis la bonne réponse :",
                                                    PromptEn = "Find the total: 3 + 2 = ? Choose the correct answer:",
                                                    SpokenTextAr = "ثَلَاثُ تُفَّاحَاتٍ زَائِدُ تُفَّاحَتَيْنِ يُسَاوِي خَمْسَ تُفَّاحَاتٍ! ثَلَاثَةٌ زَائِدُ اثْنَيْنِ يُسَاوِي خَمْسَةً.",
                                                    SpokenTextFr = "Trois pommes plus deux pommes font cinq pommes ! 3 + 2 = 5.",
                                                    SpokenTextEn = "Three apples plus two apples equals five apples! 3 + 2 = 5.",
                                                    InteractionPayloadJson = """{"countingItems":{"emoji":"🍎","count":5,"addendA":3,"addendB":2,"itemNameAr":"تُفَّاحَاتٍ فِي السَّلَّةِ","itemNameFr":"Pommes au total","itemNameEn":"Apples in Total"},"correctOptionId":"sum-5","options":[{"id":"sum-4","emoji":"4️⃣","labelAr":"٤ — أَرْبَعَةٌ","labelFr":"4 — Quatre","labelEn":"4 — Four","accentColor":"#38bdf8"},{"id":"sum-5","emoji":"5️⃣","labelAr":"٥ — خَمْسَةٌ (٣ + ٢ = ٥)","labelFr":"5 — Cinq (3 + 2 = 5)","labelEn":"5 — Five (3 + 2 = 5)","accentColor":"#4cc38a"},{"id":"sum-6","emoji":"6️⃣","labelAr":"٦ — سِتَّةٌ","labelFr":"6 — Six","labelEn":"6 — Six","accentColor":"#f59e0b"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = logicNatureDomainId,
                    AgeGroupId = age5Id,
                    Slug = "age-5-logic-and-nature-discovery",
                    DisplayOrder = 3,
                    IconEmoji = "🌱",
                    AccentColor = "#38bdf8",
                    TitleAr = "المَنْطِقُ وَالاِكْتِشَافُ العِلْمِيُّ المُبَسَّطُ",
                    TitleFr = "Logique et découverte de la nature",
                    TitleEn = "Logical Sequences and Nature Discovery",
                    SummaryAr = "تَرْتِيبُ الأَحْدَاثِ المَنْطِقِيَّةِ وَفَهْمُ كَيْفَ تَنْمُو النَّبَاتَاتُ فِي الطَّبِيعَةِ.",
                    SummaryFr = "Séquences logiques et cycle de croissance des plantes.",
                    SummaryEn = "Logical sequences and how plants grow in nature.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = logicCollectionId,
                            DomainId = logicNatureDomainId,
                            Slug = "little-thinker-and-nature",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ المُفَكِّرِ الصَّغِيرِ وَعَجَائِبِ الطَّبِيعَةِ",
                            TitleFr = "Collection Le Petit Penseur et la Nature",
                            TitleEn = "Little Thinker and Wonders of Nature Collection",
                            SummaryAr = "التَّسَلْسُلُ المَنْطِقِيُّ وَمَرَاحِلُ نُمُوِّ النَّبْتَةِ.",
                            SummaryFr = "Suites logiques et étapes de croissance d’une plante.",
                            SummaryEn = "Logical sequences and the stages of plant growth.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = plantLogicCourseId,
                                    CollectionId = logicCollectionId,
                                    Slug = "logical-sequences-and-plant-growth",
                                    TargetAge = 5,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🌻",
                                    TitleAr = "التَّسَلْسُلُ المَنْطِقِيُّ: كَيْفَ تَنْمُو الزَّهْرَةُ؟",
                                    TitleFr = "Séquence logique : comment pousse la fleur ?",
                                    TitleEn = "Logical Sequence: How Does a Flower Grow?",
                                    DescriptionAr = "نَتَتَبَّعُ مَرَاحِلَ نُمُوِّ النَّبْتَةِ مِنَ البَذْرَةِ الصَّغِيرَةِ حَتَّى تُصْبِحَ زَهْرَةً جَمِيلَةً.",
                                    DescriptionFr = "Suivons les étapes de la graine jusqu’à la fleur épanouie.",
                                    DescriptionEn = "Follow the logical steps from a tiny seed to a blooming flower.",
                                    CompletionCriteriaAr = "تَرْتِيبُ مَرَاحِلِ نُمُوِّ النَّبْتَةِ بِالتَّسَلْسُلِ الصَّحِيحِ.",
                                    CompletionCriteriaFr = "Ordonner correctement les étapes de croissance de la plante.",
                                    CompletionCriteriaEn = "Order the stages of plant growth in logical sequence.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = plantLogicCourseId,
                                            Code = "L5-LG-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "إِكْمَالُ التَّسَلْسُلِ المَنْطِقِيِّ وَالزَّمَنِيِّ لِظَوَاهِرَ طَبِيعِيَّةٍ مُبَسَّطَةٍ.",
                                            DescriptionFr = "Compléter une séquence logique et chronologique de la nature.",
                                            DescriptionEn = "Complete a chronological and logical sequence in nature.",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l8PlantGrowthId,
                                            CourseId = plantLogicCourseId,
                                            Slug = "from-seed-to-blooming-flower",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "مِنَ البَذْرَةِ إِلَى الزَّهْرَةِ المُتَفَتِّحَةِ (🌱 ← 🌿 ← 🌻)",
                                            TitleFr = "De la graine à la fleur (🌱 → 🌿 → 🌻)",
                                            TitleEn = "From Seed to Blooming Flower (🌱 → 🌿 → 🌻)",
                                            SummaryAr = "مَا المَرْحَلَةُ الأَخِيرَةُ بَعْدَ أَنْ نَسْقِيَ البَذْرَةَ وَالبُرْعُمَ الصَّغِيرَ؟",
                                            SummaryFr = "Quelle est la dernière étape après avoir arrosé la graine et la jeune pousse ?",
                                            SummaryEn = "What is the final stage after watering the seed and little sprout?",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l8PlantGrowthId,
                                                    Slug = "age5-logic-plant-sequence",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L5-LG-01",
                                                    TitleAr = "أَكْمِلِ التَّسَلْسُلَ المَنْطِقِيَّ: بَذْرَةٌ (🌰) ← بُرْعُمٌ أَخْضَرُ (🌱) ← ؟",
                                                    TitleFr = "Complète la suite : graine (🌰) → jeune pousse (🌱) → ?",
                                                    TitleEn = "Complete the Sequence: Seed (🌰) → Sprout (🌱) → ?",
                                                    PromptAr = "زَرَعْنَا البَذْرَةَ، وَسَقَيْنَاهَا بِالمَاءِ فَظَهَرَ البُرْعُمُ الأَخْضَرُ. مَاذَا يَظْهَرُ فِي النِّهَايَةِ؟",
                                                    PromptFr = "Nous avons planté la graine et la jeune pousse a grandi. Qu’apparaît-il à la fin ?",
                                                    PromptEn = "We planted the seed and watered the sprout. What blooms at the end?",
                                                    SpokenTextAr = "أَوَّلاً البَذْرَةُ، ثُمَّ البُرْعُمُ الأَخْضَرُ، وَأَخِيراً تَتَفَتَّحُ الزَّهْرَةُ الجَمِيلَةُ!",
                                                    SpokenTextFr = "D’abord la graine, puis la pousse verte, et enfin la belle fleur s’épanouit !",
                                                    SpokenTextEn = "First the seed, then the green sprout, and finally the blooming flower!",
                                                    InteractionPayloadJson = """{"correctOptionId":"seq-flower","options":[{"id":"seq-flower","emoji":"🌻","labelAr":"زَهْرَةٌ مُتَفَتِّحَةٌ جَمِيلَةٌ","labelFr":"Une belle fleur épanouie","labelEn":"A Beautiful Blooming Flower","accentColor":"#facc15"},{"id":"seq-rock","emoji":"🪨","labelAr":"صَخْرَةٌ صَلْبَةٌ","labelFr":"Un rocher","labelEn":"A Hard Rock","accentColor":"#94a3b8"},{"id":"seq-snow","emoji":"❄️","labelAr":"ثَلْجٌ أَبْيَضُ","labelFr":"De la neige","labelEn":"White Snow","accentColor":"#38bdf8"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
                new LearningDomain
                {
                    Id = scienceNatureDomainId,
                    AgeGroupId = age5Id,
                    Slug = "age-5-wonders-of-nature-and-sky",
                    DisplayOrder = 4,
                    IconEmoji = "☀️",
                    AccentColor = "#f97316",
                    TitleAr = "الطَّبِيعَةُ وَعَجَائِبُ السَّمَاءِ وَالمَاءِ",
                    TitleFr = "Merveilles de la nature, du ciel et de l’eau",
                    TitleEn = "Wonders of Nature, Day, Night, and Water",
                    SummaryAr = "نَكْتَشِفُ تَعَاقُبَ النَّهَارِ وَاللَّيْلِ، وَأَهَمِّيَّةَ المَاءِ وَالشَّمْسِ لِلحَيَاةِ.",
                    SummaryFr = "Découvrir le jour, la nuit et l’importance de l’eau dans la nature.",
                    SummaryEn = "Explore day and night and the importance of water and sunlight.",
                    Collections =
                    [
                        new CourseCollection
                        {
                            Id = scienceCollectionId,
                            DomainId = scienceNatureDomainId,
                            Slug = "wonders-of-nature-for-kids",
                            DisplayOrder = 1,
                            IsReadingPathway = false,
                            TitleAr = "سِلْسِلَةُ عَجَائِبِ الطَّبِيعَةِ لِلأَطْفَالِ",
                            TitleFr = "Collection Merveilles de la nature",
                            TitleEn = "Wonders of Nature for Kids Collection",
                            SummaryAr = "النَّهَارُ وَاللَّيْلُ وَنِعْمَةُ المَاءِ فِي حَيَاتِنَا.",
                            SummaryFr = "Le jour, la nuit et le rôle de l’eau.",
                            SummaryEn = "Day, night, and the gift of water in our lives.",
                            Courses =
                            [
                                new Course
                                {
                                    Id = dayNightCourseId,
                                    CollectionId = scienceCollectionId,
                                    Slug = "day-night-and-water-in-nature",
                                    TargetAge = 5,
                                    DisplayOrder = 1,
                                    DifficultyLevel = 2,
                                    IconEmoji = "🌤️",
                                    TitleAr = "النَّهَارُ وَاللَّيْلُ وَالمَاءُ فِي حَيَاتِنَا",
                                    TitleFr = "Le jour, la nuit et l’eau dans notre vie",
                                    TitleEn = "Day, Night, and Water in Our Lives",
                                    DescriptionAr = "نَتَعَرَّفُ عَلَى الشَّمْسِ فِي النَّهَارِ وَالقَمَرِ فِي اللَّيْلِ وَكَيْفَ يَسْقِي المَاءُ الكَائِنَاتِ الحَيَّةَ.",
                                    DescriptionFr = "Découvrons le soleil le jour, la lune la nuit et le rôle de l’eau.",
                                    DescriptionEn = "Discover the sun by day, the moon by night, and how water nourishes life.",
                                    CompletionCriteriaAr = "التَّفْرِيقُ بَيْنَ ظَوَاهِرِ النَّهَارِ وَاللَّيْلِ وَفَوَائِدِ المَاءِ.",
                                    CompletionCriteriaFr = "Distinguer le jour, la nuit et les bienfaits de l’eau.",
                                    CompletionCriteriaEn = "Distinguish day, night, and the benefits of water.",
                                    Objectives =
                                    [
                                        new LearningObjective
                                        {
                                            CourseId = dayNightCourseId,
                                            Code = "L5-NT-01",
                                            DisplayOrder = 1,
                                            DescriptionAr = "فَهْمُ الظَّوَاهِرِ الطَّبِيعِيَّةِ اليَوْمِيَّةِ (النَّهَارُ وَاللَّيْلُ وَأَهَمِّيَّةُ المَاءِ).",
                                            DescriptionFr = "Comprendre le cycle du jour et de la nuit et le rôle de l’eau.",
                                            DescriptionEn = "Understand daily natural phenomena (day, night, and water).",
                                        },
                                    ],
                                    Lessons =
                                    [
                                        new Lesson
                                        {
                                            Id = l9DayNightId,
                                            CourseId = dayNightCourseId,
                                            Slug = "sun-by-day-moon-by-night",
                                            DisplayOrder = 1,
                                            EstimatedMinutes = 5,
                                            TitleAr = "الشَّمْسُ فِي النَّهَارِ وَالقَمَرُ فِي اللَّيْلِ",
                                            TitleFr = "Le soleil le jour et la lune la nuit",
                                            TitleEn = "The Sun by Day and the Moon by Night",
                                            SummaryAr = "اِكْتَشِفْ مَتَى تُشْرِقُ الشَّمْسُ وَمَتَى يَظْهَرُ القَمَرُ وَالنُّجُومُ.",
                                            SummaryFr = "Découvre quand brille le soleil et quand apparaissent la lune et les étoiles.",
                                            SummaryEn = "Explore when the sun shines and when the moon and stars appear.",
                                            Activities =
                                            [
                                                new Activity
                                                {
                                                    LessonId = l9DayNightId,
                                                    Slug = "age5-nature-day-night",
                                                    StepOrder = 1,
                                                    ActivityType = "visual_match_sort",
                                                    ObjectiveCode = "L5-NT-01",
                                                    TitleAr = "مَاذَا يُضِيءُ السَّمَاءَ فِي النَّهَارِ وَيَمْنَحُنَا الدِّفْءَ؟",
                                                    TitleFr = "Qu’est-ce qui éclaire le ciel pendant la journée ?",
                                                    TitleEn = "What Lights Up the Sky During the Day and Gives Us Warmth?",
                                                    PromptAr = "اِسْتَكْشِفْ بِطَاقَاتِ الطَّبِيعَةِ، ثُمَّ اخْتَرْ مَا يُضِيءُ السَّمَاءَ فِي النَّهَارِ:",
                                                    PromptFr = "Explore les cartes de la nature, puis choisis ce qui éclaire le jour :",
                                                    PromptEn = "Explore the nature cards, then pick what lights up the daytime sky:",
                                                    SpokenTextAr = "فِي النَّهَارِ تُشْرِقُ الشَّمْسُ الذَّهَبِيَّةُ، وَفِي اللَّيْلِ يَظْهَرُ القَمَرُ المُنِيرُ، وَالمَاءُ يَسْقِي الكَائِنَاتِ!",
                                                    SpokenTextFr = "Le jour, le soleil brille, et la nuit, la lune éclaire le ciel !",
                                                    SpokenTextEn = "By day the golden sun shines, and at night the glowing moon appears!",
                                                    InteractionPayloadJson = """{"learningCards":[{"emoji":"☀️","titleAr":"الشَّمْسُ فِي النَّهَارِ","titleFr":"Le Soleil le jour","titleEn":"The Sun by Day","factAr":"الشَّمْسُ تَمْنَحُنَا النُّورَ وَالدِّفْءَ وَتُسَاعِدُ النَّبَاتَاتِ عَلَى النُّمُوِّ.","factFr":"Le soleil nous donne lumière et chaleur.","factEn":"The sun gives us light and warmth and helps plants grow.","accentColor":"#facc15"},{"emoji":"🌙","titleAr":"القَمَرُ فِي اللَّيْلِ","titleFr":"La Lune la nuit","titleEn":"The Moon at Night","factAr":"يَظْهَرُ القَمَرُ وَالنُّجُومُ فِي السَّمَاءِ عِنْدَمَا يَحِلُّ اللَّيْلُ.","factFr":"La lune et les étoiles apparaissent la nuit.","factEn":"The moon and stars glow in the night sky.","accentColor":"#818cf8"},{"emoji":"💧","titleAr":"المَاءُ سرُّ الحَيَاةِ","titleFr":"L’eau source de vie","titleEn":"Water Gives Life","factAr":"نَشْرَبُ المَاءَ النَّقِيَّ وَنَسْقِي بِهِ الأَشْجَارَ وَالأَزْهَارَ.","factFr":"Nous buvons l’eau et arrosons les plantes.","factEn":"We drink clean water and water trees and flowers.","accentColor":"#38bdf8"}],"correctOptionId":"sky-sun","options":[{"id":"sky-sun","emoji":"☀️","labelAr":"الشَّمْسُ المُشْرِقَةُ فِي النَّهَارِ","labelFr":"Le soleil brillant le jour","labelEn":"The Bright Sun by Day","accentColor":"#facc15"},{"id":"sky-moon","emoji":"🌙","labelAr":"القَمَرُ فِي اللَّيْلِ","labelFr":"La lune la nuit","labelEn":"The Moon at Night","accentColor":"#818cf8"},{"id":"sky-umbrella","emoji":"☂️","labelAr":"المِظَلَّةُ المُلَوَّنَةُ","labelFr":"Le parapluie","labelEn":"The Colorful Umbrella","accentColor":"#f43f5e"}]}""",
                                                },
                                            ],
                                        },
                                    ],
                                },
                            ],
                        },
                    ],
                },
            ],
        };
    }
}
