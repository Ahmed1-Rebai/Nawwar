# 3D Rendering Architecture (`docs/architecture/THREE_D_ARCHITECTURE.md`)

This document outlines the Three.js and React Three Fiber (`@react-three/fiber`) architecture used in NAWWAR.

---

## 1. Component Hierarchy

```text
App.tsx
└── ErrorBoundary (localized fallback if WebGL is unavailable)
    └── Suspense (localized loading state while Three.js chunk downloads)
        └── WorldCanvas.tsx (lazy-loaded R3F <Canvas> + <OrbitControls>)
            └── WorldScene.tsx (lights, meshes, and frame loop)
```

---

## 2. Core Engineering Rules for 3D Scenes

1. **Code Splitting**:
   - Keep `three`, `@react-three/fiber`, and `@react-three/drei` inside lazily imported modules (`WorldCanvas.tsx`) so the initial UI shell loads rapidly on low-bandwidth connections.
2. **Coordinate System & RTL**:
   - WebGL 3D world coordinates remain standard right-handed Cartesian coordinates regardless of whether the HTML document is `rtl` or `ltr`.
   - Only 2D HTML HUD overlays, labels, and controls adapt to `dir="rtl"` / `dir="ltr"`.
3. **Accessibility & Reduced Motion**:
   - Every `<Canvas>` wrapper must expose `role="img"` and a localized `aria-label`.
   - Animation loops in `useFrame` must check `prefers-reduced-motion: reduce` and skip non-essential motion when enabled.
4. **Error Resilience**:
   - Always keep `<WorldCanvas>` inside `<ErrorBoundary>` so devices or headless browsers without WebGL support still render the rest of the application cleanly.
5. **Performance & Resource Management**:
   - Cap device pixel ratio with `dpr={[1, 2]}`.
   - When loading external glTF/GLB assets in Phase 3, use compressed textures/meshes and dispose resources when scenes unmount.

