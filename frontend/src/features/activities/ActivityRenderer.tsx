import type { CurriculumActivity } from '../../lib/api';
import { InteractiveChoiceActivity } from './InteractiveChoiceActivity';
import { Scene3DExploreActivity } from './Scene3DExploreActivity';

interface ActivityRendererProps {
  activity: CurriculumActivity;
  isCompleted: boolean;
  onComplete: (activitySlug: string) => void;
}

/**
 * Dispatches a curriculum activity to the appropriate modality renderer, keeping
 * educational content decoupled from 3D scene implementation.
 */
export function ActivityRenderer({ activity, isCompleted, onComplete }: ActivityRendererProps) {
  if (activity.activityType === 'scene_3d_explore') {
    return (
      <Scene3DExploreActivity
        key={activity.slug}
        activity={activity}
        isCompleted={isCompleted}
        onComplete={onComplete}
      />
    );
  }

  return (
    <InteractiveChoiceActivity
      key={activity.slug}
      activity={activity}
      isCompleted={isCompleted}
      onComplete={onComplete}
    />
  );
}
