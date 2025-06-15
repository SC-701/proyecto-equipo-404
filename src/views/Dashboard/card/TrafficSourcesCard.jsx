import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Card, CardHeader, CardContent, Typography, Divider, Grid, LinearProgress, Box } from '@mui/material';
import { useTheme } from '@mui/material/styles';
import { gridSpacing } from 'config.js';

const TrafficSourcesCard = () => {
  const theme = useTheme();
  const navigate = useNavigate();

  const data = [
    { label: '🍕 Pizza', value: 80, color: 'primary' },
    { label: '🍔 Burgers', value: 50, color: 'secondary' },
    { label: '🥗 Salads', value: 20, color: 'success' },
    { label: '🍰 Desserts', value: 60, color: 'error' },
    { label: '🥤 Drinks', value: 40, color: 'warning' }
  ];

  return (
    <Box onClick={() => navigate('/Dishes')} sx={{ cursor: 'pointer' }}>
      <Card>
        <CardHeader
          title={
            <Typography component="div" className="card-header">
              Traffic Dishes
            </Typography>
          }
        />
        <Divider />
        <CardContent>
          <Grid container spacing={gridSpacing}>
            {data.map((item, index) => (
              <Grid item xs={12} key={index}>
                <Grid container alignItems="center" spacing={1}>
                  <Grid item sm zeroMinWidth>
                    <Typography variant="body2">{item.label}</Typography>
                  </Grid>
                  <Grid item>
                    <Typography variant="body2" align="right">
                      {item.value}%
                    </Typography>
                  </Grid>
                  <Grid item xs={12}>
                    <LinearProgress
                      variant="determinate"
                      value={item.value}
                      color={item.color}
                      aria-label={item.label}
                    />
                  </Grid>
                </Grid>
              </Grid>
            ))}
          </Grid>
        </CardContent>
      </Card>
    </Box>
  );
};

export default TrafficSourcesCard;
