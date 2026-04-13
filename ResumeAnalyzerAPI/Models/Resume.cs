namespace ResumeAnalyzerAPI.Models
{
    public class Resume
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string JobRole { get; set; } = string.Empty;
    }
}