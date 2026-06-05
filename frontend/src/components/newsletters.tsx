import "./newsletters.css";
import { useEffect, useState } from "react";
import NewsletterSummary from "./newsletterSummary";
import type { GetNewsletterDTO } from "../types/GetNewsletterDTO";

function Newsletter() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [newsletters, setNewsletters] = useState<GetNewsletterDTO[]>([]);
  const [filteredNewsletters, setFilteredNewsletters] = useState<
    GetNewsletterDTO[]
  >([]);
  const [searchItem, setSearchItem] = useState("");

  useEffect(() => {
    const GetNewsletters = async () => {
      try {
        const res = await fetch("/newsletters.json");
        if (!res.ok) {
          throw new Error(`HTTP error! status: ${res.status}`);
        }
        const data = await res.json();
        console.log("Newsletters loaded successfully");
        setNewsletters(data.newsletters);
        setFilteredNewsletters(data.newsletters);
        setError(null);
      } catch (err) {
        console.error("Failed to fetch newsletters:", err);
        setNewsletters([]);
        setError("Failed to load newsletters. Please try refreshing the page.");
      } finally {
        setLoading(false);
      }
    };
    GetNewsletters();
  }, []);

  function filterNewsletters(e: React.ChangeEvent<HTMLInputElement>) {
    let searchTerm: string = e.currentTarget.value;
    if (!searchTerm.trim()) {
      setSearchItem(searchTerm);
      setFilteredNewsletters(newsletters);
      return;
    }

    setSearchItem(searchTerm);

    const filteredItems = newsletters.filter((b) =>
      b.title.toLowerCase().trim().includes(searchTerm.toLowerCase().trim()),
    );

    setFilteredNewsletters(filteredItems);
  }

  return (
    <div className="newsletter-container container">
      <section className="newsletter-section">
        <div className="section-title-container">
          <p className="section-title-text">Newsletters</p>
        </div>
        <div className="search-container">
          <p>Search:</p>
          <input
            id="search"
            type="text"
            value={searchItem}
            placeholder="Search for a newsletter by title"
            onChange={filterNewsletters}
          />
        </div>
        {loading ? (
          <p>Loading...</p>
        ) : error ? (
          <p style={{ color: "#d32f2f", marginTop: "1rem" }}>{error}</p>
        ) : filteredNewsletters.length > 0 ? (
          filteredNewsletters.map((b) => (
            <NewsletterSummary key={b.id} newsletterSummary={b} />
          ))
        ) : (
          <p>No newsletters found</p>
        )}
      </section>
    </div>
  );
}

export default Newsletter;
