import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  pickLocalizedText,
  speakLocalizedText,
  type CommonWordExampleItem,
} from './types';

interface CommonWordsShowcaseProps {
  examples: CommonWordExampleItem[];
}

/**
 * Step 2 — Shows how the learned letter, sound, number, color, or concept appears in
 * familiar everyday words (animals, fruits, objects, nature) with spoken audio before
 * the child reaches Step 3 (Friendly Exercise).
 */
export function CommonWordsShowcase({ examples }: CommonWordsShowcaseProps) {
  const { i18n } = useTranslation();
  const [listenedIndices, setListenedIndices] = useState<number[]>([]);
  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  if (examples.length === 0) {
    return null;
  }

  const handleExampleTap = (index: number, item: CommonWordExampleItem) => {
    setActiveIndex(index);
    if (!listenedIndices.includes(index)) {
      setListenedIndices((prev) => [...prev, index]);
    }
    const highlight = pickLocalizedText(
      i18n.language,
      item.highlightAr,
      item.highlightFr,
      item.highlightEn,
    );
    const word = pickLocalizedText(i18n.language, item.wordAr, item.wordFr, item.wordEn);
    const sentence = pickLocalizedText(
      i18n.language,
      item.sentenceAr,
      item.sentenceFr,
      item.sentenceEn,
    );
    speakLocalizedText(`${highlight} : ${word}. ${sentence}`, i18n.language);
  };

  const sectionTitle = pickLocalizedText(
    i18n.language,
    '🦁🍎 الخُطْوَةُ الثَّانِيَةُ: الكَلِمَاتُ المَأْلُوفَةُ (حَيَوَانَاتٌ وَفَوَاكِهُ وَأَشْيَاءُ) — اِضْغَطْ لِسَمَاعِ الكَلِمَةِ وَالجُمْلَةِ!',
    '🦁🍎 Étape 2 : Mots familiers (animaux, fruits et objets) — Touche chaque carte pour écouter !',
    '🦁🍎 Step 2: Common Words (Animals, Fruits & Everyday Objects) — Tap each card to listen!',
  );

  const counterLabel = pickLocalizedText(
    i18n.language,
    `اِسْتَمَعْتَ إِلَى ${listenedIndices.length} مِنْ ${examples.length} كَلِمَاتٍ`,
    `${listenedIndices.length} / ${examples.length} mots écoutés`,
    `Listened to ${listenedIndices.length} of ${examples.length} words`,
  );

  return (
    <div className="common-words-showcase">
      <div className="common-words-showcase__header">
        <span className="common-words-showcase__title">{sectionTitle}</span>
        <span className="common-words-showcase__counter">🌟 {counterLabel}</span>
      </div>

      <div className="common-words-showcase__grid">
        {examples.map((item, idx) => {
          const isListened = listenedIndices.includes(idx);
          const isActive = activeIndex === idx;
          const category = pickLocalizedText(
            i18n.language,
            item.categoryAr,
            item.categoryFr,
            item.categoryEn,
          );
          const highlight = pickLocalizedText(
            i18n.language,
            item.highlightAr,
            item.highlightFr,
            item.highlightEn,
          );
          const word = pickLocalizedText(i18n.language, item.wordAr, item.wordFr, item.wordEn);
          const sentence = pickLocalizedText(
            i18n.language,
            item.sentenceAr,
            item.sentenceFr,
            item.sentenceEn,
          );

          return (
            <article
              key={`${item.wordAr}-${idx}`}
              tabIndex={0}
              className={`common-word-card ${isActive ? 'common-word-card--active' : ''} ${
                isListened ? 'common-word-card--listened' : ''
              }`}
              style={{ '--word-accent': item.accentColor || '#0ea5e9' } as React.CSSProperties}
              onClick={() => handleExampleTap(idx, item)}
              onKeyDown={(e) => {
                if (e.key === 'Enter' || e.key === ' ') {
                  e.preventDefault();
                  handleExampleTap(idx, item);
                }
              }}
            >
              <div className="common-word-card__badges">
                <span className="common-word-card__category">{category}</span>
                <span className="common-word-card__highlight">{highlight}</span>
              </div>

              <div className="common-word-card__visual">
                <span className="common-word-card__emoji" aria-hidden="true">
                  {item.emoji}
                </span>
                <span className="common-word-card__speaker" aria-hidden="true">
                  🔊 {isListened ? '⭐' : ''}
                </span>
              </div>

              <strong className="common-word-card__word">{word}</strong>
              <p className="common-word-card__sentence">{sentence}</p>
            </article>
          );
        })}
      </div>
    </div>
  );
}
