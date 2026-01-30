import "./blogSummary.css";
import type { GetBlogResponse } from "../types/blog";
import { useNavigate } from "react-router-dom";

interface props {
  summaryInfo: GetBlogResponse;
}
function BlogSummary({ summaryInfo }: props) {
  const navigate = useNavigate();

  function handleClick() {
    navigate(`/blog/${summaryInfo.slug}`);
  }
  const formatDate = (date: string) => new Date(date).toLocaleDateString();

  return (
    <div className="summary-container container">
      <p onClick={handleClick} className="article-title text-animation">{summaryInfo.title}</p>

      <img
        onClick={handleClick}
        id="summary-image"
        src={summaryInfo.coverImageUrl}
        alt={summaryInfo.title}
      />
      <p className="text-animation" onClick={handleClick}>{summaryInfo.summary}</p>
      <section className="dates-section">
        <p>Created On: {formatDate(summaryInfo.createdAt)}</p>
      </section>
    </div>
  );
}

export default BlogSummary;
