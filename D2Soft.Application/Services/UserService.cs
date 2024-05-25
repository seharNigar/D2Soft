using AutoMapper;
using D2Soft.Application.DTOs;
using D2Soft.Application.Interfaces;
using D2Soft.Domain.Entities;
using D2Soft.Domain.Interfaces;
using FluentValidation;

namespace D2Soft.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IValidator<UserDto> userValidator, IMapper mapper)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> AddUserAsync(UserDto userDto)
        {
            var validationResult = await _userValidator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = _mapper.Map<User>(userDto);

         return   await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var validationResult = await _userValidator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = _mapper.Map<User>(userDto);

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}