namespace CrossPostingSystem.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }

        public Post(string content, string imageUrl = null)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.Now;
            Tags = new List<string>();
        }

        public void AddTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag) && !Tags.Contains(tag))
                Tags.Add(tag);
        }

        public override string ToString()
        {
            return $"[{Id}] {Content} {(Tags.Count > 0 ? $"#{string.Join(" #", Tags)}" : "")}";
        }
    }
}