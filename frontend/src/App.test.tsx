import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import i18n from './i18n';
import App from './App';
import {
  type CurriculumOverview,
  isValidGlbUrl,
  type LessonDetail,
  type SystemInfo,
} from './lib/api';

// WebGL is unavailable in jsdom; expose modelUrl and activeNodeName for verification.
vi.mock('./features/world/WorldCanvas', () => ({
  default: ({
    label,
    modelUrl,
    activeNodeName,
  }: {
    label: string;
    modelUrl?: string;
    activeNodeName?: string;
  }) => (
    <div
      role="img"
      aria-label={label}
      data-testid="world-canvas"
      data-model-url={modelUrl}
      data-active-node={activeNodeName}
    />
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

const curriculumOverview = (culture: string): CurriculumOverview => ({
  culture,
  direction: culture === 'ar' ? 'rtl' : 'ltr',
  modules: [
    {
      slug: 'optics-and-light',
      order: 1,
      title: culture === 'fr' ? 'Optique et physique de la lumière' : 'البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ',
      summary: 'Summary',
      lessons: [
        {
          slug: 'alhazen-optical-lantern',
          order: 1,
          title: culture === 'fr' ? 'La lanterne optique d’Ibn al-Haytham' : 'مِصْبَاحُ ابْنِ الهَيْثَمِ وَالحُجْرَةُ المُظْلِمَةُ',
          objective: 'Objective',
          modelUrl: '/models/lantern.glb',
          modelFormat: 'model/gltf-binary',
          checkpointCount: 2,
        },
      ],
    },
    {
      slug: 'planetary-astronomy',
      order: 2,
      title: culture === 'fr' ? 'Astronomie et mécanique céleste' : 'عِلْمُ الفَلَكِ وَحَرَكَةُ الكَوَاكِبِ',
      summary: 'Astronomy summary',
      lessons: [
        {
          slug: 'sun-earth-moon-system',
          order: 1,
          title: culture === 'fr' ? 'Le système Soleil-Terre-Lune' : 'مَنْظُومَةُ الشَّمْسِ وَالأَرْضِ وَالقَمَرِ',
          objective: 'Orbit objective',
          modelUrl: '/models/solar-system.glb',
          modelFormat: 'model/gltf-binary',
          checkpointCount: 2,
        },
      ],
    },
  ],
});

const lessonDetails = (slug: string, culture: string): LessonDetail => {
  if (slug === 'sun-earth-moon-system') {
    return {
      slug,
      moduleSlug: 'planetary-astronomy',
      culture,
      direction: culture === 'ar' ? 'rtl' : 'ltr',
      title: culture === 'fr' ? 'Le système Soleil-Terre-Lune' : 'مَنْظُومَةُ الشَّمْسِ وَالأَرْضِ وَالقَمَرِ',
      objective: 'Orbit objective',
      asset: {
        url: '/models/solar-system.glb',
        format: 'model/gltf-binary',
        cameraPosition: [4.2, 3.0, 4.6],
        cameraTarget: [0, 0.8, 0],
      },
      checkpoints: [
        {
          stepOrder: 1,
          targetNodeName: 'Core_Sun',
          focusPoint: [0, 1, 0],
          title: 'الشَّمْسُ',
          explanation: 'الشمس مركز الجاذبية.',
        },
        {
          stepOrder: 2,
          targetNodeName: 'Planet_Earth',
          focusPoint: [1.75, 1, 0],
          title: 'كَوْكَبُ الأَرْضِ',
          explanation: 'دوران الأرض حول نفسها.',
        },
      ],
    };
  }

  return {
    slug: 'alhazen-optical-lantern',
    moduleSlug: 'optics-and-light',
    culture,
    direction: culture === 'ar' ? 'rtl' : 'ltr',
    title: culture === 'fr' ? 'La lanterne optique d’Ibn al-Haytham' : 'مِصْبَاحُ ابْنِ الهَيْثَمِ وَالحُجْرَةُ المُظْلِمَةُ',
    objective: 'Objective',
    asset: {
      url: '/models/lantern.glb',
      format: 'model/gltf-binary',
      cameraPosition: [3.2, 2.3, 4.0],
      cameraTarget: [0, 1.0, 0],
    },
    checkpoints: [
      {
        stepOrder: 1,
        targetNodeName: 'Light_Core',
        focusPoint: [0, 1.25, 0],
        title: 'المَصْدَرُ الضَّوْئِيُّ المَرْكَزِيُّ',
        explanation: 'ينبعث الضوء في خطوط مستقيمة.',
      },
      {
        stepOrder: 2,
        targetNodeName: 'Aperture_Ring',
        focusPoint: [0, 1.25, 0.95],
        title: 'حَلْقَةُ الثُّقْبِ الضَّيِّقِ',
        explanation: 'يسمح الثقب الضيق بمرور حزمة محددة.',
      },
    ],
  };
};

describe('App & GLB Curriculum', () => {
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
        if (url.endsWith('/v1/curriculum/modules')) {
          return Promise.resolve(new Response(JSON.stringify(curriculumOverview(language)), { status: 200 }));
        }
        if (url.includes('/v1/curriculum/lessons/')) {
          const slug = url.split('/').pop() ?? 'alhazen-optical-lantern';
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
    expect(isValidGlbUrl('/models/lantern.glb')).toBe(true);
    expect(isValidGlbUrl('/models/SOLAR-SYSTEM.GLB')).toBe(true);
    expect(isValidGlbUrl('/models/scene.gltf')).toBe(false);
    expect(isValidGlbUrl('/models/scene.obj')).toBe(false);
  });

  it('renders the Arabic interface right-to-left and loads the default .glb lesson', async () => {
    render(<App />);

    expect(screen.getByRole('heading', { level: 1, name: 'نوّار' })).toBeInTheDocument();
    expect(document.documentElement.dir).toBe('rtl');
    expect(await screen.findByText('متّصل')).toBeInTheDocument();
    expect(await screen.findByText('البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ')).toBeInTheDocument();

    const canvas = await screen.findByTestId('world-canvas');
    expect(canvas).toHaveAttribute('data-model-url', '/models/lantern.glb');
    expect(canvas).toHaveAttribute('data-active-node', 'Light_Core');
  });

  it('switches between .glb lessons and updates the target 3D checkpoint node', async () => {
    const user = userEvent.setup();
    render(<App />);

    const astronomyLessonButton = await screen.findByRole('button', {
      name: /مَنْظُومَةُ الشَّمْسِ وَالأَرْضِ وَالقَمَرِ/,
    });
    await user.click(astronomyLessonButton);

    const canvas = await screen.findByTestId('world-canvas');
    expect(canvas).toHaveAttribute('data-model-url', '/models/solar-system.glb');
    expect(canvas).toHaveAttribute('data-active-node', 'Core_Sun');

    const secondCheckpointTab = await screen.findByRole('tab', { name: /كَوْكَبُ الأَرْضِ/ });
    await user.click(secondCheckpointTab);
    expect(canvas).toHaveAttribute('data-active-node', 'Planet_Earth');
  });

  it('switches to French and left-to-right', async () => {
    const user = userEvent.setup();
    render(<App />);

    await user.click(screen.getByRole('button', { name: 'Français' }));

    expect(screen.getByRole('button', { name: 'Français' })).toHaveAttribute('aria-pressed', 'true');
    expect(document.documentElement.dir).toBe('ltr');
    expect(await screen.findByText('Connecté')).toBeInTheDocument();
    expect(await screen.findByText('Optique et physique de la lumière')).toBeInTheDocument();
  });

  it('reports the API as unavailable when the request fails', async () => {
    vi.stubGlobal('fetch', vi.fn(() => Promise.resolve(new Response('', { status: 503 }))));
    vi.spyOn(console, 'warn').mockImplementation(() => {});
    render(<App />);

    expect(await screen.findByText('غير متاح')).toBeInTheDocument();
  });
});
