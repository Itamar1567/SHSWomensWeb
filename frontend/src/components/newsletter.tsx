import "./newsletter.css"
import { useState } from "react"
import { BLOGS, type GetBlogResponse } from "../types/blog"
import BlogSummary from "./blogSummary";

function Newsletter(){

    const [blogs, setBlogs] = useState<GetBlogResponse[]>(BLOGS);

    

    return (<div className="newsletter-container container">
    <section className="newsletter-section">
        <p className="section-title">Newsletter</p>
        {blogs.map((b) => (<BlogSummary key={b.id} summaryInfo={b} />))}
    </section>
   
    </div>)
}

export default Newsletter;