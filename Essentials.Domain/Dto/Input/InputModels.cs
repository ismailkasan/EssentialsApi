namespace Essentials.Domain
{
    public record UserInputModel(string UserName, string Password)
    { }
    public record ProductInputModel(long Id, string Name, bool IsDeleted)
    { }
}
