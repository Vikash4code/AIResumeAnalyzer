using System.Collections.Generic;

namespace ResumeAnalyzerAPI.DTOs
{
    public class ResumeResponseDTO
    {
        public int Score { get; set; }

        public List<string> MatchedSkills { get; set; } = new();
        public List<string> MissingSkills { get; set; } = new();
        public List<string> Suggestions { get; set; } = new();

        public string SummaryFeedback { get; set; } = string.Empty;
    }
}