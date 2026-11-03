import { useGLTF } from '@react-three/drei';
import { useFrame } from '@react-three/fiber';
import { useEffect, useMemo, useRef } from 'react';
import type { Group, Mesh, MeshStandardMaterial, Object3D } from 'three';
import { isValidGlbUrl } from '../../lib/api';

function prefersReducedMotion(): boolean {
  return typeof window !== 'undefined' && window.matchMedia?.('(prefers-reduced-motion: reduce)').matches === true;
}

export interface WorldSceneProps {
  /** Path to a binary glTF 2.0 (.glb) 3D asset. */
  modelUrl: string;
  /** Optional scene-graph node name highlighted by the active lesson checkpoint. */
  activeNodeName?: string;
  /** Optional 3D focus coordinates [x, y, z] for the checkpoint marker ring. */
  focusPoint?: [number, number, number];
}

/**
 * Interactive 3D scene that loads a binary glTF 2.0 (.glb) educational model,
 * highlights the active checkpoint node, and respects `prefers-reduced-motion`.
 */
export function WorldScene({ modelUrl, activeNodeName, focusPoint }: WorldSceneProps) {
  if (!isValidGlbUrl(modelUrl)) {
    throw new Error(`Invalid 3D asset format for '${modelUrl}'. Only binary glTF 2.0 (.glb) models are permitted.`);
  }

  const gltf = useGLTF(modelUrl);
  const rootGroup = useRef<Group>(null);
  const reducedMotion = useMemo(() => prefersReducedMotion(), []);

  // Clone the loaded GLB scene so material highlight mutations stay isolated.
  const sceneClone = useMemo(() => {
    const cloned = gltf.scene.clone(true);
    cloned.traverse((obj: Object3D) => {
      const mesh = obj as Mesh;
      if (mesh.isMesh && mesh.material) {
        mesh.material = (mesh.material as MeshStandardMaterial).clone();
      }
    });
    return cloned;
  }, [gltf.scene]);

  useEffect(() => {
    sceneClone.traverse((obj: Object3D) => {
      const mesh = obj as Mesh;
      if (!mesh.isMesh || !mesh.material) return;
      const mat = mesh.material as MeshStandardMaterial;
      const isSelected = Boolean(activeNodeName && obj.name === activeNodeName);
      mat.emissiveIntensity = isSelected ? 1.35 : 0.45;
    });
  }, [sceneClone, activeNodeName]);

  useFrame((_, delta) => {
    if (!rootGroup.current || reducedMotion) return;
    rootGroup.current.rotation.y += delta * 0.25;
  });

  return (
    <>
      <hemisphereLight args={['#fff6e0', '#1b2a44', 0.95]} />
      <directionalLight position={[4, 6, 3]} intensity={1.65} />
      <pointLight position={[0, 1.4, 0]} color="#ffb347" intensity={5} distance={7} />

      <group ref={rootGroup}>
        <primitive object={sceneClone} />
        {focusPoint && (
          <mesh position={focusPoint} rotation={[Math.PI / 2, 0, 0]}>
            <torusGeometry args={[0.48, 0.03, 12, 36]} />
            <meshStandardMaterial color="#f4c76b" emissive="#f4c76b" emissiveIntensity={1.1} />
          </mesh>
        )}
      </group>

      <mesh rotation={[-Math.PI / 2, 0, 0]}>
        <circleGeometry args={[3.2, 64]} />
        <meshStandardMaterial color="#20385c" roughness={0.9} />
      </mesh>
    </>
  );
}
