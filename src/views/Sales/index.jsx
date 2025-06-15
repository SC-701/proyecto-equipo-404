import React from 'react';
import { Typography, Box } from '@mui/material';

const Sales = () => {
  return (
    <Box p={2}>
      <Typography variant="h4" gutterBottom>
        Sales
      </Typography>
      <Typography variant="body1">
        Aquí podrás ver y analizar las ventas realizadas.
      </Typography>
    </Box>
  );
};

export default Sales;
