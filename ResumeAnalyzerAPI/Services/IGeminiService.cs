using System.Threading.Tasks;

namespace ResumeAnalyzerAPI.Services
{
    public interface IGeminiService
    {
        Task<string> AnalyzeResumeWithAI(string resumeText, string jobRole);
        Task<string> ListModelsAsync();
    }
}