import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import i18n from './i18n';
import App from './App';
import type { SystemInfo } from './lib/api';

// WebGL is unavailable in jsdom; the 3D canvas is covered by browser validation.
vi.mock('./features/world/WorldCanvas', () => ({
  default: ({ label }: { label: string }) => <div role="img" aria-label={label} data-testid="world-canvas" />,
}));

const systemInfo = (culture: string): SystemInfo => ({
  name: 'NAWWAR',
  version: '0.1.0',
  culture,
  direction: culture === 'ar' ? 'rtl' : 'ltr',
  defaultCulture: 'ar',
  supportedCultures: ['ar', 'fr', 'en'],
});

describe('App', () => {
  beforeEach(async () => {
    await i18n.changeLanguage('ar');
    vi.stubGlobal(
      'fetch',
      vi.fn((_url: string, init?: RequestInit) => {
        const headers = (init?.headers as Record<string, string> | undefined) ?? {};
        const language = headers['Accept-Language'] ?? 'ar';
        return Promise.resolve(new Response(JSON.stringify(systemInfo(language)), { status: 200 }));
      }),
    );
  });

  afterEach(() => {
    vi.unstubAllGlobals();
  });

  it('renders the Arabic interface right-to-left by default', async () => {
    render(<App />);

    expect(screen.getByRole('heading', { level: 1, name: 'نوّار' })).toBeInTheDocument();
    expect(document.documentElement.dir).toBe('rtl');
    expect(await screen.findByTestId('world-canvas')).toBeInTheDocument();
    expect(await screen.findByText('متّصل')).toBeInTheDocument();
  });

  it('sends the active language to the API', async () => {
    render(<App />);
    await screen.findByText('متّصل');

    expect(fetch).toHaveBeenCalledWith(
      '/api/v1/system/info',
      expect.objectContaining({ headers: expect.objectContaining({ 'Accept-Language': 'ar' }) }),
    );
  });

  it('switches to French and left-to-right', async () => {
    const user = userEvent.setup();
    render(<App />);

    await user.click(screen.getByRole('button', { name: 'Français' }));

    expect(screen.getByRole('button', { name: 'Français' })).toHaveAttribute('aria-pressed', 'true');
    expect(document.documentElement.dir).toBe('ltr');
    expect(await screen.findByText('Connecté')).toBeInTheDocument();
    expect(screen.getByText('fr (ltr)')).toBeInTheDocument();
  });

  it('reports the API as unavailable when the request fails', async () => {
    vi.stubGlobal('fetch', vi.fn(() => Promise.resolve(new Response('', { status: 503 }))));
    vi.spyOn(console, 'warn').mockImplementation(() => {});
    render(<App />);

    expect(await screen.findByText('غير متاح')).toBeInTheDocument();
  });
});

