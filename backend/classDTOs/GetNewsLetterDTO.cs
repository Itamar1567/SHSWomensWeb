public class GetNewsLetterDTO
{
    public required int id { get; set; }
    public required string title { get; set; }
    public required string slug { get; set; }
    public required string author { get; set; }

    public string? image_path { get; set; }
    public string? short_description { get; set; }
    public required string story_text { get; set; }
    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }
}