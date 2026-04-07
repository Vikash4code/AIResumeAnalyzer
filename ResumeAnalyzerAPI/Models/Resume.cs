namespace ResumeAnalyzerAPI.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public string JobRole { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}