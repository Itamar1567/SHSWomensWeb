import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import NavigationBar from "./components/navigationbar";
import "./App.css";
import About from "./components/about";
import Newsletter from "./components/newsletter";
import Contact from "./components/contact";
import BlogPage from "./components/blogPage";
import Footer from "./components/footer";
import ScrollToTop from "./helpers/scrollToTop";
import FormSubmitted from "./components/formSubmitted";

function App() {
  return (
    <Router>
      <div className="page">
      <ScrollToTop />
      <NavigationBar></NavigationBar>
      <main className="content">      
      <Routes>
        <Route path="/" element={<About></About>}></Route>
        <Route path="/newsletter" element={<Newsletter></Newsletter>}></Route>
        <Route path="/contact" element={<Contact></Contact>}></Route>
        <Route path="/success" element={<FormSubmitted></FormSubmitted>}></Route>
        <Route path="/blog/:slug" element={<BlogPage></BlogPage>}></Route>
      </Routes>
      </main>
      
      <Footer></Footer>
      </div>
    </Router>
  );
}

export default App;
