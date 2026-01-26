import "./newsletter.css";
import { useState } from "react";
import { BLOGS, type GetBlogResponse } from "../types/blog";
import BlogSummary from "./blogSummary";

function Newsletter() {
  const blogs: GetBlogResponse[] = BLOGS;

  const [filteredBlogs, setFilteredBlog] = useState<GetBlogResponse[]>(BLOGS);

  const [searchItem, setSearchItem] = useState("");

  function filterBlogs(e: React.ChangeEvent<HTMLInputElement>) {
    let searchTerm: string = e.currentTarget.value;
    console.log("Entered")
    if (!searchTerm.trim()) {
      setSearchItem(searchTerm);
      setFilteredBlog(blogs);
      return;
    }

    setSearchItem(searchTerm);

    const filteredItems = blogs.filter((b) =>
      b.title.toLowerCase().trim().includes(searchTerm.toLowerCase().trim()),
    );

    setFilteredBlog(filteredItems);

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
            placeholder="Search for a blog by title"
            onChange={filterBlogs}
          />
        </div>
        {filteredBlogs.length > 0 ? filteredBlogs.map((b) => (
          <BlogSummary key={b.id} summaryInfo={b} />
        )) : <p>No blogs found</p>}
      </section>
    </div>
  );
}

export default Newsletter;
