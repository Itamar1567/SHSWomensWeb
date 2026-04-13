import "./newsletterPage.css";
import { useParams } from "react-router-dom";
import type { GetNewsletterDTO } from "../types/GetNewsletterDTO";
import { useState, useEffect } from "react";

function NewsletterPage() {
  const param = useParams<{ slug: string }>();
  const [newsletter, setNewsletter] = useState<GetNewsletterDTO>(
    {} as GetNewsletterDTO,
  );

  useEffect(() => {
    const GetNewsletter = async () => {
      try {
        const res = await fetch("/newsletters.json");
        const data = await res.json();
        const tempNewsletter = data.newsletters.find(
          (n: GetNewsletterDTO) => n.slug === param.slug,
        );
        setNewsletter(tempNewsletter);
      } catch (error) {
        console.log("Failed to fetch newsletters:", error);
      }
    };
    GetNewsletter();
  }, []);

  if (!newsletter) {
    <div className="container">
      <p>Ran into an issue retrieving the newsletter</p>
    </div>;
  }

  return (
    <div className="newsletter-container container">
      <p className="newsletter-title">{newsletter.title}</p>

      <section className="newsletter-content-section">
        <p>{newsletter.short_description}</p>
        {newsletter.image_path != null && (
          <img id="newsletter-image" src={newsletter.image_path} />
        )}
        {newsletter.story_text != null ? (
          <p>{newsletter.story_text}</p>
        ) : (
          <p>Nothing to see here</p>
        )}
      </section>
    </div>
  );
}

export default NewsletterPage;
