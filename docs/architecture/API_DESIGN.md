# API Design (`docs/architecture/API_DESIGN.md`)

All browser-consumed endpoints are versioned under `/api/v1` and honor ASP.NET Core request localization (`Accept-Language: ar | fr | en`, defaulting to `ar`). Every response includes a `Content-Language` header, and errors follow RFC 9457 Problem Details (`application/problem+json`).

---

## Endpoints

### 1. `GET /api/v1/system/info`
Returns service metadata and the culture resolved for the current request.

```json
{
  "name": "NAWWAR",
  "version": "0.1.0",
  "culture": "ar",
  "direction": "rtl",
  "defaultCulture": "ar",
  "supportedCultures": ["ar", "fr", "en"]
}
```

### 2. `GET /api/v1/curriculum/modules`
Returns all curriculum modules and their interactive `.glb` lessons localized to the active request culture (`ar` default, `fr`, `en`).

```json
{
  "culture": "ar",
  "direction": "rtl",
  "modules": [
    {
      "slug": "optics-and-light",
      "order": 1,
      "title": "البَصَرِيَّاتُ وَفِيزْيَاءُ الضَّوْءِ",
      "summary": "اِسْتَكْشِفْ كَيْفَ يَنْتَقِلُ الضَّوْءُ فِي خُطُوطٍ مُسْتَقِيمَةٍ وَكَيْفَ فَسَّرَ ابْنُ الهَيْثَمِ الرُّؤْيَةَ.",
      "lessons": [
        {
          "slug": "alhazen-optical-lantern",
          "order": 1,
          "title": "مِصْبَاحُ ابْنِ الهَيْثَمِ وَالحُجْرَةُ المُظْلِمَةُ",
          "objective": "مُلاحَظَةُ مَسَارِ الأَشِعَّةِ الضَّوْئِيَّةِ عَبْرَ الثُّقْبِ الضَّيِّقِ وَتَشَكُّلِ الصُّورَةِ المَقْلُوبَةِ.",
          "modelUrl": "/models/lantern.glb",
          "modelFormat": "model/gltf-binary",
          "checkpointCount": 3
        }
      ]
    }
  ]
}
```

### 3. `GET /api/v1/curriculum/lessons/{slug}`
Returns a single localized 3D lesson, its `.glb` asset camera configuration, and its ordered 3D scene checkpoints (`targetNodeName` matches the named mesh node inside the `.glb` binary asset). Returns `404 Not Found` (`application/problem+json`) when `slug` does not exist.
