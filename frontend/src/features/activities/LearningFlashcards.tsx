import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  pickLocalizedText,
  speakLocalizedText,
  type LearningCardItem,
} from './types';

interface LearningFlashcardsProps {
  cards: LearningCardItem[];
}

/**
 * Step 1 — Interactive spoken flashcard deck ("نَتَعَلَّمُ الدَّرْسَ وَالصَّوْتَ")
 * where the child explores and listens to each letter, sound, number, or concept
 * before seeing common words (Step 2) or playing the friendly exercise (Step 3).
 */
export function LearningFlashcards({ cards }: LearningFlashcardsProps) {
  const { i18n } = useTranslation();
  const [exploredIndices, setExploredIndices] = useState<number[]>([]);
  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  if (cards.length === 0) {
    return null;
  }

  const handleCardTap = (index: number, card: LearningCardItem) => {
    setActiveIndex(index);
    if (!exploredIndices.includes(index)) {
      setExploredIndices((prev) => [...prev, index]);
    }
    const title = pickLocalizedText(i18n.language, card.titleAr, card.titleFr, card.titleEn);
    const soundBadge = card.soundBadgeAr
      ? pickLocalizedText(
          i18n.language,
          card.soundBadgeAr,
          card.soundBadgeFr ?? card.soundBadgeAr,
          card.soundBadgeEn ?? card.soundBadgeAr,
        )
      : '';
    const fact = pickLocalizedText(i18n.language, card.factAr, card.factFr, card.factEn);
    speakLocalizedText(
      soundBadge ? `${title}. ${soundBadge}. ${fact}` : `${title}. ${fact}`,
      i18n.language,
    );
  };

  const sectionTitle = pickLocalizedText(
    i18n.language,
    '📖 الخُطْوَةُ الأُولَى: نَتَعَلَّمُ الدَّرْسَ وَالصَّوْتَ — اِضْغَطْ عَلَى كُلِّ بِطَاقَةٍ لِتَسْتَمِعَ وَتُرَدِّدَ!',
    '📖 Étape 1 : Apprenons la leçon et le son — Touche chaque carte pour écouter et répéter !',
    '📖 Step 1: Learn the Concept & Sound — Tap each card to listen and repeat!',
  );

  const exploredLabel = pickLocalizedText(
    i18n.language,
    `تَعَلَّمْتَ ${exploredIndices.length} مِنْ ${cards.length}`,
    `${exploredIndices.length} / ${cards.length} apprises`,
    `Learned ${exploredIndices.length} of ${cards.length}`,
  );

  return (
    <div className="learning-deck">
      <div className="learning-deck__header">
        <span className="learning-deck__title">{sectionTitle}</span>
        <span className="learning-deck__counter">⭐ {exploredLabel}</span>
      </div>

      <div className="learning-deck__grid">
        {cards.map((card, idx) => {
          const isExplored = exploredIndices.includes(idx);
          const isActive = activeIndex === idx;
          const title = pickLocalizedText(i18n.language, card.titleAr, card.titleFr, card.titleEn);
          const fact = pickLocalizedText(i18n.language, card.factAr, card.factFr, card.factEn);
          const soundBadge = card.soundBadgeAr
            ? pickLocalizedText(
                i18n.language,
                card.soundBadgeAr,
                card.soundBadgeFr ?? card.soundBadgeAr,
                card.soundBadgeEn ?? card.soundBadgeAr,
              )
            : null;

          return (
            <article
              key={`${card.titleAr}-${idx}`}
              tabIndex={0}
              className={`learning-flashcard ${isActive ? 'learning-flashcard--active' : ''} ${
                isExplored ? 'learning-flashcard--explored' : ''
              }`}
              style={{ '--card-accent': card.accentColor || '#f59e0b' } as React.CSSProperties}
              onClick={() => handleCardTap(idx, card)}
              onKeyDown={(e) => {
                if (e.key === 'Enter' || e.key === ' ') {
                  e.preventDefault();
                  handleCardTap(idx, card);
                }
              }}
            >
              <div className="learning-flashcard__top">
                <span className="learning-flashcard__speaker" aria-hidden="true">
                  🔊
                </span>
                {soundBadge && <span className="learning-flashcard__sound-pill">{soundBadge}</span>}
                <span className="learning-flashcard__status" aria-hidden="true">
                  {isExplored ? '⭐' : '✨'}
                </span>
              </div>
              <span className="learning-flashcard__emoji" aria-hidden="true">
                {card.emoji}
              </span>
              <strong className="learning-flashcard__title">{title}</strong>
              <span className="learning-flashcard__fact">{fact}</span>
            </article>
          );
        })}
      </div>
    </div>
  );
}
