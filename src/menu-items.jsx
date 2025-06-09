// assets
import DashboardIcon from '@mui/icons-material/HomeOutlined';
import RestaurantIcon from '@mui/icons-material/RestaurantMenuOutlined';
import SalesIcon from '@mui/icons-material/PointOfSaleOutlined';
import TaskIcon from '@mui/icons-material/TaskOutlined';
import ReportIcon from '@mui/icons-material/BarChartOutlined';
import SecurityIcon from '@mui/icons-material/SecurityOutlined';

const icons = {
  DashboardIcon,
  RestaurantIcon,
  SalesIcon,
  TaskIcon,
  ReportIcon,
  SecurityIcon
};

// ==============================|| MENU ITEMS ||============================== //

export default {
  items: [
    {
      id: 'navigation',
      title: 'Inicio',
      caption: 'Dashboard',
      type: 'group',
      children: [
        {
          id: 'dashboard',
          title: 'Dashboard',
          type: 'item',
          icon: icons.DashboardIcon,
          url: '/dashboard/default'
        }
      ]
    },
    {
      id: 'gestion',
      title: 'Gestión',
      caption: 'Módulos principales',
      type: 'group',
      children: [
        {
          id: 'platillos',
          title: 'Platillos',
          type: 'item',
          icon: icons.RestaurantIcon,
          url: '/platillos'
        },
        {
          id: 'ventas',
          title: 'Ventas',
          type: 'item',
          icon: icons.SalesIcon,
          url: '/ventas'
        },
        {
          id: 'tareas',
          title: 'Tareas',
          type: 'item',
          icon: icons.TaskIcon,
          url: '/tareas'
        },
        {
          id: 'reportes',
          title: 'Reportes',
          type: 'item',
          icon: icons.ReportIcon,
          url: '/reportes'
        }
      ]
    },
    {
      id: 'auth',
      title: 'Autenticación',
      type: 'group',
      children: [
        {
          id: 'login',
          title: 'Login',
          type: 'item',
          url: '/application/login',
          icon: icons.SecurityIcon,
          target: true
        },
        {
          id: 'register',
          title: 'Register',
          type: 'item',
          url: '/application/register',
          icon: icons.SecurityIcon,
          target: true
        }
      ]
    }
  ]
};