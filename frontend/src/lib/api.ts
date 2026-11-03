/** Typed client for the NAWWAR API. Requests are same-origin (`/api`), proxied by Vite (dev) or nginx (prod). */
const API_BASE_URL: string = import.meta.env.VITE_API_BASE_URL ?? '/api';

export class ApiError extends Error {
  readonly status: number;

  constructor(status: number, message: string) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
  }
}

export interface RequestOptions {
  /** Sent as Accept-Language so the API answers in the user's language. */
  language?: string;
  signal?: AbortSignal;
}

export async function apiGet<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const headers: Record<string, string> = { Accept: 'application/json' };
  if (options.language) headers['Accept-Language'] = options.language;

  const response = await fetch(`${API_BASE_URL}${path}`, { headers, signal: options.signal });
  if (!response.ok) {
    throw new ApiError(response.status, `GET ${path} failed with HTTP ${response.status}`);
  }
  return (await response.json()) as T;
}

/** Mirrors `SystemInfoResponse` in backend/src/Nawwar.Api/Features/System/SystemEndpoints.cs. */
export interface SystemInfo {
  name: string;
  version: string;
  culture: string;
  direction: 'rtl' | 'ltr';
  defaultCulture: string;
  supportedCultures: string[];
}

export function getSystemInfo(options?: RequestOptions): Promise<SystemInfo> {
  return apiGet<SystemInfo>('/v1/system/info', options);
}

