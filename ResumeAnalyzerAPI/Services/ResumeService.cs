using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using ResumeAnalyzerAPI.DTOs;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;

namespace ResumeAnalyzerAPI.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IGeminiService _geminiService;

        public ResumeService(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        public async Task<ResumeResponseDTO> AnalyzeResumeAsync(IFormFile file, string jobRole)
        {
            string extractedText = await ExtractTextAsync(file);

            var aiRawResponse = await _geminiService.AnalyzeResumeWithAI(extractedText, jobRole);

            Console.WriteLine("GEMINI RAW RESPONSE:");
            Console.WriteLine(aiRawResponse);

            try
            {
                string cleanJson = null;

                // Try to parse common Gemini response shapes first
                try
                {
                    var j = JsonConvert.DeserializeObject<JObject>(aiRawResponse);

                    if (j != null)
                    {
                        JToken? token = null;

                        // candidates -> content -> parts[0] -> text
                        token = j["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]
                                ?? j["candidates"]?[0]?["content"]?["text"]
                                ?? j["candidates"]?[0]?["content"]?[0]?["text"];

                        // outputs -> content -> text
                        token ??= j["outputs"]?[0]?["content"]?[0]?["text"]
                                 ?? j["outputs"]?[0]?["content"]?["text"];

                        if (token != null && token.Type == JTokenType.String)
                        {
                            var aiText = token.Value<string>();
                            int start = aiText.IndexOf('{');
                            int end = aiText.LastIndexOf('}');
                            if (start != -1 && end != -1 && end > start)
                            {
                                cleanJson = aiText.Substring(start, end - start + 1);
                            }
                        }
                    }
                }
                catch
                {
                    // ignore parse errors here and try fallback extraction below
                }

                // If we couldn't extract clean JSON from structured tokens, search the raw response
                if (string.IsNullOrEmpty(cleanJson))
                {
                    int start = aiRawResponse.IndexOf('{');
                    int end = aiRawResponse.LastIndexOf('}');
                    if (start != -1 && end != -1 && end > start)
                    {
                        cleanJson = aiRawResponse.Substring(start, end - start + 1);
                    }
                }

                if (string.IsNullOrEmpty(cleanJson))
                {
                    throw new Exception("No valid JSON found in Gemini response");
                }

                var result = JsonConvert.DeserializeObject<ResumeResponseDTO>(cleanJson);

                if (result == null)
                    throw new Exception("Failed to deserialize AI JSON to DTO");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GEMINI FAILED, USING FALLBACK");
                Console.WriteLine(ex.Message);

                // 🔥 FALLBACK (NO CRASH)
                return new ResumeResponseDTO
                {
                    Score = 75,
                    MatchedSkills = new List<string> { "C#", "SQL" },
                    MissingSkills = new List<string> { "React", "Azure" },
                    Suggestions = new List<string> { "Add frontend skills", "Improve projects" },
                    SummaryFeedback = "Fallback response used due to AI error"
                };
            }
        }

        private async Task<string> ExtractTextAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            using (var stream = file.OpenReadStream())
            {
                if (extension == ".pdf")
                    return await ExtractFromPdf(stream);

                if (extension == ".docx")
                    return await ExtractFromDocx(stream);

                throw new Exception("Unsupported file format");
            }
        }

        private Task<string> ExtractFromPdf(Stream stream)
        {
            var text = new StringBuilder();

            using (var pdf = PdfDocument.Open(stream))
            {
                foreach (Page page in pdf.GetPages())
                {
                    text.AppendLine(page.Text);
                }
            }

            return Task.FromResult(text.ToString());
        }

        private Task<string> ExtractFromDocx(Stream stream)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, false))
            {
                var body = doc.MainDocumentPart?.Document?.Body;
                return Task.FromResult(body?.InnerText ?? string.Empty);
            }
        }
    }
}