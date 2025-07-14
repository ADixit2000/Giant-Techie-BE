namespace Giant_Techie_BE.DTOs
{
    public record AddOrUpdateCompetition(string Title, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, string Status);
    public record CompetitionDto(Guid Id, string Title, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, string Status);

    public record  AddOrUpdateUser(string FullName, string Email, string CollegeName, string Password);
    public record UserDto(Guid Id, string FullName, string Email, string CollegeName, string Password);
}
