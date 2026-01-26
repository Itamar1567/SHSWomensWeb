import "./blogSummary.css";
import type { GetBlogResponse } from "../types/blog";
import { useNavigate } from "react-router-dom";

interface props {
  summaryInfo: GetBlogResponse;
}
function BlogSummary({ summaryInfo }: props) {
    
  const navigate = useNavigate();

  const formatDate = (date: string) => new Date(date).toLocaleDateString();

  return (
    <div
      onClick={() => navigate(`/blog/${summaryInfo.slug}`)}
      className="summary-container container"
    >
      <p className="article-title">{summaryInfo.title}</p>
      <p>{summaryInfo.summary}</p>
      <img id="summary-image" src={summaryInfo.coverImageUrl} alt={summaryInfo.title} />

      <section className="dates-section">
        <p>Created On: {formatDate(summaryInfo.createdAt)}</p>
        <p>Edited On: {formatDate(summaryInfo.updatedAt)}</p>
      </section>
    </div>
  );
}

export default BlogSummary;
