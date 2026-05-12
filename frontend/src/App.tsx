import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import NavigationBar from "./components/navigationbar";
import "./App.css";
import About from "./components/about";
import Newsletter from "./components/newsletters";
import Contact from "./components/contact";
import Footer from "./components/footer";
import ScrollToTop from "./helpers/scrollToTop";
import FormSubmitted from "./components/formSubmitted";
import NewsletterPage from "./components/newsletterPage";

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
        <Route path="/newsletter/:slug" element={<NewsletterPage></NewsletterPage>}></Route>
      </Routes>
      </main>  
      <Footer></Footer>
      </div>
    </Router>
  );
}

export default App;
