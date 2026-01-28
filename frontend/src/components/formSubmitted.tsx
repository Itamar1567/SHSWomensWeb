import { Button } from "@mui/material";
import { Link as RouterLink } from "react-router-dom";
import "./formSubmitted.css"
function FormSubmitted() {
  return (
    <div className="container submitted-form-container">
      <div className="submission-message-content">
        <h3>Message Revieved!</h3>
        <p>We will try to respond as quickly as possible</p>
        <Button
          component={RouterLink}
          to={"/contact"}
          variant="contained"
          fullWidth
        >
          Return to the previous page
        </Button>
      </div>
    </div>
  );
}

export default FormSubmitted;
