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

export interface SystemInfo {
  name: string;
  version: string;
  culture: string;
  direction: 'rtl' | 'ltr';
  defaultCulture: string;
  supportedCultures: string[];
}

export interface AgeGroupSummary {
  ageYears: number;
  slug: string;
  themeKey: string;
  iconEmoji: string;
  title: string;
  subtitle: string;
  description: string;
  domainCount: number;
  courseCount: number;
  activityCount: number;
}

export interface AgeGroupsOverview {
  culture: string;
  direction: 'rtl' | 'ltr';
  ageGroups: AgeGroupSummary[];
}

export interface LearningObjective {
  code: string;
  order: number;
  description: string;
}

export interface LessonSummary {
  slug: string;
  order: number;
  estimatedMinutes: number;
  title: string;
  summary: string;
  activityCount: number;
  has3DScene: boolean;
}

export interface CourseSummary {
  slug: string;
  targetAge: number;
  order: number;
  difficultyLevel: number;
  iconEmoji: string;
  title: string;
  description: string;
  completionCriteria: string;
  objectives: LearningObjective[];
  lessons: LessonSummary[];
}

export interface CourseCollection {
  slug: string;
  order: number;
  isReadingPathway: boolean;
  title: string;
  summary: string;
  courses: CourseSummary[];
}

export interface LearningDomain {
  slug: string;
  order: number;
  iconEmoji: string;
  accentColor: string;
  title: string;
  summary: string;
  collections: CourseCollection[];
}

export interface AgeGroupDetail {
  ageYears: number;
  slug: string;
  themeKey: string;
  iconEmoji: string;
  culture: string;
  direction: 'rtl' | 'ltr';
  title: string;
  subtitle: string;
  description: string;
  domains: LearningDomain[];
}

export type ActivityType =
  | 'scene_3d_explore'
  | 'audio_listen_pick'
  | 'visual_match_sort'
  | 'counting_interactive'
  | 'letter_forms_vowels'
  | 'syllable_word_builder';

export interface CurriculumActivity {
  slug: string;
  stepOrder: number;
  activityType: ActivityType;
  objectiveCode: string;
  title: string;
  prompt: string;
  spokenText: string;
  glbModelUrl?: string | null;
  targetNodeName?: string | null;
  focusPoint: [number, number, number];
  interactionPayloadJson: string;
}

export interface LessonDetail {
  slug: string;
  courseSlug: string;
  courseTitle: string;
  ageYears: number;
  culture: string;
  direction: 'rtl' | 'ltr';
  title: string;
  summary: string;
  estimatedMinutes: number;
  activities: CurriculumActivity[];
}

export interface AgeProgressSummary {
  ageYears: number;
  title: string;
  completedActivities: number;
  totalActivities: number;
}

export interface LearnerProgress {
  learnerKey: string;
  totalCompletedActivities: number;
  totalActivities: number;
  completedActivitySlugs: string[];
  byAge: AgeProgressSummary[];
}

export function getSystemInfo(options?: RequestOptions): Promise<SystemInfo> {
  return apiGet<SystemInfo>('/v1/system/info', options);
}

export function getAgeGroups(options?: RequestOptions): Promise<AgeGroupsOverview> {
  return apiGet<AgeGroupsOverview>('/v1/curriculum/age-groups', options);
}

export function getAgeGroupDetail(ageYears: number, options?: RequestOptions): Promise<AgeGroupDetail> {
  return apiGet<AgeGroupDetail>(`/v1/curriculum/age-groups/${ageYears}`, options);
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

export function completeActivity(
  activitySlug: string,
  learnerKey = 'default',
  options?: RequestOptions,
): Promise<LearnerProgress> {
  return apiPost<LearnerProgress, { learnerKey: string }>(
    `/v1/curriculum/activities/${encodeURIComponent(activitySlug)}/complete`,
    { learnerKey },
    options,
  );
}
