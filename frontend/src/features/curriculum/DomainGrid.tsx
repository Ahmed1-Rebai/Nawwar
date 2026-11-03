import { useTranslation } from 'react-i18next';
import type { LearningDomain } from '../../lib/api';

interface DomainGridProps {
  domains: LearningDomain[];
  selectedDomainSlug: string;
  onSelectDomain: (domainSlug: string) => void;
}

/**
 * Displays the LearningDomains for the active preschool age group as large,
 * color-accented interactive cards.
 */
export function DomainGrid({
  domains,
  selectedDomainSlug,
  onSelectDomain,
}: DomainGridProps) {
  const { t } = useTranslation();

  return (
    <section className="domain-section" aria-labelledby="domains-heading">
      <h3 id="domains-heading" className="section-subtitle">
        {t('curriculum.domainsHeading')}
      </h3>
      <div className="domain-grid" role="tablist" aria-label={t('curriculum.domainsHeading')}>
        {domains.map((domain) => {
          const isSelected = domain.slug === selectedDomainSlug;
          return (
            <button
              key={domain.slug}
              type="button"
              role="tab"
              aria-selected={isSelected}
              className="domain-card"
              onClick={() => onSelectDomain(domain.slug)}
            >
              <span className="domain-card__emoji" aria-hidden="true">
                {domain.iconEmoji}
              </span>
              <div className="domain-card__body">
                <strong className="domain-card__title">{domain.title}</strong>
                <span className="domain-card__summary">{domain.summary}</span>
              </div>
            </button>
          );
        })}
      </div>
    </section>
  );
}
