import "./footer.css";
import InstagramIcon from "@mui/icons-material/Instagram";
import XIcon from "@mui/icons-material/X";
import { Link } from "react-router-dom";

function Footer() {
  return (
    <div className="footer-container container">
      <section className="extra-information-section">
          <div>
            <p className="footerText">Follow Us</p>
            <section className="icons-section">
              <a href="https://www.instagram.com/" target="_blank"><InstagramIcon className="follow-icon"></InstagramIcon></a>
              <a href="https://x.com/" target="_blank"><XIcon className="follow-icon"></XIcon></a>
            </section>
          </div>
          <div className="extra-nav">
            <Link to="/" className="basic-link">About Us</Link>
            <Link to="/newsletter" className="basic-link">Newsletter</Link>
            <Link to="/contact" className="basic-link">Contact</Link>
          </div>
          <div>
            <p className="footerText">SHS Women's Health Club</p>
          </div>
      </section>
    </div>
  );
}

export default Footer;
