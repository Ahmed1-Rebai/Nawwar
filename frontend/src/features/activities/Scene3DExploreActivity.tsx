import { lazy, Suspense, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import { ErrorBoundary } from '../../components/ErrorBoundary';
import type { CurriculumActivity } from '../../lib/api';
import { CommonWordsShowcase } from './CommonWordsShowcase';
import { parseActivityPayload, pickLocalizedText, speakLocalizedText } from './types';

const WorldCanvas = lazy(() => import('../world/WorldCanvas'));

interface Scene3DExploreActivityProps {
  activity: CurriculumActivity;
  isCompleted: boolean;
  onComplete: (activitySlug: string) => void;
}

/**
 * PHASE F — Renders an age-appropriate 3D exploration activity (`scene_3d_explore`)
 * where the 3D scene supports the educational objective (3D color garden, friendly farm,
 * 3D counting blocks, or 3D Arabic letters stage) with spoken Arabic audio and common words.
 */
export function Scene3DExploreActivity({
  activity,
  isCompleted,
  onComplete,
}: Scene3DExploreActivityProps) {
  const { t, i18n } = useTranslation();
  const payload = parseActivityPayload(activity.interactionPayloadJson);
  const sceneObjects = payload.sceneObjects ?? [];
  const wordExamples = payload.wordExamples ?? [];

  const [selectedNode, setSelectedNode] = useState<string>(
    activity.targetNodeName ?? sceneObjects[0]?.nodeName ?? '',
  );

  const modelUrl = activity.glbModelUrl ?? '/models/garden-colors.glb';

  const handleSelectSceneNode = (nodeName: string) => {
    setSelectedNode(nodeName);
    const matched = sceneObjects.find((obj) => obj.nodeName === nodeName);
    if (matched) {
      const label = pickLocalizedText(
        i18n.language,
        matched.labelAr,
        matched.labelFr,
        matched.labelEn,
      );
      speakLocalizedText(label, i18n.language);
    }
  };

  return (
    <div className="activity-stage activity-stage--3d">
      <div className="activity-stage__header">
        <div>
          <span className="activity-stage__badge">{t('curriculum.scene3DBadge')}</span>
          <h4>{activity.title}</h4>
          <p className="activity-stage__prompt">{activity.prompt}</p>
        </div>
        <AudioPromptButton text={activity.spokenText} />
      </div>

      <ErrorBoundary fallback={<p className="notice">{t('world.unavailable')}</p>}>
        <Suspense fallback={<div className="world-canvas world-canvas--loading">{t('world.loading')}</div>}>
          <WorldCanvas
            label={t('world.canvasLabel')}
            modelUrl={modelUrl}
            activeNodeName={selectedNode}
            focusPoint={activity.focusPoint}
            onSelectNode={handleSelectSceneNode}
          />
        </Suspense>
      </ErrorBoundary>

      {sceneObjects.length > 0 && (
        <div className="scene-object-bar">
          <p className="scene-object-bar__hint">{t('curriculum.sceneTapHint')}</p>
          <div className="scene-object-cards">
            {sceneObjects.map((item) => {
              const isActive = item.nodeName === selectedNode;
              const label = pickLocalizedText(
                i18n.language,
                item.labelAr,
                item.labelFr,
                item.labelEn,
              );
              return (
                <button
                  key={item.nodeName}
                  type="button"
                  className="scene-object-card"
                  aria-pressed={isActive}
                  onClick={() => handleSelectSceneNode(item.nodeName)}
                >
                  <span className="scene-object-card__emoji" aria-hidden="true">
                    {item.emoji}
                  </span>
                  <span className="scene-object-card__label">{label}</span>
                </button>
              );
            })}
          </div>
        </div>
      )}

      {wordExamples.length > 0 && <CommonWordsShowcase examples={wordExamples} />}

      <div className="activity-stage__footer">
        <span className="world-badge" dir="ltr">
          {modelUrl}
        </span>
        <button
          type="button"
          className="checkpoint-complete-button"
          disabled={isCompleted}
          onClick={() => onComplete(activity.slug)}
        >
          {isCompleted ? t('curriculum.completed') : t('curriculum.markComplete')}
        </button>
      </div>
    </div>
  );
}
