export default {
  height: 228,
  type: 'donut',
  options: {
    dataLabels: {
      enabled: false
    },
    yaxis: {
      min: 0,
      max: 100
    },
    labels: ['Pending', 'In Progress', 'Completed'],
    legend: {
      show: true,
      position: 'bottom',
      fontFamily: 'inherit',
      labels: {
        colors: 'inherit'
      }
    },
    itemMargin: {
      horizontal: 10,
      vertical: 10
    },
    colors: ['#ff9800', '#2196f3', '#4caf50'] // amarillo, azul, verde
  },
  series: [20, 12, 48] // cantidad de tareas
};
