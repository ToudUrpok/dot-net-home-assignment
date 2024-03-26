import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import sassDts from 'vite-plugin-sass-dts'
import path from 'path'

export default defineConfig({
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `@use "@/styles" as common;`,
        importer(...args: string[]) {
          if (args[0] !== "@/styles") {
            return;
          }

          return {
            file: `${path.resolve(
              __dirname,
              './src/app/styles'
            )}`,
          };
        },
      },
    },
  },
  plugins: [
    react(),
    sassDts({
      global: {
        generate: true,
        outputFilePath: path.resolve(__dirname, './src/app/types/style.d.ts'),
      },
    })
  ],
})
