namespace CodePulse.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        //many to many relation 1 category can have multiple blog post
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
