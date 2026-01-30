import { useState } from "react";
import "./navigationbar.css";
import { Button, IconButton } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import MenuIcon from "@mui/icons-material/Menu";
import { Link as RouterLink, useNavigate } from "react-router-dom";

function NavigationBar() {

  const navigate = useNavigate();
  

  const [isBurgerToggled, setIsBurgerToggled] = useState(false);

  const links = [
    { label: "About Us", to: "/" },
    { label: "Newsletter", to: "/newsletter" },
    { label: "Contact Us", to: "/contact" },
  ];

  function toggleBurger() {
    setIsBurgerToggled(!isBurgerToggled);
  }

  return (
    <div className="nav-bar-container container">
      <div className="nav-bar-links">
        <p  onClick={() => navigate("/")} id="nav-title">SHS Women's Health Club</p>
        <nav id="hamburger-nav">
          <IconButton onClick={toggleBurger}>
            {isBurgerToggled ? (
              <CloseIcon sx={{ fontSize: 64 }} color="secondary" />
            ) : (
              <MenuIcon sx={{ fontSize: 64 }} color="secondary" />
            )}
          </IconButton>
          <div
            className={
              isBurgerToggled ? "hamburger-links open" : "hamburger-links"
            }
          >
            <ul className="hamburger-links-list">
              {links.map((l) => (
                <li key={l.to}>
                  <Button
                    component={RouterLink}
                    to={l.to}
                    variant="contained"
                    fullWidth
                    onClick={toggleBurger}
                  >
                    {l.label}
                  </Button>
                </li>
              ))}
            </ul>
          </div>
        </nav>
        <nav id="menu-nav">
          <ul className="menu-links-list">
            {links.map((l) => (
              <li key={l.to}>
                <Button
                  component={RouterLink}
                  to={l.to}
                  variant="contained"
                >
                  {l.label}
                </Button>
              </li>
            ))}
          </ul>
        </nav>
      </div>
    </div>
  );
}

export default NavigationBar;
