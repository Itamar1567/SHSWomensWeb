using System.ComponentModel.DataAnnotations;
public class Newsletters
{
    [Key]
    public int id {get; set;}
    [Required]
    public required string title {get; set;}
    [Required]
    public required string slug {get; set;}
    [Required]
    public required string author {get; set;}
    public  string? image_path {get; set;}
    public string? short_description {get; set;}
    public required string story_text {get; set;}
    public DateTime created_at {get; set;} = DateTime.Now;
    public DateTime updated_at {get; set;} = DateTime.Now;

}

public class StoredImages
{
    [Key]
    public int id {get; set;}
    [Required]
    public required string image_path {get; set;}
    public DateTime created_at {get; set;} = DateTime.Now;

}

