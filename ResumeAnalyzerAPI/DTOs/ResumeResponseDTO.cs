namespace ResumeAnalyzerAPI.DTOs
{
    public class ResumeResponseDTO
    {
        public int Score { get; set; }
        public List<string> MatchedSkills { get; set; }
        public List<string> MissingSkills { get; set; }
        public List<string> Suggestions { get; set; }
        public string SummaryFeedback { get; set; }
    }
}