/**
 * Deterministic generator for NAWWAR's binary glTF 2.0 (.glb) 3D lesson assets.
 *
 * Produces spec-compliant glTF 2.0 binary files (`magic = 0x46546C67` "glTF", version 2)
 * in `public/models/*.glb` with named scene-graph nodes matching the backend
 * `LessonCheckpoint.TargetNodeName` values in `CurriculumCatalog.cs`.
 */
import { mkdirSync, writeFileSync } from 'node:fs';
import { dirname, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import {
  BoxGeometry,
  CylinderGeometry,
  IcosahedronGeometry,
  OctahedronGeometry,
  SphereGeometry,
  TorusGeometry,
} from 'three';

const __dirname = dirname(fileURLToPath(import.meta.url));
const outDir = resolve(__dirname, '../public/models');
mkdirSync(outDir, { recursive: true });

/**
 * Extracts indexed triangle geometry (POSITION, NORMAL, INDICES) from a Three.js BufferGeometry.
 * @param {import('three').BufferGeometry} geometry
 */
function extractGeometryData(geometry) {
  const nonIndexed = geometry.index ? geometry : geometry.toNonIndexed();
  const posAttr = nonIndexed.getAttribute('position');
  const normAttr = nonIndexed.getAttribute('normal');

  const positions = new Float32Array(posAttr.array.length);
  positions.set(posAttr.array);

  const normals = new Float32Array(normAttr.array.length);
  normals.set(normAttr.array);

  const min = [Infinity, Infinity, Infinity];
  const max = [-Infinity, -Infinity, -Infinity];
  for (let i = 0; i < positions.length; i += 3) {
    for (let axis = 0; axis < 3; axis++) {
      const value = positions[i + axis];
      if (value < min[axis]) min[axis] = value;
      if (value > max[axis]) max[axis] = value;
    }
  }

  let indices;
  if (nonIndexed.index) {
    indices = new Uint16Array(nonIndexed.index.array.length);
    indices.set(nonIndexed.index.array);
  } else {
    const vertexCount = positions.length / 3;
    indices = new Uint16Array(vertexCount);
    for (let i = 0; i < vertexCount; i++) indices[i] = i;
  }

  return { positions, normals, indices, min, max };
}

/**
 * Packs a list of named mesh parts into a single binary glTF 2.0 (.glb) Buffer.
 */
function buildGlb(parts) {
  const bufferViews = [];
  const accessors = [];
  const materials = [];
  const meshes = [];
  const nodes = [];
  const binaryChunks = [];
  let byteOffset = 0;

  function appendChunk(typedArray, target) {
    const bytes = new Uint8Array(typedArray.buffer, typedArray.byteOffset, typedArray.byteLength);
    const pad = (4 - (bytes.byteLength % 4)) % 4;
    const viewIndex = bufferViews.length;
    bufferViews.push({
      buffer: 0,
      byteOffset,
      byteLength: bytes.byteLength,
      ...(target ? { target } : {}),
    });
    binaryChunks.push(bytes);
    if (pad > 0) binaryChunks.push(new Uint8Array(pad));
    byteOffset += bytes.byteLength + pad;
    return viewIndex;
  }

  for (const part of parts) {
    const { positions, normals, indices, min, max } = extractGeometryData(part.geometry);

    const posView = appendChunk(positions, 34962); // ARRAY_BUFFER
    const normView = appendChunk(normals, 34962);
    const idxView = appendChunk(indices, 34963); // ELEMENT_ARRAY_BUFFER

    const posAccessor = accessors.length;
    accessors.push({
      bufferView: posView,
      componentType: 5126, // FLOAT
      count: positions.length / 3,
      type: 'VEC3',
      min,
      max,
    });

    const normAccessor = accessors.length;
    accessors.push({
      bufferView: normView,
      componentType: 5126, // FLOAT
      count: normals.length / 3,
      type: 'VEC3',
    });

    const idxAccessor = accessors.length;
    accessors.push({
      bufferView: idxView,
      componentType: 5123, // UNSIGNED_SHORT
      count: indices.length,
      type: 'SCALAR',
    });

    const materialIndex = materials.length;
    materials.push({
      name: `${part.name}_Material`,
      pbrMetallicRoughness: {
        baseColorFactor: part.baseColor,
        metallicFactor: part.metallic ?? 0.2,
        roughnessFactor: part.roughness ?? 0.4,
      },
      ...(part.emissive ? { emissiveFactor: part.emissive } : {}),
    });

    const meshIndex = meshes.length;
    meshes.push({
      name: `${part.name}_Mesh`,
      primitives: [
        {
          attributes: { POSITION: posAccessor, NORMAL: normAccessor },
          indices: idxAccessor,
          material: materialIndex,
        },
      ],
    });

    nodes.push({
      name: part.name,
      mesh: meshIndex,
      translation: part.translation ?? [0, 0, 0],
      ...(part.rotation ? { rotation: part.rotation } : {}),
      ...(part.scale ? { scale: part.scale } : {}),
    });
  }

  const gltfJson = {
    asset: { version: '2.0', generator: 'NAWWAR GLB Asset Builder 0.2.0' },
    scene: 0,
    scenes: [{ name: 'NawwarLessonScene', nodes: nodes.map((_, idx) => idx) }],
    nodes,
    meshes,
    materials,
    accessors,
    bufferViews,
    buffers: [{ byteLength: byteOffset }],
  };

  const jsonBytes = Buffer.from(JSON.stringify(gltfJson), 'utf8');
  const jsonPad = (4 - (jsonBytes.byteLength % 4)) % 4;
  const jsonPadded = Buffer.concat([jsonBytes, Buffer.alloc(jsonPad, 0x20)]); // pad JSON chunk with spaces

  const binBuffer = Buffer.concat(binaryChunks.map((u8) => Buffer.from(u8)));

  const totalLength = 12 + 8 + jsonPadded.byteLength + 8 + binBuffer.byteLength;
  const header = Buffer.alloc(12);
  header.writeUInt32LE(0x46546c67, 0); // "glTF"
  header.writeUInt32LE(2, 4); // version 2
  header.writeUInt32LE(totalLength, 8);

  const jsonChunkHeader = Buffer.alloc(8);
  jsonChunkHeader.writeUInt32LE(jsonPadded.byteLength, 0);
  jsonChunkHeader.writeUInt32LE(0x4e4f534a, 4); // "JSON"

  const binChunkHeader = Buffer.alloc(8);
  binChunkHeader.writeUInt32LE(binBuffer.byteLength, 0);
  binChunkHeader.writeUInt32LE(0x004e4942, 4); // "BIN\0"

  return Buffer.concat([header, jsonChunkHeader, jsonPadded, binChunkHeader, binBuffer]);
}

// 1. Ibn al-Haytham Optical Lantern (lantern.glb)
const lanternGlb = buildGlb([
  {
    name: 'Light_Core',
    geometry: new IcosahedronGeometry(0.62, 1),
    translation: [0, 1.25, 0],
    baseColor: [0.96, 0.78, 0.42, 1.0],
    emissive: [0.85, 0.54, 0.12],
    metallic: 0.2,
    roughness: 0.3,
  },
  {
    name: 'Aperture_Ring',
    geometry: new TorusGeometry(0.95, 0.08, 16, 48),
    translation: [0, 1.25, 0],
    baseColor: [0.38, 0.75, 0.95, 1.0],
    emissive: [0.08, 0.25, 0.42],
    metallic: 0.5,
    roughness: 0.25,
  },
  {
    name: 'Lantern_Base',
    geometry: new CylinderGeometry(1.1, 1.35, 0.45, 24),
    translation: [0, 0.25, 0],
    baseColor: [0.22, 0.34, 0.52, 1.0],
    metallic: 0.4,
    roughness: 0.6,
  },
]);

// 2. Sun-Earth-Moon System (solar-system.glb)
const solarGlb = buildGlb([
  {
    name: 'Core_Sun',
    geometry: new SphereGeometry(0.72, 24, 18),
    translation: [0, 1.0, 0],
    baseColor: [0.99, 0.76, 0.28, 1.0],
    emissive: [0.92, 0.52, 0.1],
    metallic: 0.1,
    roughness: 0.35,
  },
  {
    name: 'Planet_Earth',
    geometry: new SphereGeometry(0.34, 24, 18),
    translation: [1.75, 1.0, 0],
    baseColor: [0.24, 0.62, 0.92, 1.0],
    emissive: [0.04, 0.14, 0.28],
    metallic: 0.15,
    roughness: 0.45,
  },
  {
    name: 'Moon_Satellite',
    geometry: new SphereGeometry(0.14, 16, 12),
    translation: [2.35, 1.15, 0],
    baseColor: [0.82, 0.85, 0.9, 1.0],
    emissive: [0.1, 0.12, 0.15],
    metallic: 0.1,
    roughness: 0.7,
  },
]);

// 3. Octahedral Crystal Lattice (crystal-lattice.glb)
const crystalGlb = buildGlb([
  {
    name: 'Central_Atom',
    geometry: new OctahedronGeometry(0.52, 1),
    translation: [0, 1.2, 0],
    baseColor: [0.3, 0.82, 0.65, 1.0],
    emissive: [0.08, 0.36, 0.26],
    metallic: 0.3,
    roughness: 0.25,
  },
  {
    name: 'Bond_Axis',
    geometry: new BoxGeometry(1.9, 0.14, 0.14),
    translation: [0, 1.2, 0],
    baseColor: [0.76, 0.84, 0.96, 1.0],
    metallic: 0.55,
    roughness: 0.3,
  },
  {
    name: 'Vertex_Ligand',
    geometry: new SphereGeometry(0.28, 20, 16),
    translation: [0, 2.15, 0],
    baseColor: [0.95, 0.56, 0.38, 1.0],
    emissive: [0.35, 0.14, 0.06],
    metallic: 0.25,
    roughness: 0.35,
  },
]);

const outputs = [
  ['lantern.glb', lanternGlb],
  ['solar-system.glb', solarGlb],
  ['crystal-lattice.glb', crystalGlb],
];

for (const [filename, buffer] of outputs) {
  const fullPath = resolve(outDir, filename);
  writeFileSync(fullPath, buffer);
  console.log(`Generated ${filename} (${buffer.byteLength} bytes)`);
}
