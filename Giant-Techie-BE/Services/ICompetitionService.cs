using Giant_Techie_BE.DTOs;

namespace Giant_Techie_BE.Services
{
    public interface ICompetitionService
    {
        Task<CompetitionDto> CreateCompetition(AddOrUpdateCompetition command);
        Task<CompetitionDto?> GetCompetitionById(Guid id);
        Task<IEnumerable<CompetitionDto>> GetAllCopetitions();
        Task UpdateCompetition(Guid id, AddOrUpdateCompetition command);
        Task DeleteCompetition(Guid id);
    }
}
