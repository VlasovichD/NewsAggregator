using FeedAggregator.BLL.Dtos;
using System.Collections.Generic;

namespace FeedAggregator.BLL.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        UserDto Create(UserDto userDto);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        void Update(UserDto userDto);
        void Delete(int id);
    }
}
