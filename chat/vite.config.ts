import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { resolve } from 'path'


// https://vitejs.dev/config/
export default defineConfig({
  resolve: {
    alias: {
      '@': resolve(__dirname, './src'),
    },
    extensions: ['.js', '.jsx', '.ts', '.tsx']
  },
  define: {
    'process.env': {}
  },
  plugins: [react()],
  server: {
    proxy: {
      '/v1': {
        target: 'http://localhost:5124/',
        changeOrigin: true
      },
      "/api":
      {
        target: 'http://localhost:5124/',
        changeOrigin: true
      }
    }
  }
})
