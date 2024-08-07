import "./App.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./_layout";

import { lazy, Suspense } from "react";
import Loading from "./features/Loading";

const RegisterPage = lazy(() => import("./app/pages/register/page"));
const LoginPage = lazy(() => import("./app/pages/login/page"));
const MainLayout = lazy(() => import("./app/layout"));
const WelcomeLayout = lazy(() => import("./app/pages/welcome/layout"));
const WelcomePage = lazy(() => import("./app/pages/welcome/page"));
const AppPage = lazy(() => import("./app/pages/app/page"));
const AppDetailPage = lazy(() => import("./app/pages/app-detail/page"));
const WikiPage = lazy(() => import("./app/pages/wiki/page"));
const WikiDetailPage = lazy(() => import("./app/pages/wiki-detail/page"));
const UserPage = lazy(() => import("./app/pages/user/page"));
const SettingPage = lazy(() => import("./app/pages/setting/page"));
const ChatPage = lazy(() => import("./app/pages/chat/page"));


const router = createBrowserRouter([
  {
    path: "",
    element: <Layout></Layout>,
    children: [
      {
        path: "",
        element: <Suspense fallback={<Loading />}>
          <MainLayout />
        </Suspense>,
        children: [
          {
            path: "/",
            element: <WelcomeLayout>
              <WelcomePage />
            </WelcomeLayout>,
          },
          {
            path: '/app',
            element: <Suspense fallback={<Loading />}>
              <AppPage />
            </Suspense>
          },
          {
            path: '/app/:id',
            element: <Suspense fallback={<Loading />}>
              <AppDetailPage />
            </Suspense>
          },
          {
            path: '/wiki',
            element: <Suspense fallback={<Loading />}>
              <WikiPage />
            </Suspense>
          },
          {
            path: '/wiki/:id',
            element: <Suspense fallback={<Loading />}>
              <WikiDetailPage />
            </Suspense>
          },
          {
            path:"/user",
            element: <Suspense fallback={<Loading />}>
              <UserPage />
            </Suspense>
          },
          {
            path:"/setting",
            element: <Suspense fallback={<Loading />}>
              <SettingPage />
            </Suspense>
          },
          {
            path:"/chat",
            element: <Suspense fallback={<Loading />}>
              <ChatPage />
            </Suspense>
          }
        ]
      },
      {
        path: "/login",
        element: <Suspense fallback={<Loading />}>
          <LoginPage />
        </Suspense>
      },
      {
        path: "/register",
        element: <Suspense fallback={<Loading />}>
          <RegisterPage />
        </Suspense>
      }
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
