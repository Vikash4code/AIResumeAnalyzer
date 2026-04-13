using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ResumeAnalyzerAPI.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public GeminiService(IConfiguration config)
        {
            _httpClient = new HttpClient();

            _apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("Gemini API Key not set in environment variables");
            }

            _model = config["Gemini:Model"]
                ?? "models/gemini-1.5-flash";
        }

        public async Task<string> AnalyzeResumeWithAI(string resumeText, string jobRole)
        {
            var prompt = $@"
You are an ATS (Applicant Tracking System).

Analyze the following resume for the role: {jobRole}

Resume:
{resumeText}

IMPORTANT:
- Return ONLY valid JSON
- No explanation
- No extra text

FORMAT:
{{
  ""score"": number,
  ""matchedSkills"": [string],
  ""missingSkills"": [string],
  ""suggestions"": [string],
  ""summaryFeedback"": string
}}
";

            // ✅ FIXED: Use v1beta instead of v1
            var url = $"https://generativelanguage.googleapis.com/v1beta/{_model}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var result = await response.Content.ReadAsStringAsync();

            // 🔥 DEBUG LOGS (keep these for now)
            System.Console.WriteLine("Gemini URL: " + url);
            System.Console.WriteLine("Status: " + response.StatusCode);
            System.Console.WriteLine("Response: " + result);

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception($"Gemini API failed: {result}");
            }

            return result;
        }

        public async Task<string> ListModelsAsync()
        {
            // Also update this to v1beta for consistency
            var url = $"https://generativelanguage.googleapis.com/v1beta/models?key={_apiKey}";
            return await _httpClient.GetStringAsync(url);
        }
    }
}