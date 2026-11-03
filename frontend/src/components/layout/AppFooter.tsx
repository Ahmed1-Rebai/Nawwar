import { useTranslation } from 'react-i18next';

export function AppFooter() {
  const { t } = useTranslation();
  return <footer className="app__footer">{t('footer.phase')}</footer>;
}
