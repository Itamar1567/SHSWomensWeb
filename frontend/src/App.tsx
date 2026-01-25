import { useState } from "react";
import { HashRouter as Router, Route, Routes } from "react-router-dom";
import NavigationBar from "./components/navigationbar";
import "./App.css";
import About from "./components/about";
import Newsletter from "./components/newsletter"
import Contact from "./components/contact";

function App() {
  return (
    <Router>
      <NavigationBar></NavigationBar>
      <Routes>
        <Route path="/" element={<About></About>}></Route>
        <Route path="/newsletter" element={<Newsletter></Newsletter>}></Route>
        <Route path="/contact" element={<Contact></Contact>}></Route>
      </Routes>
    </Router>
  );
}

export default App;
