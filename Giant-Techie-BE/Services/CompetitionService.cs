using Giant_Techie_BE.Database;
using Giant_Techie_BE.DTOs;
using Giant_Techie_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Giant_Techie_BE.Services
{
    public class CompetitionService : ICompetitionService
    {

        private readonly GiantTicheDbContext _dbContext;
        private readonly ILogger<CompetitionService> _logger;

        public CompetitionService(GiantTicheDbContext dbContext, ILogger<CompetitionService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CompetitionDto> CreateCompetition(AddOrUpdateCompetition command)
        {
            var competition = Competitions.Create(
                 command.Title,
                 command.Description,
                 command.StartDate,
                 command.EndDate,
                 command.Status
             );

            await _dbContext.Competitions.AddAsync(competition);
            await _dbContext.SaveChangesAsync();

            return new CompetitionDto(

                competition.Id,
                competition.Title,
                competition.Description,
                competition.StartDate,
                competition.EndDate,
                competition.Status
            );
        }

        public async Task DeleteCompetition(Guid id)
        {
            var competition = await _dbContext.Competitions
                .FindAsync(id);
            if (competition != null)
            {
                _dbContext.Competitions.Remove(competition);
                await _dbContext.SaveChangesAsync();
            }
        }

       
        public async Task<CompetitionDto?> GetCompetitionById(Guid id)
        {
           var competition = await _dbContext.Competitions
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (competition == null)
            {
                return await Task.FromResult<CompetitionDto?>(null);
            } 
            return new CompetitionDto(

               competition.Id,
               competition.Title,
               competition.Description,
               competition.StartDate,
               competition.EndDate,
               competition.Status
           );
        }

        public async Task UpdateCompetition(Guid id, AddOrUpdateCompetition command)
        {
            var competition = await _dbContext.Competitions
                .FindAsync(id);
            if (competition is null)
            {
                _logger.LogWarning("Competition with ID {Id} not found for update.", id);
                throw new KeyNotFoundException($"Competition with ID {id} not found.");
            }
            competition.Update(
                command.Title,
                command.Description,
                command.StartDate,
                command.EndDate,
                command.Status
            );
            await _dbContext.SaveChangesAsync();

        }
        public async Task<IEnumerable<CompetitionDto>> GetAllCopetitions()
        {
            return await _dbContext.Competitions
               .AsNoTracking()
               .Select(c => new CompetitionDto(
                   c.Id,
                   c.Title,
                   c.Description,
                   c.StartDate,
                   c.EndDate,
                   c.Status
               )).ToListAsync();
        }
    }
}
