import React, { useState } from 'react';
import {
  Card,
  CardActionArea,
  CardContent,
  Grid,
  Typography,
  Box,
  Divider
} from '@mui/material';

// Puedes reemplazar esta lista con datos reales o dinámicos
const dishes = [
  { name: 'Pizza', description: 'Cheesy and delicious', ingredients: ['Cheese', 'Tomato', 'Dough'] },
  { name: 'Burger', description: 'Juicy with crispy fries', ingredients: ['Beef', 'Lettuce', 'Bun'] },
  { name: 'Sushi', description: 'Fresh and authentic', ingredients: ['Rice', 'Seaweed', 'Salmon'] }
];

const Dishes = () => {
  const [selectedDish, setSelectedDish] = useState(null);

  return (
    <Grid container spacing={2}>
      <Grid item xs={12} md={4}>
        {dishes.map((dish) => (
          <Card key={dish.name} sx={{ mb: 2 }}>
            <CardActionArea onClick={() => setSelectedDish(dish)}>
              <CardContent>
                <Typography variant="h6">{dish.name}</Typography>
                <Typography variant="body2" color="textSecondary">
                  {dish.description}
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        ))}
      </Grid>

      <Grid item xs={12} md={8}>
        {selectedDish ? (
          <Card>
            <CardContent>
              <Typography variant="h5">{selectedDish.name}</Typography>
              <Typography variant="body1" sx={{ mb: 1 }}>{selectedDish.description}</Typography>
              <Divider sx={{ my: 1 }} />
              <Typography variant="subtitle1">Ingredients:</Typography>
              <ul>
                {selectedDish.ingredients.map((ing, idx) => (
                  <li key={idx}>
                    <Typography variant="body2">{ing}</Typography>
                  </li>
                ))}
              </ul>
            </CardContent>
          </Card>
        ) : (
          <Box sx={{ p: 2 }}>
            <Typography variant="h6" color="textSecondary">
              Select a dish to see its details.
            </Typography>
          </Box>
        )}
      </Grid>
    </Grid>
  );
};

export default Dishes;
