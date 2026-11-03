import { OrbitControls } from '@react-three/drei';
import { Canvas } from '@react-three/fiber';
import { WorldScene } from './WorldScene';

export interface WorldCanvasProps {
  label: string;
  /** Binary glTF 2.0 (.glb) model URL. Defaults to `/models/lantern.glb`. */
  modelUrl?: string;
  /** Optional scene-graph node name highlighted by the active checkpoint. */
  activeNodeName?: string;
  /** Optional 3D focus point [x, y, z] for the active checkpoint. */
  focusPoint?: [number, number, number];
  /** Optional callback fired when a learner clicks a named 3D mesh node inside the .glb scene. */
  onSelectNode?: (nodeName: string) => void;
}

/**
 * Lazy-loaded entry point for 3D (.glb) rendering (keeps three.js out of the initial bundle).
 * The canvas is direction-agnostic: RTL/LTR only affects the HTML around it.
 */
export default function WorldCanvas({
  label,
  modelUrl = '/models/lantern.glb',
  activeNodeName,
  focusPoint,
  onSelectNode,
}: WorldCanvasProps) {
  return (
    <div className="world-canvas" role="img" aria-label={label} data-model-url={modelUrl}>
      <Canvas
        dpr={[1, 2]}
        camera={{ position: [3.4, 2.5, 4.2], fov: 45, near: 0.1, far: 100 }}
        gl={{ antialias: true, powerPreference: 'high-performance' }}
      >
        <color attach="background" args={['#0f1b2d']} />
        <WorldScene
          modelUrl={modelUrl}
          activeNodeName={activeNodeName}
          focusPoint={focusPoint}
          onSelectNode={onSelectNode}
        />
        <OrbitControls
          enablePan={false}
          minDistance={2.5}
          maxDistance={9}
          maxPolarAngle={Math.PI / 2.05}
          target={focusPoint ?? [0, 0.9, 0]}
        />
      </Canvas>
    </div>
  );
}
