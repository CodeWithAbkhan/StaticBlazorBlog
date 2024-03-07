namespace StaticBlazorBlog;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } =string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Content { get; set; } =string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string FeaturedImage { get; set; } =string.Empty;
    public string Thumbnail { get; set; } =string.Empty;
}
