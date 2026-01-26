

export type GetBlogResponse = {
  id: number;
  slug: string; //Frontend url
  title: string;
  summary: string;
  content: string;
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
    content: "Full blog content goes here...",
    coverImageUrl: "/blog-covers/example2.jpg",
    createdAt: "2026-01-10T00:00:00.000Z",
    updatedAt: "2026-01-20T00:00:00.000Z",
  },

  {
    id: 1,
    slug: "welcome",
    title: "Welcome",
    summary: "A quick intro to what we do.",
    content: "Full blog content goes here...",
    coverImageUrl: "/blog-covers/example2.jpg",
    createdAt: "2026-01-10T00:00:00.000Z",
    updatedAt: "2026-01-20T00:00:00.000Z",
  },
];