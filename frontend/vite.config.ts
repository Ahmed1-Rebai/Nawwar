/// <reference types="vitest/config" />
import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';

// In Docker Compose (dev) the API is reachable by service name: http://api:8080.
const apiProxyTarget = process.env.VITE_API_PROXY_TARGET ?? 'http://localhost:5080';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    port: 5173,
    strictPort: true,
    proxy: {
      '/api': { target: apiProxyTarget, changeOrigin: true },
    },
    watch: {
      // Enable when bind-mount file events are not delivered (Docker Desktop on some hosts).
      usePolling: process.env.CHOKIDAR_USEPOLLING === 'true',
    },
  },
  build: {
    sourcemap: false,
    chunkSizeWarningLimit: 1200, // three.js lives in a lazily loaded chunk
  },
  test: {
    environment: 'jsdom',
    setupFiles: ['./src/test/setup.ts'],
    css: false,
    restoreMocks: true,
  },
});
