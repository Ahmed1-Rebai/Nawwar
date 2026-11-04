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
  soundBadgeAr?: string;
  soundBadgeFr?: string;
  soundBadgeEn?: string;
  factAr: string;
  factFr: string;
  factEn: string;
  accentColor: string;
}

export interface CommonWordExampleItem {
  emoji: string;
  wordAr: string;
  wordFr: string;
  wordEn: string;
  categoryAr: string;
  categoryFr: string;
  categoryEn: string;
  highlightAr: string;
  highlightFr: string;
  highlightEn: string;
  sentenceAr: string;
  sentenceFr: string;
  sentenceEn: string;
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
  wordExamples?: CommonWordExampleItem[];
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
 * Step 1 helper: Returns explicit `learningCards` if defined on the activity payload, or
 * synthesizes interactive concept & sound discovery flashcards so every course starts with learning.
 */
export function getEffectiveLearningCards(payload: ParsedActivityPayload): LearningCardItem[] {
  if (payload.learningCards && payload.learningCards.length > 0) {
    return payload.learningCards;
  }

  if (payload.letterStudio && payload.letterStudio.forms.length > 0) {
    return payload.letterStudio.forms.map((f) => ({
      emoji: f.emoji,
      titleAr: `${f.form} — ${f.positionAr}`,
      titleFr: `${f.form} — ${f.positionFr}`,
      titleEn: `${f.form} — ${f.positionEn}`,
      soundBadgeAr: `صَوْتُ الحَرْفِ: ${f.form}`,
      soundBadgeFr: `Son : ${f.form}`,
      soundBadgeEn: `Sound: ${f.form}`,
      factAr: `مِثَالٌ فِي كَلِمَةِ: «${f.wordAr}»`,
      factFr: `Exemple dans le mot : « ${f.wordFr} »`,
      factEn: `Example in word: '${f.wordEn}'`,
      accentColor: f.color,
    }));
  }

  if (payload.options && payload.options.length > 0) {
    return payload.options.map((opt) => ({
      emoji: opt.emoji,
      titleAr: opt.labelAr,
      titleFr: opt.labelFr,
      titleEn: opt.labelEn,
      soundBadgeAr: '🔊 اِسْتَمِعْ لِلصَّوْتِ',
      soundBadgeFr: '🔊 Écoute le son',
      soundBadgeEn: '🔊 Listen to the sound',
      factAr: `اِضْغَطْ لِسَمَاعِ النُّطْقِ الوَاضِحِ: ${opt.labelAr}`,
      factFr: `Touche pour écouter : ${opt.labelFr}`,
      factEn: `Tap to listen: ${opt.labelEn}`,
      accentColor: opt.accentColor,
    }));
  }

  return [];
}

/**
 * Step 2 helper: Returns explicit `wordExamples` (animals, fruits, everyday objects)
 * or synthesizes familiar word examples from the lesson payload so Step 2 always
 * shows rich contextual words before the friendly exercise in Step 3.
 */
export function getEffectiveWordExamples(payload: ParsedActivityPayload): CommonWordExampleItem[] {
  if (payload.wordExamples && payload.wordExamples.length > 0) {
    return payload.wordExamples;
  }

  if (payload.letterStudio && payload.letterStudio.forms.length > 0) {
    return payload.letterStudio.forms.map((f) => ({
      emoji: f.emoji,
      wordAr: f.wordAr,
      wordFr: f.wordFr,
      wordEn: f.wordEn,
      categoryAr: '🌟 كَلِمَةٌ مَأْلُوفَةٌ',
      categoryFr: '🌟 Mot familier',
      categoryEn: '🌟 Familiar Word',
      highlightAr: f.form,
      highlightFr: f.form,
      highlightEn: f.form,
      sentenceAr: `نَسْمَعُ وَنَرَى «${f.form}» فِي كَلِمَةِ «${f.wordAr}» (${f.positionAr}).`,
      sentenceFr: `On voit « ${f.form} » dans le mot « ${f.wordFr} ».`,
      sentenceEn: `We see '${f.form}' inside the word '${f.wordEn}'.`,
      accentColor: f.color,
    }));
  }

  if (payload.learningCards && payload.learningCards.length > 0) {
    return payload.learningCards.map((card) => ({
      emoji: card.emoji,
      wordAr: card.titleAr,
      wordFr: card.titleFr,
      wordEn: card.titleEn,
      categoryAr: '🐾 مِثَالٌ مَأْلُوفٌ',
      categoryFr: '🐾 Exemple familier',
      categoryEn: '🐾 Familiar Example',
      highlightAr: card.soundBadgeAr ?? card.titleAr,
      highlightFr: card.soundBadgeFr ?? card.titleFr,
      highlightEn: card.soundBadgeEn ?? card.titleEn,
      sentenceAr: card.factAr,
      sentenceFr: card.factFr,
      sentenceEn: card.factEn,
      accentColor: card.accentColor,
    }));
  }

  if (payload.options && payload.options.length > 0) {
    return payload.options.map((opt) => ({
      emoji: opt.emoji,
      wordAr: opt.labelAr,
      wordFr: opt.labelFr,
      wordEn: opt.labelEn,
      categoryAr: '🌟 كَلِمَةٌ مَأْلُوفَةٌ',
      categoryFr: '🌟 Mot familier',
      categoryEn: '🌟 Familiar Word',
      highlightAr: opt.labelAr,
      highlightFr: opt.labelFr,
      highlightEn: opt.labelEn,
      sentenceAr: `هَذِهِ كَلِمَةٌ مَأْلُوفَةٌ نَتَعَلَّمُهَا مَعًا: «${opt.labelAr}»!`,
      sentenceFr: `Voici un mot familier : « ${opt.labelFr} » !`,
      sentenceEn: `Here is a familiar word we learn together: '${opt.labelEn}'!`,
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
