import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  shape: {
    borderRadius: 3,
  },
  palette: {
    primary: {
      main: "#fdb5ff",
      light: "#ffb8f9",
      dark: "rgb(255, 135, 255)",
      contrastText: "#242105",
    },
  },
});

export default theme;
