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

export interface LearningCardItem {
  emoji: string;
  titleAr: string;
  titleFr: string;
  titleEn: string;
  factAr: string;
  factFr: string;
  factEn: string;
  accentColor: string;
}

export interface CountingPlaygroundConfig {
  emoji: string;
  count: number;
  addendA?: number;
  addendB?: number;
  itemNameAr: string;
  itemNameFr: string;
  itemNameEn: string;
}

export interface LetterStudioFormItem {
  form: string;
  positionAr: string;
  positionFr: string;
  positionEn: string;
  wordAr: string;
  wordFr: string;
  wordEn: string;
  emoji: string;
  color: string;
}

export interface LetterStudioConfig {
  baseLetter: string;
  forms: LetterStudioFormItem[];
}

export interface ParsedActivityPayload {
  sceneObjects?: SceneObjectItem[];
  syllables?: string[];
  learningCards?: LearningCardItem[];
  countingItems?: CountingPlaygroundConfig;
  letterStudio?: LetterStudioConfig;
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

/**
 * Returns explicit `learningCards` if defined on the activity payload, or automatically
 * synthesizes interactive discovery flashcards from the activity options so every course
 * lesson provides an interactive "learn & explore" step before the quiz challenge.
 */
export function getEffectiveLearningCards(payload: ParsedActivityPayload): LearningCardItem[] {
  if (payload.learningCards && payload.learningCards.length > 0) {
    return payload.learningCards;
  }

  if (payload.options && payload.options.length > 0) {
    return payload.options.map((opt) => ({
      emoji: opt.emoji,
      titleAr: opt.labelAr,
      titleFr: opt.labelFr,
      titleEn: opt.labelEn,
      factAr: `اِضْغَطْ لِسَمَاعِ النُّطْقِ: ${opt.labelAr}`,
      factFr: `Touche pour écouter : ${opt.labelFr}`,
      factEn: `Tap to listen: ${opt.labelEn}`,
      accentColor: opt.accentColor,
    }));
  }

  return [];
}

export function speakLocalizedText(text: string, language: string): void {
  if (typeof window === 'undefined' || !('speechSynthesis' in window) || !text) {
    return;
  }

  try {
    window.speechSynthesis.cancel();
    const utterance = new SpeechSynthesisUtterance(text);
    utterance.lang = language === 'fr' ? 'fr-FR' : language === 'en' ? 'en-US' : 'ar-SA';
    utterance.rate = 0.88;
    utterance.pitch = 1.08;
    window.speechSynthesis.speak(utterance);
  } catch {
    // Ignore speech synthesis errors in headless/test environments.
  }
}
