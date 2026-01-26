import { useEffect } from "react";
import { useLocation } from "react-router-dom";

function ScrollToTop() {
  const { pathname } = useLocation();

  useEffect(scrollUp, [pathname]);

  function scrollUp() {
    window.scrollTo({ top: 0, left: 0, behavior: "instant" });
  }

  return null;
}

export default ScrollToTop;
