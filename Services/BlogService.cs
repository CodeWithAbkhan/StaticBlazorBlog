using System.Text.Json;
using Markdig;
using Microsoft.VisualBasic;

namespace StaticBlazorBlog;

public class BlogService
{
    private readonly string _blogDirectory = "wwwroot/blogs";

    public IEnumerable<BlogPost> GetBlogPosts(){
        var blogPosts = new List<BlogPost>();

        foreach(var file in Directory.GetFiles(_blogDirectory, "*.md"))
        {
            var slug = Path.GetFileNameWithoutExtension(file);

            var markdown = File.ReadAllText(file);

            var jsonSeparator = markdown.IndexOf("---",3);

            if(jsonSeparator >= 0){
                var jsonMetadata = markdown.Substring(3, jsonSeparator -3);

                var blogPost = JsonSerializer.Deserialize<BlogPost>(jsonMetadata, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive= true
                });
                blogPost!.Slug = slug;

                blogPosts.Add(blogPost);
            }


        }
        return blogPosts.OrderByDescending(post=>post.Date);           

    }
    public BlogPost GetBlogBySlug(string slug){
        string blogFilePath  = Path.Combine(_blogDirectory, slug+".md");
        if(File.Exists(blogFilePath))
        {
            var markdown = File.ReadAllText(blogFilePath);
            var jsonSeparator = markdown.IndexOf("---",3);
            if(jsonSeparator>=0)
            {
                var jsonMeta  = markdown.Substring(3, jsonSeparator-3);
                var content = markdown.Substring(jsonSeparator);
                var blogPost = JsonSerializer.Deserialize<BlogPost>(jsonMeta, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                blogPost.Content = Markdown.ToHtml(content);

               return blogPost;
            }
        }
        return null;
    }
}
