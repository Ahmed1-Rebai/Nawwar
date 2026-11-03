/** Typed client for the NAWWAR API. Requests are same-origin (`/api`), proxied by Vite (dev) or nginx (prod). */
const API_BASE_URL: string = import.meta.env.VITE_API_BASE_URL ?? '/api';

export const GLB_MIME_TYPE = 'model/gltf-binary';
export const GLB_EXTENSION = '.glb';

export function isValidGlbUrl(url: string): boolean {
  return url.trim().toLowerCase().endsWith(GLB_EXTENSION);
}

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

export async function apiPost<TResponse, TBody>(
  path: string,
  body: TBody,
  options: RequestOptions = {},
): Promise<TResponse> {
  const headers: Record<string, string> = {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  };
  if (options.language) headers['Accept-Language'] = options.language;

  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: 'POST',
    headers,
    body: JSON.stringify(body),
    signal: options.signal,
  });
  if (!response.ok) {
    throw new ApiError(response.status, `POST ${path} failed with HTTP ${response.status}`);
  }
  return (await response.json()) as TResponse;
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

export interface LessonSummary {
  slug: string;
  order: number;
  title: string;
  objective: string;
  modelUrl: string;
  modelFormat: string;
  checkpointCount: number;
}

export interface LearningModule {
  slug: string;
  order: number;
  title: string;
  summary: string;
  lessons: LessonSummary[];
}

export interface CurriculumOverview {
  culture: string;
  direction: 'rtl' | 'ltr';
  modules: LearningModule[];
}

export interface LessonAsset {
  url: string;
  format: string;
  cameraPosition: [number, number, number];
  cameraTarget: [number, number, number];
}

export interface LessonCheckpoint {
  stepOrder: number;
  targetNodeName: string;
  focusPoint: [number, number, number];
  title: string;
  explanation: string;
}

export interface LessonDetail {
  slug: string;
  moduleSlug: string;
  culture: string;
  direction: 'rtl' | 'ltr';
  title: string;
  objective: string;
  asset: LessonAsset;
  checkpoints: LessonCheckpoint[];
}

export interface LessonProgress {
  lessonSlug: string;
  completedSteps: number[];
  totalCheckpoints: number;
}

export interface LearnerProgress {
  learnerKey: string;
  totalCompletedSteps: number;
  totalCheckpoints: number;
  lessons: LessonProgress[];
}

export function getSystemInfo(options?: RequestOptions): Promise<SystemInfo> {
  return apiGet<SystemInfo>('/v1/system/info', options);
}

export function getCurriculumModules(options?: RequestOptions): Promise<CurriculumOverview> {
  return apiGet<CurriculumOverview>('/v1/curriculum/modules', options);
}

export function getLessonBySlug(slug: string, options?: RequestOptions): Promise<LessonDetail> {
  return apiGet<LessonDetail>(`/v1/curriculum/lessons/${encodeURIComponent(slug)}`, options);
}

export function getLearnerProgress(learnerKey = 'default', options?: RequestOptions): Promise<LearnerProgress> {
  return apiGet<LearnerProgress>(
    `/v1/curriculum/progress?learnerKey=${encodeURIComponent(learnerKey)}`,
    options,
  );
}

export function completeCheckpoint(
  slug: string,
  stepOrder: number,
  learnerKey = 'default',
  options?: RequestOptions,
): Promise<LearnerProgress> {
  return apiPost<LearnerProgress, { learnerKey: string }>(
    `/v1/curriculum/lessons/${encodeURIComponent(slug)}/checkpoints/${stepOrder}/complete`,
    { learnerKey },
    options,
  );
}
