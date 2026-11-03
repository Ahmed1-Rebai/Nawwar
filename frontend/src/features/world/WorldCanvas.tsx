import { OrbitControls } from '@react-three/drei';
import { Canvas } from '@react-three/fiber';
import { WorldScene } from './WorldScene';

interface WorldCanvasProps {
  label: string;
}

/**
 * Lazy-loaded entry point for 3D rendering (keeps three.js out of the initial bundle).
 * The canvas is direction-agnostic: RTL/LTR only affects the HTML around it.
 */
export default function WorldCanvas({ label }: WorldCanvasProps) {
  return (
    <div className="world-canvas" role="img" aria-label={label}>
      <Canvas
        dpr={[1, 2]}
        camera={{ position: [3.2, 2.4, 4.2], fov: 45, near: 0.1, far: 100 }}
        gl={{ antialias: true, powerPreference: 'high-performance' }}
      >
        <color attach="background" args={['#0f1b2d']} />
        <WorldScene />
        <OrbitControls enablePan={false} minDistance={3} maxDistance={9} maxPolarAngle={Math.PI / 2.1} target={[0, 0.8, 0]} />
      </Canvas>
    </div>
  );
}

