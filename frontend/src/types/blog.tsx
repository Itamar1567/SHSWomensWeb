

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

export const BLOGS: GetBlogResponse[] = [
  {
    id: 1,
    slug: "welcome",
    title: "Welcome",
    summary: "A quick intro to what we do.",
    content: ["Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan.", "Full blog content goes here...", "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan."],
    coverImageUrl: "/blog-covers/example2.jpg",
    createdAt: "2026-01-10T00:00:00.000Z",
    updatedAt: "2026-01-20T00:00:00.000Z",
  },

  {
    id: 2,
    slug: "helloworld",
    title: "Hello World",
    summary: "A quick intro to what we do.",
    content: ["Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan.", "Full blog content goes here...", "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan."],
    coverImageUrl: "/blog-covers/example2.jpg",
    createdAt: "2026-01-10T00:00:00.000Z",
    updatedAt: "2026-01-20T00:00:00.000Z",
  },

];