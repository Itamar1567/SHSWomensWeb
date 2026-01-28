import PhoneIcon from "@mui/icons-material/Phone";
import EmailIcon from "@mui/icons-material/Email";
import "./contact.css";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

function Contact() {

  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);

    // Send POST manually to Netlify
    await fetch("/", {
      method: "POST",
      body: formData,
    });

    // Navigate to success page (client-side)
    navigate("/success");
  };

  return (
    <div className="contact-container container">
      <p className="section-title">Contact Us</p>
      <section className="contact-section">
        <div className="contact-form-container">
          <h1>Send Us a Message</h1>
          <p>Please fill out the form below to get in touch with us</p>
          <form
            className="contact-form"
            name="contact-form"
            data-netlify="true"
            method="POST"
            data-netlify-honeypot="bot-field"
            onSubmit={handleSubmit}
          >
            {/* Required hidden input */}
            <input type="hidden" name="form-name" value="contact-form" />

            {/* Honeypot field */}
            <input type="hidden" name="bot-field" />

            <div className="input-field-pair">
              <input
                name="firstname"
                placeholder="First name"
                type="text"
                required
                className="contact-form-input"
              />
              <input
                name="lastname"
                placeholder="Last name"
                type="text"
                required
                className="contact-form-input"
              />
            </div>
            <div className="input-field-pair">
              <input
                name="email"
                placeholder="Email"
                type="email"
                required
                className="contact-form-input"
              />
              <input
                name="phone"
                placeholder="Phone number"
                type="tel"
                required
                className="contact-form-input"
              />
            </div>
            <textarea
              name="message"
              id="message-input"
              placeholder="Message"
              required
            />
            <Button type="submit" variant="contained">
              Send
            </Button>
          </form>
        </div>
        <div className="micro-contact-info-container">
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
