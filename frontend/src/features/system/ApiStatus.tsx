import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { getSystemInfo, type SystemInfo } from '../../lib/api';

type Status =
  | { state: 'checking' }
  | { state: 'online'; requestedLanguage: string; info: SystemInfo }
  | { state: 'offline'; requestedLanguage: string };

/** Verifies browser → proxy → API connectivity and shows the culture the API resolved. */
export function ApiStatus() {
  const { t, i18n } = useTranslation();
  const [status, setStatus] = useState<Status>({ state: 'checking' });

  useEffect(() => {
    const controller = new AbortController();
    const requestedLanguage = i18n.language;
    getSystemInfo({ language: requestedLanguage, signal: controller.signal })
      .then((info) => setStatus({ state: 'online', requestedLanguage, info }))
      .catch((error: unknown) => {
        if (!controller.signal.aborted) {
          console.warn('API status check failed', error);
          setStatus({ state: 'offline', requestedLanguage });
        }
      });
    return () => controller.abort();
  }, [i18n.language]);

  const effectiveStatus: Status =
    status.state === 'checking' || status.requestedLanguage !== i18n.language ? { state: 'checking' } : status;

  return (
    <section className="card" aria-labelledby="api-status-title" aria-live="polite">
      <h2 id="api-status-title">{t('status.title')}</h2>
      <p className={`status status--${effectiveStatus.state}`}>
        <span className="status__dot" aria-hidden="true" />
        {t(`status.${effectiveStatus.state}`)}
      </p>
      {effectiveStatus.state === 'online' && (
        <dl className="status__details">
          <dt>{t('status.version')}</dt>
          <dd dir="ltr">{effectiveStatus.info.version}</dd>
          <dt>{t('status.serverLanguage')}</dt>
          <dd dir="ltr">
            {effectiveStatus.info.culture} ({effectiveStatus.info.direction})
          </dd>
        </dl>
      )}
    </section>
  );
}

