namespace CrossPostingSystem.Models
{
    public class PublicationResult
    {
        public string? NetworkName { get; set; }
        public bool Success { get; set; }
        public string? ExternalId { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime PublishedAt { get; set; }

        public override string ToString()
        {
            if (Success)
                return $"{NetworkName}: опубликовано (ID: {ExternalId})";
            else
                return $"{NetworkName}: ошибка - {ErrorMessage}";
        }
    }
}