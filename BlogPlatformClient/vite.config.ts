import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr'

export default defineConfig({
  plugins: [
    svgr(),
    react()
  ],
  define: {
    __API_URL__: JSON.stringify('https://localhost:7298')
  }
})
