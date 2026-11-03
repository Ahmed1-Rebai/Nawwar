# API Design — NAWWAR (`Nawwar.Api`)

## 1. Conventions

- **Base Path**: `/api/v1`
- **Localization**: Reads `Accept-Language` (`ar`, `fr`, `en`; defaults to `ar`) and echoes `Content-Language` on every response.
- **Error Format**: RFC 9457 Problem Details (`application/problem+json`).

---

## 2. Endpoints

### `GET /health/live`
Liveness probe (`200 OK`).

### `GET /health/ready`
Readiness probe (`200 OK` when PostgreSQL connectivity check succeeds, `503 Service Unavailable` otherwise).

### `GET /api/v1/system/info`
Returns API identity, version, resolved culture, text direction (`rtl` or `ltr`), default culture (`ar`), and supported cultures.

### `GET /api/v1/curriculum/modules`
Returns localized curriculum modules and their `.glb` lesson summaries in the active request culture.

```json
{
  "culture": "ar",
  "direction": "rtl",
  "modules": [
    {
      "slug": "optics-and-light",
      "order": 1,
      "title": "البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ",
      "summary": "استكشاف سلوك الأشعة الضوئية والعدسات وتجارب ابن الهيثم البصرية.",
      "lessons": [
        {
          "slug": "alhazen-optical-lantern",
          "order": 1,
          "title": "مِصْبَاحُ ابْنِ الهَيْثَمِ وَالحُجْرَةُ المُظْلِمَةُ",
          "objective": "فهم انتقال الضوء في خطوط مستقيمة عبر الثقوب الضيقة وتكوّن الصور.",
          "modelUrl": "/models/lantern.glb",
          "modelFormat": "model/gltf-binary",
          "checkpointCount": 3
        }
      ]
    }
  ]
}
```

### `GET /api/v1/curriculum/lessons/{slug}`
Returns a single localized 3D lesson, its `.glb` asset metadata (`url`, `format: "model/gltf-binary"`, `cameraPosition`, `cameraTarget`), and its ordered 3D scene checkpoints (`stepOrder`, `targetNodeName`, `focusPoint`, `title`, `explanation`). Returns `404 Not Found` (`application/problem+json`) if `{slug}` does not exist.

### `GET /api/v1/curriculum/progress?learnerKey={learnerKey}`
Returns completed 3D scene checkpoints across all curriculum lessons for `learnerKey` (defaults to `"default"`).

```json
{
  "learnerKey": "default",
  "totalCompletedSteps": 1,
  "totalCheckpoints": 9,
  "lessons": [
    {
      "lessonSlug": "alhazen-optical-lantern",
      "completedSteps": [1],
      "totalCheckpoints": 3
    }
  ]
}
```

### `POST /api/v1/curriculum/lessons/{slug}/checkpoints/{stepOrder}/complete`
Idempotently records that `learnerKey` completed checkpoint `{stepOrder}` of lesson `{slug}` and returns the updated `LearnerProgressResponse`. Returns `404 Not Found` (`application/problem+json`) if the lesson or checkpoint step does not exist.
