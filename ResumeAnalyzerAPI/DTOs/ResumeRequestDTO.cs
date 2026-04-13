using Microsoft.AspNetCore.Http;

namespace ResumeAnalyzerAPI.DTOs
{
    public class ResumeRequestDTO
    {
        public IFormFile? File { get; set; }
        public string? JobRole { get; set; }
    }
}