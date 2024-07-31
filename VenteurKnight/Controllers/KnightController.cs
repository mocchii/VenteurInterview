using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VenteurKnight.Interfaces;

namespace VenteurKnight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnightController : ControllerBase
    {
        private readonly IKnightService knightService;
        public KnightController(IKnightService _knightService)
        {
            knightService = _knightService;
        }
        [HttpPost]
        [Route("/KnightPath")]
        public async Task<IActionResult> CreateKnight(string source, string target)
        {
            var result = await knightService.CreateKnight(source, target);
            if (result.Success) {
                return Ok(result);
            }
            else {
                return StatusCode(500, result);
            }
            
        }

        [HttpGet]
        [Route("/KnightPath")]
        public async Task<IActionResult> GetKnightPath(string operationId)
        {
            var result = await knightService.GetKnightPath(operationId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
