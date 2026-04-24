import "./newsletterSummary.css";
import { useNavigate } from "react-router-dom";
import type { GetNewsletterDTO } from "../types/GetNewsletterDTO";

interface props {
  newsletterSummary: GetNewsletterDTO;
}

function NewsletterSummary({ newsletterSummary }: props) {
  const navigate = useNavigate();

  function handleClick() {
    navigate(`/newsletter/${newsletterSummary.slug}`);
  }
  const formatDate = (date: string) => new Date(date).toLocaleDateString();

  return (
    <div>
      <div className="summary-container container">
        <p className="text-animation text-overflow-wraper" onClick={handleClick}>
          {newsletterSummary.title}
        </p>
        {newsletterSummary.image_path != null && (
          <img
            onClick={handleClick}
            id="summary-image"
            src={newsletterSummary.image_path}
            alt={newsletterSummary.title}
          />
        )}

        <p className="text-animation text-overflow-wraper" onClick={handleClick}>
          {newsletterSummary.short_description}
        </p>
        <section className="dates-section">
          <p>
            Created On: {formatDate(newsletterSummary.created_at.toString())}
          </p>
          <p>
            Updated On: {formatDate(newsletterSummary.updated_at.toString())}
          </p>
        </section>
      </div>
      <hr style={{"height": "5px"}}></hr>
    </div>
  );
}

export default NewsletterSummary;
