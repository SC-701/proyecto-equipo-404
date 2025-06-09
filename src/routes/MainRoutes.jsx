import { Navigate } from 'react-router-dom';
import React, { lazy } from 'react';

// project import
import MainLayout from 'layout/MainLayout';
import Loadable from 'component/Loadable';

const DashboardDefault = Loadable(lazy(() => import('views/Dashboard/Default')));
const UtilsTypography = Loadable(lazy(() => import('views/Utils/Typography')));
const SamplePage = Loadable(lazy(() => import('views/SamplePage')));

// ==============================|| MAIN ROUTES ||============================== //

const MainRoutes = {
  path: '/',
  element: <MainLayout />,
  children: [
    {
      path: '/',
  element: <Navigate to="/application/login" />
    },
    {
  path: '/dashboard/default',
  element: localStorage.getItem('auth') === 'true'
    ? <DashboardDefault />
    : <Navigate to={`${import.meta.env.BASE_URL}/application/login`} />
}
,
    { path: '/utils/util-typography', element: <UtilsTypography /> },
    { path: '/sample-page', element: <SamplePage /> }
  ]
};

export default MainRoutes;
