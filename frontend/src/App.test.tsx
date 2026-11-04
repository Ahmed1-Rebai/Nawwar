import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import i18n from './i18n';
import App from './App';
import {
  type AgeGroupDetail,
  type AgeGroupsOverview,
  isValidGlbUrl,
  type LearnerProgress,
  type LessonDetail,
  type SystemInfo,
} from './lib/api';

vi.mock('./features/world/WorldCanvas', () => ({
  default: ({
    label,
    modelUrl,
    activeNodeName,
    onSelectNode,
  }: {
    label: string;
    modelUrl?: string;
    activeNodeName?: string;
    onSelectNode?: (nodeName: string) => void;
  }) => (
    <div
      role="img"
      aria-label={label}
      data-testid="world-canvas"
      data-model-url={modelUrl}
      data-active-node={activeNodeName}
    >
      <button
        type="button"
        data-testid="pick-yellow-flower-node"
        onClick={() => onSelectNode?.('Yellow_Flower')}
      >
        Pick Yellow_Flower
      </button>
    </div>
  ),
}));

const systemInfo = (culture: string): SystemInfo => ({
  name: 'NAWWAR',
  version: '0.1.0',
  culture,
  direction: culture === 'ar' ? 'rtl' : 'ltr',
  defaultCulture: 'ar',
  supportedCultures: ['ar', 'fr', 'en'],
});

const ageGroupsOverview = (culture: string): AgeGroupsOverview => ({
  culture,
  direction: culture === 'ar' ? 'rtl' : 'ltr',
  ageGroups: [
    {
      ageYears: 3,
      slug: 'age-3-discovery',
      themeKey: 'discovery',
      iconEmoji: '🌱',
      title: culture === 'fr' ? 'Monde de la Découverte — 3 ans' : 'عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ',
      subtitle: 'الاِكْتِشَافُ الحِسِّيُّ',
      description: 'أَنْشِطَةٌ قَصِيرَةٌ وَمَرِحَةٌ',
      domainCount: 3,
      courseCount: 4,
      activityCount: 5,
    },
    {
      ageYears: 4,
      slug: 'age-4-exploration',
      themeKey: 'exploration',
      iconEmoji: '🧭',
      title: culture === 'fr' ? 'Monde de l’Exploration — 4 ans' : 'عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ',
      subtitle: 'اِكْتِشَافُ الحُرُوفِ وَالعَدُّ',
      description: 'رِحْلَةٌ مُمْتِعَةٌ',
      domainCount: 3,
      courseCount: 4,
      activityCount: 5,
    },
    {
      ageYears: 5,
      slug: 'age-5-learning',
      themeKey: 'learning',
      iconEmoji: '🌟',
      title: culture === 'fr' ? 'Monde de l’Apprentissage — 5 ans' : 'عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ',
      subtitle: 'مَسَارُ تَعَلُّمِ القِرَاءَةِ العَرَبِيَّةِ',
      description: 'مَنْهَجٌ مُتَكَامِلٌ',
      domainCount: 3,
      courseCount: 8,
      activityCount: 9,
    },
  ],
});

