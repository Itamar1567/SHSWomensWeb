import "./newsletterPage.css";
import { useParams } from "react-router-dom";
import type { GetNewsletterDTO } from "../types/GetNewsletterDTO";
import { useState, useEffect } from "react";

function NewsletterPage() {
  const param = useParams<{ slug: string }>();
  const [newsletter, setNewsletter] = useState<GetNewsletterDTO | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const GetNewsletter = async () => {
      try {
        if (!param.slug) {
          throw new Error("Newsletter slug not found in URL");
        }

        const res = await fetch("/newsletters.json");
        if (!res.ok) {
          throw new Error(`HTTP error! status: ${res.status}`);
        }
        const data = await res.json();
        const tempNewsletter = data.newsletters.find(
          (n: GetNewsletterDTO) => n.slug === param.slug,
        );

        if (!tempNewsletter) {
          setError("Newsletter not found");
          console.warn(`Newsletter with slug "${param.slug}" not found`);
        } else {
          setNewsletter(tempNewsletter);
          setError(null);
          console.log(`Newsletter loaded: ${param.slug}`);
        }
      } catch (err) {
        console.error("Failed to fetch newsletter:", err);
        setError("Failed to load newsletter. Please try refreshing the page.");
      } finally {
        setLoading(false);
      }
    };
    GetNewsletter();
  }, [param.slug]);

  if (loading) {
    return (
      <div className="container">
        <p>Loading...</p>
      </div>
    );
  }

  if (error || !newsletter) {
    return (
      <div className="newsletter-container container">
        <p style={{ color: "#d32f2f", marginTop: "1rem" }}>
          {error || "Newsletter not found"}
        </p>
      </div>
    );
  }

  return (
    <div className="newsletter-container container">
      <p className="newsletter-title">{newsletter.title}</p>

      <section className="newsletter-content-section">
        <p>{newsletter.short_description}</p>
        {newsletter.image_path != null && (
          <img id="newsletter-image" src={newsletter.image_path} alt={newsletter.title} />
        )}
        {newsletter.story_text != null ? (
          <p className="newsletter-story-text">{newsletter.story_text}</p>
        ) : (
          <p>Nothing to see here</p>
        )}
      </section>
    </div>
  );
}


export default NewsletterPage;
