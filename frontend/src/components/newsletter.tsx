import "./newsletter.css"
import { useState } from "react"
import type { GetBlogResponse } from "../types/blog"
import BlogSummary from "./blogSummary";

function Newsletter(){

    const [blogs, setBlogs] = useState<GetBlogResponse[]>([

        {id: 1, title: "Hello world but its a title", summary: "Hello me", content: "I dont know any more", coverImageUrl: "/blog-covers/example2.jpg", updatedAt: "11/11/1111", createdAt: "11/11/1111", slug: "newDrug"},
        {id: 1, title: "Hello world but its a title", summary: "Hello me", content: "I dont know any more", coverImageUrl: "/blog-covers/example2.jpg", updatedAt: "11/11/1111", createdAt: "11/11/1111", slug: "newA"}
    ]);

    

    return (<div className="newsletter-container container">
    <section className="newsletter-section">
        <p className="section-title">Newsletter</p>
        {blogs.map((b) => (<BlogSummary summaryInfo={b}></BlogSummary>))}
    </section>
   
    </div>)
}

export default Newsletter;