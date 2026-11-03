import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import type { AgeGroupSummary } from '../../lib/api';

interface AgeWorldSelectorProps {
  ageGroups: AgeGroupSummary[];
  onSelectAge: (ageYears: number) => void;
}

/**
 * PHASE D — Main entry point of NAWWAR: presents the three visually distinct preschool
 * age-group worlds:
 * - عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ (Discovery)
 * - عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ (Exploration)
 * - عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ (Learning)
 */
export function AgeWorldSelector({ ageGroups, onSelectAge }: AgeWorldSelectorProps) {
  const { t } = useTranslation();

  return (
    <section className="age-selector" aria-labelledby="age-selector-heading">
      <div className="age-selector__intro">
        <h2 id="age-selector-heading">{t('ageSelector.heading')}</h2>
        <p>{t('ageSelector.subheading')}</p>
      </div>

      <div className="age-world-grid" role="list">
        {ageGroups.map((world) => (
          <article
            key={world.slug}
            role="listitem"
            className={`age-world-card age-world-card--${world.themeKey}`}
            data-testid={`age-world-card-${world.ageYears}`}
          >
            <div className="age-world-card__header">
              <span className="age-world-card__emoji" aria-hidden="true">
                {world.iconEmoji}
              </span>
              <span className="age-world-card__age-badge">{world.ageYears}</span>
            </div>

            <h3 className="age-world-card__title">{world.title}</h3>
            <p className="age-world-card__subtitle">{world.subtitle}</p>
            <p className="age-world-card__description">{world.description}</p>

            <div className="age-world-card__stats">
              <span className="stat-chip">
                <strong dir="ltr">{world.domainCount}</strong> {t('ageSelector.domainsBadge')}
              </span>
              <span className="stat-chip">
                <strong dir="ltr">{world.courseCount}</strong> {t('ageSelector.coursesBadge')}
              </span>
              <span className="stat-chip">
                <strong dir="ltr">{world.activityCount}</strong> {t('ageSelector.activitiesBadge')}
              </span>
            </div>

            <div className="age-world-card__actions">
              <AudioPromptButton text={`${world.title}. ${world.subtitle}`} />
              <button
                type="button"
                className="age-world-card__cta"
                onClick={() => onSelectAge(world.ageYears)}
              >
                {t('ageSelector.enterWorld')}: {world.title}
              </button>
            </div>
          </article>
        ))}
      </div>
    </section>
  );
}
