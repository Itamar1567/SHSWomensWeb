import { useState } from "react";
import Home from "./components/home";
import { HashRouter as Router, Route, Routes } from "react-router-dom";
import NavigationBar from "./components/navigationbar";
import "./App.css";

function App() {
  return (
    <Router>
      <NavigationBar></NavigationBar>
      <Routes>
        <Route path="/" element={<Home></Home>}></Route>
      </Routes>
    </Router>
  );
}

export default App;
