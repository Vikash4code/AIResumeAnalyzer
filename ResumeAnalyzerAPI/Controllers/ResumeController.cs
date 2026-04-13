using Microsoft.AspNetCore.Mvc;
using ResumeAnalyzerAPI.DTOs;
using ResumeAnalyzerAPI.Services;
using System.Threading.Tasks;

namespace ResumeAnalyzerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working");
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeResume([FromForm] ResumeRequestDTO request)
        {
            if (request.File == null || string.IsNullOrEmpty(request.JobRole))
            {
                return BadRequest("File and JobRole are required");
            }

            var result = await _resumeService.AnalyzeResumeAsync(request.File, request.JobRole);
            return Ok(result);
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetModels([FromServices] IGeminiService gemini)
        {
            var models = await gemini.ListModelsAsync();
            return Ok(models);
        }
    }
}