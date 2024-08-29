using Essentials.Domain;

namespace Essentials.Application
{
    public class UserService : IUserService
    {
        public UserOutputModel Login(UserInputModel userInputDto)
        {
            return new UserOutputModel("ismail", "123456", "ismail", "kaşan");
        }
    }
}
