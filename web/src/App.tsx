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
import WikiDetail from './pages/wiki-detail/page'
import ShareChat from './pages/share-chat/page'


const router = createBrowserRouter([{
  path: '/',
  element: <Home />
}, {
  element: <MainLayout />,
  children: [
    { path: '/app', element: <App /> },
    { path: '/app/:id', element: <AppDetail /> },
    { path: '/wiki', element: <Wiki /> },
    { path: '/wiki/:id', element: <WikiDetail /> },
    { path: '/chat', element: <Chat /> },
    { path: '/user', element: <User /> },
  ]
}, {
  path: '/login',
  element: <Login />
}, {
  path: '/share-chat',
  element: <ShareChat />
}])


export default function AppPage() {
  return (
    <ThemeProvider defaultThemeMode='dark'>
      <RouterProvider router={router} />
    </ThemeProvider>
  )
}
