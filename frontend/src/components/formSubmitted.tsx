import { Button } from "@mui/material";
import { Link as RouterLink } from "react-router-dom";

function FormSubmitted() {
  return (
    <div className="container">
      <div>
        <h3>Message Revieved!</h3>
        <p>We will try to respond as quickly as possible</p>
        <Button
          component={RouterLink}
          to={"/contact"}
          variant="contained"
          fullWidth
        >
          Return to the Contact Us Page
        </Button>
      </div>
    </div>
  );
}

export default FormSubmitted;
