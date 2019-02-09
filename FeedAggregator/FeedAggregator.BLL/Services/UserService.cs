using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.BLL.Interfaces;
using FeedAggregator.DAL.Entities;
using FeedAggregator.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedAggregator.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public UserDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ValidationException("Username is empty");

            if (string.IsNullOrEmpty(password))
                throw new ValidationException("Password is empty");

            var user = _database.Users.GetAll(u => u.Username == username).SingleOrDefault();

            // check if username exists
            if (user == null)
                throw new ValidationException($"Username \"{user.Username}\" not found");

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new ValidationException("Password is incorrect");

            // authentication successful
            return _mapper.Map<UserDto>(user);
        }

        public UserDto Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            // validation
            if (string.IsNullOrWhiteSpace(userDto.Username))
                throw new ValidationException("Username is required");

            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ValidationException("Password is required");

            var users = _database.Users.GetAll().ToList();

            if (users.Any(x => x.Username == user.Username))
                throw new ValidationException($"Username \"{user.Username}\" is already taken");

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _database.Users.Create(user);
            _database.Save();

            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _database.Users.GetAll();

            if (users == null)
                throw new ValidationException("List of users is empty");

            return _mapper.Map<List<UserDto>>(users);
        }

        public UserDto GetById(int userId)
        {
            var user = _database.Users.GetById(userId);

            if (user == null)
                throw new ValidationException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        public void Update(UserDto userParam)
        {
            var user = _database.Users.GetById(userParam.Id);

            if (user == null)
                throw new ValidationException("User not found");

            // check if Username is not empty and changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                var users = _database.Users.GetAll().ToList();

                if (users.Any(x => x.Username == userParam.Username))
                    throw new ValidationException($"Username \"{user.Username}\" is already taken");

                user.Username = userParam.Username;
            }

            // update other user properties if entered
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                CreatePasswordHash(userParam.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _database.Save();
        }

        public void Delete(int userId)
        {
            var user = _database.Users.GetById(userId);

            if (user == null)
            {
                throw new ValidationException("User not found");
            }

            _database.Users.Delete(userId);
            _database.Save();

        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
