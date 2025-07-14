using Giant_Techie_BE.Database;
using Giant_Techie_BE.DTOs;
using Giant_Techie_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Giant_Techie_BE.Services
{
    public class UserService : IUserService
    {

        private readonly GiantTicheDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(GiantTicheDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserDto> CreateUsers(AddOrUpdateUser command)
        {
            var user = User.Create(
                command.FullName,
                command.Email,
                command.CollegeName,
                command.Password
            );
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new UserDto(
                user.Id,
                user.FullName,
                user.Email,
                user.CollegeName,
                user.Password
            );

        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return await _dbContext.Users
                .Select(user => new UserDto(
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.CollegeName,
                    user.Password
                )).ToListAsync();
        }

        public async Task<UserDto?> GetUserById(Guid id)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            return new UserDto(
                user.Id,
                user.FullName,
                user.Email,
                user.CollegeName,
                user.Password
            );

        }

        public async Task<UserDto?> LoginUser(string email, string password)
        {
            var userDetails = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (userDetails == null)
            {
                return null;
            }
            return new UserDto(
                userDetails.Id,
                userDetails.FullName,
                userDetails.Email,
                userDetails.CollegeName,
                userDetails.Password
            );
        }


        public async Task UpdateUser(Guid id, AddOrUpdateUser command)
        {
            var user = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"Competition with ID {id} not found.");
            }

            user.Update(
                command.FullName,
                command.Email,
                command.CollegeName,
                command.Password
            );
            await _dbContext.SaveChangesAsync();
        }
    }
}
