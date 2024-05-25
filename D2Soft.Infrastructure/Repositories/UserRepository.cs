using D2Soft.Domain.Entities;
using D2Soft.Domain.Interfaces;

namespace D2Soft.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;
        private int _nextId;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User { UserId = 1, UserName = "John Doe", CNIC = "1234567890123", PhoneNumber = "1234567890" },
                new User { UserId = 2, UserName = "Jane Smith", CNIC = "2345678901234", PhoneNumber = "2345678901" }
            };
            _nextId = _users.Max(u => u.UserId) + 1;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.Run(() => _users.ToList());
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await Task.Run(() => _users.FirstOrDefault(u => u.UserId == userId));
        }

        public async Task<User> AddUserAsync(User user)
        {
            await Task.Run(() =>
            {
                user.UserId = _nextId++;
                _users.Add(user);
            });
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await Task.Run(() =>
            {
                var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
                if (existingUser != null)
                {
                    existingUser.UserName = user.UserName;
                    existingUser.CNIC = user.CNIC;
                    existingUser.PhoneNumber = user.PhoneNumber;
                }
            });
        }

        public async Task DeleteUserAsync(int userId)
        {
            await Task.Run(() =>
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    _users.Remove(user);
                }
            });
        }
    }
}