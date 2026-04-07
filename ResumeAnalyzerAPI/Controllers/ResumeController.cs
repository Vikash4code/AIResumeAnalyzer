using Microsoft.AspNetCore.Mvc;
using ResumeAnalyzerAPI.DTOs;

namespace ResumeAnalyzerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working");
        }

        [HttpPost("analyze")]
        public IActionResult Analyze([FromForm] ResumeRequestDTO request)
        {
            // Dummy response (real logic later)
            var response = new ResumeResponseDTO
            {
                Score = 85,
                MatchedSkills = new List<string> { "Java", "SQL" },
                MissingSkills = new List<string> { "Docker", "AWS" },
                Suggestions = new List<string>
                {
                    "Add cloud projects",
                    "Improve project descriptions"
                },
                SummaryFeedback = "Good resume but needs improvement"
            };

            return Ok(response);
        }
    }
}