import './App.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import { Suspense, lazy } from 'react'
import MainLayout from './layouts/main-layout'
import Home from './pages/home/page'
import Login from './pages/login/page'
import App from './pages/app/page'

import { ThemeProvider } from '@lobehub/ui'
import FunctionCall from './pages/function-call/page'
const Chat = lazy(() => import('./pages/chat/page'));
const User = lazy(() => import('./pages/user/page'));

const AppDetail = lazy(() => import('./pages/app-detail/page'));

const WikiDetail = lazy(() => import('./pages/wiki-detail/page'));

const ShareChat = lazy(() => import('./pages/share-chat/page'));

const Wiki = lazy(() => import('./pages/wiki/page'));

const Register = lazy(() => import('./pages/register/page'));

const CreateFunctionCall = lazy(() => import('./pages/function-call/features/CreateFunctionCall'));

const router = createBrowserRouter([{
  path: '/',
  element: <Home />
}, {
  element: <MainLayout />,
  children: [
    {
      path: '/app', element: <Suspense fallback={'加载中'}>
        <App />
      </Suspense>
    },
    {
      path: '/app/:id', element: <Suspense fallback={'加载中'}>
        <AppDetail />
      </Suspense>
    },
    {
      path: '/wiki', element: <Suspense fallback={'加载中'}>
        <Wiki />
      </Suspense>
    },
    {
      path: '/wiki/:id', element: <Suspense fallback={'加载中'}>
        <WikiDetail />
      </Suspense>
    },
    {
      path: '/chat', element: <Suspense fallback={'加载中'}>
        <Chat />
      </Suspense>
    },
    {
      path: '/user', element: <Suspense fallback={'加载中'}>
        <User />
      </Suspense>
    },
    {
      path: '/function-call', element: <Suspense fallback={'加载中'}>
        <FunctionCall />
      </Suspense>
    },
    {
      path: '/function-call/create', element: <Suspense fallback={'加载中'}>
        <CreateFunctionCall />
      </Suspense>
    }
  ]
}, {
  path: '/login',
  element: <Login />
}, {
  path: '/register',
  element: <Suspense fallback={'加载中'}>
    <Register />
  </Suspense>
}, {
  path: '/share-chat',
  element: <Suspense fallback={'加载中'}>
    <ShareChat />
  </Suspense>
}])


export default function AppPage() {
  return (
    <ThemeProvider defaultThemeMode='dark'>
      <RouterProvider router={router} />
    </ThemeProvider>
  )
}
