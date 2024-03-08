import './App.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import MainLayout from './layouts/main-layout'
import Home from './pages/home'
import Login from './pages/login'


const router = createBrowserRouter([{
  path: '/',
  element: <Home />
}, {
  path: '/admin',
  element: <MainLayout />,
  children: [
    { path: '', element: <h1>Home</h1> },
  ]
},{
  path: '/login',
  element: <Login/>
}])


export default function App() {
  return (
    <RouterProvider router={router} />
  )
}
