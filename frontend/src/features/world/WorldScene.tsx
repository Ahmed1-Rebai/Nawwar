import { useFrame } from '@react-three/fiber';
import { useMemo, useRef } from 'react';
import type { Mesh } from 'three';

function prefersReducedMotion(): boolean {
  return typeof window !== 'undefined' && window.matchMedia?.('(prefers-reduced-motion: reduce)').matches === true;
}

/**
 * Demonstration scene that validates the R3F pipeline (lighting, materials,
 * animation loop, controls). It is NOT product content.
 */
export function WorldScene() {
  const lantern = useRef<Mesh>(null);
  const reducedMotion = useMemo(() => prefersReducedMotion(), []);

  useFrame((_, delta) => {
    if (!lantern.current || reducedMotion) return;
    lantern.current.rotation.y += delta * 0.35;
    lantern.current.rotation.x += delta * 0.1;
  });

  return (
    <>
      <hemisphereLight args={['#fff6e0', '#1b2a44', 0.9]} />
      <directionalLight position={[4, 6, 3]} intensity={1.6} />
      <pointLight position={[0, 1.2, 0]} color="#ffb347" intensity={6} distance={6} />

      <mesh ref={lantern} position={[0, 1.2, 0]}>
        <icosahedronGeometry args={[0.8, 1]} />
        <meshStandardMaterial color="#f4c76b" emissive="#d98a1f" emissiveIntensity={0.6} roughness={0.35} metalness={0.2} flatShading />
      </mesh>

      <mesh rotation={[-Math.PI / 2, 0, 0]}>
        <circleGeometry args={[3, 64]} />
        <meshStandardMaterial color="#20385c" roughness={0.9} />
      </mesh>
    </>
  );
}

