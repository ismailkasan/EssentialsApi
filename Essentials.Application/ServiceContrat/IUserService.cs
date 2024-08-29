using Essentials.Domain;

namespace Essentials.Application
{
    public interface IUserService 
    {
        UserOutputModel Login(UserInputModel userInputDto);
    }
}
