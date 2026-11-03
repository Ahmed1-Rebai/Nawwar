export interface SceneObjectItem {
  nodeName: string;
  emoji: string;
  labelAr: string;
  labelFr: string;
  labelEn: string;
  color: string;
}

export interface InteractiveOptionItem {
  id: string;
  emoji: string;
  labelAr: string;
  labelFr: string;
  labelEn: string;
  accentColor: string;
}

export interface ParsedActivityPayload {
  sceneObjects?: SceneObjectItem[];
  syllables?: string[];
  correctOptionId?: string;
  options?: InteractiveOptionItem[];
}

export function parseActivityPayload(json: string): ParsedActivityPayload {
  try {
    return JSON.parse(json) as ParsedActivityPayload;
  } catch {
    return {};
  }
}

export function pickLocalizedText(language: string, ar: string, fr: string, en: string): string {
  if (language === 'fr') return fr;
  if (language === 'en') return en;
  return ar;
}
