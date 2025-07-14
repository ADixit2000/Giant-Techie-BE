using Giant_Techie_BE.DTOs;
using Giant_Techie_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Giant_Techie_BE.Controller
{
    [Route("Competition")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly ICompetitionService _competitionService;
        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }
        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateCompetition([FromBody] AddOrUpdateCompetition command)
        {
            var competition = await _competitionService.CreateCompetition(command);
            return CreatedAtAction(nameof(GetCompetitionById), new { id = competition.Id }, competition);
        }
        [HttpGet]
        [Route("GetById/{id}")]

        public async Task<IActionResult> GetCompetitionById(Guid id)
        {
            var competition = await _competitionService.GetCompetitionById(id);
            if (competition == null)
                return NotFound();
            return Ok(competition);
        }
        [HttpGet]
        [Route("GetAll")]

        public async Task<IActionResult> GetAllCompetitions()
        {
            var competitions = await _competitionService.GetAllCopetitions();
            return Ok(competitions);
        }
        [Route("Update/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCompetition(Guid id, [FromBody] AddOrUpdateCompetition command)
        {
            await _competitionService.UpdateCompetition(id, command);
            return NoContent();
        }
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCompetition(Guid id)
        {
            await _competitionService.DeleteCompetition(id);
            return NoContent();
        }

    }
}
