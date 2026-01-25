import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  shape: {
    borderRadius: 15,
  },
  palette: {
    primary: {
      main: "#fdc5ff",
      light: "#ffb8f9",
      dark: "rgb(255, 135, 255)",
      contrastText: "#242105",
    },
  },
});

export default theme;
