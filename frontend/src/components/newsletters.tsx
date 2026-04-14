import "./newsletters.css";
import { useEffect, useState } from "react";
import NewsletterSummary from "./newsletterSummary";
import type { GetNewsletterDTO } from "../types/GetNewsletterDTO";

function Newsletter() {
  const [loading, setLoading] = useState(true);

  const [newsletters, setNewsletters] = useState<GetNewsletterDTO[]>([]);
  const [filteredNewsletters, setFilteredNewsletters] = useState<GetNewsletterDTO[]>([]);
  const [searchItem, setSearchItem] = useState("");

  useEffect(() => {
    const GetNewsletters = async () => {
      try {
        const res = await fetch("/newsletters.json");
        const data = await res.json();
        console.log("Fetched newsletters:", data.newsletters);
        setNewsletters(data.newsletters);
        setFilteredNewsletters(data.newsletters);

      } catch (error) {
        console.log("Failed to fetch newsletters:", error);
        setNewsletters([]);
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
        <p className="section-title">Newsletter</p>
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
