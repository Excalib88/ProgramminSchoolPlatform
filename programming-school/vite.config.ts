import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    host: '0.0.0.0',
    hmr: {
      host: 'localhost',
      port:81,
      overlay: false
    },
  },
  plugins: [react()],
})
