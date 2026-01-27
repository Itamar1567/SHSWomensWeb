import PhoneIcon from "@mui/icons-material/Phone";
import EmailIcon from "@mui/icons-material/Email";
import "./contact.css";
import { Button } from "@mui/material";

function Contact() {
  return (
    <div className="contact-container container">
      <p className="section-title">Contact Us</p>
      <section className="contact-section">
        <div className="contact-information">
          <div className="contact-form-container">
            <h1>Send Us a Message</h1>
            <p>Please fill out the form below to get in touch with us</p>
            <form className="contact-form">
              <div className="input-field-pair">
                <input
                  placeholder="First name"
                  type="text"
                  className="contact-form-input"
                />
                <input
                  placeholder="Last name"
                  type="text"
                  className="contact-form-input"
                />
              </div>
              <div className="input-field-pair">
                <input
                  placeholder="Email"
                  type="email"
                  className="contact-form-input"
                />
                <input
                  placeholder="Phone number"
                  type="tel"
                  className="contact-form-input"
                />
              </div>

              <textarea id="message-input" placeholder="Message" />
              <Button type="submit" variant="contained">Send</Button>
            </form>
          </div>
          <div className="micro-contact-info">
            <PhoneIcon></PhoneIcon>
            <p>973-1111-111</p>
          </div>
          <div className="micro-contact-info">
            <EmailIcon></EmailIcon>
            <p>Example@gmail.com</p>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Contact;