const ageGroupDetail = (ageYears: number, culture: string): AgeGroupDetail => {
  if (ageYears === 5) {
    return {
      ageYears: 5,
      slug: 'age-5-learning',
      themeKey: 'learning',
      iconEmoji: '🌟',
      culture,
      direction: culture === 'ar' ? 'rtl' : 'ltr',
      title: 'عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ',
      subtitle: 'مَسَارُ تَعَلُّمِ القِرَاءَةِ العَرَبِيَّةِ',
      description: 'مَنْهَجٌ مُتَكَامِلٌ',
      domains: [
        {
          slug: 'age-5-arabic-language-and-reading',
          order: 1,
          iconEmoji: '📖',
          accentColor: '#f4c76b',
          title: 'اللُّغَةُ العَرَبِيَّةُ',
          summary: 'مَسَارُ تَعَلُّمِ القِرَاءَةِ المُتَدَرِّجُ',
          collections: [
            {
              slug: 'arabic-reading-pathway',
              order: 1,
              isReadingPathway: true,
              title: 'مَسَارُ تَعَلُّمِ القِرَاءَةِ',
              summary: 'سِتُّ دَوْرَاتٍ مُتَسَلْسِلَةٍ',
              courses: [
                {
                  slug: 'syllable-construction',
                  targetAge: 5,
                  order: 4,
                  difficultyLevel: 2,
                  iconEmoji: '🧩',
                  title: 'بِنَاءُ المَقَاطِعِ',
                  description: 'نَجْمَعُ الحُرُوفَ المُتَحَرِّكَةَ',
                  completionCriteria: 'دَمْجُ المَقَاطِعِ',
                  objectives: [
                    {
                      code: 'L5-RD-04',
                      order: 1,
                      description: 'تَرْكِيبُ المَقَاطِعِ الصَّوْتِيَّةِ',
                    },
                  ],
                  lessons: [
                    {
                      slug: 'blending-syllables-ka-ta-ba',
                      order: 1,
                      estimatedMinutes: 5,
                      title: 'نَبْنِي المَقَاطِعَ: (كَـ + تَـ + بَ)',
                      summary: 'اِدمِجِ المَقَاطِعَ الصَّوْتِيَّةَ',
                      activityCount: 1,
                      has3DScene: false,
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

  return {
    ageYears: 3,
    slug: 'age-3-discovery',
    themeKey: 'discovery',
    iconEmoji: '🌱',
    culture,
    direction: culture === 'ar' ? 'rtl' : 'ltr',
    title: 'عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ',
    subtitle: 'الاِكْتِشَافُ الحِسِّيُّ',
    description: 'أَنْشِطَةٌ قَصِيرَةٌ وَمَرِحَةٌ',
    domains: [
      {
        slug: 'age-3-colors-and-shapes',
        order: 1,
        iconEmoji: '🎨',
        accentColor: '#ff7a70',
        title: 'الأَلْوَانُ وَالأَشْكَالُ الأَسَاسِيَّةُ',
        summary: 'تَعَرَّفْ عَلَى الأَلْوَانِ',
        collections: [
          {
            slug: 'garden-of-colors-and-shapes',
            order: 1,
            isReadingPathway: false,
            title: 'سِلْسِلَةُ حَدِيقَةِ الأَلْوَانِ وَالأَشْكَالِ',
            summary: 'رِحْلَةٌ بَصَرِيَّةٌ',
            courses: [
              {
                slug: 'my-first-four-colors',
                targetAge: 3,
                order: 1,
                difficultyLevel: 1,
                iconEmoji: '🍎',
                title: 'أَلْوَانِي الأَرْبَعَةُ الأُولَى',
                description: 'نَكْتَشِفُ الأَلْوَانَ',
                completionCriteria: 'إِتْمَامُ الأَنْشِطَةِ',
                objectives: [
                  {
                    code: 'L3-CL-01',
                    order: 1,
                    description: 'التَّعَرُّفُ عَلَى الأَلْوَانِ الأَسَاسِيَّةِ',
                  },
                ],
                lessons: [
                  {
                    slug: 'red-apple-yellow-sun-garden',
                    order: 1,
                    estimatedMinutes: 3,
                    title: 'التُّفَّاحَةُ الحَمْرَاءُ وَالزَّهْرَةُ الصَّفْرَاءُ فِي الحَدِيقَةِ',
                    summary: 'نَسْتَكْشِفُ الأَلْوَانَ فِي المَشْهَدِ ثُلَاثِيِّ الأَبْعَادِ',
                    activityCount: 1,
                    has3DScene: true,
                  },
                ],
              },
            ],
          },
        ],
      },
    ],
  };
};

const lessonDetails = (slug: string, culture: string): LessonDetail => {
  if (slug === 'blending-syllables-ka-ta-ba') {
    return {
      slug,
      courseSlug: 'syllable-construction',
      courseTitle: 'بِنَاءُ المَقَاطِعِ',
      ageYears: 5,
      culture,
      direction: culture === 'ar' ? 'rtl' : 'ltr',
      title: 'نَبْنِي المَقَاطِعَ: (كَـ + تَـ + بَ)',
      summary: 'اِدمِجِ المَقَاطِعَ الصَّوْتِيَّةَ',
      estimatedMinutes: 5,
      activities: [
        {
          slug: 'age5-rd4-build-kataba',
          stepOrder: 1,
          activityType: 'syllable_word_builder',
          objectiveCode: 'L5-RD-04',
          title: 'اِدمِجِ المَقَاطِعَ الثَّلَاثَةَ: كَـ + تَـ + بَ',
          prompt: 'عِنْدَمَا نَجْمَعُ المَقَاطِعَ: (كَـ) + (تَـ) + (بَ)، مَا الكَلِمَةُ الَّتِي نَحْصُلُ عَلَيْهَا؟',
          spokenText: 'كَـ .. تَـ .. بَ : كَتَبَ!',
          glbModelUrl: null,
          targetNodeName: null,
          focusPoint: [0, 0, 0],
          interactionPayloadJson: JSON.stringify({
            syllables: ['كَـ', 'تَـ', 'بَ'],
            correctOptionId: 'word-kataba',
            options: [
              {
                id: 'word-kataba',
                emoji: '✍️',
                labelAr: 'كَتَبَ',
                labelFr: 'كَتَبَ',
                labelEn: 'كَتَبَ',
                accentColor: '#4cc38a',
              },
              {
                id: 'word-qaraa',
                emoji: '📖',
                labelAr: 'قَرَأَ',
                labelFr: 'قَرَأَ',
                labelEn: 'قَرَأَ',
                accentColor: '#38bdf8',
              },
            ],
          }),
        },
      ],
    };
  }

  return {
    slug: 'red-apple-yellow-sun-garden',
    courseSlug: 'my-first-four-colors',
    courseTitle: 'أَلْوَانِي الأَرْبَعَةُ الأُولَى',
    ageYears: 3,
    culture,
    direction: culture === 'ar' ? 'rtl' : 'ltr',
    title: 'التُّفَّاحَةُ الحَمْرَاءُ وَالزَّهْرَةُ الصَّفْرَاءُ فِي الحَدِيقَةِ',
    summary: 'نَسْتَكْشِفُ الأَلْوَانَ فِي المَشْهَدِ ثُلَاثِيِّ الأَبْعَادِ',
    estimatedMinutes: 3,
    activities: [
      {
        slug: 'age3-colors-3d-garden',
        stepOrder: 1,
        activityType: 'scene_3d_explore',
        objectiveCode: 'L3-CL-01',
        title: 'اِكْتَشِفْ حَدِيقَةَ الأَلْوَانِ ثُلَاثِيَّةَ الأَبْعَادِ',
        prompt: 'اِضْغَطْ عَلَى التُّفَّاحَةِ الحَمْرَاءِ أَوِ الزَّهْرَةِ الصَّفْرَاءِ!',
        spokenText: 'هَذِهِ حَدِيقَةُ الأَلْوَانِ!',
        glbModelUrl: '/models/garden-colors.glb',
        targetNodeName: 'Red_Apple',
        focusPoint: [-1.1, 0.8, 0.2],
        interactionPayloadJson: JSON.stringify({
          sceneObjects: [
            {
              nodeName: 'Red_Apple',
              emoji: '🍎',
              labelAr: 'تُفَّاحَةٌ حَمْرَاءُ',
              labelFr: 'Pomme rouge',
              labelEn: 'Red apple',
              color: '#ef4444',
            },
            {
              nodeName: 'Yellow_Flower',
              emoji: '🌻',
              labelAr: 'زَهْرَةٌ صَفْرَاءُ',
              labelFr: 'Fleur jaune',
              labelEn: 'Yellow flower',
              color: '#facc15',
            },
          ],
        }),
      },
    ],
  };
};

const learnerProgress = (completedSlugs: string[]): LearnerProgress => ({
  learnerKey: 'default',
  totalCompletedActivities: completedSlugs.length,
  totalActivities: 19,
  completedActivitySlugs: completedSlugs,
  byAge: [
    { ageYears: 3, title: 'عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ', completedActivities: 0, totalActivities: 5 },
    { ageYears: 4, title: 'عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ', completedActivities: 0, totalActivities: 5 },
    {
      ageYears: 5,
      title: 'عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ',
      completedActivities: completedSlugs.length,
      totalActivities: 9,
    },
  ],
});

describe('NAWWAR Preschool Age-Based Curriculum & Multi-Modal Activities', () => {
  beforeEach(async () => {
    await i18n.changeLanguage('ar');
    vi.stubGlobal(
      'fetch',
      vi.fn((url: string, init?: RequestInit) => {
        const headers = (init?.headers as Record<string, string> | undefined) ?? {};
        const language = headers['Accept-Language'] ?? 'ar';
        if (url.endsWith('/v1/system/info')) {
          return Promise.resolve(new Response(JSON.stringify(systemInfo(language)), { status: 200 }));
        }
        if (url.endsWith('/v1/curriculum/age-groups')) {
          return Promise.resolve(new Response(JSON.stringify(ageGroupsOverview(language)), { status: 200 }));
        }
        if (url.includes('/v1/curriculum/age-groups/')) {
          const age = Number(url.split('/').pop() ?? '3');
          return Promise.resolve(new Response(JSON.stringify(ageGroupDetail(age, language)), { status: 200 }));
        }
        if (url.includes('/v1/curriculum/progress')) {
          return Promise.resolve(new Response(JSON.stringify(learnerProgress([])), { status: 200 }));
        }
        if (url.endsWith('/complete') && init?.method === 'POST') {
          return Promise.resolve(
            new Response(JSON.stringify(learnerProgress(['age5-rd4-build-kataba'])), { status: 200 }),
          );
        }
        if (url.includes('/v1/curriculum/lessons/')) {
          const slug = url.split('/').pop() ?? 'red-apple-yellow-sun-garden';
          return Promise.resolve(new Response(JSON.stringify(lessonDetails(slug, language)), { status: 200 }));
        }
        return Promise.resolve(new Response('', { status: 404 }));
      }),
    );
  });

  afterEach(() => {
    vi.unstubAllGlobals();
  });

  it('validates that 3D model URLs use the .glb extension', () => {
    expect(isValidGlbUrl('/models/garden-colors.glb')).toBe(true);
    expect(isValidGlbUrl('/models/FRIENDLY-FARM.GLB')).toBe(true);
    expect(isValidGlbUrl('/models/scene.gltf')).toBe(false);
    expect(isValidGlbUrl('/models/scene.obj')).toBe(false);
  });

  it('renders the main Age-World Selection portal in Arabic (RTL) with Ages 3, 4, and 5', async () => {
    render(<App />);

    expect(screen.getByRole('heading', { level: 1, name: 'نوّار' })).toBeInTheDocument();
    expect(document.documentElement.dir).toBe('rtl');
    expect(await screen.findByText('متّصل')).toBeInTheDocument();

    expect(await screen.findByText('عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ')).toBeInTheDocument();
    expect(screen.getByText('عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ')).toBeInTheDocument();
    expect(screen.getByText('عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ')).toBeInTheDocument();
  });

  it('enters Age 3 (عالم الاكتشاف) and interacts with the playful 3D color garden (.glb)', async () => {
    const user = userEvent.setup();
    render(<App />);

    const enterAge3Button = await screen.findByRole('button', {
      name: /اُدْخُلِ العَالَمَ: عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ/,
    });
    await user.click(enterAge3Button);

    expect(
      await screen.findByRole('heading', { level: 4, name: 'أَلْوَانِي الأَرْبَعَةُ الأُولَى' }),
    ).toBeInTheDocument();
    const canvas = await screen.findByTestId('world-canvas');
    expect(canvas).toHaveAttribute('data-model-url', '/models/garden-colors.glb');
    expect(canvas).toHaveAttribute('data-active-node', 'Red_Apple');

    await user.click(screen.getByTestId('pick-yellow-flower-node'));
    expect(canvas).toHaveAttribute('data-active-node', 'Yellow_Flower');
  });

  it('enters Age 5 (عالم التعلم), displays مسار تعلم القراءة, and completes syllable blending', async () => {
    const user = userEvent.setup();
    render(<App />);

    const enterAge5Button = await screen.findByRole('button', {
      name: /اُدْخُلِ العَالَمَ: عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ/,
    });
    await user.click(enterAge5Button);

    expect(await screen.findByText('مَسَارُ تَعَلُّمِ القِرَاءَةِ')).toBeInTheDocument();
    expect(screen.getByRole('heading', { level: 4, name: 'بِنَاءُ المَقَاطِعِ' })).toBeInTheDocument();

    // Step 1: Child starts in the "Learn & Sound" phase first (not directly in the quiz)
    expect(await screen.findByTestId('journey-step-learn')).toBeInTheDocument();
    expect(screen.queryByTestId('journey-step-exercise')).not.toBeInTheDocument();

    // Advance to Step 2: Common words (animals, fruits, everyday objects)
    await user.click(screen.getByTestId('go-to-step-2'));
    expect(await screen.findByTestId('journey-step-words')).toBeInTheDocument();

    // Advance to Step 3: Friendly exercise at the end
    await user.click(screen.getByTestId('go-to-step-3'));
    expect(await screen.findByTestId('journey-step-exercise')).toBeInTheDocument();

    const katabaOption = await screen.findByRole('button', { name: /كَتَبَ/ });
    await user.click(katabaOption);

    expect(await screen.findByText(/أَحْسَنْتَ! إِجَابَةٌ رَائِعَةٌ وَصَحِيحَةٌ/)).toBeInTheDocument();
    expect(screen.getByTestId('progress-summary')).toHaveTextContent('1/19');
  });

  it('switches to French and left-to-right', async () => {
    const user = userEvent.setup();
    render(<App />);

    await user.click(screen.getByRole('button', { name: 'Français' }));

    expect(screen.getByRole('button', { name: 'Français' })).toHaveAttribute('aria-pressed', 'true');
    expect(document.documentElement.dir).toBe('ltr');
    expect(await screen.findByText('Connecté')).toBeInTheDocument();
    expect(await screen.findByText('Monde de la Découverte — 3 ans')).toBeInTheDocument();
  });

  it('reports the API as unavailable when the request fails', async () => {
    vi.stubGlobal('fetch', vi.fn(() => Promise.resolve(new Response('', { status: 503 }))));
    vi.spyOn(console, 'warn').mockImplementation(() => {});
    render(<App />);

    expect(await screen.findByText('غير متاح')).toBeInTheDocument();
  });
});
