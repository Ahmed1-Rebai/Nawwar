import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  pickLocalizedText,
  speakLocalizedText,
  type LetterStudioConfig,
  type LetterStudioFormItem,
} from './types';

interface LetterVowelStudioProps {
  studio: LetterStudioConfig;
}

/**
 * Interactive Arabic Letter Forms & Short Vowels Studio (`letter_forms_vowels`).
 * Allows children to tap positional letter forms (بـ / ـبـ / ـب) or short vowels
 * (بَ / بِ / بُ) to see a giant highlighted letter card, illustrated anchor word,
 * and spoken Arabic audio.
 */
export function LetterVowelStudio({ studio }: LetterVowelStudioProps) {
  const { i18n } = useTranslation();
  const [selectedIndex, setSelectedIndex] = useState<number>(0);

  const forms = studio.forms ?? [];
  if (forms.length === 0) {
    return null;
  }

  const activeForm: LetterStudioFormItem = forms[selectedIndex] ?? forms[0];

  const handleSelectForm = (idx: number, item: LetterStudioFormItem) => {
    setSelectedIndex(idx);
    speakLocalizedText(`${item.form} ، ${item.wordAr}`, 'ar');
  };

  const studioTitle = pickLocalizedText(
    i18n.language,
    `✍️ مَرْسَمُ الحَرْفِ العَرَبِيِّ (${studio.baseLetter}) — اِضْغَطْ عَلَى كُلِّ شَكْلٍ لِتَسْمَعَ صَوْتَهُ وَكَلِمَتَهُ!`,
    `✍️ Atelier de la lettre (${studio.baseLetter}) — Touche chaque forme pour écouter !`,
    `✍️ Arabic Letter Studio (${studio.baseLetter}) — Tap each form to hear its sound & word!`,
  );

  return (
    <div className="letter-studio">
      <div className="letter-studio__header">
        <span className="letter-studio__title">{studioTitle}</span>
      </div>

      <div className="letter-studio__stage">
        <div
          className="letter-studio__spotlight"
          style={{ '--studio-color': activeForm.color || '#f59e0b' } as React.CSSProperties}
        >
          <span className="letter-studio__giant-char" lang="ar" dir="rtl">
            {activeForm.form}
          </span>
          <span className="letter-studio__position">
            {pickLocalizedText(
              i18n.language,
              activeForm.positionAr,
              activeForm.positionFr,
              activeForm.positionEn,
            )}
          </span>
        </div>

        <div className="letter-studio__word-box">
          <span className="letter-studio__word-emoji" aria-hidden="true">
            {activeForm.emoji}
          </span>
          <div className="letter-studio__word-text">
            <strong className="letter-studio__word-ar" lang="ar" dir="rtl">
              {activeForm.wordAr}
            </strong>
            <span className="letter-studio__word-sub">
              {pickLocalizedText(
                i18n.language,
                'مِثَالٌ مُصَوَّرٌ نَاطِقٌ',
                activeForm.wordFr,
                activeForm.wordEn,
              )}
            </span>
          </div>
          <button
            type="button"
            className="letter-studio__speak-btn"
            onClick={() => speakLocalizedText(`${activeForm.form} ، ${activeForm.wordAr}`, 'ar')}
          >
            🔊 {pickLocalizedText(i18n.language, 'اِسْتَمِعْ', 'Écouter', 'Listen')}
          </button>
        </div>
      </div>

      <div className="letter-studio__tabs">
        {forms.map((item, idx) => {
          const isSelected = idx === selectedIndex;
          return (
            <button
              key={`${item.form}-${idx}`}
              type="button"
              className={`letter-studio__tab ${isSelected ? 'letter-studio__tab--active' : ''}`}
              onClick={() => handleSelectForm(idx, item)}
            >
              <span className="letter-studio__tab-char" lang="ar" dir="rtl">
                {item.form}
              </span>
              <span className="letter-studio__tab-label">
                {pickLocalizedText(i18n.language, item.positionAr, item.positionFr, item.positionEn)}
              </span>
            </button>
          );
        })}
      </div>
    </div>
  );
}
