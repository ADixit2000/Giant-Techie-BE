using Giant_Techie_BE.DTOs;

namespace Giant_Techie_BE.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUsers(AddOrUpdateUser command);
        Task<UserDto?> GetUserById(Guid id);
        Task<UserDto?> LoginUser(string email, string password);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task UpdateUser(Guid id, AddOrUpdateUser command);
        Task DeleteUser(Guid id);
    }
}
