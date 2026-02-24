import blogsDb from '../assets/blogs.json'

export type GetBlogResponse = {
  id: number;
  slug: string; //Frontend url
  title: string;
  summary: string;
  content: string[];
  coverImageUrl: string;
  createdAt: string;
  updatedAt: string;
};

export const BLOGS: GetBlogResponse[] = blogsDb;

/*
 TO ADD A NEW BLOG

 How to create the blog:
 
 inside the above box parenthesis in the json file add this:

 {

  id: ;
  slug: ; 
  title: ;
  summary: ;
  content: ;
  coverImageUrl: ;
  createdAt: ;
  updatedAt: ;

 },
 
 Now fill each row with its corresponding data as shown below 

 1. Increment the previous id by 1 (using numbers)

 Anything Under here must be within qoutes (i.e a string)

 2. Add a unique slug, i.e use blog
 3. Add a title
 4. Add a summary
 5. Add a paragraph like so "I am a paragraph" and split paragraphs by using a comma (,) after the qoutes Ex: "Par1", "Par2"
 6. Add a cover image for the post by typing the location of the image and the name of the image Ex: /blog-covers/example.png
 7. Add a date, use this format: yyyy-mm-ddT00:00:00.000Z

 


*/
