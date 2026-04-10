public class CreateNewsLetterDTO
{
    public required string title {get; set;}
    public required string slug {get; set;}
    public required string  author {get; set;}
    public string? image_path {get; set;}
    public string? short_description {get; set;}
    public required string story_text {get; set;}
}