using EnglishVoNo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnglishVoNo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VocabularyController : ControllerBase
    {
        private readonly IVocabularyService _vocabularyService;
        public VocabularyController(IVocabularyService vocabularyService) 
        {
            _vocabularyService= vocabularyService;
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _vocabularyService.GetAllAsync();
            return Ok(result);
        }
    }
}
