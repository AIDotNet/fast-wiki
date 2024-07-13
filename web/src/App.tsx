import { RouterProvider, createBrowserRouter } from "react-router-dom"
import { Suspense, lazy, useEffect } from "react"
import './App.css'

import Loading from "./app/(main)/(loading)/Client"
import SettingLoading from "./app/(main)/settings/loading"
const RootLayout = lazy(() => import("./app/layout"));
const WelcomeLayout = lazy(() => import("./app/(main)/welcome/layout"));
const WelcomePage = lazy(() => import("./app/(main)/welcome/page"));
const ChatLayout = lazy(() => import("./app/(main)/chat/layout"));
const SettingLayout = lazy(() => import('./app/(main)/settings/layout'));
const SettingModal = lazy(() => import('./app/(main)/settings/modal/page'));
const Category = lazy(() => import("./app/(main)/settings/@category/default"));
const SettingCommon = lazy(() => import("./app/(main)/settings/common/page"));
const SettingSystemAgent = lazy(() => import("./app/(main)/settings/system-agent/page"));
const SettingSync = lazy(() => import("./app/(main)/settings/sync/page"));
const SettingLLM = lazy(() => import("./app/(main)/settings/llm/page"));
const SettingTTS = lazy(() => import("./app/(main)/settings/tts/page"));
const SettingAgent = lazy(() => import("./app/(main)/settings/agent/page"));
const SettingAbout = lazy(() => import("./app/(main)/settings/about/page"));
const Auth = lazy(() => import('./app/auth/page'));
const MeLayout = lazy(() => import('@/app/(main)/(mobile)/me/(home)/layout'));
const MePage = lazy(() => import('@/app/(main)/(mobile)/me/(home)/page'));
const MeDataLayout = lazy(() => import('@/app/(main)/(mobile)/me/data/layout'));
const MeDataPage = lazy(() => import('@/app/(main)/(mobile)/me/data/page'));
const MainLayout = lazy(() => import("./app/(main)/layout"));
const ChatSettingLayout = lazy(() => import("./app/(main)/chat/settings/layout"));
const ChatSettingPage = lazy(() => import("./app/(main)/chat/settings/page"));
const RegisterPage = lazy(() => import("@/app/register/page"));
const AuthLogin = lazy(() => import("@/app/auth-login/page"));
const UserPage = lazy(() => import("@/app/(main)/user/page"));
const WikiPage = lazy(() => import("@/app/(main)/wiki/page"));
const WikiDetailPage = lazy(() => import("@/app/(main)/wiki-detail/page"));
const FunctionPage = lazy(() => import("@/app/(main)/function-call/page"));
const FunctionCreatePage = lazy(() => import("@/app/(main)/function-call/create/page"));
const AppPage = lazy(() => import("@/app/(main)/app/page"));
const AppDetailPage = lazy(() => import("@/app/(main)/app-detail/page"));

const router = createBrowserRouter([
  {
    element: <RootLayout></RootLayout>,
    children: [
      {
        path: '/app',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <AppPage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/app-detail',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <AppDetailPage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/function-call',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <FunctionPage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/function-call/create',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <FunctionCreatePage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/user',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <UserPage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/wiki',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <WikiPage />
        </MainLayout>
        </Suspense>
      }, {
        path: '/wiki-detail',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <WikiDetailPage />
        </MainLayout>
        </Suspense>
      },
      {
        path: '/me',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <MeLayout>
            <MePage />
          </MeLayout>
        </MainLayout>
        </Suspense>
      },
      {
        path: '/me/data',
        element:
          <Suspense fallback={<Loading />}><MainLayout>
            <MeDataLayout>
              <MeDataPage />
            </MeDataLayout>
          </MainLayout>
          </Suspense>
      },
      {
        path: '',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <WelcomeLayout>
            <WelcomePage />
          </WelcomeLayout>
        </MainLayout>
        </Suspense>
      },
      {
        path: 'chat',
        element: <Suspense fallback={<Loading />}><MainLayout>
          <ChatLayout>
          </ChatLayout>
        </MainLayout>
        </Suspense>,
        children: [
          {
            path: '*',
            element: <Suspense fallback={<Loading />}>
              <ChatSettingLayout >
                <ChatSettingPage />
              </ChatSettingLayout>
            </Suspense>
          }
        ]
      },
      {
        path: '/settings/modal',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingModal />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/common',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingCommon />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/system-agent',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}><Suspense fallback={<SettingLoading />}>
            <SettingSystemAgent />
          </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/sync',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingSync />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/llm',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingLLM />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/tts',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingTTS />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/agent',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingAgent />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      },
      {
        path: '/settings/about',
        element: <MainLayout>
          <SettingLayout category={<Category></Category>}>
            <Suspense fallback={<SettingLoading />}>
              <SettingAbout />
            </Suspense>
          </SettingLayout>
        </MainLayout>
      }
    ]
  },
  {
    path: '/auth',
    element: <Auth />
  }, {
    path: '/register',
    element: <Suspense fallback={<Loading />}>
      <RegisterPage />
    </Suspense>
  }, {
    path: '/auth-login',
    element: <Suspense fallback={<Loading />}>
      <AuthLogin />
    </Suspense>
  }
])

function App() {

  return (
    <>
      <RouterProvider router={router} />
    </>
  )
}

export default App
