import "./blogPage.css";
import { useParams } from "react-router-dom";
import { BLOGS } from "../types/blog";

function BlogPage() {
  const param = useParams<{ slug: string }>();

  const blog = BLOGS.find((b) => b.slug === param.slug);

  if (!blog) {
    <div className="container">
      <p>Ran into an issue retrieving the blog</p>
    </div>;
  }

  const formatDate = (date: string) => new Date(date).toLocaleDateString();

  return (
    <div className="blog-container container">
      <p className="blog-title">{blog?.title}</p>

      <section className="blog-content-section">
        <p>{blog?.summary}</p>
        <img id="blog-image" src={blog?.coverImageUrl} />
        {blog?.content.map((p, i) => (<p key={i}>{p}</p>))}
      </section>
    </div>
  );
}

export default BlogPage;
