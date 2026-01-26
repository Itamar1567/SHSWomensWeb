import { useParams } from "react-router-dom";
import { BLOGS } from "../types/blog";

function BlogPage(){

    const slug = useParams<{slug: string}>();

    const blog = BLOGS.find((b) => b.slug === slug);

    if(!blog) {<div className="container">Ran into an issue retrieving the blog</div>}

    const formatDate = (date: string) => new Date(date).toLocaleDateString();


    return (<div className="container">
        This is a blog
    </div>)
}

export default BlogPage;