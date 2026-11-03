import { useTranslation } from 'react-i18next';
import type { CourseCollection } from '../../lib/api';

interface CoursePathwayViewProps {
  collections: CourseCollection[];
  activeLessonSlug: string;
  onSelectLesson: (lessonSlug: string) => void;
}

/**
 * Displays the CourseCollections and Courses inside the selected LearningDomain,
 * including the dedicated 6-course Age 5 Reading Pathway (`مَسَارُ تَعَلُّمِ القِرَاءَةِ`).
 */
export function CoursePathwayView({
  collections,
  activeLessonSlug,
  onSelectLesson,
}: CoursePathwayViewProps) {
  const { t } = useTranslation();

  return (
    <div className="collection-stack">
      {collections.map((collection) => (
        <section
          key={collection.slug}
          className={`collection-block ${collection.isReadingPathway ? 'collection-block--reading-pathway' : ''}`}
          aria-labelledby={`collection-${collection.slug}`}
        >
          <div className="collection-block__header">
            <div>
              {collection.isReadingPathway && (
                <span className="pathway-ribbon">📖 {t('curriculum.readingPathwayBadge')}</span>
              )}
              <h3 id={`collection-${collection.slug}`}>{collection.title}</h3>
              <p>{collection.summary}</p>
            </div>
          </div>

          <div className="course-grid" role="list">
            {collection.courses.map((course) => (
              <article key={course.slug} className="course-card" role="listitem">
                <div className="course-card__top">
                  <span className="course-card__emoji" aria-hidden="true">
                    {course.iconEmoji}
                  </span>
                  <div className="course-card__meta">
                    <span className="course-card__order">#{course.order}</span>
                    <span className="course-card__stars" aria-hidden="true">
                      {'⭐'.repeat(course.difficultyLevel)}
                    </span>
                  </div>
                </div>

                <h4 className="course-card__title">{course.title}</h4>
                <p className="course-card__description">{course.description}</p>

                {course.objectives.length > 0 && (
                  <div className="course-card__objectives">
                    <span className="course-card__label">{t('curriculum.objectivesLabel')}:</span>
                    <ul>
                      {course.objectives.map((obj) => (
                        <li key={obj.code}>
                          <code dir="ltr">{obj.code}</code> — {obj.description}
                        </li>
                      ))}
                    </ul>
                  </div>
                )}

                <div className="course-card__lessons">
                  <span className="course-card__label">{t('curriculum.lessonsLabel')}:</span>
                  {course.lessons.map((lesson) => {
                    const isActive = lesson.slug === activeLessonSlug;
                    return (
                      <button
                        key={lesson.slug}
                        type="button"
                        className="lesson-button"
                        aria-pressed={isActive}
                        onClick={() => onSelectLesson(lesson.slug)}
                      >
                        <div className="lesson-button__info">
                          <span className="lesson-button__title">{lesson.title}</span>
                          <span className="lesson-button__sub">
                            {lesson.estimatedMinutes} {t('curriculum.minutesUnit')}
                            {lesson.has3DScene ? ` · 🧊 ${t('curriculum.scene3DBadge')}` : ''}
                          </span>
                        </div>
                        <span className="lesson-button__cta">
                          {isActive ? t('curriculum.activeLesson') : t('curriculum.startLesson')}
                        </span>
                      </button>
                    );
                  })}
                </div>
              </article>
            ))}
          </div>
        </section>
      ))}
    </div>
  );
}
