import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  pickLocalizedText,
  speakLocalizedText,
  type CountingPlaygroundConfig,
} from './types';

interface CountingPlaygroundProps {
  config: CountingPlaygroundConfig;
}

const ARABIC_NUMBER_WORDS: Record<number, { digit: string; wordAr: string; wordFr: string; wordEn: string }> = {
  1: { digit: '١', wordAr: 'وَاحِدٌ', wordFr: 'Un', wordEn: 'One' },
  2: { digit: '٢', wordAr: 'اِثْنَانِ', wordFr: 'Deux', wordEn: 'Two' },
  3: { digit: '٣', wordAr: 'ثَلَاثَةٌ', wordFr: 'Trois', wordEn: 'Three' },
  4: { digit: '٤', wordAr: 'أَرْبَعَةٌ', wordFr: 'Quatre', wordEn: 'Four' },
  5: { digit: '٥', wordAr: 'خَمْسَةٌ', wordFr: 'Cinq', wordEn: 'Five' },
  6: { digit: '٦', wordAr: 'سِتَّةٌ', wordFr: 'Six', wordEn: 'Six' },
  7: { digit: '٧', wordAr: 'سَبْعَةٌ', wordFr: 'Sept', wordEn: 'Seven' },
  8: { digit: '٨', wordAr: 'ثَمَانِيَةٌ', wordFr: 'Huit', wordEn: 'Eight' },
  9: { digit: '٩', wordAr: 'تِسْعَةٌ', wordFr: 'Neuf', wordEn: 'Nine' },
  10: { digit: '١٠', wordAr: 'عَشَرَةٌ', wordFr: 'Dix', wordEn: 'Ten' },
};

/**
 * Interactive JavaScript/React Counting & Visual Addition Playground for preschool learners.
 * Children tap each illustrated object one by one to light it up, see the Arabic numeral,
 * and hear the spoken count aloud.
 */
export function CountingPlayground({ config }: CountingPlaygroundProps) {
  const { i18n } = useTranslation();
  const [countedIndices, setCountedIndices] = useState<number[]>([]);

  const totalItems = Math.max(1, Math.min(config.count, 10));
  const items = Array.from({ length: totalItems }, (_, i) => i);
  const currentCount = countedIndices.length;
  const itemName = pickLocalizedText(
    i18n.language,
    config.itemNameAr,
    config.itemNameFr,
    config.itemNameEn,
  );

  const handleTapItem = (index: number) => {
    if (countedIndices.includes(index)) {
      return;
    }
    const nextOrder = countedIndices.length + 1;
    setCountedIndices((prev) => [...prev, index]);

    const numEntry = ARABIC_NUMBER_WORDS[nextOrder];
    if (numEntry) {
      const spokenWord = pickLocalizedText(
        i18n.language,
        numEntry.wordAr,
        numEntry.wordFr,
        numEntry.wordEn,
      );
      speakLocalizedText(spokenWord, i18n.language);
    }
  };

  const handleReset = () => {
    setCountedIndices([]);
  };

  const headerTitle = pickLocalizedText(
    i18n.language,
    `🔢 لَوْحَةُ العَدِّ التَّفَاعُلِيَّةُ — اِضْغَطْ عَلَى كُلِّ عُنْصُرٍ لِنَعُدَّ (${itemName}) مَعاً!`,
    `🔢 Tableau de comptage interactif — Touche chaque élément (${itemName}) !`,
    `🔢 Interactive Counting Board — Tap each item (${itemName}) to count aloud!`,
  );

  const resetLabel = pickLocalizedText(
    i18n.language,
    '🔄 أَعِدِ العَدَّ',
    '🔄 Recommencer',
    '🔄 Reset Count',
  );

  return (
    <div className="counting-playground">
      <div className="counting-playground__header">
        <span className="counting-playground__title">{headerTitle}</span>
        <div className="counting-playground__controls">
          <span className="counting-playground__score">
            {currentCount} / {totalItems}
          </span>
          {currentCount > 0 && (
            <button type="button" className="counting-playground__reset" onClick={handleReset}>
              {resetLabel}
            </button>
          )}
        </div>
      </div>

      {config.addendA && config.addendB && (
        <div className="counting-playground__equation" dir="ltr">
          <span className="equation-pill">{config.addendA}</span>
          <span className="equation-op">+</span>
          <span className="equation-pill">{config.addendB}</span>
          <span className="equation-op">=</span>
          <span className="equation-pill equation-pill--total">
            {currentCount === totalItems ? totalItems : '?'}
          </span>
        </div>
      )}

      <div className="counting-playground__items">
        {items.map((idx) => {
          const orderIndex = countedIndices.indexOf(idx);
          const isCounted = orderIndex !== -1;
          const displayOrder = orderIndex + 1;
          const numberInfo = ARABIC_NUMBER_WORDS[displayOrder];
          const isSecondGroup = Boolean(config.addendA && idx >= config.addendA);

          return (
            <button
              key={idx}
              type="button"
              className={`counting-token ${isCounted ? 'counting-token--counted' : ''} ${
                isSecondGroup ? 'counting-token--group-b' : ''
              }`}
              onClick={() => handleTapItem(idx)}
            >
              <span className="counting-token__emoji" aria-hidden="true">
                {config.emoji}
              </span>
              <span className="counting-token__badge">
                {isCounted && numberInfo
                  ? `${numberInfo.digit} · ${pickLocalizedText(
                      i18n.language,
                      numberInfo.wordAr,
                      numberInfo.wordFr,
                      numberInfo.wordEn,
                    )}`
                  : pickLocalizedText(i18n.language, 'اِضْغَطْ لِلعَدِّ', 'Touche', 'Tap')}
              </span>
            </button>
          );
        })}
      </div>
    </div>
  );
}
