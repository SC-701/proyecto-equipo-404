import PropTypes from 'prop-types';
import React from 'react';


// material-ui
import { useTheme } from '@mui/material/styles';
import { Box, Grid, IconButton } from '@mui/material';
import MenuTwoToneIcon from '@mui/icons-material/MenuTwoTone';


// project import
import SearchSection from './SearchSection';
import ProfileSection from './ProfileSection';
import NotificationSection from './NotificationSection';

// assets
import logo from 'assets/images/logo.png';

// ==============================|| HEADER ||============================== //

const Header = (drawerToggle ) => {
  const theme = useTheme();

  return (
    <>
      <Box width="100%" sx={{ zIndex: 1201 }}>
  <Grid
    container
    alignItems="center"
    justifyContent="space-between"
    sx={{
      height: '64px',
      backgroundColor: theme.palette.primary.main,
      px: 2
    }}
  >
    {/* Logo + Botón del menú */}
    <Grid item sx={{ display: 'flex', alignItems: 'center' }}>
      <Box sx={{ display: { xs: 'block', md: 'none' }, mr: 1 }}>
        <IconButton
          edge="start"
          color="inherit"
          aria-label="open drawer"
          onClick={drawerToggle}
          size="large"
        >
          <MenuTwoToneIcon sx={{ fontSize: '1.5rem' }} />
        </IconButton>
      </Box>
      <img
        src={logo}
        alt="Logo"
        style={{
          height: '40px',
          width: 'auto',
          objectFit: 'contain'
        }}
      />
    </Grid>

    {/* Secciones a la derecha */}
    <Grid item sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
      <SearchSection theme="light" />
      <NotificationSection />
      <ProfileSection />
    </Grid>
  </Grid>
</Box>




     
    </>
  );
};

Header.propTypes = {
  drawerToggle: PropTypes.func
};

export default Header;
