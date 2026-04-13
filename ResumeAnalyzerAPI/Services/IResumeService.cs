using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResumeAnalyzerAPI.DTOs;

namespace ResumeAnalyzerAPI.Services
{
    public interface IResumeService
    {
        Task<ResumeResponseDTO> AnalyzeResumeAsync(IFormFile file, string jobRole);
    }
}