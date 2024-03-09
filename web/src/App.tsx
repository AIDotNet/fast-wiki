import './App.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import MainLayout from './layouts/main-layout'
import Home from './pages/home/page'
import Login from './pages/login/page'
import App from './pages/app/page'

import { ThemeProvider } from '@lobehub/ui'
import Wiki from './pages/wiki/page'
import Chat from './pages/chat/page'
import User from './pages/user/page'
import AppDetail from './pages/app-detail/page'


const router = createBrowserRouter([{
  path: '/',
  element: <Home />
}, {
  element: <MainLayout />,
  children: [
    { path: '/app', element: <App /> },
    { path: '/app/:id', element: <AppDetail /> }, // Add this line to bind /app/id
    { path: '/wiki', element: <Wiki /> },
    { path: '/chat', element: <Chat /> },
    { path: '/user', element: <User /> },
  ]
}, {
  path: '/login',
  element: <Login />
}])


export default function AppPage() {
  return (
    <ThemeProvider defaultThemeMode='dark'>
      <RouterProvider router={router} />
    </ThemeProvider>
  )
}
